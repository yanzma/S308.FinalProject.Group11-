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

namespace FitnessClub
{
    /// <summary>
    /// Interaction logic for MemberInfo.xaml
    /// </summary>
    public partial class MemberInfo : Window
    {
        public MemberInfo()
        {
            InitializeComponent();
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
            
        }

    }
}
