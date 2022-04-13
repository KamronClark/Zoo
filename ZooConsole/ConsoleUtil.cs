using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Animals;
using People;
using Reproducers;
using Utilities;

namespace ZooConsole
{
    /// <summary>
    /// The class which contains utilities used by the console.
    /// </summary>
    internal static class ConsoleUtil
    {
        /// <summary>
        /// Changes the first letter of a string to an upper-case letter.
        /// </summary>
        /// <param name="value">String to be changed.</param>
        /// <returns>Returns initial upper-case string.</returns>
        public static string InitialUpper(string value)
        {
            if (value != null && value.Length > 0)
            {
                return value = char.ToUpper(value[0]) + value.Substring(1);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Reads an alphabetic value.
        /// </summary>
        /// <param name="prompt">Prompt to be shown.</param>
        /// <returns>Returns user-submitted alphabetic value.</returns>
        public static string ReadAlphabeticValue(string prompt)
        {
            string result = null;

            bool found = false;

            while (!found)
            {
                result = ConsoleUtil.ReadStringValue(prompt);

                if (Regex.IsMatch(result, @"^[a-zA-Z ]+$"))
                {
                    found = true;
                }
                else
                {
                    Console.WriteLine(prompt + " must contain only letters or spaces.");
                }
            }

            return result;
        }

        /// <summary>
        /// Reads a double value.
        /// </summary>
        /// <param name="prompt">Prompt to be shown.</param>
        /// <returns>Returns user-submitted double value.</returns>
        public static double ReadDoubleValue(string prompt)
        {
            double result = 0;

            string stringValue = result.ToString();

            bool found = false;

            while (!found)
            {
                stringValue = ConsoleUtil.ReadStringValue(prompt);

                if (double.TryParse(stringValue, out result))
                {
                    found = true;
                }
                else
                {
                    Console.WriteLine(prompt + " must be either a whole number or a decimal number.");
                }
            }

            return result;
        }

        /// <summary>
        /// Reads gender.
        /// </summary>
        /// <returns>Returns user-submitted gender.</returns>
        public static Gender ReadGender()
        {
            Gender result = Gender.Female;

            string stringValue = result.ToString();

            bool found = false;

            while (!found)
            {
                stringValue = ConsoleUtil.ReadAlphabeticValue("Gender");

                stringValue = ConsoleUtil.InitialUpper(stringValue);

                // If a matching enumerated value can be found...
                if (Enum.TryParse<Gender>(stringValue, out result))
                {
                    found = true;
                }
                else
                {
                    Console.WriteLine("Invalid gender.");
                }
            }

            return result;
        }

        /// <summary>
        /// Reads an int value.
        /// </summary>
        /// <param name="prompt">Prompt to be shown.</param>
        /// <returns>Returns user-submitted int value.</returns>
        public static int ReadIntValue(string prompt)
        {
            int result = 0;

            string stringValue = result.ToString();

            bool found = false;

            while (!found)
            {
                stringValue = ConsoleUtil.ReadStringValue(prompt);

                if (int.TryParse(stringValue, out result))
                {
                    found = true;
                }
                else
                {
                    Console.WriteLine(prompt + " must be a whole number.");
                }
            }

            return result;
        }

        /// <summary>
        /// Reads a string value.
        /// </summary>
        /// <param name="prompt">Prompt to be shown.</param>
        /// <returns>Returns user-submitted string value.</returns>
        public static string ReadStringValue(string prompt)
        {
            string result = null;

            bool found = false;

            while (!found)
            {
                Console.Write(prompt + "] ");

                string stringValue = Console.ReadLine().ToLower().Trim();

                Console.WriteLine();

                if (stringValue != string.Empty)
                {
                    result = stringValue;
                    found = true;
                }
                else
                {
                    Console.WriteLine(prompt + " must have a value.");
                }
            }

            return result;
        }

        /// <summary>
        /// Reads animal type.
        /// </summary>
        /// <returns>Returns user-submitted animal type.</returns>
        public static AnimalType ReadAnimalType()
        {
            AnimalType result = AnimalType.Dingo;

            string stringValue = result.ToString();

            bool found = false;

            while (!found)
            {
                stringValue = ConsoleUtil.ReadAlphabeticValue("Type");

                stringValue = ConsoleUtil.InitialUpper(stringValue);

                // If a matching enumerated value can be found...
                if (Enum.TryParse<AnimalType>(stringValue, out result))
                {
                    found = true;
                }
                else
                {
                    Console.WriteLine("Invalid animal type.");
                }
            }

            return result;
        }

        /// <summary>
        /// Reads wallet color.
        /// </summary>
        /// <returns>Returns user-submitted wallet color.</returns>
        public static WalletColor ReadWalletColor()
        {
            WalletColor result = WalletColor.Black;

            string stringValue = result.ToString();

            bool found = false;

            while (!found)
            {
                stringValue = ConsoleUtil.ReadAlphabeticValue("Wallet Color");

                stringValue = ConsoleUtil.InitialUpper(stringValue);

                // If a matching enumerated value can be found...
                if (Enum.TryParse<WalletColor>(stringValue, out result))
                {
                    found = true;
                }
                else
                {
                    Console.WriteLine("Invalid wallet color.");
                }
            }

            return result;
        }

        public static void WriteHelpDetail(string command, string overview, Dictionary<string, string> arguments)
        {
            Console.WriteLine("Command name: " + command);
            Console.WriteLine("Overview: " + overview);
            if (arguments != null)
            {
                Console.WriteLine("Usage: " + command + " " + arguments.Keys.Flatten(" "));
                Console.WriteLine("");
                Console.WriteLine("Parameters: ");

                arguments.ToList().ForEach(kvp => Console.WriteLine(kvp.Key + ":" + kvp.Value));
            }            
        }

        public static void WriteHelpDetail(string command, string overview, string argument, string argumentUsage)
        {
            Dictionary<string, string> arguments = new Dictionary<string, string>();
            arguments.Add(argument, argumentUsage);

            WriteHelpDetail(command, overview, arguments);
        }

        public static void WriteHelpDetail(string command, string overview)
        {
            WriteHelpDetail(command, overview, null);
        }
    }
}
