using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Principal;
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
using System.Xml.Linq;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace UTEMerchant
{
    /// <summary>
    /// Interaction logic for WinSignUp.xaml
    /// </summary>
    public partial class WinSignUp : Window
    {
        private User User { get; set; }
        private string verifycode ;
        public WinSignUp()
        {
            InitializeComponent();
            UCSignUpStep1.NextButtonClicked += UCSignUpStep1_NextButtonClicked;
            UCSignUpStep2.BackButtonClicked += UCSignUpStep2_BackButtonClicked;
            UCSignUpStep2.FinishButtonClicked += UCSignUpStep2_FinishButtonClicked;
            UCCompleted.ContinueButtonClicked += UCCompleted_ContinueButtonClicked;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
        private void UCSignUpStep1_NextButtonClicked(object sender, EventArgs e)
        {
            if (sender is UC_SignUpStep1 clickedItemView )
            {
                if (!string.IsNullOrWhiteSpace(UCSignUpStep1.ucEmail.textbox.Text.ToString()) && CheckValid.IsValidEmail(UCSignUpStep1.ucEmail.textbox.Text.ToString()))
                {
                    UCSignUpStep1.Visibility = Visibility.Collapsed;
                    UCSignUpStep2.Visibility = Visibility.Visible;
                    User = new User(UCSignUpStep1.ucUserName.textbox.Text.ToString(), UCSignUpStep1.ucPassword.textbox.Text.ToString(),
                    UCSignUpStep1.ucName.textbox.Text.ToString(), UCSignUpStep1.selectedCity, UCSignUpStep1.selectedDistrict,
                    UCSignUpStep1.ucWard.textbox.Text.ToString(), UCSignUpStep1.ucPhone.textbox.Text.ToString(), 
                    UCSignUpStep1.ucEmail.textbox.Text.ToString(),
                    @"C:\Users\FPTSHOP\Desktop\Window_Proj_UTEMerchant\UTEMerchant\Img\icons8-account-50.png");

  
                    Random random = new Random();
                    // Any random integer
                    verifycode = random.Next().ToString();
                    SendMail.Send(verifycode, UCSignUpStep1.ucEmail.textbox.Text.ToString());

                }
                else
                {
                    MessageBox.Show("invalid information");
                }
  
            }

        }
        private void UCSignUpStep2_BackButtonClicked(object sender, EventArgs e)
        {
            if (sender is UC_SignUpStep2 clickedItemView)
            {
                UCSignUpStep2.Visibility = Visibility.Collapsed;
                UCSignUpStep1.Visibility =Visibility.Visible;
            }

        }

        private void UCSignUpStep2_FinishButtonClicked(object sender, EventArgs e)
        {
            if (sender is UC_SignUpStep2 clickedItemView)
            {
                UCSignUpStep2.Visibility = Visibility.Collapsed;
                UCSignUpStep1.Visibility = Visibility.Collapsed;
                UCCompleted.Visibility = Visibility.Visible;
                
                if (UCSignUpStep2.ucVerifyCode.textbox.Text.ToString() == verifycode)
                {
                    new user_DAO().Add(User);
                }
            }

        }
        private void UCCompleted_ContinueButtonClicked(object sender, EventArgs e)
        {
            if (sender is UC_RegistrationCompleted clickedItemView)
            {
                this.Close();
            }

        }
    }
}