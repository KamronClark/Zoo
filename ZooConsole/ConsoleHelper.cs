using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accounts;
using Animals;
using People;
using Zoos;

namespace ZooConsole
{
    /// <summary>
    /// Class which contains some functionality of the console.
    /// </summary>
    internal static class ConsoleHelper
    {
        /// <summary>
        /// Processes the add command.
        /// </summary>
        /// <param name="zoo">Current zoo.</param>
        /// <param name="type">Type of thing to be added.</param>
        public static void ProcessAddCommand(Zoo zoo, string type)
        {
            switch (type)
            {
                case "animal":
                    AddAnimal(zoo);
                    break;

                case "guest":
                    AddGuest(zoo);
                    break;

                default:
                    Console.WriteLine("The add command only supports adding animals and guests.");
                    break;
            }
        }

        /// <summary>
        /// Processes the remove command.
        /// </summary>
        /// <param name="zoo">Current zoo.</param>
        /// <param name="type">Type of thing to be removed.</param>
        /// <param name="name">Name of thing to be removed.</param>
        public static void ProcessRemoveCommand(Zoo zoo, string type, string name)
        {
            switch (type)
            {
                case "animal":
                    RemoveAnimal(zoo, ConsoleUtil.InitialUpper(name));
                    break;

                case "guest":
                    RemoveGuest(zoo, ConsoleUtil.InitialUpper(name));
                    break;

                default:
                    Console.WriteLine("The remove command only supports removing animals and guests.");
                    break;
            }
        }
        
        /// <summary>
        /// Processes the show command.
        /// </summary>
        /// <param name="zoo">The present zoo.</param>
        /// <param name="type">Type of object to be shown.</param>
        /// <param name="name">Name of object to be shown.</param>
        public static void ProcessShowCommand(Zoo zoo, string type, string name)
        {
            name = ConsoleUtil.InitialUpper(name);
            switch (type)
            {
                case "animal":
                    ShowAnimal(zoo, name);
                    break;

                case "guest":
                    ShowGuest(zoo, name);
                    break;

                case "cage":
                    ShowCage(zoo, name);
                    break;

                case "children":
                    ShowChildren(zoo, name);
                    break;

                default:
                    Console.WriteLine("Only animals, guests, and cages can be shown");
                    break;
            }
        }

