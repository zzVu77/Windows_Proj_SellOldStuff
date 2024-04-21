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
    /// Interaction logic for UC_SignUpStep1.xaml
    /// </summary>
    public partial class UC_SignUpStep1 : UserControl
    {
        public event EventHandler NextButtonClicked;

        public UC_SignUpStep1()
        {
            InitializeComponent();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)    
        {
           
            NextButtonClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
