using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Accounts;
using Animals;
using BirthingRooms;
using BoothItems;
using MoneyCollectors;
using People;
using Reproducers;
using VendingMachines;

namespace Zoos
{
    /// <summary>
    /// The class which is used to represent a zoo.
    /// </summary>
    [Serializable]
    public class Zoo
    {
        /// <summary>
        /// A list of all animals currently residing within the zoo.
        /// </summary>
        private List<Animal> animals;

        /// <summary>
        /// The zoo's vending machine which allows guests to buy snacks for animals.
        /// </summary>
        private VendingMachine animalSnackMachine;

        /// <summary>
        /// The zoo's room for birthing animals.
        /// </summary>
        private BirthingRoom b168;

        /// <summary>
        /// The maximum number of guests the zoo can accommodate at a given time.
        /// </summary>
        private int capacity;

        /// <summary>
        /// A list of all guests currently visiting the zoo.
        /// </summary>
        private List<Guest> guests;

        /// <summary>
        /// The zoo's ladies' restroom.
        /// </summary>
        private Restroom ladiesRoom;

        /// <summary>
        /// The zoo's men's restroom.
        /// </summary>
        private Restroom mensRoom;

        /// <summary>
        /// The name of the zoo.
        /// </summary>
        private string name;

        /// <summary>
        /// The zoo's ticket booth.
        /// </summary>
        private MoneyCollectingBooth ticketBooth;

        /// <summary>
        /// The zoo's information booth.
        /// </summary>
        private GivingBooth informationBooth;

        /// <summary>
        /// The zoos list of cages.
        /// </summary>
        private Dictionary<Type, Cage> cages;

        [NonSerialized]
        private Action<double, double> onBirthingRoomTemperatureChange;

        [NonSerialized]
        private Action<Guest> onAddGuest;

        [NonSerialized]
        private Action<Guest> onRemoveGuest;

        [NonSerialized]
        private Action<Animal> onAddAnimal;

        [NonSerialized]
        private Action<Animal> onRemoveAnimal;

