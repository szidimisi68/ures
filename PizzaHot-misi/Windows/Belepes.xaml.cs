using MySql.Data.MySqlClient;
using PizzaHot.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PizzaHot
{
    public partial class Belepes : Window
    {
        private readonly string connectionString = "Server=localhost;Database=pizzahot;Uid=root;Pwd=;";
        public Belepes()
        {
            InitializeComponent();
            
        }

        private void btnBelepes_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string username = tbFelhasznalonev.Text; 
                string password = tbJelszo.Password; 

                string query = "SELECT `password`, `name`, `isAdmin` FROM `users` WHERE `name` = @username";

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    using (MySqlCommand selectCommand = new MySqlCommand(query, connection))
                    {
                        selectCommand.Parameters.AddWithValue("@username", username);

                        using (MySqlDataReader reader = selectCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string storedHashedPassword = reader.GetString("password");
                                bool isPasswordValid = PasswordHash.Verify(password, storedHashedPassword);

                                if (isPasswordValid)
                                {
                                    MessageBox.Show($"Login successful! Welcome, {username}.");
                                }
                                else MessageBox.Show("Invalid password. Please try again.");
                            }
                            else MessageBox.Show("No user found with the provided name.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
            Close();
        }
    }
}
