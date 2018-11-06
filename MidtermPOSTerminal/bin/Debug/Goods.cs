using System;
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
            if (this.Name == other.Name)
            {
                return this.Price.CompareTo(other.Price);
            }
            return this.Name.CompareTo(other.Name);
        }

        public void ViewItem()
        {//shows detailed description of selected item in list
            int maxLength = Name.Length;
            
            Console.WriteLine($"{"Name",-25} {"Category",-25} {"Price",-5}");
            Console.WriteLine($"{"----",-25} {"--------",-25} {"-----",-5}");
            Console.WriteLine($"{Name, -25} {Category,-25} {Price,-5:C}");
            Console.WriteLine($"\n{Description}");
        }
    }
}
