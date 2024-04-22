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
    /// Interaction logic for UC_PlaceOrderItemsBox.xaml
    /// </summary>
    public partial class UC_PlaceOrderItemsBox : UserControl
    {
        private readonly Seller _seller;

        public UC_PlaceOrderItemsBox()
        {
            InitializeComponent();
        }

        public UC_PlaceOrderItemsBox(Seller seller) : this()
        {
            this._seller = seller;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (_seller != null)
            {
                tbShopName.Text = _seller.ShopName;
            }
        }

        public Seller GetSeller()
        {
            return _seller;
        }

        public void AddItem(Item item)
        {
            UC_PlaceOrderItem ucPlaceOrderItem = new UC_PlaceOrderItem(item);
            if (spItems.Children.Count == 0)
            {
                spItems.Children.Add(ucPlaceOrderItem);
            }
            else if (spItems.Children.Count == 1)
            {
                if (spItems.Children[0] is UC_PlaceOrderItem first) first.Margin = new Thickness(0, 0, 0, 10);
                ucPlaceOrderItem.Margin = new Thickness(0, 10, 0, 0);
                spItems.Children.Add(ucPlaceOrderItem);
            }
            else if (spItems.Children.Count > 1)
            {
                if (spItems.Children[spItems.Children.Count - 1] is UC_PlaceOrderItem last) last.Margin = new Thickness(0, 10, 0, 10);
                ucPlaceOrderItem.Margin = new Thickness(0, 10, 0, 0);
                spItems.Children.Add(ucPlaceOrderItem);
            }
        }
    }
}
