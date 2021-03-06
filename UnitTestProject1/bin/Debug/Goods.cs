﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidtermPOSTerminal
{
    public class Goods : IComparable<Goods>
    {
        private string name;
        private double price;
        private string category;
        private string description;
        private int quantity;

        public string Name { get { return name; } set { name = value; } }
        public double Price { get { return price; } set { price = value; } }
        public string Category { get { return category; } set { category = value; } }
        public string Description { get { return description; } set { description = value; } }
        public int Quantity { get { return quantity; } set { quantity = value; } }

        public Goods(string name, double price, string category, string description)
        {
            Name = name;
            Price = price;
            Category = category;
            Description = description;
            Quantity = 0;
        }

        public int CompareTo(Goods other)
        {
            if (this.Category == other.Category)
            {
                return this.Name.CompareTo(other.Name);
            }
            return this.Category.CompareTo(other.Category);
        }

        public void ViewItem()
        {//shows detailed description of selected item in list
            Console.WriteLine($"{"Name",-30} {"Category",-15} {"Price",-5}");
            Console.WriteLine($"{"----",-30} {"--------",-15} {"-----",-5}");
            Console.WriteLine($"{Name, -30} {Category,-15} {Price,-5:C}");
            Console.WriteLine($"\n{Description}");
        }
    }
}