        /// <summary>
        /// Initializes a new instance of the Zoo class.
        /// </summary>
        /// <param name="name">The name of the zoo.</param>
        /// <param name="capacity">The maximum number of guests the zoo can accommodate at a given time.</param>
        /// <param name="restroomCapacity">The capacity of the zoo's restrooms.</param>
        /// <param name="animalFoodPrice">The price of a pound of food from the zoo's animal snack machine.</param>
        /// <param name="ticketPrice">The price of an admission ticket to the zoo.</param>
        /// <param name="waterBottlePrice">The price of a water bottle at the zoo.</param>
        /// <param name="boothMoneyBalance">The initial money balance of the zoo's ticket booth.</param>
        /// <param name="attendant">The zoo's ticket booth attendant.</param>
        /// <param name="vet">The zoo's birthing room vet.</param>
        public Zoo(string name, int capacity, int restroomCapacity, decimal animalFoodPrice, decimal ticketPrice, decimal waterBottlePrice, decimal boothMoneyBalance, Employee attendant, Employee vet)
        {
            this.animals = new List<Animal>();
            this.animalSnackMachine = new VendingMachine(animalFoodPrice, new Account());
            this.b168 = new BirthingRoom(vet);

            this.b168.OnTemperatureChange = (previousTemp, currentTemp) =>
            {
                this.OnBirthingRoomTemperatureChange(previousTemp, currentTemp);
            };
            
            this.capacity = capacity;
            this.guests = new List<Guest>();
            this.ladiesRoom = new Restroom(restroomCapacity, Gender.Female);
            this.mensRoom = new Restroom(restroomCapacity, Gender.Male);
            this.name = name;
            this.ticketBooth = new MoneyCollectingBooth(attendant, ticketPrice, 3, new MoneyBox());
            this.informationBooth = new GivingBooth(attendant);
            this.ticketBooth.AddMoney(boothMoneyBalance);

            this.cages = new Dictionary<Type, Cage>();

            foreach (AnimalType a in Enum.GetValues(typeof(AnimalType)))
            {
                this.cages.Add(Animal.ConvertAnimalTypetoType(a), new Cage(400, 800, Animal.ConvertAnimalTypetoType(a)));
            }

            this.animals = new List<Animal>();

            // Animals for sorting
            this.AddAnimal(new Chimpanzee("Bobo", 10, 128.2, Gender.Male));
            this.AddAnimal(new Chimpanzee("Bubbles", 3, 103.8, Gender.Female));
            this.AddAnimal(new Dingo("Spot", 5, 41.3, Gender.Male));
            this.AddAnimal(new Dingo("Maggie", 6, 37.2, Gender.Female));
            this.AddAnimal(new Dingo("Toby", 0, 15.0, Gender.Male));
            this.AddAnimal(new Eagle("Ari", 12, 10.1, Gender.Female));
            this.AddAnimal(new Hummingbird("Buzz", 2, 0.02, Gender.Male));
            this.AddAnimal(new Hummingbird("Bitsy", 1, 0.03, Gender.Female));
            this.AddAnimal(new Kangaroo("Kanga", 8, 72.0, Gender.Female));
            this.AddAnimal(new Kangaroo("Roo", 0, 23.9, Gender.Male));
            this.AddAnimal(new Kangaroo("Jake", 9, 153.5, Gender.Male));
            this.AddAnimal(new Ostrich("Stretch", 26, 231.7, Gender.Male));
            this.AddAnimal(new Ostrich("Speedy", 30, 213.0, Gender.Female));
            this.AddAnimal(new Platypus("Patti", 13, 4.4, Gender.Female));
            this.AddAnimal(new Platypus("Bill", 11, 4.9, Gender.Male));
            this.AddAnimal(new Platypus("Ted", 0, 1.1, Gender.Male));
            this.AddAnimal(new Shark("Bruce", 19, 810.6, Gender.Female));
            this.AddAnimal(new Shark("Anchor", 17, 458.0, Gender.Male));
            this.AddAnimal(new Shark("Chum", 14, 377.3, Gender.Male));
            this.AddAnimal(new Squirrel("Chip", 4, 1.0, Gender.Male));
            this.AddAnimal(new Squirrel("Dale", 4, 0.9, Gender.Male));

            // Guests for sorting
            this.AddGuest(new Guest("Greg", 35, 100.0m, WalletColor.Crimson, Gender.Male, new Account()), new Ticket(0m, 0, 0));
            this.AddGuest(new Guest("Darla", 7, 10.0m, WalletColor.Brown, Gender.Female, new Account()), new Ticket(0m, 0, 0));
            this.AddGuest(new Guest("Anna", 8, 12.56m, WalletColor.Brown, Gender.Female, new Account()), new Ticket(0m, 0, 0));
            this.AddGuest(new Guest("Matthew", 42, 10.0m, WalletColor.Brown, Gender.Male, new Account()), new Ticket(0m, 0, 0));
            this.AddGuest(new Guest("Doug", 7, 11.10m, WalletColor.Brown, Gender.Male, new Account()), new Ticket(0m, 0, 0));
            this.AddGuest(new Guest("Jared", 17, 31.70m, WalletColor.Brown, Gender.Male, new Account()), new Ticket(0m, 0, 0));
            this.AddGuest(new Guest("Sean", 34, 20.50m, WalletColor.Brown, Gender.Male, new Account()), new Ticket(0m, 0, 0));
            this.AddGuest(new Guest("Sally", 52, 134.20m, WalletColor.Brown, Gender.Female, new Account()), new Ticket(0m, 0, 0));

            this.FindGuest(g => g.Name == "Greg").AdoptedAnimal = this.FindAnimal(a => a.Name == "Chip");
            this.FindGuest(g => g.Name == "Darla").AdoptedAnimal = this.FindAnimal(a => a.Name == "Chum");
        }

        /// <summary>
        /// Gets the zoo's animal snack machine.
        /// </summary>
        public VendingMachine AnimalSnackMachine
        {
            get
            {
                return this.animalSnackMachine;
            }
        }

        /// <summary>
        /// Gets the average weight of all animals in the zoo.
        /// </summary>
        public double AverageAnimalWeight
        {
            get
            {
                return this.TotalAnimalWeight / this.animals.Count;
            }
        }

