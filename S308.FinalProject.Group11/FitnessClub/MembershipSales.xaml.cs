using System;
using System.Collections.Generic;
using System.IO;
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
            //Initiate variable 
            double dblSubtotal = 0;

            //Validate that member type is selected
            if (cblMembershipType.SelectedIndex == -1)
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
            //Tab order for training and locker combo boxes
            //Validate whether or not to choose the additional features
            //If chosen, add the amount back to the subtotal
            if (cboPersonalTraining.SelectedIndex == -1)
            {
                MessageBox.Show("At least select one option.");
                return;
            }
            if (cboLocker.SelectedIndex == -1)
            {
                MessageBox.Show("At least select one option.");
                return;
            }
            ComboBoxItem selectedTraining = (ComboBoxItem)cboPersonalTraining.SelectedItem;
            ComboBoxItem selectedLocker = (ComboBoxItem)cboLocker.SelectedItem;
            if (selectedTraining.Content.Equals("Yes"))
            {
                dblSubtotal += 5;
            }
            if(selectedLocker.Content.Equals("Yes"))
            {
                dblSubtotal += 1;
            }


            //Clear outputs from previous 
            txtPreviewWindow.Text = "";



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
            if (selectedType.Content.("Individual 1 Month" "Two Person 1 Month""Family 1 Month"))
            {
                dblSubtotal += 1;
            }
            //read price from combo
            string strSubtotal;
            foreach (Pricing item in pricingList)
            {
                if (item.Type.Equals(selectedType))
                    strSubtotal = item.Price;
            }
            //double dblSubtotal = Convert.ToDouble(strSubtotal.Substring(1));
            //double  .Parse(strSubtotal,NumberStyles.Currency) *= intLength;
            if(selectedLength.Content.ToString()=="1 Month")
            {
               intLength = 1;
            }
            else
            {
                intLength = 12;
            }
            datEndDate = datTime1.AddMonths(intLength);
            string strPreview = "Membership Type: Individual 1 Month " + Environment.NewLine
                + "Start Date: 6/3/2018" + Environment.NewLine
                + "End Date: 7/3/2018" + Environment.NewLine
                + "Cost/Month: $9.99" + Environment.NewLine
                + "Subtotal: $9.99" + Environment.NewLine
                + Environment.NewLine
                + "Additional Features:" + Environment.NewLine
                + Environment.NewLine
                + "Total: $9.99";




        }

        private void ImportPricingData()
        {
            string strFilePath = @"..\..\..\Data\Pricing.json";


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
            string strFinalWindow = 
                "First Name: " + txtFirstName.Text.ToString() + Environment.NewLine
                +"Last Name: "+ txtLastName.Text.ToString()+Environment.NewLine
                +"Age: "+txtAge.Text.ToString()+Environment.NewLine
                +"Weight: "+ txtWeight.Text.ToString()+ Environment.NewLine
                +"Gener: "+ cboGender.SelectedItem.ToString()+Environment.NewLine
                +"CreditCard Type: " + cboCardType.SelectedItem.ToString()+ Environment.NewLine
                +"CreditCard Number: "+ txtCardNum.Text.ToString()+Environment.NewLine
                +"Email: "+txtEmail.Text.ToString()+Environment.NewLine
                +"Phone Num: "+ txtPhoneNumber.Text
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
