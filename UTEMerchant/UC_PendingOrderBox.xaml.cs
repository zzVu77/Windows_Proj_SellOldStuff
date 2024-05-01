using System;
using System.Collections;
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
using Wpf.Ui.Controls;
using MessageBox = System.Windows.MessageBox;
using MessageBoxButton = System.Windows.MessageBoxButton;
using MessageBoxResult = System.Windows.MessageBoxResult;

namespace UTEMerchant
{
    /// <summary>
    /// Interaction logic for UC_PendingOrderBox.xaml
    /// </summary>
    public partial class UC_PendingOrderBox : UserControl
    {
        private readonly IGrouping<DateTime, Item> _items;
        private readonly User _user;

        public UC_PendingOrderBox()
        {
            InitializeComponent();
        }

        public UC_PendingOrderBox(IGrouping<DateTime, Item> items, User user) : this()
        {
            _items = items;
            _user = user;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (_items != null && _user != null)
            {
                spItems.Children.Clear();
                IOrderedEnumerable<Item> order = _items.OrderBy(x => x.SellerID);
                int k = 0;
                for (int i = 0; i < order.Count(); i++)
                {
                    if (i == order.Count() - 1) ;
                    else if (order.ElementAt(i).SellerID == order.ElementAt(i + 1).SellerID) continue;
                    UC_PendingItemsBox ucPendingItemsBox = new UC_PendingItemsBox(order.Skip(k).Take(i - k + 1).ToList(), new Seller_DAO().GetSeller(order.ElementAt(i).SellerID), _user.Id_user);
                    spItems.Children.Add(ucPendingItemsBox);
                    k = i + 1;
                }
            }
        }

        private double CalculateTotalPrice()
        {
            double totalPrice = 0;
            foreach (UC_PendingItemsBox ucPendingItemsBox in spItems.Children)
            {
                foreach (UC_PendingItem ucPendingItem in ucPendingItemsBox.spItems.Children)
                {
                    totalPrice += ucPendingItem.Item.Price;
                }
            }
            return totalPrice;
        }

        private int CountItems()
        {
            int count = 0;
            foreach (UC_PendingItemsBox ucPendingItemsBox in spItems.Children)
            {
                count += ucPendingItemsBox.spItems.Children.Count;
            }
            return count;
        }

        private void tbNumberOfItems_Loaded(object sender, RoutedEventArgs e)
        {
            tbTotalValue.Text = $"${CalculateTotalPrice().ToString("F", System.Globalization.CultureInfo.CurrentCulture)}";
        }

        private void tbTotalValue_Loaded(object sender, RoutedEventArgs e)
        {
            tbNumberOfItems.Text = $"{CountItems().ToString()} items";
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            // Show a message box to confirm the cancellation
            MessageBoxResult result = MessageBox.Show("Are you sure you want to cancel this order?", "Cancel Order", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                // Cancel the order
                foreach (UC_PendingItemsBox ucPendingItemsBox in spItems.Children)
                {
                    foreach (UC_PendingItem ucPendingItem in ucPendingItemsBox.spItems.Children)
                    {
                        new PurchasedItem_DAO().UpdateDeliveryStatus(ucPendingItem.Item.Item_Id, _user.Id_user, "cancelled");
                    }
                }
                ((StackPanel)Parent).Children.Remove(this);
            }
        }
    }
}
