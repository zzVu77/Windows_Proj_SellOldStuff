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
    /// Interaction logic for UC_ShoppingCart.xaml
    /// </summary>
    public partial class UC_ShoppingCart : UserControl
    {
        public List<UC_ShoppingCartItemView> listUC_ShoppingCartItemViews = new List<UC_ShoppingCartItemView>();


        public UC_ShoppingCart()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {

        }

        public void CheckCart()
        {
            if (spItems.Children.Count == 0)
            {
                svItems.Visibility = Visibility.Collapsed;
                grdEmptyShoppingCart.Visibility = Visibility.Visible;
            }
            else
            {
                grdEmptyShoppingCart.Visibility = Visibility.Collapsed;
                svItems.Visibility = Visibility.Visible;
            }
        }
        
    }
}
