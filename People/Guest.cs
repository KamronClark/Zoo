using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Timers;
using Accounts;
using Animals;
using BoothItems;
using CagedItems;
using Foods;
using MoneyCollectors;
using Reproducers;
using Utilities;
using VendingMachines;

namespace People
{
    /// <summary>
    /// The class which is used to represent a guest.
    /// </summary>
    [Serializable]
    public class Guest : IEater, ICageable
    {
        /// <summary>
        /// Determines the random sequences of the guest class.
        /// </summary>
        private static Random random = new Random(DateTime.Now.Millisecond);

        /// <summary>
        /// The age of the guest.
        /// </summary>
        private int age;

        /// <summary>
        /// The name of the guest.
        /// </summary>
        private string name;

        /// <summary>
        /// The guest's wallet.
        /// </summary>
        private Wallet wallet;

        /// <summary>
        /// The guest's bag of items.
        /// </summary>
        private List<Item> bag;

        /// <summary>
        /// Gender of the guest.
        /// </summary>
        private Gender gender;

        /// <summary>
        /// The guest's checking account.
        /// </summary>
        private IMoneyCollector checkingAccount;

        [NonSerialized]
        private Action<Guest> onTextChange;

        private Animal adoptedAnimal;

        [NonSerialized]
        private Timer feedTimer;

        private bool isActive;

        /// <summary>
        /// Initializes a new instance of the Guest class.
        /// </summary>
        /// <param name="name">The name of the guest.</param>
        /// <param name="age">The age of the guest.</param>
        /// <param name="moneyBalance">The initial amount of money to put into the guest's wallet.</param>
        /// <param name="walletColor">The color of the guest's wallet.</param>
        /// <param name="gender">Gender of the guest.</param>
        /// <param name="checkingAccount">The guest's checking account.</param>
        public Guest(string name, int age, decimal moneyBalance, WalletColor walletColor, Gender gender, IMoneyCollector checkingAccount)
        {
            this.age = age;
            this.name = name;
            this.wallet = new Wallet(walletColor);
            this.bag = new List<Item>();
            this.wallet.AddMoney(moneyBalance);
            this.gender = gender;
            this.checkingAccount = checkingAccount;
            this.checkingAccount.OnBalanceChange += this.HandleBalanceChange;
            this.wallet.OnBalanceChange += this.HandleBalanceChange;

            this.XPositionMax = 800;
            this.YPositionMax = 400;
            this.XPosition = random.Next(1, this.XPositionMax += 1);
            this.YPosition = random.Next(1, this.YPositionMax += 1);
            this.XDirection = random.Next(0, 2) == 0 ? HorizontalDirection.Left : HorizontalDirection.Right;
            this.YDirection = random.Next(0, 2) == 0 ? VerticalDirection.Up : VerticalDirection.Down;

            this.CreateTimers();
        }

        /// <summary>
        /// Gets the guest's checking account.
        /// </summary>
        public IMoneyCollector CheckingAccount
        {
            get
            {
                return this.checkingAccount;
            }
        }

        /// <summary>
        /// Gets the guests wallet.
        /// </summary>
        public Wallet Wallet
        {
            get
            {
                return this.wallet;
            }
        }

