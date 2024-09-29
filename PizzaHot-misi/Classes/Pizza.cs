using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaHot
{
    class Pizza
    {
        int id;
        string name;
        string description;
        int price;

        public Pizza(int id, string name, string description, int price)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.price = price;
        }

        public int Id { get => id;}
        public string Name { get => name;}
        public string Description { get => description;}
        public int Price { get => price;}
    }
}
