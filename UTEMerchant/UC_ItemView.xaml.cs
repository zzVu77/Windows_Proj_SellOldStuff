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
    /// Interaction logic for UC_ItemView.xaml
    /// </summary>
    public partial class UC_ItemView : UserControl
    {
        public event EventHandler ItemClicked;


        public UC_ItemView()
        {
            InitializeComponent();
            this.MouseUp += UC_ItemView_MouseUp;

        }

        private void UC_ItemView_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ItemClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
