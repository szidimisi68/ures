using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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

namespace PizzaHot.Windows
{
    /// <summary>
    /// Interaction logic for Hozzavalok.xaml
    /// </summary>
    public partial class Hozzavalok : Window
    {
        ObservableCollection<Ingredients> hozzavalok = new();
        private string connectionString = "Server=localhost;Database=pizzahot;Uid=root;Pwd=;";
        public Hozzavalok()
        {
            InitializeComponent();
            Lekerdez();
        }

        void Lekerdez()
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand mySqlCommand = new MySqlCommand("SELECT name, amount FROM ingredients;", connection);
            connection.Open();
            DataTable dt = new DataTable();
            dt.Load(mySqlCommand.ExecuteReader());
            connection.Close();
            dgHozzavalok.ItemsSource = dt.DefaultView;
        }

        private void btnVissza_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
