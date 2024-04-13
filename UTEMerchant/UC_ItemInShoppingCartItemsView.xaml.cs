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
    /// Interaction logic for UC_ItemInShoppingCartItemsView.xaml
    /// </summary>
    public partial class UC_ItemInShoppingCartItemsView : UserControl
    {
        public event EventHandler<RoutedEventArgs> RmItemClicked;

        public event EventHandler<RoutedEventArgs> ChkItemChecked;

        public event EventHandler<RoutedEventArgs> ChkItemUnchecked;

        private readonly Item _item;

        public UC_ItemInShoppingCartItemsView()
        {
            InitializeComponent();
        }

        public UC_ItemInShoppingCartItemsView(Item item) : this()
        {
            this._item = item;
        }

        public Item GetItem()
        {
            return _item;
        }

        private void chkItem_Checked(object sender, RoutedEventArgs e)
        {
            ChkItemChecked?.Invoke(this, EventArgs.Empty as RoutedEventArgs);
        }


        private void chkItem_Unchecked(object sender, RoutedEventArgs e)
        {
            ChkItemUnchecked?.Invoke(this, EventArgs.Empty as RoutedEventArgs);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (_item != null)
            {
                imgItem.Source = new BitmapImage(new Uri(_item.Image_Path, UriKind.RelativeOrAbsolute)) as ImageSource;
                LblItemName.Content = _item.Name;
                txtItemOriginalPrice.Text = $"{_item.Original_Price.ToString(CultureInfo.InvariantCulture)}$";
                txtItemDiscountPrice.Text = $"{_item.Price.ToString(CultureInfo.InvariantCulture)}$";
            }
        }

        public void SetRemoveButtonVisibility(bool visibility)
        {
            btnRemove.Visibility = visibility ? Visibility.Visible : Visibility.Collapsed;
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            RmItemClicked?.Invoke(this, EventArgs.Empty as RoutedEventArgs);
        }
    }
}
