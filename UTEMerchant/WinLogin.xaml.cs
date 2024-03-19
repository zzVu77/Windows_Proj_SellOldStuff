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
    /// Interaction logic for WinLogin.xaml
    /// </summary>
    public partial class WinLogin : Window
    {
        public WinLogin()
        {
            InitializeComponent();
        }

        private void txtUserName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserName.Text) && txtUserName.Text.Length > 0) 
            {
                textUserName.Visibility = Visibility.Collapsed;
            }
            else
            {
                textUserName.Visibility = Visibility.Visible;
            }
        }

        private void textUserName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            textUserName.Text=string.Empty;
            txtUserName.Focus();
        }

        private void textPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            textPassword.Text = string.Empty;
            txtPassword.Focus();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPassword.Password) && !string.IsNullOrEmpty(txtUserName.Text))
            {
                this.Hide();
                var purchasing = new WinSellerInterface();
                MessageBox.Show("successfully signed up!!!!!");
                purchasing.ShowDialog();
                this.Show();
                
            }

        }

        private void textForgot_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void textRegister_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void txtPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Password) && txtPassword.Password.Length > 0)
            {
                textPassword.Visibility = Visibility.Collapsed;
            }
            else
            {
                textPassword.Visibility = Visibility.Visible;
            }
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
