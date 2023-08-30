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
    public partial class Units : Window
    {
        private DataClasses1DataContext db_con = new DataClasses1DataContext(Properties.Settings.Default.RentConnectionString);
        public Units()
        {
            InitializeComponent();
            var A = from s in db_con.Units select s.UnitFloor;
            string[] EF = A.ToArray();
            string[] FINAl = EF.Distinct().ToArray();
            Floor.ItemsSource = FINAl;
            Units_Display.IsEnabled = false;
            Reset.IsEnabled = false;

            Occupied.IsEnabled = false;
            Vacancy.IsEnabled = false;
        }

        private void MainMenu_Click(object sender, RoutedEventArgs e)
        {
            new Menu().Show();
            this.Close();
        }
        
        private void Vacancy_Checked(object sender, RoutedEventArgs e)
        {
            var A = from s in db_con.Units select s.UnitFloor;
            string[] EF = A.ToArray();
            string[] FINAl = EF.Distinct().ToArray();
            int index = Floor.SelectedIndex;

            var B = from s in db_con.Units where s.UnitFloor == FINAl[index] && s.UnitStatus == 1 select s.UnitNo;
            string[] C = B.ToArray();
            Units_Display.IsEnabled = true;
            Units_Display.ItemsSource = C;

            Occupied.IsEnabled = false;
            Reset.IsEnabled = true;
        }

        private void Occupied_Checked(object sender, RoutedEventArgs e)
        {
            var A = from s in db_con.Units select s.UnitFloor;
            string[] EF = A.ToArray();
            string[] FINAl = EF.Distinct().ToArray();
            int index = Floor.SelectedIndex;

            var B = from s in db_con.Units where s.UnitFloor == FINAl[index] && s.UnitStatus == 0 select s.UnitNo;
            string[] C = B.ToArray();
            Units_Display.IsEnabled = true;
            Units_Display.ItemsSource = C;

            Vacancy.IsEnabled = false;
            Reset.IsEnabled = true;
        }

        private void Reset_Checked(object sender, RoutedEventArgs e)
        {
            if(Occupied.IsEnabled == true && Vacancy.IsEnabled == true)
            {
                Reset.IsChecked = false;
            }
            else
            {
                Occupied.IsChecked = false;
                Vacancy.IsChecked = false;
                Reset.IsChecked = false;
                Occupied.IsEnabled = true;
                Vacancy.IsEnabled = true;
                Units_Display.SelectedValue = 0;
            }
        }

        private void Units_Display_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            View.Opacity = 100;
            OccupiedBy.Content = "";
            Unit_Housing.Content = "";
            Name.Content = "";
            Nationality.Content = "";
            ContactNo.Content = "";
        }

        private void View_Click(object sender, RoutedEventArgs e)
        {
            var A = from s in db_con.Units select s.UnitFloor;
            string[] EF = A.ToArray();
            string[] FINAl = EF.Distinct().ToArray();
            int index = Floor.SelectedIndex;
            if (Occupied.IsChecked == true)
            {
                var B = from s in db_con.Units
                        where s.UnitFloor == FINAl[index]
                        && s.UnitStatus == 0
                        select s.UnitNo;
                string[] C = B.ToArray();
                int index2 = Units_Display.SelectedIndex;

                var c = from s in db_con.Units
                        where s.UnitFloor == FINAl[index]
                        && s.UnitStatus == 0
                        && C[index2] == s.UnitNo
                        select s.UnitSize;
                string[] Z = c.ToArray();

                var f = from s in db_con.Units
                        join r in db_con.Tenants on s.UnitID equals r.UnitID
                        where s.UnitFloor == FINAl[index] && s.UnitStatus == 0 && C[index2] == s.UnitNo
                        select r.Tenant_FirstName;
                string[] F = f.ToArray();

                var o = from s in db_con.Units
                        join r in db_con.Tenants on s.UnitID equals r.UnitID
                        where s.UnitFloor == FINAl[index] && s.UnitStatus == 0 && C[index2] == s.UnitNo
                        select r.Tenant_LastName;
                string[] O = o.ToArray();

                var k = from s in db_con.Units
                        join r in db_con.Tenants on s.UnitID equals r.UnitID
                        where s.UnitFloor == FINAl[index] && s.UnitStatus == 0 && C[index2] == s.UnitNo
                        select r.Nationality;
                string[] K = k.ToArray();

                var q = from s in db_con.Units
                        join r in db_con.Tenants on s.UnitID equals r.UnitID
                        where s.UnitFloor == FINAl[index] && s.UnitStatus == 0 && C[index2] == s.UnitNo
                        select r.Contact_Number;
                string[] Q = q.ToArray();

                OccupiedBy.Content = "Occupied By:";
                Unit_Housing.Content = Z[0];
                Name.Content = F[0] + " " + O[0];
                Nationality.Content = K[0];
                ContactNo.Content = Q[0];
            }
            else if (Vacancy.IsChecked == true)
            {
                var B = from s in db_con.Units where s.UnitFloor == FINAl[index] && s.UnitStatus == 1 select s.UnitNo;
                string[] D = B.ToArray();
                int index2 = Units_Display.SelectedIndex;
                var p = from s in db_con.Units where s.UnitFloor == FINAl[index] && s.UnitStatus == 1 && D[index2] == s.UnitNo select s.UnitSize;
                string[] Y = p.ToArray();

                OccupiedBy.Content = "VACANT UNIT";
                Unit_Housing.Content = Y[0];
            }
            View.Opacity = 0;
        }

        private void NewFloor_Click(object sender, RoutedEventArgs e)
        {
            new NewFloor().Show();
        }

        private void Floor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Occupied.IsEnabled = true;
            Vacancy.IsEnabled = true;
        }
    }
}
