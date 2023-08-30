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
    public partial class Employees : Window
    {
        private DataClasses1DataContext db_con = new DataClasses1DataContext(Properties.Settings.Default.RentConnectionString);
        private int df = 0;
        private string FirstName = "";
        private string LastName = "";
        private int availibility = 0;
        public Employees()
        {
            InitializeComponent();

            var C = from s in db_con.Employees select s.Job;
            string[] EF = C.ToArray();
            string[] FINAl = EF.Distinct().ToArray();
            Jobs_CB.ItemsSource = FINAl;
            Status.IsEnabled = false;
            Next.IsEnabled = false;
            Previous.IsEnabled = false;
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            df++;
            var C = from s in db_con.Employees select s.Job;
            string[] EF = C.ToArray();
            string[] FINAl = EF.Distinct().ToArray();
            int index = Jobs_CB.SelectedIndex;

            var A = from s in db_con.Employees where s.Job == EF[index] select s.Employee_FirstName;
            string[] AB = A.ToArray();
            var B = from s in db_con.Employees where s.Job == EF[index] select s.Employee_LastName;
            string[] CD = B.ToArray();
            var D = from s in db_con.Employees where s.Job == EF[index] select s.Status;
            string[] GH = D.ToArray();
            if (df > AB.Length)
            {
                df = 0;
                string name = CD[df] + ", " + AB[df];
                Name.Content = name;
                int a = Int32.Parse(GH[df]);
                if (a == 0)
                {
                    Avail.Content = "Not Available at the moment";
                }
                else
                {
                    Avail.Content = "Available for more work";
                }
            }
            else if(df < AB.Length)
            {
                string name = CD[df] + ", " + AB[df];
                Name.Content = name;
                int a = Int32.Parse(GH[df]);
                if (a == 0)
                {
                    Avail.Content = "Not Available at the moment";
                }
                else
                {
                    Avail.Content = "Available for more work";
                }
                FirstName = AB[df];
                LastName = CD[df];
                availibility = Int32.Parse(GH[df]);

            }
        }

        private void Previous_Click(object sender, RoutedEventArgs e)
        {
            df--;
            var C = from s in db_con.Employees select s.Job;
            string[] EF = C.ToArray();
            string[] FINAl = EF.Distinct().ToArray();
            int index = Jobs_CB.SelectedIndex;

            var A = from s in db_con.Employees where s.Job == EF[index] select s.Employee_FirstName;
            string[] AB = A.ToArray();
            var B = from s in db_con.Employees where s.Job == EF[index] select s.Employee_LastName;
            string[] CD = B.ToArray();
            var D = from s in db_con.Employees where s.Job == EF[index] select s.Status;
            string[] GH = D.ToArray();
            if (df < 0)
            {
                df = AB.Length;
            }
            else if (df < AB.Length)
            {
                string name = CD[df] + ", " + AB[df];
                Name.Content = name;
                int a = Int32.Parse(GH[df]);
                if (a == 0)
                {
                    Avail.Content = "Not Available at the moment";
                }
                else
                {
                    Avail.Content = "Available for more work";
                }
                FirstName = AB[df];
                LastName = CD[df];
                availibility = Int32.Parse(GH[df]);

            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            new Menu().Show();
            this.Close();
        }

        private void Jobs_CB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Next.IsEnabled = true;
            Previous.IsEnabled = true;
            Status.IsEnabled = true;
        }

        private void Status_Click(object sender, RoutedEventArgs e)
        {
                if (availibility == 0)
                {
                availibility = 1;
                }
                else if (availibility == 1)
                {
                availibility = 0;
                }
                db_con.EmployeeStatus(availibility, FirstName, LastName);
                MessageBox.Show("Status has been changed for " + FirstName + " " + LastName);
        }
    }
}