        /// <summary>
        /// Sets the temperature of the zoo's birthing room.
        /// </summary>
        /// <param name="zoo">The present zoo.</param>
        /// <param name="temperature">Temperature to be set.</param>
        public static void SetTemperature(Zoo zoo, string temperature)
        {
            double currentTemp = zoo.BirthingRoomTemperature;

            try
            {
                double newTemp = zoo.BirthingRoomTemperature = double.Parse(temperature);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (FormatException)
            {
                Console.WriteLine("The number must be submitted as a parameter.");
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("A parameter must be entered to set temperature.");
            }
        }

        public static void ShowHelpDetail(string command)
        {
            Dictionary<string, string> arguments;

            switch (command)
            {
                case "show":
                    arguments = new Dictionary<string, string>();
                    arguments.Add("objectType", "The type of object to show (animal, guest, or cage).");
                    arguments.Add("objectName", "The name of the object to show(use an animal name for cage).");
                    ConsoleUtil.WriteHelpDetail("SHOW", "Shows the details of an object.", arguments);
                    break;

                case "remove":
                    arguments = new Dictionary<string, string>();
                    arguments.Add("objectType", "The type of object to remove (animal or guest).");
                    arguments.Add("objectName", "The name of the object to remove.");
                    ConsoleUtil.WriteHelpDetail("REMOVE", "Removes an object from the zoo.", arguments);
                    break;

                case "temp":
                    ConsoleUtil.WriteHelpDetail("TEMP", "Sets the temperature of the birthing room.", "temperature", "Integer between 35 and 95 representing the temperature you want to set the birthing room to.");
                    break;

                case "add":
                    ConsoleUtil.WriteHelpDetail("ADD", "Adds an object to the zoo", "objectType", "The type of object to be added to the zoo (animal or guest).");
                    break;

                case "restart":
                    ConsoleUtil.WriteHelpDetail("RESTART", "Creates a new zoo.");
                    break;

                case "exit":
                    ConsoleUtil.WriteHelpDetail("EXIT", "Exits the application.");
                    break;
            }
        }

        public static void ShowHelp()
        {
            ConsoleUtil.WriteHelpDetail("help", "Show help details.", "[command]", "The (optional) command for which to show help details.");

            Console.WriteLine("Known commands: ");
            Console.WriteLine("RESTART: Creates a new zoo.");
            Console.WriteLine("EXIT: Exits the application.");
            Console.WriteLine("TEMP: Sets the temperature of the zoo's birthing room.");
            Console.WriteLine("SHOW: Shows the properties of an animal, guest, or cage.");
            Console.WriteLine("ADD: Adds an animal or guest to the zoo.");
            Console.WriteLine("REMOVE: Removes a guest or animal from the zoo.");
        }

        public static void SaveFile(Zoo zoo, string fileName)
        {
            try
            {
                zoo.SaveToFile(fileName);
                Console.WriteLine("Your zoo has been successfully saved.");
            }
            catch
            {
                Console.WriteLine("Your zoo was not saved successfully.");
            }
        }

        public static Zoo LoadFile(string fileName)
        {
            Zoo result = null;

            try
            {                
                result = Zoo.LoadFromFile(fileName);
                Console.WriteLine("Your zoo has been loaded successfully.");
            }
            catch
            {
                Console.WriteLine("Your zoo was not loaded successfully.");
            }

            return result;
        }

        public static void AttachDelegates(Zoo zoo)
        {
            zoo.OnBirthingRoomTemperatureChange = (previousTemp, currentTemp) =>
            {
                Console.WriteLine(previousTemp);
                Console.WriteLine(currentTemp);
            };
        }

        public static IEnumerable<object> QueryHelper(Zoo zoo, string query)
        {
            List<Animal> animals = zoo.Animals as List<Animal>;
            IEnumerable<object> queryResult = null;

            switch (query)
            {
                case "totalanimalweight":
                    //queryResult = animals.Sum(tw => tw.Weight).ToString();
                    break;

                case "averageanimalweight":
                    //queryResult = animals.Average(aw => aw.Weight).ToString();
                    break;

                case "animalcount":
                    //queryResult = animals.Count().ToString();
                    break;

                case "getheavyanimals":
                    queryResult = zoo.GetHeavyAnimals();
                    break;

                case "getyoungguests":
                    queryResult = zoo.GetYoungGuests();
                    break;

                case "getfemaledingoes":
                    queryResult = zoo.GetFemaleDingoes();
                    break;

                case "getguestsbyage":
                    queryResult = zoo.GetGuestsByAge();
                    break;

                case "getflyinganimals":
                    queryResult = zoo.GetFlyingAnimals();
                    break;

                case "getadoptedanimals":
                    queryResult = zoo.GetAdoptedAnimals();
                    break;

                case "totalbalancebycolor":
                    queryResult = zoo.GetTotalBalanceByWalletColor();
                    break;

                case "averageweightbyanimaltype":
                    queryResult = zoo.GetAverageWeightByAnimalType();
                    break;
            }

            return queryResult;
        }

        /// <summary>
        /// Adds an animal to the zoo.
        /// </summary>
        /// <param name="zoo">Current zoo.</param>
        private static void AddAnimal(Zoo zoo)
        {
            Animal animal = null;
            
            bool success = false;

            while (!success)
            {
                try
                {
                    AnimalType type = ConsoleUtil.ReadAnimalType();
                    animal = AnimalFactory.CreateAnimal(type, "Kam", 1, 1.0, Reproducers.Gender.Male);
                    success = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Age could not be set: " + ex.Message);
                }
            }

            success = false;

            while (!success)
            {
                try
                {
                    animal.Name = ConsoleUtil.InitialUpper(ConsoleUtil.ReadAlphabeticValue("Name"));
                    success = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Name could not be set: " + ex.Message);
                }
            }

            success = false;

            while (!success)
            {
                try
                {
                    animal.Gender = ConsoleUtil.ReadGender();
                    success = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Gender could not be set (Male or Female): " + ex.Message);
                }
            }

            success = false;

            while (!success)
            {
                try
                {
                    animal.Age = ConsoleUtil.ReadIntValue("Age");
                    success = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Age could not be set: " + ex.Message);
                }
            }

            success = false;

            while (!success)
            {
                try
                {
                    animal.Weight = ConsoleUtil.ReadDoubleValue("Weight");
                    success = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Weight could not be set: " + ex.Message);
                }
            }

            zoo.AddAnimal(animal);
            ShowAnimal(zoo, animal.Name);
        }

        /// <summary>
        /// Removes an animal from the zoo.
        /// </summary>
        /// <param name="zoo">Current zoo.</param>
        /// <param name="name">Name of animal to be removed.</param>
        private static void RemoveAnimal(Zoo zoo, string name)
        {
            Animal animal = zoo.FindAnimal(a => a.Name == name);

            if (animal != null)
            {
                zoo.RemoveAnimal(animal);
                Console.WriteLine(animal.ToString() + " Was removed.");
            }
            else
            {
                Console.WriteLine("Animal could not be found.");
            }
        }

        /// <summary>
        /// Adds a guest to the zoo.
        /// </summary>
        /// <param name="zoo">Current zoo.</param>
        private static void AddGuest(Zoo zoo)
        {
            Guest guest = new Guest("Kam", 19, 0.0m, WalletColor.Black, Reproducers.Gender.Male, new Account());

            bool success = false;

            while (!success)
            {
                try
                {
                    guest.Name = ConsoleUtil.InitialUpper(ConsoleUtil.ReadAlphabeticValue("Name"));
                    success = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Name could not be set: " + ex.Message);
                }
            }

            success = false;

            while (!success)
            {
                try
                {
                    guest.Gender = ConsoleUtil.ReadGender();
                    success = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Gender could not be set (Male or Female): " + ex.Message);
                }
            }

            success = false;

            while (!success)
            {
                try
                {
                    guest.Age = ConsoleUtil.ReadIntValue("Age");
                    success = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Age could not be set: " + ex.Message);
                }
            }

            success = false;

            while (!success)
            {
                try
                {
                    guest.Wallet.AddMoney(ConsoleUtil.ReadIntValue("Wallet money balance"));
                    success = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Wallet money balance could not be set: " + ex.Message);
                }
            }

            success = false;

            while (!success)
            {
                try
                {
                    guest.Wallet.WalletColor = ConsoleUtil.ReadWalletColor();
                    success = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Wallet color could not be set: " + ex.Message);
                }
            }

            success = false;

            while (!success)
            {
                try
                {
                    guest.CheckingAccount.AddMoney(ConsoleUtil.ReadIntValue("Checking account balance"));
                    success = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Wallet color could not be set: " + ex.Message);
                }
            }

            try
            {
                zoo.AddGuest(guest, zoo.SellTicket(guest));
                ShowGuest(zoo, guest.Name);
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("The zoo is out of tickets, Sorry!");
            }           
        }

        /// <summary>
        /// Removes a guest from the zoo.
        /// </summary>
        /// <param name="zoo">Current zoo.</param>
        /// <param name="name">Name of guest to be removed.</param>
        private static void RemoveGuest(Zoo zoo, string name)
        {
            Guest guest = zoo.FindGuest(g => g.Name == name);

            if (guest != null)
            {
                zoo.RemoveGuest(guest);
                Console.WriteLine(guest.ToString() + " Was removed.");
            }
            else
            {
                Console.WriteLine("Guest could not be found.");
            }
        }

        /// <summary>
        /// Shows the desired animal.
        /// </summary>
        /// <param name="zoo">The present zoo.</param>
        /// <param name="name">Name of the animal.</param>
        private static void ShowAnimal(Zoo zoo, string name)
        {
            try
            {              
                Animal animalFound = zoo.FindAnimal(a => a.Name == name);
                if (animalFound != null)
                {
                    Console.WriteLine("The following animal was found: " + animalFound.ToString());
                }
                else
                {
                    Console.WriteLine("The specified animal could not be found");
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (FormatException)
            {
                Console.WriteLine("The animal name must be submitted as a parameter.");
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("A parameter must be entered to show the animal.");
            }
        }

        /// <summary>
        /// Shows the desired guest.
        /// </summary>
        /// <param name="zoo">The present zoo.</param>
        /// <param name="name">Name of the guest to be shown.</param>
        private static void ShowGuest(Zoo zoo, string name)
        {
            try
            {
                Guest guestFound = zoo.FindGuest(g => g.Name == name);
                if (guestFound != null)
                {
                    Console.WriteLine("The following guest was found: \n" + guestFound.ToString());
                }
                else
                {
                    Console.WriteLine("The specified guest could not be found");
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (FormatException)
            {
                Console.WriteLine("The guest name must be submitted as a parameter.");
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("A parameter must be entered to show the guest.");
            }
        }

        private static void ShowCage(Zoo zoo, string animalName)
        {
            try
            {
                Animal animalFound = zoo.FindAnimal(a => a.Name == animalName);
                if (animalFound != null)
                {
                    Cage cageFound = zoo.FindCage(animalFound.GetType());

                    Console.WriteLine("The following cage was found: \n" + cageFound.ToString());
                }
                else
                {
                    Console.WriteLine("The specified cage could not be found.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("The animal name must be submitted as a parameter.");
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("A parameter must be entered to show the animal.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void ShowChildren(Zoo zoo, string name)
        {
            WalkTree(zoo.FindAnimal(a => a.Name == name), "");
        }

        private static void WalkTree(Animal animal, string prefix)
        {            
            Console.WriteLine(prefix + animal.Name);
            
            foreach (Animal a in animal.Children)
            {
                WalkTree(a, prefix + "  ");
            }
        }
    }
}
