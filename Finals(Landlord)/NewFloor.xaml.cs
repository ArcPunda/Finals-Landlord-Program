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
using System.Collections;
namespace Finals_Landlord_
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class NewFloor : Window
    {
        private DataClasses1DataContext db_con = new DataClasses1DataContext(Properties.Settings.Default.RentConnectionString);
        private int count = 1;
        private int studio = 0;
        private int TwoBedroom = 0;
        private int PH = 0;
        private bool Confirmation = false;
        public NewFloor()
        {
            InitializeComponent();
            var A = from s in db_con.Units select s.UnitFloor;
            string[] EF = A.ToArray();
            string[] FINAl = EF.Distinct().ToArray();

            string lastElement = FINAl.Last();

            string[] subs = lastElement.Split(' ');

            int convert = 0; //Add 1
            convert = Int32.Parse(subs[1]);

            int FLOORNUMBER = convert + 1;
            string FloorName = "Floor " + FLOORNUMBER;
            Floor.Content = FloorName;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            int identityLength = Identification.Text.Length;
            try
            {
                studio = Int32.Parse(Studio.Text);
                TwoBedroom = Int32.Parse(_2BedroomSize.Text);
                PH = Int32.Parse(Penthouse.Text);
            }
            catch (Exception ex)
            {

            }
            if (Confirmation == false)
            {
                MessageBox.Show("This action is irreversible once done (Contact the Database manager in order to remove the floor), please check if all information given is correct, if so click the button again.");
                MessageBox.Show("If ever you have placed anything that isn't a number in the the textbox it will be considered as 0" + "\n" +
                    "Studio: " + studio + "\n" +
                    "2 Bedroom: " + TwoBedroom + "\n" +
                    "Penthouse: " + PH);

                Confirmation = true;
            }
            else if (Confirmation == true && identityLength > 0)
            {
                if(studio > 0)
                {
                    for(int a = 0; a < studio; a++)
                    {
                        string NameOfTheUnit = count + "-" + Identification.Text;
                        db_con.NewFloor_Studio(NameOfTheUnit, Floor.Content.ToString());
                        count++;
                    }
                }
                if (TwoBedroom > 0)
                {
                    for (int a = 0; a < studio; a++)
                    {
                        string NameOfTheUnit = count + "-" + Identification.Text;
                        db_con.NewFloor_2Bedroom(NameOfTheUnit, Floor.Content.ToString());
                        count++;
                    }
                }
                if (PH > 0)
                {
                    for (int a = 0; a < studio; a++)
                    {
                        string NameOfTheUnit = count + "-" + Identification.Text;
                        db_con.NewFloor_Penthouse(NameOfTheUnit, Floor.Content.ToString());
                        count++;
                    }
                }
                MessageBox.Show("Information has been inserted" +"\n" + "Please close the Unit Window in order to refresh the floor");
                this.Close();
                new Menu().Show();
            }
            if(identityLength == 0)
            {
                MessageBox.Show("Identity is empty, please input a character");
            }
        }

        private void Identification_TextChanged(object sender, TextChangedEventArgs e)
        {
            string Length = "";
            string LengthF = "";
            Length = Identification.Text;
            LengthF = Length.ToUpper();
            Identification.Text = LengthF;
            if(LengthF.Length > 1)
            {
                Identification.Text = "";
            }
            else
            {
                var A = from s in db_con.Units select s.UnitNo;
                string[] EF = A.ToArray();
                var UNITNO = new ArrayList();
                for (int a = 0; a < EF.Length; a++)
                {
                    string B = EF[a].Substring(EF[a].Length - 1);
                    UNITNO.Add(B);
                }
                if (UNITNO.Contains(Identification.Text))
                {
                    MessageBox.Show("Identification already exists in a floor, please use a different one");
                    Identification.Text = "";
                }
            }
        }
    }
}
