using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaHot
{
    class Delivery
    {
        int id;
        int userId;
        DateTime date;
        string location;
        string telephoneNumber;

        public Delivery(int id, int userId, DateTime date, string location, string telephoneNumber)
        {
            this.id = id;
            this.userId = userId;
            this.date = date;
            this.location = location;
            this.telephoneNumber = telephoneNumber;
        }

        public int Id { get => id;}
        public int UserId { get => userId;}
        public DateTime Date { get => date;}
        public string Location { get => location;}
        public string TelephoneNumber { get => telephoneNumber;}
    }
}
