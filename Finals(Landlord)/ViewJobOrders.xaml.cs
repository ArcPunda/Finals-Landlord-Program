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
    public partial class ViewJobOrders : Window
    {
        private DataClasses1DataContext db_con = new DataClasses1DataContext(Properties.Settings.Default.RentConnectionString);
        private bool CONFIRM = false;
        public ViewJobOrders()
        {
            InitializeComponent();

            var o = from s in db_con.JobOrders
                    join r in db_con.Units on s.UnitID equals r.UnitID
                    select r.UnitNo;
            string[] OA = o.ToArray();
            Unit.ItemsSource = OA;
            Desc.IsReadOnly = true;

            Update.IsEnabled = false;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            new JobOrders().Show();
            this.Close();
        }

        private void Unit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update.IsEnabled = true;
            int index = Unit.SelectedIndex;
            var o = from s in db_con.JobOrders
                    join r in db_con.Units on s.UnitID equals r.UnitID
                    select r.UnitID;
            int[] OA = o.ToArray();

            var a = from s in db_con.JobOrders
                    join r in db_con.Employees on s.EmployeeID equals r.EmployeeID
                    where OA[index] == s.UnitID
                    select r.Employee_FirstName;
            string[] A = a.ToArray();

            var b = from s in db_con.JobOrders
                    join r in db_con.Employees on s.EmployeeID equals r.EmployeeID
                    where OA[index] == s.UnitID
                    select r.Employee_LastName;
            string[] B = b.ToArray();

            var c = from s in db_con.JobOrders
                    join r in db_con.Tenants on s.TenantID equals r.TenantID
                    where OA[index] == s.UnitID
                    select r.Tenant_FirstName;
            string[] C = c.ToArray();

            var d = from s in db_con.JobOrders
                    join r in db_con.Tenants on s.TenantID equals r.TenantID
                    where OA[index] == s.UnitID
                    select r.Tenant_LastName;
            string[] D = d.ToArray();

            var z = from s in db_con.JobOrders
                    join r in db_con.Tenants on s.TenantID equals r.TenantID
                    where OA[index] == s.UnitID
                    select s.JobOrder_Desc;
            string[] E = z.ToArray();

            var p = from s in db_con.JobOrders
                    join r in db_con.Tenants on s.TenantID equals r.TenantID
                    where OA[index] == s.UnitID
                    select s.Payment;
            decimal[] P = p.ToArray();

            E_Name.Content = A[0] + " " + B[0];
            FN.Content = C[0];
            LN.Content = D[0];
            Desc.Text = E[0];
            Money.Content = P[0].ToString();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            int index = Unit.SelectedIndex;
            var o = from s in db_con.JobOrders
                    join r in db_con.Units on s.UnitID equals r.UnitID
                    select r.UnitID;
            int[] OA = o.ToArray();

            var a = from s in db_con.JobOrders
                    join r in db_con.Employees on s.EmployeeID equals r.EmployeeID
                    where OA[index] == s.UnitID
                    select r.EmployeeID;
            int[] A = a.ToArray();
            if (CONFIRM == true)
            {
                db_con.JobOrder_Update(A[0]);
                MessageBox.Show("Job Order has been completed");
                new Menu().Show();
                this.Close();   
            }
            else
            {
                CONFIRM = true;
                MessageBox.Show("Check if you have chosen the right Job Order to be completed, if so click the button again");
            }
        }
    }
}