        /// <summary>
        /// Gets or sets the temperature of the zoo's birthing room.
        /// </summary>
        public double BirthingRoomTemperature
        {
            get
            {
                return this.b168.Temperature;
            }

            set
            {
                this.b168.Temperature = value;
            }
        }

        /// <summary>
        /// Gets the total weight of all animals in the zoo.
        /// </summary>
        public double TotalAnimalWeight
        {
            get
            {
                // Define accumulator variable.
                double totalWeight = 0;

                // Loop through the list of animals.
                foreach (Animal a in this.animals)
                {
                    // Add current animal's weight to the total.
                    totalWeight += a.Weight;
                }

                return totalWeight;
            }
        }

        /// <summary>
        /// Gets the animals list and returns it as an IEnumerable.
        /// </summary>
        public IEnumerable<Animal> Animals
        {
            get
            {
                return this.animals;
            }
        }

        /// <summary>
        /// Gets the guest list and returns it as an IEnumerable.
        /// </summary>
        public IEnumerable<Guest> Guests
        {
            get
            {
                return this.guests;
            }
        }

        public Action<double, double> OnBirthingRoomTemperatureChange
        {
            get
            {
                return this.onBirthingRoomTemperatureChange;
            }

            set
            {
                this.onBirthingRoomTemperatureChange = value;
            }
        }

        public Action<Guest> OnAddGuest
        {
            get
            {
                return this.onAddGuest;
            }

            set
            {
                this.onAddGuest = value;
            }
        }

        public Action<Guest> OnRemoveGuest
        {
            get
            {
                return this.onRemoveGuest;
            }

            set
            {
                this.onRemoveGuest = value;
            }
        }

        public Action<Animal> OnAddAnimal
        {
            get
            {
                return this.onAddAnimal;
            }

            set
            {
                this.onAddAnimal = value;
            }
        }

        public Action<Animal> OnRemoveAnimal
        {
            get
            {
                return this.onRemoveAnimal;
            }

            set
            {
                this.onRemoveAnimal = value;
            }
        }

        /// <summary>
        /// Creates a new zoo.
        /// </summary>
        /// <returns>Returns created zoo.</returns>
        public static Zoo NewZoo()
        {
            // Create an instance of the Zoo class.
            Zoo comoZoo = new Zoo("Como Zoo", 1000, 4, 0.75m, 25.00m, 3, 3640.25m, new Employee("Sam", 42), new Employee("Flora", 98));

            // Add money to the animal snack machine.
            comoZoo.AnimalSnackMachine.AddMoney(42.75m);
           
            return comoZoo;
        }

        /// <summary>
        /// Adds an animal to the zoo.
        /// </summary>
        /// <param name="animal">The animal to add.</param>
        public void AddAnimal(Animal animal)
        {
            this.animals.Add(animal);
            animal.IsActive = true;

            if (this.OnAddAnimal != null)
            {
                this.OnAddAnimal(animal);
            }

            animal.OnPregnant = addedAnimal =>
            {
                this.b168.PregnantAnimals.Enqueue(animal);
            };

            if (animal.IsPregnant)
            {
                this.b168.PregnantAnimals.Enqueue(animal);
            }

            Cage cage = this.cages[animal.GetType()];
            cage.Add(animal);
        }

        /// <summary>
        /// Removes an animal from the zoo.
        /// </summary>
        /// <param name="animal">Animal to be removed.</param>
        public void RemoveAnimal(Animal animal)
        {
            this.animals.Remove(animal);
            animal.IsActive = false;

            if (this.OnRemoveAnimal != null)
            {
                this.OnRemoveAnimal(animal);
            }

            Cage cage = this.cages[animal.GetType()];
            cage.Remove(animal);

            foreach (Guest g in this.guests)
            {
                if (g.AdoptedAnimal == animal)
                {
                    g.AdoptedAnimal = null;
                    cage.Remove(g);
                }
            }
        }

