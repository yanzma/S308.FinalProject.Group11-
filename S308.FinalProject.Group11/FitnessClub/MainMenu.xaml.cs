//Yanzhi Ma ; Yiwen Chen; Yijing Zhao 
//image source: https://www.equinox.com/
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
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        public MainMenu()
        {
            InitializeComponent();

            //Some changes 1
           
        }

        private void btnMembershipSales_Click(object sender, RoutedEventArgs e)
        {
            Window winMemberSales = new MembershipSales();
            winMemberSales.Show();
            this.Close();
        }

        private void btnPricingMgt_Click(object sender, RoutedEventArgs e)
        {
            Window winPriceManagement = new PriceManagement();
            winPriceManagement.Show();
            this.Close();
        }

        private void btnMembershipInfo_Click(object sender, RoutedEventArgs e)
        {
            Window winMemberInfo = new MemberInfo();
            winMemberInfo.Show();
            this.Close();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
