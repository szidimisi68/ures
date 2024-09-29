using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaHot
{
    class User
    {
        int id;
        string name;
        string password;
        string email;
        bool isAdmin;

        public User(int id, string name, string password, string phoneNumber, string email, bool isAdmin)
        {
            this.id = id;
            this.name = name;
            this.password = password;
            this.email = email;
            this.isAdmin = isAdmin;
        }

        public int Id { get => id;}
        public string Name { get => name;}
        public string Password { get => password; }
        public string Email { get => email;}
    }
}
