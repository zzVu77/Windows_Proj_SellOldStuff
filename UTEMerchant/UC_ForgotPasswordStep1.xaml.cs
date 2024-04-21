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
    /// Interaction logic for UC_ForgotPasswordStep1.xaml
    /// </summary>
    public partial class UC_ForgotPasswordStep1 : UserControl
    {
        new user_DAO dao = new user_DAO();
        public event EventHandler SendButtonClicked;
       
        public UC_ForgotPasswordStep1()
        {
            InitializeComponent();
        }
        private void textUserName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtEmail.Focus();
        }

        private void txtUserName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEmail.Text) && txtEmail.Text.Length > 0)
            {
                textEmail.Visibility = Visibility.Collapsed;
            }
            else
            {
                textEmail.Visibility = Visibility.Visible;
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
          
            SendButtonClicked?.Invoke(this, EventArgs.Empty);
            
        }

       
    }
}