        /// <summary>
        /// Adds a guest to the zoo.
        /// </summary>
        /// <param name="guest">The guest to add.</param>
        /// <param name="ticket">The guest's ticket.</param>
        public void AddGuest(Guest guest, Ticket ticket)
        {
            if (ticket != null && ticket.IsRedeemed == false)
            {
                ticket.Redeem();

                if (ticket.IsRedeemed == true)
                {
                    this.guests.Add(guest);

                    if (this.OnAddGuest != null)
                    {
                        this.OnAddGuest(guest);
                    }

                    guest.GetVendingMachine += this.ProvideVendingMachine;
                }
            }           
            else
            {
                throw new NullReferenceException("Guest could not be admitted because they did not have a ticket.");
            }
        }

        /// <summary>
        /// Removes guest from the zoo.
        /// </summary>
        /// <param name="guest">Guest to be removed.</param>
        public void RemoveGuest(Guest guest)
        {
            this.guests.Remove(guest);
            guest.IsActive = false;

            if (this.OnRemoveGuest != null)
            {
                this.OnRemoveGuest(guest);
            }

            if (guest.AdoptedAnimal != null)
            {
                this.FindCage(guest.AdoptedAnimal.GetType()).Remove(guest);
            }
        }

        /// <summary>
        /// Sells a ticket.
        /// </summary>
        /// <param name="guest">The guest the ticket is being sold to.</param>
        /// <returns>Returns a ticket.</returns>
        public Ticket SellTicket(Guest guest)
        {
            guest.VisitInformationBooth(this.informationBooth);

            return guest.VisitTicketBooth(this.ticketBooth);
        }

        /// <summary>
        /// Aids a reproducer in giving birth.
        /// </summary>
        public void BirthAnimal()
        {
            // Birth animal.
            IReproducer baby = this.b168.BirthAnimal();

            // If the baby is an animal...
            if (baby is Animal)
            {
                // Add the baby to the zoo's list of animals.
                this.AddAnimal(baby as Animal);
            }
        }        

        /// <summary>
        /// Gets a list of animals based on type.
        /// </summary>
        /// <param name="type">Type of animals to be found.</param>
        /// <returns>Returns list containing animals of desired type.</returns>
        public IEnumerable<Animal> GetAnimals(Type type)
        {
            List<Animal> result = new List<Animal>();

            foreach (Animal a in this.animals)
            {
                if (a.GetType() == type)
                {
                    result.Add(a);
                }
            }

            return result;
        }

        /// <summary>
        /// Finds a cage based on the type of animal it contains.
        /// </summary>
        /// <param name="animalType">Type of animal cage to be found.</param>
        /// <returns>Returns desired cage.</returns>
        public Cage FindCage(Type animalType)
        {
            Cage result = null;

            this.cages.TryGetValue(animalType, out result);

            return result;
        }

        public SortResult SortObjects(string sortType, string sortValue, IList list)
        {
            SortResult result = null;
            Func<object, object, int> compareDelegate;

            if (sortValue == "animalname")
            {
                compareDelegate = AnimalNameSortComparer;
            }
            else if (sortValue == "guestname")
            {
                compareDelegate = GuestNameSortComparer;
            }
            else if (sortValue == "weight")
            {
                compareDelegate = WeightSortComparer;
            }
            else if (sortValue == "age")
            {
                compareDelegate = AgeSortComparer;
            }
            else //if (sortValue == "moneybalance")
            {
                compareDelegate = MoneyBalanceSortComparer;
            }

            switch (sortType)
            {
                case "bubble":
                    result = list.BubbleSort(compareDelegate);
                    break;

                case "selection":
                    result = list.SelectionSort(compareDelegate);
                    break;

                case "insertion":
                    result = list.InsertionSort(compareDelegate);
                    break;

                case "quick":
                    SortResult sortResult = new SortResult();
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();

                    list.QuickSort(0, list.Count - 1, sortResult, compareDelegate);
                    stopwatch.Stop();
                    sortResult.ElapsedMilliseconds = stopwatch.Elapsed.TotalMilliseconds;

                    result = sortResult;                   
                    break;

                default:
                    break;
            }

            return result;
        }

        public SortResult SortAnimals(string sortType, string sortValue)
        {
            return this.SortObjects(sortType, sortValue, this.animals);
        }

