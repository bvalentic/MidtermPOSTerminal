﻿using System;
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
        {//allows user to select an item from the goods list and view its description
            if (inputString.Length > 3)
            {//for user input "add [number]"
                inputString = inputString.Remove(0, 3).Trim(' ');
                if (inputString.Length > 0)
                {
                    int inputNum = Validator.CheckNum(inputString, 0, goodsList.Count);
                    AddToCart(goodsList, cart, inputNum);
                }
                else
                {
                    Program.PrintMenu(goodsList);
                    Console.WriteLine("\nWhat do you want to add to your cart?");
                    GoToCart(goodsList, cart);
                }
            }
            else
            {//for user input "add"
                Program.PrintMenu(goodsList);
                Console.WriteLine("What do you want to add to your cart?");
                GoToCart(goodsList, cart);
            }
        }

        public static void GoToCart(List<Goods> goodsList, List<Goods> cart)
        {//if user did not select an item initially, this allows them to do so now
            Console.Write("\nEnter a number to view a description of that item,\n" +
                        "or enter \"0\" to go back: ");
            string inputString = Console.ReadLine();
            int inputNum = Validator.CheckNum(inputString, 0, goodsList.Count);
            if (inputNum == 0)
            {
                Console.WriteLine("Returnin' to the main menu. . .");
                return;
            }
            else
            {
                AddToCart(goodsList, cart, inputNum);
            }
        }

        public static void AddToCart(List<Goods> goodsList, List<Goods> cart, int inputNum)
        {//displays selected item, and adds it to user's cart if so desired
            Goods inputGood = goodsList[inputNum - 1];
            Console.WriteLine($"\nYou have chosen {inputNum} -- {inputGood.Name}\n");
            inputGood.ViewItem();
            bool check = Validator.CheckYes("\nWould you like to add this to your cart? (y/n) ");
            if (check)
            {
                if (cart.Contains(inputGood))
                {//if the selected item is already in the user's cart
                    Console.WriteLine("This is already in your cart! \n" +
                        "If you want to change the amount of this in your cart, you can edit it through to cart menu.");
                    return;
                }
                Console.Write("Quantity? ");
                string inputString = Console.ReadLine();
                int amount = Validator.CheckNum(inputString);
                if (amount > 0)
                {
                    cart.Add(inputGood);
                    inputGood.Quantity += amount;
                    Console.WriteLine($"\nAdded ({amount}) {inputGood.Name} to cart! ");
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

        public static double PrintCart(List<Goods> cart)
        {
            const int nameLength = 30;
            Console.WriteLine($"\n{"#",-3} {"Name",-nameLength} {"Category",-15} {"Quantity",-10} {"Price",-8} {"Line Total",-10}");
            Console.WriteLine($"{"-",-3} {"----",-nameLength} {"--------",-15} {"--------",-10} {"-----",-8} {"----------",-10}");
            double subtotal = 0;

            for (int i = 0; i < cart.Count; i++)
            {
                Console.WriteLine($"{i + 1,-3} {cart[i].Name,-nameLength} {cart[i].Category,-15} {cart[i].Quantity,-10} " +
                    $"{cart[i].Price,-8:C} {cart[i].Quantity * cart[i].Price,10:C}");
                subtotal += cart[i].Quantity * cart[i].Price;
            }
            Console.WriteLine($"\n{"Subtotal:",-70} {subtotal,10:C}");
            double tax = subtotal * 0.06;
            double total = Math.Round(subtotal + tax, 2);
            Console.WriteLine($"{"Sales tax:",-70} {tax,10:C}");
            Console.WriteLine($"{"Total:",-70} {total,10:C}");
            return total;
        }

        public static void ViewCartOptions(List<Goods> cart)
        {//shows items in cart and gives option to edit said items
            if (cart.Count > 0)
            {
                bool edit = true;
                Console.WriteLine("\nHere's what's in your cart:\n");
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
                Console.WriteLine("\nThere ain't nothin' in yer cart!");
            }
        }

        public static void EditCart(List<Goods> cart)
        {//allows user to change the quantity of, or remove items from, their cart
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

                switch (inputNum)
                {
                    case 1:
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
                            Console.WriteLine("\nThat's the same amount in yer cart already! " +
                                "You tryin' to pull somethin' here, buddy?");
                        }
                        break;
                    }
                    case 2:
                    {
                        RemoveFromCart(cart, editGood);
                        break;
                    }
                    case 3:
                    {
                        break;
                    }
                }
            }
        }

        public static void RemoveFromCart(List<Goods> cart, Goods removeGood)
        {//allows user to remove an item from their cart (after confirming they wish to do so)
            bool remove = Validator.CheckYes("Are you sure you want to remove this item from your cart? ");
            if (remove)
            {
                cart.Remove(removeGood);
            }
            else
            {
                Console.WriteLine("I'll leave it in yer cart, then.");
            }
        }
    }
}
