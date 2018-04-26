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

namespace FitnessClub
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class MembershipSales : Window
    {
        List<Pricing> pricingList;
        public MembershipSales()
        {
            InitializeComponent();
            pricingList = new List<Pricing>();
        }
        //Provide a way for user to close window and return to the main menu
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Window winMain = new MainMenu();
            winMain.Show();
            this.Close();
        }

        private void btnSubmitInput_Click(object sender, RoutedEventArgs e)
        {
            //Validate that member type is selected
            if (cblMembershipType.SelectedIndex == 0)
            {
                MessageBox.Show("Please select a membership type.");
                return;
             }

            //Validate that the membership start date is not in the past 
            DateTime datToday = DateTime.Today;

            DateTime? datStartDate = dtpStartDate.SelectedDate;
            DateTime datTime1 = (DateTime)datStartDate;
            DateTime datEndDate;
            if(datStartDate < datToday)
            {
                MessageBox.Show("Please select a valid date.");
                return;
            }
            //Validate that at least check 1 checkbox for additional features

            //Clear outputs from previous 
            txtPreviewWindow.Text = "";

            //Validation:
            //1 Should be one checkbox selected

            //Calculate
            //1. Based on type and start date, get end date
            //2. If checkbox for personal training is "yes", +$5.00/month
            //   If checkbox for locker rental is "yes", +$1.00/mont.Sh
            //3. Cost/Month = price for each membership type
            //4. Subtotal = Cost/Month * #of month >> (End-Start?)
            //5. Total = Subtotal + additional features * # of month
            int intLength;
            ComboBoxItem selectedType = (ComboBoxItem)cblMembershipType.SelectedItem;
            ComboBoxItem selectedLength = (ComboBoxItem)cboLength.SelectedItem;
            //read price from combo

            if(selectedLength.Content.ToString()=="1 Month")
            {
               intLength = 1;
            }
            else
            {
                intLength = 12;
            }
            datEndDate = datTime1.AddMonths(intLength);
            string strPreview = "Membership Type: " + selectedType.Content + Environment.NewLine
                + "Start Date: " + datStartDate + Environment.NewLine
                + "End Date: "+ datEndDate + Environment.NewLine
                + 

        }

        private void ImportPricingData()
        {
            string strFilePath = @"..\..\..\..\Pricing.json";


            string jsonData = File.ReadAllText(strFilePath);
            pricingList = JsonConvert.DeserializeObject<List<Pricing>>(jsonData);

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            //After the quote preview, user starts to input their personal info
            if(txtFirstName.Text == "")
            {
                MessageBox.Show("Please enter your firstname.");
                return;
            }
            if(txtLastName.Text == "")
            {
                MessageBox.Show("Please enter your lastname.");
                return;
            }

            if (cboCardType.SelectedIndex == 0)
            {
                MessageBox.Show("Please select a card type.");
                return;
            }
            if (txtCardNum.Text =="")
            {
                 MessageBox.Show("Please select a membership type.");
                 return;
            }
            if (txtPhoneNumber.Text=="")
            {
                MessageBox.Show("Please enter a phone number.");
                return;
            }
            if (txtEmail.Text=="")
            {
                MessageBox.Show("Please enter an email address.");
                return;
            }
            if (cboGender.SelectedIndex == 0)
            {
                MessageBox.Show("This is a required field.");
                return;
            }
            //Validate phone numbers need to be stored as 10 digits without any formatting
            //Reutrn false as status to the calling if it fails
            string strAreaCode, strPhonePart1, strPhonePart2, strPhoneNum;
            strPhoneNum = txtPhoneNumber.Text.ToString();
            strAreaCode = strPhoneNum.Substring(0, 3);
            strPhonePart1 = strPhoneNum.Substring(3, 3);
            strPhonePart2 = strPhoneNum.Substring(6, 4);
            if (strPhoneNum != String.Format("{0}{1}{2}", strAreaCode, strPhonePart1, strPhonePart2))
            {
                MessageBox.Show("Please enter a 10-digit phone number without formatting.");
                return;
            }

            List<Membership> MembershipList;
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            //Clear out value 
            txtFirstName.Text = "";
            txtLastName.Text = "";
            cboGender.SelectedIndex = -1;
            txtPhoneNumber.Text = "";
            txtEmail.Text = "";
            cboCardType.SelectedIndex = -1;
            txtCardNum.Text = "";
            txtAge.Text = "";
            txtWeight.Text = "";
            cboFitnessGoal.SelectedIndex = -1;

        }
    }
}
