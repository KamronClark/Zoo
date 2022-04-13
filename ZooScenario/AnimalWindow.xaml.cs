using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Animals;
using Reproducers;

namespace ZooScenario
{
    /// <summary>
    /// Interaction logic for AnimalWindow.xaml.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Event handlers may begin with lower-case letters.")]
    public partial class AnimalWindow : Window
    {
        /// <summary>
        /// Animal currently being added.
        /// </summary>
        private Animal animal;

        /// <summary>
        /// Initializes a new instance of the AnimalWindow class.
        /// </summary>
        /// <param name="animal">Animal to be added.</param>
        public AnimalWindow(Animal animal)
        {
            this.animal = animal;
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes several fields on window load.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.nameTextBox.Text = this.animal.Name;
            this.genderComboBox.ItemsSource = Enum.GetValues(typeof(Gender));
            this.genderComboBox.SelectedItem = this.animal.Gender;
            this.ageTextBox.Text = this.animal.Age.ToString();
            this.weightTextBox.Text = this.animal.Weight.ToString();

            // this.animal.IsPregnant ? this.pregnanceStatusLabel.Content = "Yes" : this.pregnanceStatusLabel.Content = "No";
            // I really don't understand why the above line wouldn't work.
            if (this.animal.IsPregnant)
            {
                this.pregnanceStatusLabel.Content = "Yes";
            }
            else
            {
                this.pregnanceStatusLabel.Content = "No";
            }
        }

        /// <summary>
        /// Configures to OK button.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        /// <summary>
        /// Sets the animal name upon losing focus.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void nameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                this.animal.Name = this.nameTextBox.Text;
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Name must only include letters and spaces." + ex.Message);
            }
        }

        /// <summary>
        /// Sets the animal age upon losing focus.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void ageTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                this.animal.Age = int.Parse(this.ageTextBox.Text);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show("Age must be between 0 and 100." + ex.Message);
            }
        }

        /// <summary>
        /// Sets the animal weight upon losing focus.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void weightTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                this.animal.Weight = double.Parse(this.weightTextBox.Text);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show("Weight must be between 0 and 1000." + ex.Message);
            }
        }

        /// <summary>
        /// Sets the animal gender upon selection change.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void genderComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.animal.Gender = (Gender)this.genderComboBox.SelectedItem;
            if (this.animal.Gender == Gender.Female)
            {
                this.makePregnantButton.IsEnabled = true;
            }
            else if (this.animal.Gender == Gender.Male)
            {
                this.makePregnantButton.IsEnabled = false;
            }
        }

        /// <summary>
        /// Makes the animal pregnant if they can be.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void makePregnantButton_Click(object sender, RoutedEventArgs e)
        {
            this.animal.MakePregnant();
            this.pregnanceStatusLabel.Content = "Yes";
            this.makePregnantButton.IsEnabled = false;
        }
    }
}
