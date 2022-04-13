using System;
using System.Collections.Generic;
using System.Linq;
using Animals;
using BirthingRooms;
using People;
using Zoos;

namespace ZooConsole
{
    /// <summary>
    /// The class that is used to manage interaction with the console.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Serves as the entry point for the console application.
        /// </summary>
        /// <param name="args">The command-line arguments passed into the console application.</param>
        public static void Main(string[] args)
        {
            Console.Title = "Object-Oriented Programming 2: Zoo";

            bool exit = false;

            Zoo zoo = Zoo.NewZoo();
            ConsoleHelper.AttachDelegates(zoo);

            Console.WriteLine("Welcome to the Como Zoo!");

            while (!exit)
            {
                Console.Write("] ");

                string command = Console.ReadLine();
                command = command.ToLower().Trim();
                string[] commandWords = command.Split();

                switch (commandWords[0])
                {
                    case "help":
                        if (commandWords.Length == 2)
                        {
                            ConsoleHelper.ShowHelpDetail(commandWords[1]);
                        }
                        else if (commandWords.Length == 1)
                        {
                            ConsoleHelper.ShowHelp();
                        }
                        else
                        {
                            Console.WriteLine("Too many parameters entered.");
                        }

                        break;

                    case "exit":
                        exit = true;
                        break;

                    case "restart":
                        ConsoleHelper.AttachDelegates(zoo);
                        zoo = Zoo.NewZoo();
                        zoo.BirthingRoomTemperature = 77;
                        Console.WriteLine("A new Como Zoo has been created.");
                        break;

                    case "temp":
                        ConsoleHelper.SetTemperature(zoo, commandWords[1]);
                        break;

                    case "show":
                        ConsoleHelper.ProcessShowCommand(zoo, commandWords[1], commandWords[2]);                        
                        break;

                    case "add":
                        ConsoleHelper.ProcessAddCommand(zoo, commandWords[1]);
                        break;

                    case "remove":
                        ConsoleHelper.ProcessRemoveCommand(zoo, commandWords[1], commandWords[2]);
                        break;

                    case "sort":

                        if (commandWords[1] == "animals")
                        {
                            SortResult resultList = null;

                            try
                            {
                                resultList = zoo.SortAnimals(commandWords[2], commandWords[3]);
                            }
                            catch
                            {
                                Console.WriteLine("Sort command must be entered as: sort [sort type] [sort by -- weight or name].");
                            }

                            Console.WriteLine("SORT TYPE: " + commandWords[2].ToUpper());
                            Console.WriteLine("SORT BY: " + commandWords[1].ToUpper());
                            Console.WriteLine("SORT VALUE: " + commandWords[3].ToUpper());
                            Console.WriteLine("SWAP COUNT: " + resultList.SwapCount);
                            Console.WriteLine("COMPARE COUNT:" + resultList.CompareCount);

                            foreach (Animal a in resultList.Objects)
                            {
                                Console.WriteLine(a.ToString());
                            }
                        }
                        else if (commandWords[1] == "guests")
                        {
                            SortResult resultList = null;

                            try
                            {
                                resultList = zoo.SortGuests(commandWords[2], commandWords[3]);
                            }
                            catch
                            {
                                Console.WriteLine("Sort command must be entered as: sort [sort type] [sort by -- weight or name].");
                            }

                            Console.WriteLine("SORT TYPE: " + commandWords[2].ToUpper());
                            Console.WriteLine("SORT BY: " + commandWords[1].ToUpper());
                            Console.WriteLine("SORT VALUE: " + commandWords[3].ToUpper());
                            Console.WriteLine("SWAP COUNT: " + resultList.SwapCount);
                            Console.WriteLine("COMPARE COUNT:" + resultList.CompareCount);

                            foreach (Guest g in resultList.Objects)
                            {
                                Console.WriteLine(g.ToString());
                            }
                        }

                        break;

                    case "search":

                        if (commandWords[1] == "linear")
                        {
                            int loop = 0;

                            foreach (Animal a in zoo.Animals)
                            {
                                loop++;
                                if (a.Name.ToLower() == commandWords[2])
                                {
                                    Console.WriteLine(a.Name + " found. " + loop + " loops complete.");
                                    break;
                                }
                            }
                        }

                        if (commandWords[1] == "binary")
                        {
                            int loop = 0;
                            string animalName = ConsoleUtil.InitialUpper(commandWords[2]);
                            SortResult animals = zoo.SortAnimals("bubble", "name");
                            int minPosition = 0;
                            int maxPosition = zoo.Animals.Count() - 1;

                            while (minPosition <= maxPosition)
                            {
                                int midPosition = (minPosition + maxPosition) / 2;

                                loop++;

                                if (string.Compare(animalName, (animals.Objects[midPosition] as Animal).Name) > 0)
                                {
                                    minPosition = midPosition + 1;
                                }
                                else if (string.Compare(animalName, (animals.Objects[midPosition] as Animal).Name) < 0)
                                {
                                    maxPosition = midPosition - 1;
                                }
                                else
                                {
                                    Console.WriteLine((animals.Objects[midPosition] as Animal).Name + " found. " + loop + " loops complete.");
                                    break;
                                }                                                                
                            }
                        }

                        break;

                    case "save":
                        ConsoleHelper.SaveFile(zoo, commandWords[1]);
                        break;

                    case "load":
                        ConsoleHelper.AttachDelegates(zoo);
                        ConsoleHelper.LoadFile(commandWords[1]);
                        break;

                    case "query":
                        IEnumerable<object> query = ConsoleHelper.QueryHelper(zoo, commandWords[1]);
                        Console.Write(commandWords[1].ToUpper() + ": ");
                        query.ToList().ForEach(q => Console.WriteLine(q.ToString()));
                        break;

                    default:
                        Console.WriteLine("Invalid Command: " + command);
                        break;
                }
            }
        }
    }
}
