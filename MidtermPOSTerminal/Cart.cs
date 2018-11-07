using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidtermPOSTerminal
{
    class Cart
    {

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
                    Program.PrintMenu(goodsList);
                    Console.WriteLine("What do you want to add to your cart?");
                    GoToCart(goodsList, cart);
                }
            }
            else
            {//user input "add"
                Program.PrintMenu(goodsList);
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
            double total = Math.Round(subtotal + tax, 2);
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
                if (cart.Contains(inputGood))
                {
                    Console.WriteLine("This is already in your cart!");
                    return;
                }
                Console.Write("Quantity? ");
                string inputString = Console.ReadLine();
                int amount = Validator.CheckNum(inputString);
                if (amount > 0)
                {
                    cart.Add(inputGood);
                    inputGood.Quantity += amount;
                    Console.WriteLine($"\nAdded {amount} {inputGood.Name} to cart! ");
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
                    else if (amount != editGood.Quantity)
                    {
                        Console.WriteLine($"Changed amount in cart from {editGood.Quantity} to {amount}");
                        editGood.Quantity = amount;
                    }
                    else
                    {
                        Console.WriteLine("That's the same amount in your cart already! " +
                            "You tryin' to pull somethin' here?");
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

    }
}
