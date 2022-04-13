using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using Accounts;
using Animals;
using BirthingRooms;
using BoothItems;
using Microsoft.Win32;
using People;
using Reproducers;
using Zoos;

namespace ZooScenario
{
    /// <summary>
    /// Contains interaction logic for MainWindow.xaml.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Event handlers may begin with lower-case letters.")]
    public partial class MainWindow : Window
    {
        private const string AutoSaveFileName = "Autosave.zoo";

        /// <summary>
        /// Minnesota's Como Zoo.
        /// </summary>
        private Zoo comoZoo;     

        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();

#if DEBUG
            this.Title += " [DEBUG]";
#endif
        }        

        /// <summary>
        /// Admits a guest into the como zoo.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void admitGuestButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Guest guest = new Guest("Ethel", 42, 30, WalletColor.Salmon, Reproducers.Gender.Female, new Account());

                this.comoZoo.AddGuest(guest, this.comoZoo.SellTicket(guest));
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show(ex.Message);
            }            
        }

        /// <summary>
        /// Feeds an animal.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void feedAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            Guest guest = this.guestListBox.SelectedItem as Guest;
            Animal animal = this.animalListBox.SelectedItem as Animal;

            if (guest != null && animal != null)
            {
                this.comoZoo.FindGuest(g => g.Name == guest.Name).FeedAnimal(this.comoZoo.FindAnimal(a => a.Name == animal.Name));
            }
            else
            {
                MessageBox.Show("Please choose both a guest and an animal to feed an animal.");
            }

            this.guestListBox.SelectedItem = guest;
            this.animalListBox.SelectedItem = animal;
        }

        /// <summary>
        /// Increases the zoo's birthing room temperature.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void increaseTemperatureButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.comoZoo.BirthingRoomTemperature++;
                //this.ConfigureBirthingRoomControls();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Decreases the zoo's birthing room temperature.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void decreaseTemperatureButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.comoZoo.BirthingRoomTemperature--;
                // this.ConfigureBirthingRoomControls();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Creates the zoo and window features on window load.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            bool result = this.LoadZoo(AutoSaveFileName);

            if (!result)
            {
                this.comoZoo = Zoo.NewZoo();
            }

            this.AttachDelegates();
            this.animalTypeComboBox.ItemsSource = Enum.GetValues(typeof(AnimalType));
            this.changeMoveBehaviorComboBox.ItemsSource = Enum.GetValues(typeof(MoveBehaviorType));

            // Set border height
            // this.ConfigureBirthingRoomControls();
        }

        /// <summary>
        /// Removes an animal from the zoo.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void removeAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            Animal animal = this.animalListBox.SelectedItem as Animal;
            if (animal != null)
            {
                if (MessageBox.Show(string.Format("Are you sure you want to remove animal: {0}?", animal.Name), "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    this.comoZoo.RemoveAnimal(animal);
                }
            }            
            else
            {
                MessageBox.Show("Please select an animal to remove.");
            }
        }

        /// <summary>
        /// Removes a guest from the zoo.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void removeGuestButton_Click(object sender, RoutedEventArgs e)
        {
            Guest guest = this.guestListBox.SelectedItem as Guest;
            if (guest != null)
            {
                if (MessageBox.Show(string.Format("Are you sure you want to remove guest: {0}?", guest.Name), "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    this.comoZoo.RemoveGuest(guest);
                }
            }
            else
            {
                MessageBox.Show("Please select a guest to remove.");
            }
        }

        /// <summary>
        /// Adds an animal to the zoo.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void addAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            Animal animal = null;
            
            try
            {
                AnimalType animalType = (AnimalType)this.animalTypeComboBox.SelectedItem;
                animal = AnimalFactory.CreateAnimal(animalType, "Name", 0, 0, Reproducers.Gender.Male);
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("An animal type must be selected before adding an animal.");
            }

            if (animal != null)
            {
                AnimalWindow animalWindow = new AnimalWindow(animal);
                animalWindow.ShowDialog();

                if (animalWindow.DialogResult == true)
                {
                    this.comoZoo.AddAnimal(animal);
                }
            }                        
        }

        /// <summary>
        /// Adds a guest to the zoo.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void addGuestButton_Click(object sender, RoutedEventArgs e)
        {
            Account checkingAccount = new Account();
            Guest guest = new Guest("Name", 0, 0, WalletColor.Black, Gender.Male, checkingAccount);

            GuestWindow guestWindow = new GuestWindow(guest);
            guestWindow.ShowDialog();

            if (guestWindow.DialogResult == true)
            {
                try
                {
                    Ticket ticket = this.comoZoo.SellTicket(guest);
                    this.comoZoo.AddGuest(guest, ticket);
                }
                catch (NullReferenceException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        /// <summary>
        /// Allows user to edit an animal.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void animalListBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Animal animal = this.animalListBox.SelectedItem as Animal;
            
            if (animal != null)
            {
                AnimalWindow animalWindow = new AnimalWindow(animal);
                animalWindow.ShowDialog();

                if (animalWindow.DialogResult == true)
                {
                    if (animal.IsPregnant)
                    {
                        this.comoZoo.RemoveAnimal(animal);
                        this.comoZoo.AddAnimal(animal);
                    }
                }
            }            
        }

        /// <summary>
        /// Allows user to edit a guest.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void guestListBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Guest guest = this.guestListBox.SelectedItem as Guest;

            if (guest != null)
            {
                GuestWindow guestWindow = new GuestWindow(guest);
                guestWindow.ShowDialog();

                if (guestWindow.DialogResult == true)
                {
                }
            }
        }

        /// <summary>
        /// Shows the desired animal cage.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void showCageButton_Click(object sender, RoutedEventArgs e)
        {
            Animal animal = this.animalListBox.SelectedItem as Animal;

            if (animal != null)
            {
                CageWindow cageWindow = new CageWindow(this.comoZoo.FindCage(animal.GetType()));
                cageWindow.Show();
            }
            else
            {
                MessageBox.Show("Please select an animal to show.");
            }
        }

        /// <summary>
        /// Selected guest adopts selected animal.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void adoptAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            Guest guest = this.comoZoo.FindGuest(g => g.AdoptedAnimal == null);
            Animal animal = this.animalListBox.SelectedItem as Animal;
            try
            {
                guest.AdoptedAnimal = animal;
                this.comoZoo.FindCage(animal.GetType()).Add(guest);
            }
            catch
            {
                MessageBox.Show("Please select a guest and an animal.");
            }
        }

        /// <summary>
        /// Selected guest unadopts selected animal.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void unadoptAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            Guest guest = this.guestListBox.SelectedItem as Guest;
            this.comoZoo.FindCage(guest.AdoptedAnimal.GetType()).Remove(guest);
            guest.AdoptedAnimal = null;
        }

        private void changeMoveBehaviorButton_Click(object sender, RoutedEventArgs e)
        {
            Animal selectedAnimal;
            MoveBehaviorType moveBehaviorType;

            try
            {
                selectedAnimal = this.animalListBox.SelectedItem as Animal;                
                moveBehaviorType = (MoveBehaviorType)this.changeMoveBehaviorComboBox.SelectedItem;

                if (selectedAnimal != null)
                {
                    selectedAnimal.MoveBehavior = MoveBehaviorFactory.CreateMoveBehavior(moveBehaviorType);
                }
            }
            catch
            {
                MessageBox.Show("Please select both an animal and a behavior type before trying to change behavior");
            }           
        }

        private void giveBirthButton_Click(object sender, RoutedEventArgs e)
        {
            Animal selectedAnimal;

            try
            {
                selectedAnimal = this.animalListBox.SelectedItem as Animal;

                if (selectedAnimal.IsPregnant)
                {
                    IReproducer baby = selectedAnimal.Reproduce();
                }
                else
                {
                    MessageBox.Show("Please select a pregnant animal.");
                }
            }
            catch
            {
                MessageBox.Show("Please select an animal.");
            }
        }

        private void birthAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.comoZoo.BirthAnimal();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Zoo save-game files (*.zoo)|*.zoo";

            if (saveFileDialog.ShowDialog() == true)
            {
                this.SaveZoo(saveFileDialog.FileName);
            }
        }

        private void SaveZoo(string fileName)
        {
            this.comoZoo.SaveToFile(fileName);
            this.SetWindowTitle(fileName);
        }

        private void SetWindowTitle(string fileName)
        {
            // Set the title of the window using the current file name
            this.Title = string.Format("Object-Oriented Programming 2: Zoo [{0}]", Path.GetFileName(fileName));
        }

        private void loadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Zoo save-game files (*.zoo)|*.zoo";

            if (openFileDialog.ShowDialog() == true)
            {
                this.ClearWindow();
                this.LoadZoo(openFileDialog.FileName);
            }
        }

        private bool LoadZoo(string fileName)
        {
            bool result = true;

            try
            {
                this.comoZoo = Zoo.LoadFromFile(fileName);
                this.AttachDelegates();
                this.SetWindowTitle(fileName);
            }
            catch
            {
                result = false;
                MessageBox.Show("File could not be loaded.");
            }

            return result;
        }

        private void ClearWindow()
        {
            this.guestListBox.Items.Clear();
            this.animalListBox.Items.Clear();
        }

        private void resetButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to start over?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                this.ClearWindow();
                this.comoZoo = Zoo.NewZoo();
                this.AttachDelegates();
                this.Title = "Object-Oriented Programming 2: Zoo";
            }
        }

        private void window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.SaveZoo(AutoSaveFileName);
        }

        private void AttachDelegates()
        {
            this.comoZoo.OnBirthingRoomTemperatureChange = (previousTemp, currentTemp) =>
            {
                this.temperatureBorder.Height = this.comoZoo.BirthingRoomTemperature * 2;
                this.temperatureLabel.Content = this.comoZoo.BirthingRoomTemperature + "°F";

                double colorLevel = ((this.comoZoo.BirthingRoomTemperature - BirthingRoom.MinTemperature) * 255) / (BirthingRoom.MaxTemperature - BirthingRoom.MinTemperature);

                this.temperatureBorder.Background = new SolidColorBrush(Color.FromRgb(
                Convert.ToByte(colorLevel),
                Convert.ToByte(255 - colorLevel),
                Convert.ToByte(255 - colorLevel)));
            };
            this.comoZoo.OnDeserialized();

            this.comoZoo.OnAddGuest = guest =>
            {
                this.guestListBox.Items.Add(guest);
                guest.OnTextChange += this.UpdateGuestDisplay;
            };

            this.comoZoo.OnRemoveGuest = guest =>
            {
                this.guestListBox.Items.Remove(guest);
                guest.OnTextChange -= this.UpdateGuestDisplay;
            };

            this.comoZoo.OnAddAnimal = animal =>
            {
                this.animalListBox.Items.Add(animal);
                animal.OnTextChange += this.UpdateAnimalDisplay;
            };

            this.comoZoo.OnRemoveAnimal = animal =>
            {
                this.animalListBox.Items.Remove(animal);
                animal.OnTextChange -= this.UpdateAnimalDisplay;
            };
        }

        private void UpdateGuestDisplay(Guest guest)
        {
            Dispatcher.Invoke(() =>
            {
                int index = this.guestListBox.Items.IndexOf(guest);
                if (index >= 0)
                {
                    // disconnect the guest
                    this.guestListBox.Items.RemoveAt(index);

                    // create new guest item in the same spot
                    this.guestListBox.Items.Insert(index, guest);

                    // re-select the guest
                    this.guestListBox.SelectedItem = this.guestListBox.Items[index];
                }
            });        
        }

        private void UpdateAnimalDisplay(Animal animal)
        {
            Dispatcher.Invoke(() =>
            {
                int index = this.animalListBox.Items.IndexOf(animal);
                if (index >= 0)
                {
                    // disconnect the animal
                    this.animalListBox.Items.RemoveAt(index);

                    // create new animal item in the same spot
                    this.animalListBox.Items.Insert(index, animal);

                    // re-select the animal
                    this.animalListBox.SelectedItem = this.animalListBox.Items[index];
                }
            });            
        }
    }
}