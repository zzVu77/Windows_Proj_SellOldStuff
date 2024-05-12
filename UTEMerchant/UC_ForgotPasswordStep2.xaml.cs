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
    /// Interaction logic for UC_ForgotPasswordStep2.xaml
    /// </summary>
    public partial class UC_ForgotPasswordStep2 : UserControl
    {
        public event EventHandler EnterButtonClicked;
        public event EventHandler BackButtonClicked;
        public UC_ForgotPasswordStep2()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {


            EnterButtonClicked?.Invoke(this, EventArgs.Empty);

        }

        

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            BackButtonClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
