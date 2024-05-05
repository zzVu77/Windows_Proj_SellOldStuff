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
    /// Interaction logic for UC_DeliveringItem.xaml
    /// </summary>
    public partial class UC_DeliveringItem : UserControl
    {
        private readonly purchasedItem _orders;

        public UC_DeliveringItem()
        {
            InitializeComponent();
        }

        public UC_DeliveringItem(purchasedItem order) : this()
        {
            this._orders = order;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (_orders != null)
            {
                Item item = new PurchasedItem_DAO().GetItem(_orders.Item_Id);
                tbName.Text = item.Name;
                tbDiscountedPrice.Text = item.Price.ToString("C", CultureInfo.CurrentCulture);
                tbOriginalPrice.Text = item.Original_Price.ToString("C", CultureInfo.CurrentCulture);
                var resourceUri = new Uri(item.Image_Path, UriKind.RelativeOrAbsolute);
                imgItem.Source = new BitmapImage(resourceUri);
            }
        }

        public purchasedItem Item => _orders;
    }
}
