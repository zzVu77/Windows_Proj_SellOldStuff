//using HandyControl.Controls;
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

namespace UTEMerchant
{
    /// <summary>
    /// Interaction logic for WinForgotPassword.xaml
    /// </summary>
    public partial class WinForgotPassword : Window
    {
        new user_DAO dao = new user_DAO();
        private string verifycode;
        private string gmail;
        public WinForgotPassword()
        {
            InitializeComponent();
            UC_Step1.SendButtonClicked += UCCompleted_SendButtonClicked;
            UC_Step2.EnterButtonClicked += UCCompleted_EnterButtonClicked;
            UC_Step2.BackButtonClicked += UCStep2_BackButtonClicked;
            UC_Step3.EnterButtonClicked += UCCompleted_EnterButtonClickedPassword;
            UC_Step4.ContinueButtonClicked += UCCompleted_ContinueButtonClicked1;
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
        private void UCCompleted_SendButtonClicked(object sender, EventArgs e)
        {
            
            if (sender is UC_ForgotPasswordStep1 clickedItemView)
            {
                User user = dao.GetUserByItemEmail(UC_Step1.ucEmail.textbox.Text.ToString());
                if (user != null)
                {
                    gmail = user.Email;
                    Random random = new Random();
                    verifycode = random.Next().ToString();
                    SendMail.Send(verifycode, user.Email);
                    Step2.Visibility = Visibility.Visible;
                    Step1.Visibility = Visibility.Collapsed;
                    Step3.Visibility = Visibility.Collapsed;
                    Step4.Visibility = Visibility.Collapsed;
                }
                else
                {
                    MessageBox.Show("Email is not registered !!!");
                }

            }
        }
        private void UCCompleted_EnterButtonClicked(object sender, EventArgs e)
        {

            if (sender is UC_ForgotPasswordStep2 clickedItemView)
            {
                if (UC_Step2.ucVerifyCode.textbox.Text == verifycode)
                {
                    Step3.Visibility = Visibility.Visible;
                    Step2.Visibility = Visibility.Collapsed;
                    Step1.Visibility = Visibility.Collapsed;
                    Step4.Visibility = Visibility.Collapsed;
                }

            }
        }
        private void UCStep2_BackButtonClicked(object sender, EventArgs e)
        {

            if (sender is UC_ForgotPasswordStep2 clickedItemView)
            {
                Step1.Visibility = Visibility.Visible;
                Step2.Visibility = Visibility.Collapsed;
                Step3.Visibility = Visibility.Collapsed;
                Step4.Visibility = Visibility.Collapsed;
            }
        }
        private void UCCompleted_EnterButtonClickedPassword(object sender, EventArgs e)
        {

            if (sender is UC_ForgotPasswordStep3 clickedItemView)
            {
                if (UC_Step3.txtCFNewPassword.Text == UC_Step3.txtNewPassword.Text)
                {
                    dao.updateUser(UC_Step3.txtNewPassword.Text.ToString(), gmail);
                    Step4.Visibility = Visibility.Visible;
                    Step2.Visibility = Visibility.Collapsed;
                    Step1.Visibility = Visibility.Collapsed;
                    Step3.Visibility = Visibility.Collapsed;
                }
                else
                {
                    MessageBox.Show("the passwords do not match each other !!");
                }

            }
        }
        private void UCCompleted_ContinueButtonClicked1(object sender, EventArgs e)
        {

            if (sender is UC_ForgotPasswordCompleted clickedItemView)
            {
                this.Close();
            }
        }
    }
}