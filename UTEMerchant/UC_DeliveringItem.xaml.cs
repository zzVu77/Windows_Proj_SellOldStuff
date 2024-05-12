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
        private readonly PurchasedProduct _orders;

        public UC_DeliveringItem()
        {
            InitializeComponent();
        }

        public UC_DeliveringItem(PurchasedProduct order) : this()
        {
            this._orders = order;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (_orders != null)
            {
                Item item = new PurchasedItem_DAO().GetItem(_orders.PurchaseID);
                tbName.Text = item.name;
                tbDiscountedPrice.Text = $"{item.price:C}";
                tbOriginalPrice.Text = $"{item.original_price:C}";
                var resourceUri = new Uri(item.image_path, UriKind.RelativeOrAbsolute);
                imgItem.Source = new BitmapImage(resourceUri);
            }
        }

        public PurchasedProduct Item => _orders;
    }
}
