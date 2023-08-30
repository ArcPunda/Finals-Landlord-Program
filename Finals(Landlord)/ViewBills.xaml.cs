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
    public partial class View_Bills : Window
    {
        private DataClasses1DataContext db_con = new DataClasses1DataContext(Properties.Settings.Default.RentConnectionString);
        private bool confirmation = false;
        public View_Bills()
        {
            InitializeComponent();

            var o = from s in db_con.Tenants
                    join r in db_con.Bills on s.TenantID equals r.TenantID
                    join a in db_con.Units on s.UnitID equals a.UnitID
                    where r.Bill_status == 1
                    select a.UnitNo;
            string[] OA = o.ToArray();
            string[] FINAl = OA.Distinct().ToArray();
            Unit.ItemsSource = FINAl;
            Desc.IsReadOnly = true;
            Payment.IsReadOnly = true;
        }

        private void Unit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var o = from s in db_con.Tenants
                    join r in db_con.Bills on s.TenantID equals r.TenantID
                    join a in db_con.Units on s.UnitID equals a.UnitID
                    where r.Bill_status == 1
                    select a.UnitNo;
            string[] OA = o.ToArray();
            string[] FINAl = OA.Distinct().ToArray();
            int index = Unit.SelectedIndex;

            var l = from s in db_con.Tenants
                    join r in db_con.Bills on s.TenantID equals r.TenantID
                    join a in db_con.Units on s.UnitID equals a.UnitID
                    where a.UnitNo == OA[index] && r.Bill_status == 1
                    select r.Bill_ID;
            int[] m = l.ToArray();
            Bill_ID.ItemsSource = m;
        }

        private void Payment_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            new Bills().Show();
            this.Close();
        }

        private void PAID_Click(object sender, RoutedEventArgs e)
        {
            if(confirmation == false)
            {
                MessageBox.Show("Please click the button again to finalize the Bill");
                confirmation = true;
            }
            else
            {
                var o = from s in db_con.Tenants
                        join r in db_con.Bills on s.TenantID equals r.TenantID
                        join a in db_con.Units on s.UnitID equals a.UnitID
                        where r.Bill_status == 1
                        select a.UnitNo;
                string[] OA = o.ToArray();
                string[] FINAl = OA.Distinct().ToArray();
                int index = Unit.SelectedIndex;

                var l = from s in db_con.Tenants
                        join r in db_con.Bills on s.TenantID equals r.TenantID
                        join a in db_con.Units on s.UnitID equals a.UnitID
                        where a.UnitNo == OA[index] && r.Bill_status == 1
                        select r.Bill_ID;
                int[] m = l.ToArray();
                int index2 = Bill_ID.SelectedIndex;

                var k = from s in db_con.Tenants
                        join r in db_con.Bills on s.TenantID equals r.TenantID
                        join p in db_con.Units on s.UnitID equals p.UnitID
                        where p.UnitNo == OA[index] && r.Bill_ID == m[index2]
                        select s.TenantID;
                int[] K = k.ToArray();

                var h = from s in db_con.Tenants
                        join r in db_con.Bills on s.TenantID equals r.TenantID
                        join p in db_con.Units on s.UnitID equals p.UnitID
                        where p.UnitNo == OA[index] && r.Bill_ID == m[index2]
                        select r.Bill_ID;
                int[] H = h.ToArray();

                db_con.updateBilling(K[0],H[0]);
                MessageBox.Show("Bill has been paid");
                new Menu().Show();
                this.Close();
            }
        }

        private void Bill_ID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var o = from s in db_con.Tenants
                        join r in db_con.Bills on s.TenantID equals r.TenantID
                        join a in db_con.Units on s.UnitID equals a.UnitID
                        where r.Bill_status == 1
                        select a.UnitNo;
                string[] OA = o.ToArray();
                int index = Unit.SelectedIndex;

                var l = from s in db_con.Tenants
                        join r in db_con.Bills on s.TenantID equals r.TenantID
                        join a in db_con.Units on s.UnitID equals a.UnitID
                        where a.UnitNo == OA[index] && r.Bill_status == 1
                        select r.Bill_ID;
                int[] m = l.ToArray();
                int index2 = Bill_ID.SelectedIndex;

                var z = from s in db_con.Tenants
                        join r in db_con.Bills on s.TenantID equals r.TenantID
                        join p in db_con.Units on s.UnitID equals p.UnitID
                        where p.UnitNo == OA[index] && r.Bill_ID == m[index2]
                        select s.Tenant_FirstName;
                string[] AO = z.ToArray();

                var Z = from s in db_con.Tenants
                        join r in db_con.Bills on s.TenantID equals r.TenantID
                        join p in db_con.Units on s.UnitID equals p.UnitID
                        where p.UnitNo == OA[index] && r.Bill_ID == m[index2]
                        select s.Tenant_LastName;
                string[] q = Z.ToArray();

                FirstName.Content = AO[0];
                LastName.Content = q[0];

                var Q = from s in db_con.Tenants
                        join r in db_con.Bills on s.TenantID equals r.TenantID
                        join p in db_con.Units on s.UnitID equals p.UnitID
                        where p.UnitNo == OA[index] && r.Bill_ID == m[index2]
                        select r.BillingPeriod_Beginning;
                DateTime[] R = Q.ToArray();

                BillingStart.Content = R[0];

                var i = from s in db_con.Tenants
                        join r in db_con.Bills on s.TenantID equals r.TenantID
                        join p in db_con.Units on s.UnitID equals p.UnitID
                        where p.UnitNo == OA[index] && r.Bill_ID == m[index2]
                        select r.BillingPeriod_End;
                DateTime[] I = i.ToArray();

                BillingEnd.Content = I[0];

                var k = from s in db_con.Tenants
                        join r in db_con.Bills on s.TenantID equals r.TenantID
                        join p in db_con.Units on s.UnitID equals p.UnitID
                        where p.UnitNo == OA[index] && r.Bill_ID == m[index2]
                        select r.Bill_Desc;
                string[] K = k.ToArray();
                Desc.Text = K[0];

                var h = from s in db_con.Tenants
                        join r in db_con.Bills on s.TenantID equals r.TenantID
                        join p in db_con.Units on s.UnitID equals p.UnitID
                        where p.UnitNo == OA[index] && r.Bill_ID == m[index2]
                        select r.Payment_Required;
                decimal[] H = h.ToArray();
                Payment.Text = H[0].ToString();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
