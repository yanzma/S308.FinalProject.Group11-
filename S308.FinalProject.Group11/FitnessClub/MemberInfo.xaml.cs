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
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            //validate users at least input one field
            if (txtLastName.Text == "" && txtEmail.Text == "" && txtPhoneNum.Text == "")
            {
                MessageBox.Show("You must fill out at least one search field.");
            }
            if (txtLastName.Text != "")
            {
                foreach (Membership item in MembershipList)
                {
                    if (item.Lastname != txtLastName.Text)
                    {
                        MembershipList.Remove(item);
                    }
                }
            }

            if (txtEmail.Text != "")
            {
                foreach (Membership item in MembershipList)
                {
                    if (item.Email != txtEmail.Text)
                    {
                        MembershipList.Remove(item);
                    }
                }
            }

            if (txtPhoneNum.Text != "")
            {
                foreach (Membership item in MembershipList)
                {
                    if (item.Phonenum != txtPhoneNum.Text)
                    {
                        MembershipList.Remove(item);
                    }
                }
            }
        }

        //read membership jason file
        private void ImportMembershipData()
        {
            string strFilePath = @"..\..\..\Membership.json";


            string jsonData = File.ReadAllText(strFilePath);
            MembershipList = JsonConvert.DeserializeObject<List<Membership>>(jsonData);

        }

    }
}
