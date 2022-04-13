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
using People;
using Reproducers;

namespace ZooScenario
{
    /// <summary>
    /// Interaction logic for GuestWindow.xaml.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Event handlers may begin with lower-case letters.")]
    public partial class GuestWindow : Window
    {
        /// <summary>
        /// The guest being added.
        /// </summary>
        private Guest guest;

        /// <summary>
        /// Initializes a new instance of the GuestWindow class.
        /// </summary>
        /// <param name="guest">Guest to be added.</param>
        public GuestWindow(Guest guest)
        {
            this.guest = guest;
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes several fields on window load.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.nameTextBox.Text = this.guest.Name;
            this.genderComboBox.ItemsSource = Enum.GetValues(typeof(Gender));
            this.genderComboBox.SelectedItem = this.guest.Gender;
            this.ageTextBox.Text = this.guest.Age.ToString();
            this.walletColorComboBox.ItemsSource = Enum.GetValues(typeof(WalletColor));
            this.walletColorComboBox.SelectedItem = this.guest.Wallet.WalletColor;
            
            this.moneyBalanceLabel.Content = this.guest.Wallet.MoneyBalance.ToString("C");
            List<int> moneyAmounts = new List<int>();
            moneyAmounts.Add(1);
            moneyAmounts.Add(5);
            moneyAmounts.Add(10);
            moneyAmounts.Add(20);
            this.moneyAmountComboBox.ItemsSource = moneyAmounts;
            this.accountBalanceLabel.Content = this.guest.CheckingAccount.MoneyBalance.ToString("C");
            this.accountComboBox.ItemsSource = moneyAmounts;
        }

        /// <summary>
        /// Configures the OK button.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        /// <summary>
        /// Sets the guest name upon losing focus.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void nameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                this.guest.Name = this.nameTextBox.Text;
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Name must only include letters and spaces." + ex.Message);
            }
        }

        /// <summary>
        /// Sets the guest gender upon selection change.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void genderComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.guest.Gender = (Gender)this.genderComboBox.SelectedItem;
        }

        /// <summary>
        /// Sets the guest age upon losing focus.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void ageTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                this.guest.Age = int.Parse(this.ageTextBox.Text);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show("Age must be between 0 and 100." + ex.Message);
            }
        }

        /// <summary>
        /// Sets the guest wallet color upon selection change.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void walletColorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.guest.Wallet.WalletColor = (WalletColor)this.walletColorComboBox.SelectedItem;
        }

        /// <summary>
        /// Adds money to the guest's wallet.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void addMoneyButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.guest.Wallet.AddMoney(decimal.Parse(this.moneyAmountComboBox.Text));
                this.moneyBalanceLabel.Content = this.guest.Wallet.MoneyBalance.ToString("C");
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Please select an amount to add or subtract by." + ex.Message);
            }                       
        }

        /// <summary>
        /// Subtracts money from the guest's wallet.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void subtractMoneyButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.guest.Wallet.RemoveMoney(decimal.Parse(this.moneyAmountComboBox.Text));
                this.moneyBalanceLabel.Content = this.guest.Wallet.MoneyBalance.ToString("C");
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Please select an amount to add or subtract by." + ex.Message);
            }            
        }

        /// <summary>
        /// Adds money to the guest's checking account.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void addAccountButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.guest.CheckingAccount.AddMoney(decimal.Parse(this.accountComboBox.Text));
                this.accountBalanceLabel.Content = this.guest.CheckingAccount.MoneyBalance.ToString("C");
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Please select an amount to add or subtract by." + ex.Message);
            }           
        }

        /// <summary>
        /// Removes money from the guest's checking account.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void subtractAccountButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.guest.CheckingAccount.RemoveMoney(decimal.Parse(this.accountComboBox.Text));
                this.accountBalanceLabel.Content = this.guest.CheckingAccount.MoneyBalance.ToString("C");
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Please select an amount to add or subtract by." + ex.Message);
            }            
        }
    }
}
