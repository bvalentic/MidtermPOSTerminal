using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidtermPOSTerminal
{
    public class Goods : IComparable<Goods>
    {
        //class of goods (kinda self-explanatory)
        //every item sold in the store is a good

        private string name;
        private double price;
        private string category;
        private string description;
        private int quantity; //used only in the cart

        public string Name { get { return name; } set { name = value; } }
        public double Price { get { return price; } set { price = value; } }
        public string Category { get { return category; } set { category = value; } }
        public string Description { get { return description; } set { description = value; } }
        public int Quantity { get { return quantity; } set { quantity = value; } }

        public Goods(string name, double price, string category, string description)
        {//does not take an actual quantity until the good in question is added to the cart
            Name = name;
            Price = price;
            Category = category;
            Description = description;
            Quantity = 0;
        }

        public int CompareTo(Goods other)
        {//will automatically sort by category, then name, then price
            if (this.Category == other.Category)
            {
                if (this.Name == other.Name)
                {
                    return this.Price.CompareTo(other.Price);
                }
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
