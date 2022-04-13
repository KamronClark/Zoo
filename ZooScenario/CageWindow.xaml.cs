using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Animals;
using CagedItems;
using Utilities;
using Zoos;

namespace ZooScenario
{
    /// <summary>
    /// Interaction logic for CageWindow.xaml.
    /// </summary>
    public partial class CageWindow : Window
    {
        /// <summary>
        /// Current cage.
        /// </summary>
        private Cage cage;

        TransformGroup transformGroup = new TransformGroup();

        /// <summary>
        /// Initializes a new instance of the CageWindow class.
        /// </summary>
        /// <param name="cage">Current cage.</param>
        public CageWindow(Cage cage)
        {
            this.InitializeComponent();
            this.cage = cage;

            this.cage.OnImageUpdate = item =>
            {
                try
                {
                    this.Dispatcher.Invoke(new Action(delegate ()
                    {
                        ICageable selectedItem = null;
                        int zIndex = 0;

                        foreach (Viewbox v in this.cageGrid.Children)
                        {
                            if (v.Tag == item)
                            {
                                this.cageGrid.Children.Remove(v);
                                selectedItem = v.Tag as ICageable;
                                break;
                            }

                            zIndex++;
                        }

                        if (selectedItem.IsActive)
                        {
                            this.DrawItem(selectedItem, zIndex);
                        }
                    }));
                }
                catch (TaskCanceledException)
                {
                }
            };
        }

        /// <summary>
        /// Draws an item on the window.
        /// </summary>
        /// <param name="item">Item to be drawn.</param>
        private void DrawItem(ICageable item, int zIndex)
        {            
            Viewbox viewbox = this.GetViewBox(800, 400, item.XPosition, item.YPosition, item.ResourceKey, item.DisplaySize);
            viewbox.HorizontalAlignment = HorizontalAlignment.Left;
            viewbox.VerticalAlignment = VerticalAlignment.Top;

            if (item.HungerState == HungerState.Unconscious)
            {
                // Create a new SkewTransform and set its AngleX to 30 degrees in the direction the cageable is facing.
                SkewTransform unconsciousSkew = new SkewTransform();
                unconsciousSkew.AngleX = item.XDirection == HorizontalDirection.Left ? 30.0 : -30.0;

                // Add the SkewTransform to the transform group.
                this.transformGroup.Children.Add(unconsciousSkew);

                // Add a new ScaleTransform of 75% of width, 50% of height to the transform group.
                this.transformGroup.Children.Add(new ScaleTransform(0.75, 0.5));              
            }

            // If the animal is moving to the left
            if (item.XDirection == HorizontalDirection.Left)
            {
                // Set the origin point of the transformation to the middle of the viewbox.
                viewbox.RenderTransformOrigin = new Point(0.5, 0.5);

                // Initialize a ScaleTransform instance.
                ScaleTransform flipTransform = new ScaleTransform();

                // Flip the viewbox horizontally so the animal faces to the left
                flipTransform.ScaleX = -1;

                // Apply the ScaleTransform to the viewbox
                this.transformGroup.Children.Add(flipTransform);

                // Apply all transforms in the transform group to the viewbox.
                viewbox.RenderTransform = this.transformGroup;
            }

            viewbox.Tag = item;
            this.cageGrid.Children.Insert(zIndex, viewbox);
        }

        /// <summary>
        /// Gets the window view box.
        /// </summary>
        /// <param name="maxXPosition">Maximum x position.</param>
        /// <param name="maxYPosition">Maximum y position.</param>
        /// <param name="xPosition">Current x position.</param>
        /// <param name="yPosition">Current y position.</param>
        /// <param name="resourceKey">View box resource key.</param>
        /// <param name="displayScale">View box display scale.</param>
        /// <returns>Returns finished view box.</returns>
        private Viewbox GetViewBox(double maxXPosition, double maxYPosition, int xPosition, int yPosition, string resourceKey, double displayScale)
        {
            Canvas canvas = Application.Current.Resources[resourceKey] as Canvas;

            // Finished viewbox.
            Viewbox finishedViewBox = new Viewbox();

            // Gets image ratio.
            double imageRatio = canvas.Width / canvas.Height;

            // Sets width to a percent of the window size based on it's scale.
            double itemWidth = this.cageGrid.ActualWidth * 0.2 * displayScale;

            // Sets the height to the ratio of the width.
            double itemHeight = itemWidth / imageRatio;

            // Sets the width of the viewbox to the size of the canvas.
            finishedViewBox.Width = itemWidth;
            finishedViewBox.Height = itemHeight;

            // Sets the animals location on the screen.
            double xPercent = (this.cageGrid.ActualWidth - itemWidth) / maxXPosition;
            double yPercent = (this.cageGrid.ActualHeight - itemHeight) / maxYPosition;

            int posX = Convert.ToInt32(xPosition * xPercent);
            int posY = Convert.ToInt32(yPosition * yPercent);

            finishedViewBox.Margin = new Thickness(posX, posY, 0, 0);

            // Adds the canvas to the view box.
            finishedViewBox.Child = canvas;

            // Returns the finished viewbox.
            return finishedViewBox;
        }

        /// <summary>
        /// Initializes window on load.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.DrawAllItems();
        }

        /// <summary>
        /// Handles redrawing items.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void RedrawHandler(object sender, ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke(new Action(delegate()
            {
                this.DrawAllItems();
            }));
        }

        /// <summary>
        /// Draws all items.
        /// </summary>
        private void DrawAllItems()
        {
            this.cageGrid.Children.Clear();
            int zIndex = 0;

            this.cage.CagedItems.ToList().ForEach(c => this.DrawItem(c, zIndex++));
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.cage.OnImageUpdate = null;
        }
    }
}
