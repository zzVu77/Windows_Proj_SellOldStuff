using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for UC_CancelledItem.xaml
    /// </summary>
    public partial class UC_CancelledItem : UserControl
    {
        private readonly purchasedItem _order;
        private readonly User _user;

        public UC_CancelledItem()
        {
            InitializeComponent();
        }

        public UC_CancelledItem(purchasedItem order, User user) : this()
        {
            this._order = order;
            this._user = user;
        }

        private void btnReorder_Click(object sender, RoutedEventArgs e)
        {
            if (_order != null)
            {
                Item item = new PurchasedItem_DAO().GetItem(_order.PurchaseID);
                Seller seller = new Seller_DAO().GetSeller(item.SellerID);

                //Add item to cart
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (_order != null)
            {
                Item item = new PurchasedItem_DAO().GetItem(_order.PurchaseID);
                tbName.Text = item.Name;
                tbPrice.Text = item.Price.ToString("C", CultureInfo.CurrentCulture);
                tbOriginalPrice.Text = item.Original_Price.ToString("C", CultureInfo.CurrentCulture);
                var resourceUri = new Uri(item.Image_Path, UriKind.RelativeOrAbsolute);
                imgItem.Source = new BitmapImage(resourceUri);
            }
        }
    }
}
