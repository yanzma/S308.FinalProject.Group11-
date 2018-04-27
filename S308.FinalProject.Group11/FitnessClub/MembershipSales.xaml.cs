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
        List<Membership> membershipList;
        public MembershipSales()
        {
            InitializeComponent();
            pricingList = new List<Pricing>();
            membershipList = new List<Membership>();
            DateTime dateToday = DateTime.Today;
            dtpStartDate.SelectedDate = dateToday;
        }
        //Provide a way for user to close window and return to the main menu
        private string GetMonthlyCost()
        {
            string strFilePath = @"..\..\..\Data\Pricing.json";
            
            string jsonData = File.ReadAllText(strFilePath);
            pricingList = JsonConvert.DeserializeObject<List<Pricing>>(jsonData);

            string strCostpermonth = "";
            //get combo box selected item index

            ComboBoxItem selectedItem = (ComboBoxItem)cblMembershipType.SelectedItem;
            string strSelectedName = selectedItem.Content.ToString().Trim();

            //get corresponded item price and avaliability
            foreach (Pricing item in pricingList)
            {

                if (item.Type == strSelectedName)
                {
                    strCostpermonth = item.Price;
                }
            }
            return strCostpermonth;
        }
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
            string strPersonalTraining = "";
            string strLocker = "";
            if (selectedTraining.Content.Equals("Yes"))
            {
                dblSubtotal += 5;
                strPersonalTraining = "Yes";
            }
            else
            {
                strPersonalTraining = "No";
            }
            if(selectedLocker.Content.Equals("Yes"))
            {
                dblSubtotal += 1;
                strLocker = "Yes";
            }
            else
            {
                strLocker = "No";
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
            ComboBoxItem selectedLength = (ComboBoxItem)cblMembershipType.SelectedItem;

            
            //double dblSubtotal = Convert.ToDouble(strSubtotal.Substring(1));
            //double  .Parse(strSubtotal,NumberStyles.Currency) *= intLength;
            string strCost = GetMonthlyCost();
            double dblCost = Convert.ToDouble(strCost.Substring(1));
            double dblMoCo;
            if (selectedLength.Content.ToString().Contains("1 Month"))
            {
               intLength = 1;
               dblMoCo = dblCost/1;
            }
            else
            {
                intLength = 12;
                dblMoCo = dblCost/12;
            }
            double dblSubtotal_2 = dblSubtotal * intLength;
            double dblTotal = dblSubtotal_2 + dblCost;
            datEndDate = datTime1.AddMonths(intLength);


            string strPreview = "Membership Type: " + selectedType.Content.ToString() + Environment.NewLine
                + "Start Date: " + datTime1.ToShortDateString() + Environment.NewLine
                + "End Date: " + datEndDate.ToShortDateString() + Environment.NewLine
                + "Cost/Month: " + dblMoCo.ToString("C0") + Environment.NewLine
                + "Subtotal: " + strCost + Environment.NewLine
                + "Additional Features: " + Environment.NewLine
                + "Personal Training Plan: " + strPersonalTraining + Environment.NewLine
                + "Locker Rental: " + strLocker + Environment.NewLine
                + "Total: " + dblTotal.ToString("C0");


            txtPreviewWindow.Text = strPreview;

        }


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //user must confirm what they selected
            if(txtPreviewWindow.Text == "")
            {
                MessageBox.Show("Preview must be confirmed before entering user's information.");
                return;
            }

            //get membership type length
            int intLength;
            ComboBoxItem selectedLength = (ComboBoxItem)cblMembershipType.SelectedItem;
            DateTime? datStartDate = dtpStartDate.SelectedDate;
            DateTime datTime1 = (DateTime)datStartDate;
            DateTime datEndDate;
            if (selectedLength.Content.ToString().Contains("1 Month"))
            {
                intLength = 1;
            }
            else
            {
                intLength = 12;
            }
            datEndDate = datTime1.AddMonths(intLength);
            //After the quote preview, user starts to input their personal info
            if (txtFirstName.Text == "")
            {
                MessageBox.Show("Please enter your firstname.");
                return;
            }
            if(txtLastName.Text == "")
            {
                MessageBox.Show("Please enter your lastname.");
                return;
            }

            if (cboCardType.SelectedIndex == -1)
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
            if (cboGender.SelectedIndex == -1)
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

            //create a new member
            string strMoCost = GetMonthlyCost();
            ComboBoxItem selectedTraining = (ComboBoxItem)cboPersonalTraining.SelectedItem;
            ComboBoxItem selectedLocker = (ComboBoxItem)cboLocker.SelectedItem;
            double dblSubtotal = 0;
            if (selectedTraining.Content.Equals("Yes"))
            {
                dblSubtotal += 5;
            }
            if (selectedLocker.Content.Equals("Yes"))
            {
                dblSubtotal += 1;
            }
            
            if (selectedLength.Content.ToString() == "1 Month")
            {
                intLength = 1;
            }
            else
            {
                intLength = 12;
            }
            double dblSubtotal_2 = dblSubtotal * intLength;
            double dblCostpermonth = Convert.ToDouble(strMoCost.Substring(1));
            double dblTotal = dblSubtotal_2 + dblCostpermonth;

            Membership newMember = new Membership();
            ComboBoxItem personaltraining = (ComboBoxItem)cboPersonalTraining.SelectedItem;
            ComboBoxItem locker = (ComboBoxItem)cboLocker.SelectedItem;
            //format gender
            string strGender;
            if (cboGender.SelectedItem.ToString() == "Male")
            {
                strGender = "Male";
            }
            else if (cboGender.SelectedItem.ToString() == "Female")
            {
                strGender = "Female";
            }
            else
            {
                strGender = "Not Provided";
            }
            //Format Creditcard Type
            string strCardtype;
            if(cboCardType.SelectedItem.ToString()=="MasterCard")
            {
                strCardtype = "MasterCard";
            }
            else if (cboCardType.SelectedItem.ToString() == "Visa")
                {
                strCardtype = "Visa";
            }
            else
            { strCardtype = "Discover"; }

            //Formatting Traning Goal
            string strGoal;
            if (cboPersonalTraining.SelectedItem.ToString()=="Athletic Performance")
            {
                strGoal = "Athletic Performance";
            }
            else if (cboPersonalTraining.SelectedItem.ToString() == "Overall Health")
            {
                strGoal = "Overall Health";
            }
            else if(cboPersonalTraining.SelectedItem.ToString() == "Weight Management")
            {
                strGoal = "Weight Management";
            }
            else
            {
                strGoal = "Strength Training Weight Loss";
            }

            //Formatting Membership Type
            string strType;
            if(cblMembershipType.SelectedItem.ToString()=="Individual 1 Month")
            {
                strType = "Individual 1 Month";
            }
        else if (cblMembershipType.SelectedItem.ToString() == "Individual 12 Month")
            { strType = "Individual 12 Month"; }
            else if (cblMembershipType.SelectedItem.ToString() == "Two Person 1 Month")
            { strType = "Two Person 1 Month"; }
            else if (cblMembershipType.SelectedItem.ToString() == "Two Person 12 Month")
            { strType = "Two Person 12 Month"; }
            else if (cblMembershipType.SelectedItem.ToString() == "Family 1 Month")
            { strType = "Family 1 Month"; }
            else
            { strType = "Family 12 Month"; }

            //assign values to new newMember
            newMember.Firstname = txtFirstName.Text;
            newMember.Lastname = txtLastName.Text;
            newMember.Age = txtAge.Text;
            newMember.Weight = txtWeight.Text;
            newMember.Gender = strGender;
            newMember.CreditCardType = strCardtype;
            newMember.CreditCardNum = txtCardNum.Text;
            newMember.Email = txtEmail.Text;
            newMember.Phonenum = txtPhoneNumber.Text;
            newMember.ProfessionalGoal = strGoal;
            newMember.MembershipType = strType;
            newMember.StartDate = datStartDate.ToString();
            newMember.EndDate = datEndDate.ToString();
            newMember.CostperMonth = strMoCost;
            newMember.Subtotal = dblSubtotal_2.ToString();
            newMember.AdditionalFeatures = "Additional Features:" + Environment.NewLine
                + "Personal Training Plan: " + personaltraining.Content.ToString() + Environment.NewLine
                + "Locker Rental: " + locker.Content.ToString();
            newMember.Total = dblTotal.ToString();

            //read the data in the membership json file
            string strFilePath = @"..\..\..\Data\Membership.json";
            try
            {
                //use System.IO.File to read the entire data file
                string jsonData = File.ReadAllText(strFilePath);

                //serialize the json data to a list of pricing 
                membershipList = JsonConvert.DeserializeObject<List<Membership>>(jsonData);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in import process: " + ex.Message);
            }
            
            membershipList.Add(newMember);

            //write member into json file
            try
            {
                string jsonData = JsonConvert.SerializeObject(membershipList);
                System.IO.File.WriteAllText(strFilePath, jsonData);
                MessageBox.Show("New customer has been saved.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in export process: " + ex.Message);
            }
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
            cblMembershipType.SelectedIndex = -1;
            cboPersonalTraining.SelectedIndex = -1;
            cboLocker.SelectedIndex = -1;
            DateTime dateToday = DateTime.Today;
            dtpStartDate.SelectedDate = dateToday;
            txtPreviewWindow.Text = "";

        }
    }
}
