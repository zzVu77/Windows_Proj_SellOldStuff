using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Policy;
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
    /// Interaction logic for UC_PendingItem.xaml
    /// </summary>
    public partial class UC_PendingItem : UserControl
    {
        private readonly purchasedItem _order;

        public UC_PendingItem()
        {
            InitializeComponent();
        }

        public UC_PendingItem(purchasedItem order) : this()
        {
            this._order = order;
        }


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (_order != null)
            {
                Item item = new PurchasedItem_DAO().GetItem(_order.PurchaseID);
                tbName.Text = item.Name;
                tbDiscountedPrice.Text = item.Price.ToString("C", CultureInfo.CurrentCulture);
                tbOriginalPrice.Text = item.Original_Price.ToString("C", CultureInfo.CurrentCulture);
                var resourceUri = new Uri(item.Image_Path, UriKind.RelativeOrAbsolute);
                imgItem.Source = new BitmapImage(resourceUri);
                if (_order.Delivery_Status == "declined")
                {
                    imgDeclined.Visibility = Visibility.Visible;
                }
            }
            else
            {
                MessageBox.Show("There are no Items that need to be confirmed", "No Items", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public purchasedItem Order => _order;
    }
}