        public SortResult SortGuests(string sortType, string sortValue)
        {
            return this.SortObjects(sortType, sortValue, this.guests);
        }

        public void SaveToFile(string fileName)
        {
            // Create a binary formatter
            BinaryFormatter formatter = new BinaryFormatter();

            // Create a file using the passed-in file name
            // Use a using statement to automatically clean up object references
            // and close the file handle when the serialization process is complete
            using (Stream stream = File.Create(fileName))
            {
                // Serialize (save) the current instance of the zoo
                formatter.Serialize(stream, this);
            }
        }

        public static Zoo LoadFromFile(string fileName)
        {
            Zoo result = null;

            // Create a binary formatter
            BinaryFormatter formatter = new BinaryFormatter();

            // Open and read a file using the passed-in file name
            // Use a using statement to automatically clean up object references
            // and close the file handle when the deserialization process is complete
            using (Stream stream = File.OpenRead(fileName))
            {
                // Deserialize (load) the file as a zoo
                result = formatter.Deserialize(stream) as Zoo;
            }

            return result;
        }

        public void OnDeserialized()
        {
            if (this.onBirthingRoomTemperatureChange != null)
            {
                this.onBirthingRoomTemperatureChange(this.b168.Temperature, this.b168.Temperature);
            }

            this.guests.ForEach(g => 
            { 
                if (this.OnAddGuest != null) 
                { 
                    this.OnAddGuest(g); 
                } 
            });

            foreach (Animal a in this.animals)
            {
                if (this.OnAddAnimal != null)
                {
                    this.OnAddAnimal(a);
                }

                a.OnPregnant = addedAnimal =>
                {
                    this.b168.PregnantAnimals.Enqueue(a);
                };
            }
        }

        private void AddAnimalsToZoo(IEnumerable<Animal> animals)
        {
            // loop through passed-in list of animals
            foreach (Animal a in animals)
            {
                // add the current animal to the list (use AddAnimal)
                this.AddAnimal(a);

                // using recursion, add the current animal's children to the zoo
                this.AddAnimalsToZoo(a.Children);
            }
        }

        private VendingMachine ProvideVendingMachine()
        {
            return this.animalSnackMachine;
        }

        private static int AnimalNameSortComparer(object object1, object object2)
        {
            return string.Compare((object1 as Animal).Name, (object2 as Animal).Name);
        }

        private static int WeightSortComparer(object object1, object object2)
        {
            int returnValue = 0;

            if ((object1 as Animal).Weight == (object2 as Animal).Weight)
            {
                returnValue = 0;
            }
            else if ((object1 as Animal).Weight > (object2 as Animal).Weight)
            {
                returnValue = 1;
            }
            else
            {
                returnValue = -1;
            }         

            return returnValue;
        }

        private static int AgeSortComparer(object object1, object object2)
        {
            int returnValue;

            if ((object1 as Animal).Age == (object2 as Animal).Age)
            {
                returnValue = 0;
            }
            else if ((object1 as Animal).Age > (object2 as Animal).Age)
            {
                returnValue = 1;
            }
            else
            {
                returnValue = -1;
            }

            return returnValue;
        }

        private static int GuestNameSortComparer(object object1, object object2)
        {
            return string.Compare((object1 as Guest).Name, (object2 as Guest).Name);
        }

        private static int MoneyBalanceSortComparer(object object1, object object2)
        {
            int returnValue = 0;

            if ((object1 as Guest).Wallet.MoneyBalance + (object1 as Guest).CheckingAccount.MoneyBalance == (object2 as Guest).Wallet.MoneyBalance + (object2 as Guest).CheckingAccount.MoneyBalance)
            {
                returnValue = 0;
            }
            else if ((object1 as Guest).Wallet.MoneyBalance + (object1 as Guest).CheckingAccount.MoneyBalance > (object2 as Guest).Wallet.MoneyBalance + (object2 as Guest).CheckingAccount.MoneyBalance)
            {
                returnValue = 1;
            }
            else
            {
                returnValue = -1;
            }

            return returnValue;
        }
    }
}