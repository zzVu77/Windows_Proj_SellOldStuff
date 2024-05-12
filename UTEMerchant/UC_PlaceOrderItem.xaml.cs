using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.RightsManagement;
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
    /// Interaction logic for UC_PlaceOrderItem.xaml
    /// </summary>
    public partial class UC_PlaceOrderItem : UserControl
    {

        private readonly Item _item;

        public UC_PlaceOrderItem()
        {
            InitializeComponent();
        }

        public UC_PlaceOrderItem(Item item) : this()
        {
            this._item = item;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (_item != null)
            {
                imgItem.Source = new BitmapImage(new Uri(_item.image_path, UriKind.RelativeOrAbsolute));
                tbItemName.Text = _item.name;
                tbItemOriginalPrice.Text = $"${_item.original_price}";
                tbItemDiscountPrice.Text = $"${ _item.price}";
            }
        }
    }
}
