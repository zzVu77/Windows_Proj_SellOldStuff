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
        private readonly List<purchasedItem> _orders;
        private readonly Seller _seller;
        private readonly int _userId;

        public UC_PendingItemsBox()
        {
            InitializeComponent();
        }

        public UC_PendingItemsBox(List<purchasedItem> orders, Seller seller, int userId) : this()
        {
            this._orders = orders;
            this._userId = userId;
            this._seller = seller;

            LoadOrders();
        }

        private void AddItem(purchasedItem order)
        {
            foreach (UC_PendingItem ucPendingItem in spItems.Children)
            {
                if (ucPendingItem.Order.PurchaseID == order.PurchaseID)
                {
                    return;
                }
            }
            var ucItem = new UC_PendingItem(order);
            spItems.Children.Add(ucItem);
        }

        private void LoadOrders()
        {
            if (_seller != null)
            {
                tbShopName.Text = _seller.ShopName;
            }

            if (_orders != null)
            {
                foreach (var item in _orders)
                {
                    AddItem(item);
                }
            }
        }
    }
}
