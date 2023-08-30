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
    public partial class Bills : Window
    {
        private DataClasses1DataContext db_con = new DataClasses1DataContext(Properties.Settings.Default.RentConnectionString);
        private bool confirmation = false;
        public Bills()
        {
            InitializeComponent();
            Units.IsEnabled = false;
            Calendar.BlackoutDates.Add(new CalendarDateRange(new DateTime(1990, 1, 1), DateTime.Now.AddDays(-1)));

            var A = from s in db_con.Units select s.UnitFloor;
            string[] EF = A.ToArray();
            string[] FINAl = EF.Distinct().ToArray();
            Floor.ItemsSource = FINAl;
            int index = Floor.SelectedIndex;
            ADD.IsEnabled = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new Menu().Show();
            this.Close();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            DateTime Decider = new DateTime(2012, 1, 1);
                try
                {
                    Decider = Calendar.SelectedDate.Value;
                    if(Decider != null)
                    {
                        var Final = Decider.ToShortDateString();
                        BillingS.Content = Final.ToString();
                    }
                    else
                    {
                        MessageBox.Show("You have not selected a date");
                    }
                }
                catch(Exception ex)
                {

                }
            if (BillingE != null && BillingS != null && Payment.Text != "000.00")
            {
                ADD.IsEnabled = true;
            }
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

        private void Units_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
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

        private void Billing_Period_End_Click(object sender, RoutedEventArgs e)
        {
            DateTime Decider = new DateTime(2012, 1, 1);
            try
            {
                Decider = Calendar.SelectedDate.Value;
                if (Decider != null)
                {
                    var Final = Decider.ToShortDateString();
                    BillingE.Content = Final.ToString();
                }
                else
                {
                    MessageBox.Show("You have not selected a date");
                }
            }
            catch (Exception ex)
            {

            }
            if(BillingE != null && BillingS != null && Payment.Text != "000.00")
            {
                ADD.IsEnabled = true;
            }
        }

        private void Payment_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Payment.Text != "000.00" && Units.IsEnabled == true)
            {
                ADD.IsEnabled = true;
            }
        }

        private void ADD_Click(object sender, RoutedEventArgs e)
        {
            decimal MONEY = 0;
            if (confirmation == false)
            {
                MessageBox.Show("Make sure all of the data that have been inputted are correct");
                confirmation = true;
            }
            else
            {
                    var A = from s in db_con.Units select s.UnitFloor;
                    string[] EF = A.ToArray();
                    string[] FINAl = EF.Distinct().ToArray();
                    int index = Floor.SelectedIndex;
                    int index2 = Units.SelectedIndex;

                    var B = from s in db_con.Units where s.UnitFloor == FINAl[index] && s.UnitStatus == 0 select s.UnitNo;
                    string[] C = B.ToArray();

                    var f = from s in db_con.Units
                            join r in db_con.Tenants on s.UnitID equals r.UnitID
                            where s.UnitFloor == FINAl[index] && s.UnitStatus == 0 && C[index2] == s.UnitNo
                            select r.TenantID;
                    int[] F = f.ToArray();

                    try
                    {
                        MONEY = decimal.Parse(Payment.Text);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Invalid Input [Payment Box]");
                        confirmation = false;
                    }
                    if (confirmation == true)
                    {
                        DateTime BE = DateTime.Now;
                        DateTime BS = DateTime.Now;
                        BS = DateTime.Parse(BillingS.Content.ToString());
                        BE = DateTime.Parse(BillingE.Content.ToString());
                        db_con.Billing(F[0], BS, BE, MONEY, Desc.Text);

                        MessageBox.Show("Bill has been inserted in the Database, if you wish to view bills simply hover onto the View Bills window");
                    }
            }
        }

        private void ViewBills_Click(object sender, RoutedEventArgs e)
        {
            new View_Bills().Show();
            this.Close();
        }
    }
}
