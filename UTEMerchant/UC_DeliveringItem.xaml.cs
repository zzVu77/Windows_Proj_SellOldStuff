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
        private readonly Item _item;

        public UC_DeliveringItem()
        {
            InitializeComponent();
        }

        public UC_DeliveringItem(Item item) : this()
        {
            this._item = item;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (_item != null)
            {
                tbName.Text = _item.Name;
                tbDiscountedPrice.Text = _item.Price.ToString("C", CultureInfo.CurrentCulture);
                tbOriginalPrice.Text = _item.Original_Price.ToString("C", CultureInfo.CurrentCulture);
                var resourceUri = new Uri(_item.Image_Path, UriKind.RelativeOrAbsolute);
                imgItem.Source = new BitmapImage(resourceUri);
            }
        }

        public Item Item => _item;
    }
}
