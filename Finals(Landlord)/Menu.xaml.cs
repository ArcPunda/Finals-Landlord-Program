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

namespace Finals_Landlord_
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void Tenants_Click(object sender, RoutedEventArgs e)
        {
            new Register().Show();
            this.Close();
        }

        private void Units_Click(object sender, RoutedEventArgs e)
        {
            new Units().Show();
            this.Close();
        }

        private void Job_Orders_Click(object sender, RoutedEventArgs e)
        {
            new JobOrders().Show();
            this.Close();
        }

        private void Bills_Click(object sender, RoutedEventArgs e)
        {
            new Bills().Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new Employees().Show();
            this.Close();
        }

        private void Exit_Btn_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
