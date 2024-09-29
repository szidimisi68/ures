using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaHot
{
    class PizzaIngredients
    {
        int pizzaId;
        int ingredientId;
        int amount;

        public PizzaIngredients(int pizzaId, int ingredientId, int amount)
        {
            this.pizzaId = pizzaId;
            this.ingredientId = ingredientId;
            this.amount = amount;
        }

        public int PizzaId { get => pizzaId;}
        public int IngredientId { get => ingredientId;}
        public int Amount { get => amount;}
    }
}
