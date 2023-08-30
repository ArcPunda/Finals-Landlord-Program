using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Finals_Landlord_
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string bypass = "";
        public MainWindow()
        {
            InitializeComponent();
            foreach (string passcode in System.IO.File.ReadLines(@"C:\Users\User's\source\repos\Finals(Landlord)\Finals(Landlord)\Security.txt"))
            {
                bypass = passcode;
            }
            foreach (string passcode in System.IO.File.ReadLines(@"C:\Users\User's\source\repos\Finals(Landlord)\Finals(Landlord)\Bypass.txt"))
            {
                if (passcode == "True")
                {
                    new Menu().Show();
                    this.Close();
                }
            }
        }

        private void Passcode_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (bypass == Passcode.Text)
            {
                using (StreamWriter writer = new StreamWriter(@"C:\Users\User's\source\repos\Finals(Landlord)\Finals(Landlord)\Bypass.txt"))
                {
                    writer.WriteLine("True");
                }
                MessageBox.Show("Thank you for purchasing this software");
                new Menu().Show();
                this.Close();

            }
            else
            {
                MessageBox.Show("Product code invalid");
            }
        }
    }
}
