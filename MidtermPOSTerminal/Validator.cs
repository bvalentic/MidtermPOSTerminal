using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MidtermPOSTerminal
{
    class Validator
    {//contains all the useful input validation methods

        public static string CheckNumString(string promptString)
        {//verifies input string is all digits
            Console.WriteLine(promptString);
            string inputString = Console.ReadLine();
            Regex.Replace(inputString, @"\s+", "");
            while (!Regex.IsMatch(inputString, $@"\d+"))
            {
                Console.Write($"That's not a valid number. Please enter only digits: ");
                inputString = Console.ReadLine();
            }
            return inputString;
        }

        public static string CheckNumString(string promptString, int stringLength)
        {//verifies input string is all digits of a specified length
            Console.Write(promptString);
            string inputString = Console.ReadLine();
            inputString = Regex.Replace(inputString, @"\s+", "");
            while (!Regex.IsMatch(inputString, @"^\d+$") || inputString.Length != stringLength )
            {
                Console.Write($"That's not a valid number. Please enter a number {stringLength} digits long: ");
                inputString = Console.ReadLine();
                inputString = Regex.Replace(inputString, @"\s+", "");
            }
            return inputString;
        }

        public static string CheckNumString(string promptString, int minNum, int maxNum)
        {//verifies input string is all digits of a specified range
            Console.Write(promptString);
            string inputString = Console.ReadLine();
            inputString = Regex.Replace(inputString, @"\s+", "");
            while (!Regex.IsMatch(inputString, @"^\d+$") || inputString.Length < minNum || inputString.Length > maxNum)
            {
                Console.Write($"That's not a valid number. Please enter a number between {minNum} and {maxNum} digits long: ");
                inputString = Console.ReadLine();
                inputString = Regex.Replace(inputString, @"\s+", "");
            }
            return inputString;
        }

        public static int CheckNum(string inputString, string errorString, int minNum, int maxNum)
        {//confirms input number is an integer in a specified range
            int inputNum;
            while (!(int.TryParse(inputString, out inputNum)) || inputNum < minNum || inputNum > maxNum)
            {
                Console.Write(errorString);
                inputString = Console.ReadLine();
                inputString = Regex.Replace(inputString, @"\s+", "");
            }

            return inputNum;
        }

            public static int CheckNum(string inputString, int minNum, int maxNum)
        {//confirms input number is an integer in a specified range
            int inputNum;
            while (!(int.TryParse(inputString, out inputNum)) || inputNum < minNum || inputNum > maxNum)
            {
                Console.Write($"I'm sorry, that's not a valid input. Please enter a number between {minNum} and {maxNum}: ");
                inputString = Console.ReadLine();
                inputString = Regex.Replace(inputString, @"\s+", "");
            }

            return inputNum;
        }

        public static int CheckNum(string inputString)
        {//confirms input number is a positive integer
            int inputNum;
            while (!(int.TryParse(inputString, out inputNum)) || inputNum < 0)
            {
                Console.Write("I'm sorry, that's not a valid input. Please enter a positive whole number (or 0): ");
                inputString = Console.ReadLine();
                inputString = Regex.Replace(inputString, @"\s+", "");
            }
            return inputNum;
        }

        public static double VerifyCash(string inputString, double total)
        {//confirms input number is an amount greater than the total due
            double inputDouble;
            if (inputString.StartsWith("$"))
            {
                inputString = inputString.Remove(0, 1);
            }
            while (!(double.TryParse(inputString, out inputDouble)) || inputDouble < total)
            {
                Console.Write("I'm sorry, that's not a valid input. Please enter a number greater than or equal to the total: ");
                inputString = Console.ReadLine();
                inputString = Regex.Replace(inputString, @"\s+", "");
                if (inputString.StartsWith("$"))
                {
                    inputString = inputString.Remove(0, 1);
                }
            }
            return inputDouble;
        }

        public static bool CheckYes(string promptString)
        {//verifies input is a "yes" or "no" and returns a bool to that effect
            bool loop = true;
            Console.Write(promptString);
            string inputString = Console.ReadLine().ToLower();
            while (loop)
            {
                if (inputString == "y" || inputString == "yes")
                {
                    return true;
                }
                else if (inputString == "n" || inputString == "no")
                {
                    return false;
                }
                else
                {
                    Console.Write("I'm sorry, I didn't understand. Please enter yes or no: ");
                    inputString = Console.ReadLine().ToLower();
                }
            }
            return true;
        }

        public static string CheckExpy(string promptString)
        {
            Console.Write(promptString);
            string inputString = Console.ReadLine();
            while (!(Regex.IsMatch(inputString, @"^\d{2}[/]\d{4}$")))
            {

                Console.Write("Please enter the expiration date in MM/YYYY format: ");
                inputString = Console.ReadLine();
            }
            string month = inputString.Substring(0, 2);
            string year = inputString.Substring(3, 4);    
            if (int.Parse(month) < 1 || int.Parse(month) > 12)
            {
                Console.WriteLine("\nYou've entered a month that don't exist!\n");
            }
            if (int.Parse(year) < 1840 || int.Parse(year) > 2040)
            {
                Console.WriteLine("\nThis year doesn't seem right, partner. " +
                    "We only accept cards in a two century range after 1840.\n");
            }
            int monthInt = Validator.CheckNum(month, "Please enter a valid month (between 1 and 12): ", 1, 12);
            int yearInt = Validator.CheckNum(year, "Please enter a valid year after 1840 AD, " +
                "up to 2040 AD (if man is still alive): ", 1840, 2040);

            inputString = monthInt.ToString("00") + "/" + yearInt.ToString();
            return inputString;
        }
    }
}
