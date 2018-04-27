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
        //creating a list of pricing 
        List<Pricing> pricingList;
        public PriceManagement()
        {
       
            InitializeComponent();
            //Initialize the list of customers
            pricingList = new List<Pricing>();
            //call method for importing pricing data
            ImportPricingData();

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            //go back to main window 
            Window winMain = new MainMenu();
            winMain.Show();
            this.Close();
        }

        private void ImportPricingData()
        {
            string strFilePath = @"..\..\..\Data\Pricing.json";

            try { 
                //use System.IO.File to read the entire data file
                string jsonData = File.ReadAllText(strFilePath);
                
                //serialize the json data to a list of pricing 
                pricingList = JsonConvert.DeserializeObject<List<Pricing>>(jsonData);
            }

            catch(Exception ex)
            {
                MessageBox.Show("Error in import process: " + ex.Message);
            }



        }

        private void cboSelectType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Extract desired json file data 

            if (cboSelectType.SelectedIndex != -1)
            {

                //get combo box selected item content

                ComboBoxItem selectedItem = (ComboBoxItem)cboSelectType.SelectedItem;
                string strSelectedName = selectedItem.Content.ToString().Trim();
               
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
            //declare variables

            double dblNum;

            //validate input fields 

            if (txtPriceChange.Text != "")
            {
                if (!Double.TryParse(txtPriceChange.Text, out dblNum))
                {
                    MessageBox.Show("Please enter a valid number!");
                    return;
                }
            }
            if (txtAvailabilityChange.Text != "")
            {
                if (txtAvailabilityChange.Text.ToLower() != "yes" && txtAvailabilityChange.Text.ToLower() != "no")
                {
                    MessageBox.Show("Please enter either Yes or No!");
                    return;
                }
            }


            UpdateJsonFile();
        }

        private void UpdateJsonFile()
        {
            //get combo box select item content

            ComboBoxItem selectedItem = (ComboBoxItem)cboSelectType.SelectedItem;
            string strSelectedName = selectedItem.Content.ToString().Trim();
      
            //update json file 

            foreach (Pricing item in pricingList)
            {
                if(txtPriceChange.Text != "")
                {
               if (item.Type == strSelectedName)
                   item.Price = txtPriceChange.Text;
                    item.Price.ToString("C2");
                    
                }

                if (txtAvailabilityChange.Text != "")
                { 
                if (item.Type == strSelectedName)
                    item.Availability = txtAvailabilityChange.Text.ToUpper();
                }
            }
            //write new information into jason file 
            string strFilePath = @"..\..\..\Data\Pricing.json";
            try
            {
            //serialize the new list of pricing to json format
                string jsonData = JsonConvert.SerializeObject(pricingList);
            //use System.IO.File to write over the file with the json data 
                System.IO.File.WriteAllText(strFilePath,jsonData);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in export processs: " + ex.Message);
            }
        }
       
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            // clear results
            txtAvailabilityChange.Text = "";
            txtPriceChange.Text = "";
            cboSelectType.SelectedIndex = -1;
            lblPriceResult.Content = "";
            lblAvailabilityResult.Content = ""; 
        }
    }
}

