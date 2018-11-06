using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MidtermPOSTerminal
{
    class Program
    {
        static void Main(string[] args)
        {

            //directory and file I/O setup stuff

            List<Goods> goodsList = FileUser.MakeGoods();
            goodsList.Sort();
            List<Goods> cart = new List<Goods> { };

            //rest of program

            Console.WriteLine("Howdy, partner! \n" +
                "Welcome to the Independence General Store, of Independence, Missouri. \n" +
                "Your one-stop shop for headin' down the Oregon Trail!");
            
            MainMenu(goodsList, cart);

            //once user decides to quit
            Console.WriteLine("Thank you for your patronage, and good luck on the trail!");
            Console.ReadKey();
        }

        //main "hub" method

        public static void MainMenu(List<Goods> goodsList, List<Goods> cart)
        {
            bool keepGoing = true;
            while (keepGoing)
            {
                ListOptions();
                string inputString = Console.ReadLine().ToLower();
                Console.WriteLine(new string('_',inputString.Length));
                if (inputString == "1" || inputString == "goods")
                {//view list of goods for sale
                    PrintMenu(goodsList);
                    GoToCart(goodsList, cart);
                }
                else if (inputString == "2" || inputString == "sort")
                {//sort list
                    Console.WriteLine("\nList of goods for sale:\n");
                    goodsList = SortMenu(goodsList);
                }
                else if (inputString == "3" || inputString.Contains("add"))
                {//add to cart
                    AddCartOptions(inputString, goodsList, cart);
                }
                else if (inputString == "4" || inputString == "cart")
                {//view cart
                    Console.WriteLine("\nHere's what's in your cart:\n");
                    ViewCartOptions(cart);
                }
                else if (inputString == "5" || inputString == "buy")
                {//buy items in cart
                    if (cart.Count > 0)
                    {
                        Buy(cart);
                    }
                    else Console.WriteLine("\nThere's nothin' in your cart!");
                }
                else if (inputString == "6" || inputString == "exit" || inputString == "quit")
                {//quit
                    keepGoing = Quitter();
                    if (keepGoing) Console.WriteLine("\nOkay!\n");
                }
                else
                {
                    Console.WriteLine("I'm sorry, I don't understand. \n" +
                        "Please enter a number or word corresponding to the choice you wish to make.\n");
                }
            }
        }

        public static void ListOptions()
        {
            Console.WriteLine("\nWhat would you like to do?");
            Console.WriteLine($@"
{"1) goods",-8} -- view list of goods for sale
{"2) sort",-8} -- sort list of goods by name, price, or category
{"3) add",-8} -- view and choose an item to add to your cart of potential purchases");
            Console.WriteLine("(or \"add [number]\" to add corresponding item to cart)");
Console.Write($@"{"4) cart",-8} -- view goods added to your cart
{"5) buy",-8} -- purchase the goods you have in your cart
{"6) exit",-8} -- leave the general store

");
        }

        //methods involving menu (printing, sorting, adding to cart)
        

        public static void PrintMenu(List<Goods> goodsList)
        {//displays list of goods
            const int nameLength = 30;
            Console.WriteLine($"\n{"#",-3} {"Name",-nameLength} {"Category",-15} {"Price", -5}");
            Console.WriteLine($"{"-",-3} {"----",-nameLength} {"--------",-15} {"-----",-5}");
            for (int i = 0; i < goodsList.Count; i++)
            {
                Console.WriteLine($"{i + 1,-3} {goodsList[i].Name,-nameLength} {goodsList[i].Category,-15} {goodsList[i].Price,-5:C}");
            }
        }

        public static List<Goods> SortMenu(List<Goods> goodsList)
        {//sorts list of goods by either name, price (high or low), and category
            bool sortLoop = true;
            Console.WriteLine("Sort by name, price, or category? ");
            string inputString = Console.ReadLine().ToLower();
            while (sortLoop)
            {
                if (inputString == "name")
                {
                    goodsList.Sort();
                    Console.WriteLine("Sorted by name!");
                    PrintMenu(goodsList);
                    sortLoop = false;
                }
                else if (inputString == "price")
                {
                    goodsList = goodsList.OrderBy(good => good.Price).ToList<Goods>();
                    Console.WriteLine("Sorted by price!");
                    PrintMenu(goodsList);
                    sortLoop = false;
                }
                else if (inputString == "category")
                {
                    goodsList = goodsList.OrderBy(good => good.Category).ToList<Goods>();
                    Console.WriteLine("Sorted by category!");
                    PrintMenu(goodsList);
                    sortLoop = false;
                }
                else
                {
                    Console.Write("I'm sorry, I didn't understand. " +
                        "Please enter \"name\", \"price\", or \"category\": ");
                    inputString = Console.ReadLine().ToLower();
                }
            }
            Console.WriteLine("\nPress any key to continue.");
            Console.ReadKey();
            return goodsList;
        }

        //cart-related methods

        public static void AddCartOptions(string inputString, List<Goods> goodsList, List<Goods> cart)
        {
            if (inputString.Length > 3)
            {//user input add [number]
                inputString = inputString.Remove(0, 3).Trim(' ');
                if (inputString.Length > 0)
                {
                    int inputNum = Validator.CheckNum(inputString, 0, goodsList.Count);
                    AddToCart(goodsList, cart, inputNum);
                }
                else
                {
                    PrintMenu(goodsList);
                    Console.WriteLine("What do you want to add to your cart?");
                    GoToCart(goodsList, cart);
                }
            }
            else
            {//user input "add"
                PrintMenu(goodsList);
                Console.WriteLine("What do you want to add to your cart?");
                GoToCart(goodsList, cart);
            }
        }

        public static double PrintCart(List<Goods> cart)
        {
            const int nameLength = 30;
            Console.WriteLine($"\n{"#",-3} {"Name",-nameLength} {"Category",-15} {"Quantity",-10} {"Price",-8} {"Line Total",-10}");
            Console.WriteLine($"{"-",-3} {"----",-nameLength} {"--------",-15} {"--------",-10} {"-----",-8} {"----------",-10}");
            double subtotal = 0;

            for (int i = 0; i < cart.Count; i++)
            {
                Console.WriteLine($"{i + 1,-3} {cart[i].Name,-nameLength} {cart[i].Category,-15} {cart[i].Quantity,-10} " +
                    $"{cart[i].Price,-8:C} {cart[i].Quantity * cart[i].Price,-10:C}");
                subtotal += cart[i].Quantity * cart[i].Price;
            }
            Console.WriteLine($"\n{"Subtotal:",-25} {subtotal,51:C}");
            double tax = subtotal * 0.06;
            double total = Math.Round(subtotal + tax,2);
            Console.WriteLine($"{"Sales tax:",-25} {tax,51:C}");
            Console.WriteLine($"{"Total:",-25} {total,51:C}");
            return total;
        }

        public static void ViewCartOptions(List<Goods> cart)
        {
            if (cart.Count > 0)
            {
                bool edit = true;
                while (edit)
                {
                    PrintCart(cart);
                    edit = Validator.CheckYes("\nWould you like to edit your cart? ");
                    if (edit)
                    {
                        EditCart(cart);
                    }
                }
            }
            else
            {
                Console.WriteLine("\nThere's nothin' in your cart!");
            }
        }

        public static void GoToCart(List<Goods> goodsList, List<Goods> cart)
        {
            Console.Write("\nEnter a number to view a description of that item,\n" +
                        "or enter \"0\" to go back: ");
            string inputString = Console.ReadLine();
            int inputNum = Validator.CheckNum(inputString, 0, goodsList.Count);
            if (inputNum == 0)
            {
                Console.WriteLine("Returning to the main menu. . .");
                return;
            }
            else
            {
                AddToCart(goodsList, cart, inputNum);
            }
        }

        public static void AddToCart(List<Goods> goodsList, List<Goods> cart, int inputNum)
        {
            Goods inputGood = goodsList[inputNum - 1];
            Console.WriteLine($"\nYou have chosen {inputNum} -- {inputGood.Name}\n");
            inputGood.ViewItem();
            bool check = Validator.CheckYes("\nWould you like to add this to your cart? (y/n) ");
            if (check)
            {
                Console.Write("Quantity? ");
                string inputString = Console.ReadLine();
                int amount = Validator.CheckNum(inputString);
                if (amount > 0)
                {
                    cart.Add(inputGood);
                    inputGood.Quantity += amount;
                    Console.WriteLine($"Added {amount} {inputGood.Name} to cart! ");
                }
                else
                {
                    Console.WriteLine("Not buyin', eh? Fine by me. Feel free to keep browsin'!");
                }
            }
            else
            {
                Console.WriteLine("Returnin' to the main menu. . .");
            }
        }

        public static void EditCart(List<Goods> cart)
        {
            Console.Write("Enter the number of the item you want to edit, or enter \"0\" to return: ");
            string inputString = Console.ReadLine();
            int inputNum = Validator.CheckNum(inputString, 0, cart.Count);
            if (inputNum == 0)
            {
                Console.WriteLine("Returnin' to the main menu. . .");
            }
            else
            {
                Goods editGood = cart[inputNum - 1];
                Console.WriteLine();
                editGood.ViewItem();
                Console.WriteLine(@"
Would you like to:
1 -- change the quantity of this item
2 -- remove this item from the cart
3 -- return to the cart menu");
                inputString = Console.ReadLine();
                inputNum = Validator.CheckNum(inputString, 1, 3);

                if (inputNum == 1)
                {//change item quantity
                    Console.Write("What would you like the new quantity to be? ");
                    string amountString = Console.ReadLine();
                    int amount = Validator.CheckNum(amountString);
                    if (amount == 0)
                    {//remove item
                        RemoveFromCart(cart, editGood);
                    }
                    else
                    {
                        editGood.Quantity = amount;
                    }
                }
                else if (inputNum == 2)
                {//remove item
                    RemoveFromCart(cart, editGood);
                }
                else if (inputNum == 3)
                {//return to cart menu
                    ViewCartOptions(cart);
                }
            }
        }

        public static void RemoveFromCart(List<Goods> cart, Goods removeGood)
        {
            bool remove = Validator.CheckYes("Are you sure you want to remove this item from your cart? ");
            if (remove)
            {
                cart.Remove(removeGood);
            }
            else
            {
                Console.WriteLine("I'll leave it in your cart, then.");
            }
        }


        public static void Buy(List<Goods> cart)
        {
            double total = PrintCart(cart);
            Console.WriteLine($"\nYour total comes to {total:C}.");
            bool buy = Validator.CheckYes("\nGot everythin' you need? ");
            if (buy)
            {
                Console.WriteLine(@"Which of the big three frontier payment methods are you using?
1 -- cash
2 -- check
3 -- credit card");
                string inputString = Console.ReadLine();
                int inputNum = Validator.CheckNum(inputString);
                if (inputNum == 1)
                {//cash
                    double change = PayCash(total);
                    PrintReceipt(cart, total + change, change);
                }
                else if (inputNum == 2)
                {//check
                    PayCheck(total, cart);
                }
                else if (inputNum == 3)
                {//credit card
                    Console.WriteLine("Credit cards won't be invented for another century, but we'll give it a shot.");
                    PayCard();
                }
                cart = new List<Goods> { };
                Console.WriteLine("\nPleasure doing business with you!");
            }
            else
            {
                Console.WriteLine("\nTake your time. No rush. It's gonna be a long time until you find another general store.\n" +
                    "And you ain't never gonna find one as well-stocked as the Independence General Store!\n");
            }
        }

        public static double PayCash(double total)
        {
            Console.Write("Enter amount tendered: ");
            string paymentString = Console.ReadLine();
            if (paymentString.StartsWith("$"))
            {
                paymentString = paymentString.Remove(0, 1);
            }
            double payment = Validator.VerifyCash(paymentString, total);
            double change = payment - total;
            return change;
        }

        public static void PayCheck(double total, List<Goods> cart)
        {
            bool confirm;
            string checkNum;
            string routingNum;
            string routingHidden;
            string accountNum;
            string accountHidden;
            double change;
            do
            {
                checkNum = Validator.CheckNumString("Enter the check number: ", 1, 5);
                routingNum = Validator.CheckNumString("Enter the routing number: ", 9);
                routingHidden = new String('*', 5) + routingNum.Substring(routingNum.Length - 4);
                accountNum = Validator.CheckNumString("Enter the checking account number: ", 8, 18);
                accountHidden = new string('*', (accountNum.Length - 4)) + accountNum.Substring(accountNum.Length - 4);
                change = PayCash(total);

                Console.WriteLine($"\nCheck Number: {checkNum} \n" +
                    $"Routing Number: {routingHidden} \n" +
                    $"Account Number: {accountHidden} \n" +
                    $"Amount tendered: {total + change:C}");
                confirm = Validator.CheckYes("\nIs this information correct? (y/n) ");
            } while (!confirm);
            Console.WriteLine("Check received!");

            PrintReceipt(cart, total + change, change);

            Console.WriteLine($"{"Check Number:",-25} {checkNum,51} \n" +
                    $"{"Routing Number:",-25} {routingHidden,51} \n" +
                    $"{"Account Number:",-25} {accountHidden,51} \n");
        }

        public static void PayCard()
        {
            string cardNum = Validator.CheckNumString("Enter the credit card number: ", 16);
            string expy = Validator.CheckExpy("Enter card expiration date in MM/YYYY format: ");
            string cvv = Validator.CheckNumString("Enter the security code on the back of the card: ", 3);

        }

        public static void PrintReceipt(List<Goods> cart, double payment, double change)
        {
            Console.WriteLine();
            Console.WriteLine($"{"RECEIPT",25}");
            Console.WriteLine(new String('_',81));
            PrintCart(cart);
            Console.WriteLine($"{"Amount tendered:",-25} {payment,51:C} \n" +
                    $"{"Change:",-25} {change,51:C}");
        }

        public static bool Quitter()
        {
            return !Validator.CheckYes("Are you sure you wanna leave? ");
        }

        
    }
}