        /// <summary>
        /// Gets or sets the name of the guest.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (Regex.IsMatch(value, @"^[a-zA-Z ]+$"))
                {
                    if (this.OnTextChange != null)
                    {
                        this.OnTextChange(this);
                    }

                    this.name = value;
                }
                else
                {
                    throw new FormatException();
                }
            }
        }

        /// <summary>
        /// Gets or sets the weight of the guest.
        /// </summary>
        public double Weight
        {
            get
            {
                // Confidential.
                return 0.0;
            }

            set
            {
            }
        }

        /// <summary>
        /// Gets or sets the age of the guest.
        /// </summary>
        public int Age
        {
            get
            {
                return this.age;
            }

            set
            {
                if (value > 0 && value < 120)
                {
                    if (this.OnTextChange != null)
                    {
                        this.OnTextChange(this);
                    }

                    this.age = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        /// <summary>
        /// Gets or sets the guest's gender.
        /// </summary>
        public Gender Gender
        {
            get
            {
                return this.gender;
            }

            set
            {
                this.gender = value;
            }
        }

        public double WeightGainPercentage
        {
            get
            {
                return 0;
            }
        }

        /// <summary>
        /// Gets or sets the guest's adopted animal.
        /// </summary>
        public Animal AdoptedAnimal
        {
            get
            {
                return this.adoptedAnimal;
            }

            set
            {
                if (this.OnTextChange != null)
                {
                    this.OnTextChange(this);
                }

                if (value != null)
                {
                    value.OnHunger = null;
                }

                this.adoptedAnimal = value;

                if (value != null)
                {
                    value.OnHunger += this.HandleAnimalHungry;
                }
            }
        }

        /// <summary>
        /// Gets the display size of the guest.
        /// </summary>
        public double DisplaySize
        {
            get
            {
                return 0.6;
            }
        }

        /// <summary>
        /// Gets the guest resource key.
        /// </summary>
        public string ResourceKey
        {
            get
            {
                return "Guest";
            }
        }

        public Action<Guest> OnTextChange
        {
            get
            {
                return this.onTextChange;
            }

            set
            {
                this.onTextChange = value;
            }
        }

        public bool IsActive
        {
            get
            {
                bool returnValue = false;

                if (this.isActive == true && this.AdoptedAnimal == null)
                {
                    returnValue = false;
                }
                else
                {
                    returnValue = true;
                }

                return returnValue;
            }

            set
            {
                this.isActive = value;
            }
        }

        /// <summary>
        /// Gets or sets the maximum x position of the guest.
        /// </summary>
        public int XPositionMax { get; set; }

        /// <summary>
        /// Gets or sets the maximum y position of the guest.
        /// </summary>
        public int YPositionMax { get; set; }

        /// <summary>
        /// Gets the x position of the guest.
        /// </summary>
        public int XPosition { get; private set; }

        /// <summary>
        /// Gets the y position of the guest.
        /// </summary>
        public int YPosition { get; private set; }

        /// <summary>
        /// Gets the x direction of the guest.
        /// </summary>
        public HorizontalDirection XDirection { get; private set; }

        /// <summary>
        /// Gets the y direction of the guest.
        /// </summary>
        public VerticalDirection YDirection { get; private set; }

        public HungerState HungerState { get; }

        public Func<VendingMachine> GetVendingMachine { get; set; }

        public Action<ICageable> OnImageUpdate { get; set; }       

        /// <summary>
        /// Eats the specified food.
        /// </summary>
        /// <param name="food">The food to eat.</param>
        public void Eat(Food food)
        {
            // Eat the food.
        }

        /// <summary>
        /// Feeds the specified eater.
        /// </summary>
        /// <param name="eater">The eater to be fed.</param>
        public void FeedAnimal(IEater eater)
        {
            VendingMachine animalSnackMachine = null; 

            if (this.GetVendingMachine() != null)
            {
                animalSnackMachine = this.GetVendingMachine();
            }      

            // Find food price.
            decimal price = animalSnackMachine.DetermineFoodPrice(eater.Weight);

            if (this.wallet.MoneyBalance < animalSnackMachine.DetermineFoodPrice(eater.Weight))
            {
                this.WithdrawMoney(animalSnackMachine.DetermineFoodPrice(eater.Weight) * 10);
            }

            // Get money from wallet.
            decimal payment = this.wallet.RemoveMoney(price);

            // Buy food.
            Food food = animalSnackMachine.BuyFood(payment);

            // Feed animal.
            eater.Eat(food);
        }

        /// <summary>
        /// Causes the guest to visit the ticket booth.
        /// </summary>
        /// <param name="ticketBooth">The ticket booth at the zoo.</param>
        /// <returns>Returns a ticket.</returns>
        public Ticket VisitTicketBooth(MoneyCollectingBooth ticketBooth)
        {                                     
            if (this.wallet.MoneyBalance < ticketBooth.TicketPrice || this.wallet.MoneyBalance < ticketBooth.WaterBottlePrice)
            {
                this.WithdrawMoney((ticketBooth.TicketPrice + ticketBooth.WaterBottlePrice) * 2);
            }
            
            Ticket ticket = ticketBooth.SellTicket(this.wallet.RemoveMoney(ticketBooth.TicketPrice));

            WaterBottle zooBottle = ticketBooth.SellWaterBottle(this.wallet.RemoveMoney(ticketBooth.WaterBottlePrice));
            this.bag.Add(zooBottle);
           
            return ticket;
        }

        /// <summary>
        /// Causes the guest to visit the information booth.
        /// </summary>
        /// <param name="informationBooth">The information booth at the zoo.</param>
        public void VisitInformationBooth(GivingBooth informationBooth)
        {
            this.bag.Add(informationBooth.GiveFreeMap());

            this.bag.Add(informationBooth.GiveFreeCouponBook());
        }

        /// <summary>
        /// Renders the guest's information as a formatted string.
        /// </summary>
        /// <returns>Returns formatted string.</returns>
        public override string ToString()
        {
            if (this.AdoptedAnimal != null)
            {
                return this.Name + ": " + this.Age + " [$" + this.wallet.MoneyBalance + " / $" + this.checkingAccount.MoneyBalance + "]" + "Adopted: " + this.AdoptedAnimal.Name;
            }
            else
            {
                return this.Name + ": " + this.Age + " [$" + this.wallet.MoneyBalance + " / $" + this.checkingAccount.MoneyBalance + "]";
            }            
        }

        /// <summary>
        /// Withdraws money from the checking account.
        /// </summary>
        /// <param name="amount">Amount to be withdrawn.</param>
        public void WithdrawMoney(decimal amount)
        {
            this.checkingAccount.RemoveMoney(amount);
            this.wallet.AddMoney(amount);
        }

        public void HandleAnimalHungry()
        {
            this.feedTimer.Start();
        }

        public void HandleReadyToFeed(object sender, ElapsedEventArgs e)
        {
            this.FeedAnimal(this.AdoptedAnimal);
            this.feedTimer.Stop();
        }

        private void HandleBalanceChange()
        {
            if (this.OnTextChange != null)
            {
                this.OnTextChange(this);
            }           
        }

        private void CreateTimers()
        {
            this.feedTimer = new Timer(5000);
            this.feedTimer.Elapsed += this.HandleReadyToFeed;        
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            this.CreateTimers();
        }
    }
}