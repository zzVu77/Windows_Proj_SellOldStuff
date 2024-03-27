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
        List<User> users = new List<User>();
        public WinLogin()
        {
            InitializeComponent();
            user_DAO a = new user_DAO();
            a.Load();
            users=a.Users;
        }

        private void txtUserName_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUserName.Text) && txtUserName.Text.Length > 0)
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
            txtUserName.Focus();
        }

        private void textPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
            txtPassword.Focus();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (User user in users)
            {
                if (user.Password == txtPassword.Password && user.User_name == txtUserName.Text)
                {
                    this.Hide();
                    var purchasing = new WinSellerInterface(user);
                    purchasing.ShowDialog();
                    this.Show();
                }
                else
                {
                    MessageBox.Show("user name or password is incorrect !!!!!");
                }
            }

        }

        private void textForgot_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void textRegister_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }


        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPassword.Password) && txtPassword.Password.Length > 0)
            {
                textPassword.Visibility = Visibility.Collapsed;
            }
            else
            {
                textPassword.Visibility = Visibility.Visible;
            }
        }

      
    }
}
