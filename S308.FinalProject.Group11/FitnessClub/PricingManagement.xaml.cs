using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
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
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class PriceManagement : Window
    {
        List<Pricing> pricingList;
        public PriceManagement()
        {
            InitializeComponent();

            pricingList = new List<Pricing>();

            ImportPricingData();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Window winMain = new MainMenu();
            winMain.Show();
            this.Close();
        }
        
        private void ImportPricingData()
        {
            string strFilePath = @"..\..\..\Data\Pricing.json";

         
            string jsonData = File.ReadAllText(strFilePath);
            pricingList = JsonConvert.DeserializeObject<List<Pricing>>(jsonData);
            
            


            lblPriceResult.Content = pricingList["Type"];



        }

    }
}
