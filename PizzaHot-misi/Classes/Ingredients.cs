using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaHot
{
    class Ingredients
    {
        int id;
        string name;
        int amount;

        public Ingredients(int id, string name, int amount)
        {
            this.id = id;
            this.name = name;
            this.amount = amount;
        }

        public int Id { get => id;}
        public string Name { get => name;}
        public int Amount { get => amount;}
    }
}
