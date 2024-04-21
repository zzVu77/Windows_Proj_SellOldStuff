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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace UTEMerchant
{
    /// <summary>
    /// Interaction logic for UC_ForgotPasswordStep3.xaml
    /// </summary>
    public partial class UC_ForgotPasswordStep3 : UserControl
    {
        public event EventHandler EnterButtonClicked;

        public UC_ForgotPasswordStep3()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {


            EnterButtonClicked?.Invoke(this, EventArgs.Empty);


        }

        private void textVerifyCode_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtNewPassword.Focus();
        }

        private void txtCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNewPassword.Text) && textNewPassword.Text.Length > 0)
            {
                textNewPassword.Visibility = Visibility.Collapsed;
            }
            else
            {
                textNewPassword.Visibility = Visibility.Visible;
            }
        }

        private void textNewPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtCFNewPassword.Focus();
        }

        private void txtNewPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCFNewPassword.Text) && txtCFNewPassword.Text.Length > 0)
            {
                textCFNewPassword.Visibility = Visibility.Collapsed;
            }
            else
            {
                textCFNewPassword.Visibility = Visibility.Visible;
            }
        }
    }
}
