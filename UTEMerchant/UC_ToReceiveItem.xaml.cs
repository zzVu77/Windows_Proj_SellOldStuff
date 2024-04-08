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
    /// Interaction logic for UC_ToReceiveItem.xaml
    /// </summary>
    public partial class UC_ToReceiveItem : UserControl
    {

        private readonly Item Item;
        private readonly Seller SellerOfItem;

        public UC_ToReceiveItem()
        {
            InitializeComponent();
            this.Width = 1300;
        }

        public UC_ToReceiveItem(Item item, Seller seller) : this()
        {
            this.Item = item;
            this.SellerOfItem = seller;
            SetData(item, SellerOfItem);
        }

        //Binding data to the UI
        private void SetData(Item item, Seller seller)
        {
            var resourceUri = new Uri(item.Image_Path, UriKind.RelativeOrAbsolute);
            imgToReceiveItem.Source = new BitmapImage(resourceUri);
            txblShopName.Text = SellerOfItem.ShopName;
            txblToReceiveOriginalPrice.Text = $"{Item.Original_Price.ToString(CultureInfo.InvariantCulture)}$";
            txblToReceivePrice.Text = $"{Item.Price.ToString(CultureInfo.InvariantCulture)}$";
            txblToReceiveConditon.Text = $"{Item.Condition.ToString(CultureInfo.InvariantCulture)}%";
            txblToReceiveItemName.Text = Item.Name;
        }
    }
}
