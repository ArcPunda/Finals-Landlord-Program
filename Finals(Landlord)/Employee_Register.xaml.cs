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
    public partial class Employee_Register : Window
    {
        private DataClasses1DataContext db_con = new DataClasses1DataContext(Properties.Settings.Default.RentConnectionString);
        private int counter = 0;
        public Employee_Register()
        {
            InitializeComponent();
            var C = from s in db_con.Employees select s.Job;
            string[] EF = C.ToArray();
            string[] FINAl = EF.Distinct().ToArray();
            Jobs.ItemsSource = FINAl;

            employee.IsReadOnly = true;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
            Register.IsEnabled = false;
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
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new Register().Show();
            this.Close();
        }

        private void Jobs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Register.IsEnabled = true;
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            var C = from s in db_con.Employees select s.Job;
            string[] EF = C.ToArray();
            string[] FINAl = EF.Distinct().ToArray();
            int index = Jobs.SelectedIndex;
            db_con.Employee_Register(FirstName.Text,LastName.Text,FINAl[index]);
            MessageBox.Show("Employee has been inserted onto the Database");
            new Menu().Show();
            this.Close();
        }
    }
}
