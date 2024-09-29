using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
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
    public partial class Rendeles : Window
    {
        private string connectionString = "Server=localhost;Database=pizzahot;Uid=root;Pwd=;";
        public Rendeles()
        {
            InitializeComponent();


            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    // Kapcsolat megnyitása
                    connection.Open();
                    //MessageBox.Show("Kapcsolat megnyitva.");

                    // SELECT utasítás
                    string query = "SELECT * FROM pizzas";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Lekérdezés végrehajtása
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            // Eredmények kiírása
                            while (reader.Read())
                            {
                                int id = reader.GetInt32("id");
                                string name = reader.GetString("name");
                                string description = reader.GetString("description");
                                int price = reader.GetInt32("price");
                                //MessageBox.Show($"ID: {id}, Név: {name}, Email: {description}, Admin: {price}");
                                CreateRow(name, description, price);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hiba: {ex.Message}");
                }
                finally
                {
                    // Kapcsolat bezárása
                    connection.Close();
                    //MessageBox.Show("Kapcsolat bezárva.");
                }

                void CreateRow(string name, string desc, int price)
                {
                    Image kep = new Image();
                    kep.Width = 100;
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri("..\\Images\\pizza.png", UriKind.Relative);
                    bitmap.DecodePixelWidth = 100;
                    bitmap.EndInit();
                    kep.Source = bitmap;
                    kep.Margin = new Thickness(0, 0, 20, 0); //bal, fent, jobb, lent

                    Label nev = new Label();
                    nev.Content = name;
                    nev.FontSize = 30;
                    nev.FontFamily = new FontFamily("Century Gothic");
                    nev.Margin = new Thickness(0, 0, 50, 0); //bal, fent, jobb, lent


                    TextBlock leiras = new TextBlock();
                    leiras.Width = 500;
                    leiras.TextWrapping = TextWrapping.Wrap;
                    leiras.VerticalAlignment = VerticalAlignment.Center;
                    leiras.Text = desc;
                    leiras.Margin = new Thickness(0, 0, 20, 0); //bal, fent, jobb, lent
                    leiras.FontSize = 18;



                    Button kosar = new Button();
                    kosar.Width = 120;
                    kosar.Height = 30;
                    kosar.Content = "Kosár!";

                    StackPanel myStackPanel = new StackPanel();
                    myStackPanel.Orientation = Orientation.Horizontal;
                    //myStackPanel.Width = 800;

                    myStackPanel.Children.Add(kep);
                    myStackPanel.Children.Add(nev);
                    myStackPanel.Children.Add(leiras);
                    myStackPanel.Children.Add(kosar);

                    lbPizzak.Items.Add(myStackPanel);
                }
            }

            RemoveIngredientsForPizzas(new List<int> { 1 });
        }

        public void RemoveIngredientsForPizzas(List<int> pizzaIds)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    foreach (var pizzaId in pizzaIds)
                    {
                        // Szükséges alapanyagok
                        string query = @"SELECT ingredientID, amount 
                                 FROM pizzaingredients 
                                 WHERE pizzaID = @pizzaID";

                        List<(int ingredientID, int requiredAmount)> ingredientsList = new List<(int, int)>();

                        using (MySqlCommand cmd = new MySqlCommand(query, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@pizzaID", pizzaId);

                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                // alapanyagok összegyűjtése
                                while (reader.Read())
                                {
                                    int ingredientID = reader.GetInt32("ingredientID");
                                    int requiredAmount = reader.GetInt32("amount");

                                    ingredientsList.Add((ingredientID, requiredAmount));
                                }
                            }
                        }

                        // alapanyagok levonása a raktárból
                        foreach (var (ingredientID, requiredAmount) in ingredientsList)
                        {
                            // tábla update
                            if (!UpdateIngredientAmount(conn, transaction, ingredientID, requiredAmount))
                            {
                                throw new Exception($"Nincs elég a következő alapanyagból: {ingredientID} ehhez a pizzához: {pizzaId}");
                            }
                        }
                    }

                    transaction.Commit(); // jóváhagyás
                    MessageBox.Show("Sikeresen kivéve a raktárból.");
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); // visszatekerés hibánál
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        private bool UpdateIngredientAmount(MySqlConnection conn, MySqlTransaction transaction, int ingredientID, int requiredAmount)
        {
            // raktár jelenlegi állása
            string checkQuery = @"SELECT amount FROM ingredients WHERE id = @ingredientID";

            using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn, transaction))
            {
                checkCmd.Parameters.AddWithValue("@ingredientID", ingredientID);

                object result = checkCmd.ExecuteScalar();

                int currentAmount = Convert.ToInt32(result);

                if (currentAmount >= requiredAmount)
                {
                    // raktár frissítése
                    string updateQuery = @"UPDATE ingredients 
                                   SET amount = amount - @requiredAmount 
                                   WHERE id = @ingredientID";

                    using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn, transaction))
                    {
                        updateCmd.Parameters.AddWithValue("@ingredientID", ingredientID);
                        updateCmd.Parameters.AddWithValue("@requiredAmount", requiredAmount);

                        updateCmd.ExecuteNonQuery();
                    }

                    return true; // sikeres
                }
                else
                {
                    return false; // sikertelen
                }
            }
        }
    }
}