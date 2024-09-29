using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaHot
{
    class Order
    {
        int deliveryId;
        int pizzaId;
        int amount;

        public Order(int deliveryId, int pizzaId, int amount)
        {
            this.deliveryId = deliveryId;
            this.pizzaId = pizzaId;
            this.amount = amount;
        }

        public int DeliveryId { get => deliveryId;}
        public int PizzaId { get => pizzaId;}
        public int Amount { get => amount;}
    }
}
