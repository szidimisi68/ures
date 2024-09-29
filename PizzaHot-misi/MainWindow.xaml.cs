using PizzaHot.Classes;
using PizzaHot.Windows;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PizzaHot
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnBelepes_Click(object sender, RoutedEventArgs e)
        {
            Belepes subWindow = new Belepes();
            if (subWindow.ShowDialog()==false)
            {
            }
            //subWindow.Show();

        }

        private void btnRegisztracio_Click(object sender, RoutedEventArgs e)
        {
            Regisztracio regisztracio = new Regisztracio();
            if (regisztracio.ShowDialog()==false)
            {
            }
        }



        private void btnRendeles_Click(object sender, RoutedEventArgs e)
        {
            Rendeles rendeles = new Rendeles();
            if (rendeles.ShowDialog()==false)
            {
            }
        }
    }
}