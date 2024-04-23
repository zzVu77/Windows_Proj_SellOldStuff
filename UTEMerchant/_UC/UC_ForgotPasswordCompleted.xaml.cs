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
    /// Interaction logic for UC_ForgotPasswordCompleted.xaml
    /// </summary>
    public partial class UC_ForgotPasswordCompleted : UserControl
    {
        public event EventHandler ContinueButtonClicked;
        public UC_ForgotPasswordCompleted()
        {
            InitializeComponent();
        }
        private void btnContinue_Click(object sender, RoutedEventArgs e)
        {
            ContinueButtonClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
