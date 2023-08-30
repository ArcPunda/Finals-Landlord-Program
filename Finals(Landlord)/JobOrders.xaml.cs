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
    public partial class JobOrders : Window
    {
        private DataClasses1DataContext db_con = new DataClasses1DataContext(Properties.Settings.Default.RentConnectionString);
        private bool CONFRIM = false;
        public JobOrders()
        {
            InitializeComponent();
            var A = from s in db_con.Units select s.UnitFloor;
            string[] EF = A.ToArray();
            string[] FINAl = EF.Distinct().ToArray();
            Floor.ItemsSource = FINAl;
            int index = Floor.SelectedIndex;

            var B = from s in db_con.Employees select s.Job;
            string[] DF = B.ToArray();
            string[] FINAl2 = DF.Distinct().ToArray();
            Jobs.ItemsSource = FINAl2;

            Units.IsEnabled = false;
            Employee.IsEnabled = false;
            Add.IsEnabled = false;
        }

        private void Exit__Click(object sender, RoutedEventArgs e)
        {
            new Menu().Show();
            this.Close();
        }

        private void Units_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Add.IsEnabled = true;
            var A = from s in db_con.Units select s.UnitFloor;
            string[] EF = A.ToArray();
            string[] FINAl = EF.Distinct().ToArray();
            int index = Floor.SelectedIndex;
            int index2 = Units.SelectedIndex;

            string Name = "";
            string Name2 = "";

            var B = from s in db_con.Units where s.UnitFloor == FINAl[index] && s.UnitStatus == 0 select s.UnitNo;
            string[] C = B.ToArray();
            try
            {
                var f = from s in db_con.Units
                        join r in db_con.Tenants on s.UnitID equals r.UnitID
                        where s.UnitFloor == FINAl[index] && s.UnitStatus == 0 && C[index2] == s.UnitNo
                        select r.Tenant_FirstName;
                string[] F = f.ToArray();

                Name = F[0];
            }
            catch (Exception ex)
            {

            }
            try
            {
                var o = from s in db_con.Units
                        join r in db_con.Tenants on s.UnitID equals r.UnitID
                        where s.UnitFloor == FINAl[index] && s.UnitStatus == 0 && C[index2] == s.UnitNo
                        select r.Tenant_LastName;
                string[] O = o.ToArray();
                Name2 = O[0];
            }
            catch (Exception ex)
            {

            }
            Tenant_Name.Content = Name;
            Tenant_LN.Content = Name2;
        }

        private void Floor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var A = from s in db_con.Units select s.UnitFloor;
            string[] EF = A.ToArray();
            string[] FINAl = EF.Distinct().ToArray();
            int index = Floor.SelectedIndex;

            var B = from s in db_con.Units where s.UnitFloor == FINAl[index] && s.UnitStatus == 0 select s.UnitNo;
            string[] C = B.ToArray();
            Units.ItemsSource = C;
            Units.IsEnabled = true;
        }

        private void Jobs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var B = from s in db_con.Employees select s.Job;
            string[] DF = B.ToArray();
            string[] FINAl2 = DF.Distinct().ToArray();
            int index = Jobs.SelectedIndex;

            var C = from s in db_con.Employees where s.Job == FINAl2[index] && s.Status == "1" select s.Employee_FirstName;
            string[] CA = C.ToArray();
            Employee.ItemsSource = CA;
            Employee.IsEnabled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            decimal MONEY = 0;
            try
            {
                MONEY = decimal.Parse(Payment.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid Input [Payment Box]");
            }
            if (MONEY > 0 && CONFRIM == true)
            {
                var B = from s in db_con.Employees select s.Job;
                string[] DF = B.ToArray();
                string[] FINAl2 = DF.Distinct().ToArray();

                int index = Jobs.SelectedIndex;
                int index2 = Employee.SelectedIndex
                    ;
                var C = from s in db_con.Employees where s.Job == FINAl2[index] && s.Status == "1" select s.Employee_FirstName;
                string[] CA = C.ToArray();

                var D = from s in db_con.Employees
                        where s.Job == FINAl2[index] && s.Status == "1" &&
                        CA[index2] == s.Employee_FirstName
                        select s.EmployeeID;
                int[] DA = D.ToArray();

                var A = from s in db_con.Units select s.UnitFloor;
                string[] EF = A.ToArray();
                string[] FINAl = EF.Distinct().ToArray();
                int index3 = Floor.SelectedIndex;
                int index4 = Units.SelectedIndex;

                var a = from s in db_con.Units where s.UnitFloor == FINAl[index3] && s.UnitStatus == 0 select s.UnitNo;
                string[] c = a.ToArray();

                var f = from s in db_con.Units
                        join r in db_con.Tenants on s.UnitID equals r.UnitID
                        where s.UnitFloor == FINAl[index3] && s.UnitStatus == 0 && c[index4] == s.UnitNo
                        select r.UnitID;
                int[] F = f.ToArray();

                var z = from s in db_con.Units
                        join r in db_con.Tenants on s.UnitID equals r.UnitID
                        where s.UnitFloor == FINAl[index3] && s.UnitStatus == 0 && c[index4] == s.UnitNo
                        select r.TenantID;
                int[] Z = z.ToArray();

                db_con.JobOrders_ADD(Description.Text, DA[0], F[0], Z[0], MONEY);
                MessageBox.Show("Job Order has been added, to view Job Order proceed to the View Button");
                MessageBox.Show("Please remember to bill tenant if ever tenant decides to pay at a later date");
            }
            else
            {
                MessageBox.Show("Please check if all information that has been decided is correct, if so click the button again");
                CONFRIM = true;
            }
        }

        private void View_Click(object sender, RoutedEventArgs e)
        {
            new ViewJobOrders().Show();
            this.Close();
        }
    }
}
