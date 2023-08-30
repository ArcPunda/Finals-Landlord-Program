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
using System.Windows.Threading;

namespace Finals_Landlord_
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Register : Window
    {
        private DataClasses1DataContext db_con = new DataClasses1DataContext(Properties.Settings.Default.RentConnectionString);
        private bool Confirmation = false;
        private DispatcherTimer dispatcherTimer;
        private int counter = 0;
        public Register()
        {
            InitializeComponent();
            var A = from s in db_con.Units select s.UnitFloor;
            string[] EF = A.ToArray();
            string[] FINAl = EF.Distinct().ToArray();
            Floor.ItemsSource = FINAl;
            int index = Floor.SelectedIndex;

            Register1.IsEnabled = false;
            FirstName.Text = "First Name";
            LastName.Text = "Last Name";
            Nationality.Text = "Nationality";
            ContactNo.Text = "Contact No.";
            Identification.Text = "Identification";

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            counter++;
            if (FirstName.Text == "" && counter >= 2)
            {
                FirstName.Text = "First Name";
                counter = 0;
            }
            if (LastName.Text == "" && counter >= 2)
            {
                LastName.Text = "Last Name";
                counter = 0;
            }
            if (ContactNo.Text == "" && counter >= 2)
            {
                ContactNo.Text = "Contact No.";
                counter = 0;
            }
            if (Nationality.Text == "" && counter >= 2)
            {
                Nationality.Text = "Nationality";
                counter = 0;
            }
            if (Identification.Text == "" && counter >= 2)
            {
                Identification.Text = "Identification";
                counter = 0;
            }
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            new Menu().Show();
            this.Close();
        }

        private void Floor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var A = from s in db_con.Units select s.UnitFloor;
            string[] EF = A.ToArray();
            string[] FINAl = EF.Distinct().ToArray();
            int index = Floor.SelectedIndex;

            var B = from s in db_con.Units where s.UnitFloor == FINAl[index] && s.UnitStatus == 1 select s.UnitNo;
            string[] C = B.ToArray();
            Units.ItemsSource = C;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool contact = false;
            string cn = "";
            int index = Floor.SelectedIndex;
            int index2 = Units.SelectedIndex;

            var A = from s in db_con.Units select s.UnitFloor;
            string[] EF = A.ToArray();
            string[] FINAl = EF.Distinct().ToArray();

            var B = from s in db_con.Units where s.UnitFloor == FINAl[index] && s.UnitStatus == 1 select s.UnitNo;
            string[] C = B.ToArray();

            var F = from s in db_con.Units where s.UnitFloor == FINAl[index] && s.UnitNo == C[index2] select s.UnitID;
            int[] G = F.ToArray();
            try
            {
                double result = double.Parse(ContactNo.Text);
                cn = ContactNo.Text;
                contact = true;
            }
            catch (FormatException)
            {
                MessageBox.Show("Please Input a number in the contact box");
            }
            if(Confirmation == true && contact == true)
            {
                db_con.TenantRegister(FirstName.Text, LastName.Text, Nationality.Text, ContactNo.Text, Identification.Text, G[0]);
                MessageBox.Show("Tenant has been added");
                new Menu().Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Please confirm the data you have inputted" + "\n" + " First Name: " + FirstName.Text + "\n" + "Last Name: " + LastName.Text + "\n" +
                    "Nationality: " + Nationality.Text +"\n" + "Contact No: " + ContactNo.Text + "\n" + "Identification: " + Identification.Text + "\n" +
                    "Unit Floor: " + FINAl[index] + "\n" + "Unit No: " + C[index2]);
                Confirmation = true;
            }
        }

        private void Units_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Register1.IsEnabled = true;
        }

        private void FirstName_TextChanged_1(object sender, TextChangedEventArgs e)
        {
        }

        private void LastName_TextChanged_1(object sender, TextChangedEventArgs e)
        {
        }

        private void ContactNo_TextChanged_1(object sender, TextChangedEventArgs e)
        {
        }

        private void Nationality_TextChanged_1(object sender, TextChangedEventArgs e)
        {
        }

        private void Identification_TextChanged_1(object sender, TextChangedEventArgs e)
        {
        }
        private void Register_Employee_Click(object sender, RoutedEventArgs e)
        {
            new Employee_Register().Show();
            this.Close();
        }
    }
}
