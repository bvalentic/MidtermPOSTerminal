using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidtermPOSTerminal
{
    class Validator
    {//contains all the useful input validation methods

        //public static int CheckNum(string promptString, int minNum, int maxNum)
        //{
        //    Console.Write(promptString);
        //    string inputString = Console.ReadLine();
        //    int inputNum;
        //    while(!(int.TryParse(inputString, out inputNum)) || inputNum < minNum || inputNum > maxNum)
        //    {
        //        Console.Write($"I'm sorry, that's not a valid input. Please enter a number between {minNum} and {maxNum}: ");
        //        inputString = Console.ReadLine();
        //    }

        //    return inputNum;
        //}

        public static int CheckNum(string inputString, int minNum, int maxNum)
        {
            int inputNum;
            while (!(int.TryParse(inputString, out inputNum)) || inputNum < minNum || inputNum > maxNum)
            {
                Console.Write($"I'm sorry, that's not a valid input. Please enter a number between {minNum} and {maxNum}: ");
                inputString = Console.ReadLine();
            }

            return inputNum;
        }

        public static int CheckNum(string inputString)
        {
            int inputNum;
            while (!(int.TryParse(inputString, out inputNum)) || inputNum < 0)
            {
                Console.Write($"I'm sorry, that's not a valid input. Please enter a number greater than 0: ");
                inputString = Console.ReadLine();
            }

            return inputNum;
        }

        public static bool CheckYes(string promptString)
        {
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
    }
}
