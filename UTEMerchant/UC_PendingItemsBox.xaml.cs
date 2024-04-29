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
    /// Interaction logic for UC_PendingItemsBox.xaml
    /// </summary>
    public partial class UC_PendingItemsBox : UserControl
    {
        private readonly List<Item> _items;
        private readonly Seller _seller;
        private readonly int _userId;

        public UC_PendingItemsBox()
        {
            InitializeComponent();
        }

        public UC_PendingItemsBox(List<Item> items, Seller seller, int userId) : this()
        {
            this._items = items;
            this._userId = userId;
            this._seller = seller;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (_seller != null)
            {
                tbShopName.Text = _seller.ShopName;
            }

            if (_items != null)
            {
                spItems.Children.Clear();
                foreach (var item in _items)
                {
                    AddItem(item);
                }
            }
        }

        private void AddItem(Item item)
        {
            foreach (UC_PendingItem ucPendingItem in spItems.Children)
            {
                if (ucPendingItem.Item.Item_Id == item.Item_Id)
                {
                    return;
                }
            }
            var ucItem = new UC_PendingItem(item);
            spItems.Children.Add(ucItem);
        }
    }
}
