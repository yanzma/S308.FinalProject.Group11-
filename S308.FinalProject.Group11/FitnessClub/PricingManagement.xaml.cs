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

            //get combo box selected item index
            //String[] option = new String[6] { "Individual 1 Month", "Individual 12 Month", "Two Person 1 Month", "Two Person 12 Month", "Family 1 Month", "Family 12 Month" };
            //ComboBoxItem selectedItem = (ComboBoxItem)cboSelectType.SelectedItem;
            //string strSelectedName = selectedItem.Content.ToString();
            //int i = Array.IndexOf(option,strSelectedName);
            // Pricing item = pricingList[i];

            //get corresponded item price and avaliability
            //lblPriceResult.Content = strSelectedName;
            //lblAvailabilityResult.Content = item.Availability;



        }

        private void cboSelectType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboSelectType.SelectedIndex != -1)
            {


                //get combo box selected item index

                ComboBoxItem selectedItem = (ComboBoxItem)cboSelectType.SelectedItem;
                string strSelectedName = selectedItem.Content.ToString();
               
                //get corresponded item price and avaliability
                foreach (Pricing item in pricingList)
                {
                                          
                    if (item.Type == strSelectedName)
                    {
                        lblPriceResult.Content = item.Price;
                        lblAvailabilityResult.Content = item.Availability;
                    }
                }


            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //validate input fields 
            int intNum;
            if (!Int32.TryParse(txtPriceChange.Text, out intNum))
            { MessageBox.Show("Please enter a valid number!"); }

            if (txtAvailabilityChange.Text != "Yes" && txtAvailabilityChange.Text != "No")
            { MessageBox.Show("Please enter either Yes or No!"); }

            //update json file 
       
            string strFilePath = @"..\..\..\Data\Pricing.json";

            string jsonData = File.ReadAllText(strFilePath);
            pricingList = JsonConvert.DeserializeObject<List<Pricing>>(jsonData);

            foreach (Pricing item in pricingList)
            {
                if (txtPriceChange.Text != lblPriceResult.Content.ToString()) 
                {
                    item.Price = txtPriceChange.Text;               
                }
                if (lblAvailabilityResult.Content.ToString() != txtAvailabilityChange.Text)
                {
                    item.Availability = txtAvailabilityChange.Text;
                } 
                
            }
        }
        // clear results
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtAvailabilityChange.Text = "";
            txtPriceChange.Text = "";
            cboSelectType.SelectedIndex = -1;
            lblPriceResult.Content = "";
            lblAvailabilityResult.Content = ""; 
        }
    }
}

