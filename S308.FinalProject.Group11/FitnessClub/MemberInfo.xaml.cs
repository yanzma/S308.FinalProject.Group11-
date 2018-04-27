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
using Newtonsoft.Json;
using System.IO;



namespace FitnessClub
{
    /// <summary>
    /// Interaction logic for MemberInfo.xaml
    /// </summary>
    public partial class MemberInfo : Window
    {
        List<Membership> MembershipList;
        public MemberInfo()
        {
            InitializeComponent();
            ImportMembershipData();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Window winMain = new MainMenu();
            winMain.Show();
            this.Close();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            //clear all users inputs
            txtLastName.Text = "";
            txtEmail.Text = "";
            txtPhoneNum.Text = "";
            dtgSearchResult.ItemsSource = "";
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            //validate users at least input one field
            if (txtLastName.Text == "" && txtEmail.Text == "" && txtPhoneNum.Text == "")
            {
                MessageBox.Show("You must fill out at least one search field.");
            }

            List<Membership> listSearch =
                MembershipList.Where(m => (m.Lastname.ToUpper() == txtLastName.Text.ToUpper() || (txtLastName.Text == "")) &&
                (m.Phonenum == txtPhoneNum.Text || txtPhoneNum.Text == "") &&
                (m.Email.ToUpper() == txtEmail.Text.ToUpper() || txtEmail.Text == "")).ToList();

            dtgSearchResult.ItemsSource = listSearch;

        }


        //read membership jason file
        private void ImportMembershipData()
        {
            string strFilePath = @"..\..\..\Data\Membership.json";


            string jsonData = File.ReadAllText(strFilePath);
            MembershipList = JsonConvert.DeserializeObject<List<Membership>>(jsonData);

        }

        private void btnClear_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void btnSearch_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
