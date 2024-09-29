using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using PizzaHot.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace PizzaHot
{
    public partial class Regisztracio : Window
    {
        private readonly string connectionString = "Server=localhost;Database=pizzahot;Uid=root;Pwd=;";
        public Regisztracio()
        {
            InitializeComponent();
            tbFelhasznalonev.Focus();
        }

        public void Registration()
        {
            try
            {
                string query = "INSERT INTO `users` (`password`, `name`, `email`, `isAdmin`) VALUES (@password, @name, @email, @isAdmin)";

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    using (MySqlCommand insertCommand = new MySqlCommand(query, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@password", PasswordHash.Hash(tbJelszo.Password));
                        insertCommand.Parameters.AddWithValue("@name", tbFelhasznalonev.Text);
                        insertCommand.Parameters.AddWithValue("@email", tbEmail.Text);
                        insertCommand.Parameters.AddWithValue("@isAdmin", 0);

                        insertCommand.ExecuteNonQuery();

                        MessageBox.Show("User successfully inserted into the database.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        public bool Validation()
        {
            if (IsValidEmail() && IsValidPassword() && IsValidUsername() && IsValidPhoneNumber())
            {
                return true;
            }
            return false;
        }

        private bool IsValidUsername()
        {
            if (tbFelhasznalonev.Text.Length < 3 || tbFelhasznalonev.Text.Length > 32 || !tbFelhasznalonev.Text.All(char.IsLetter))
            {
                tbFelhasznalonev.Text = string.Empty;
                tbFelhasznalonev.Focus();
                MessageBox.Show("Helytelenül kitöltött felhasználónév! A felhasználónévnek betűkből kell állnia és legalább 3 karakter hosszúnak kell lennie", "PizzaHot system", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        private bool IsValidEmail()
        {
            if (!Regex.IsMatch(tbEmail.Text, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
            {
                tbEmail.Text = string.Empty;
                tbEmail.Focus();
                MessageBox.Show("Helytelenül kitöltött email! Adjon meg egy létező emailt!", "PizzaHot system", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private bool IsValidPassword()
        {
            Regex hasNumber = new(@"[0-9]+");
            Regex hasUpperChar = new(@"[A-Z]+");

            if (!hasNumber.IsMatch(tbJelszo.Password) || !hasUpperChar.IsMatch(tbJelszo.Password) ||
                tbJelszo.Password.Length < 6)
            {
                if (tbJelszo.Password != tbJelszoUjra.Password)
                {
                    tbJelszo.Password = string.Empty;
                    tbJelszoUjra.Password = string.Empty;
                    tbJelszo.Focus();
                    MessageBox.Show("Helytelenül kitöltött jelszó! Nem egyezik a két mező tartalma", "PizzaHot system", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                tbJelszo.Password = string.Empty;
                tbJelszoUjra.Password = string.Empty;
                tbJelszo.Focus();
                MessageBox.Show("Helytelenül kitöltött jelszó! A jelszónak tartalmaznia kell legalább 1 nagy betűt, 1 számot és legalább 6 karakter hosszunak kell lennie", "PizzaHot system", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private bool IsValidPhoneNumber()
        {
            Regex regex = new(@"^(\+?\d{1,3}?[-.\s]?(\(?\d{1,4}\)?)[-.\s]?)*\d{1,4}[-.\s]?\d{1,4}([-.\s]?\d{1,9})?$");

            if (!regex.IsMatch(tbTel.Text))
            {
                tbTel.Text = string.Empty;
                tbTel.Focus();
                MessageBox.Show("Helytelenül kitöltött telefonszám! A telefonszámnak meg kell felelnie valamelyik formátumnak:\n" +
                           "- (123) 456-7890\n" +
                           "- +1 123-456-7890\n" +
                           "- 123-456-7890\n" +
                           "- 1234567890", "PizzaHot system", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return true;
        }

        private void btnRegisztracio_Click(object sender, RoutedEventArgs e)
        {
            if (true)
            {
                Registration();
                Close();
            }
        }
    }
}
