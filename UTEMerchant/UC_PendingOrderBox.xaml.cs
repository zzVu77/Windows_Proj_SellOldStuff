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
        private readonly IGrouping<DateTime, purchasedItem> _orders;
        private readonly User _user;

        public UC_PendingOrderBox()
        {
            InitializeComponent();
        }

        public UC_PendingOrderBox(IGrouping<DateTime, purchasedItem> orders, User user) : this()
        {
            _orders = orders;
            _user = user;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (_orders != null && _user != null)
            {
                spItems.Children.Clear();
                IOrderedEnumerable<purchasedItem> order = _orders.OrderBy(x => new PurchasedItem_DAO().GetItem(x.PurchasedID).SellerID);
                int k = 0;
                for (int i = 0; i < order.Count(); i++)
                {
                    if (i == order.Count() - 1) ;
                    else if (new PurchasedItem_DAO().GetItem(order.ElementAt(i).PurchasedID).SellerID == new PurchasedItem_DAO().GetItem(order.ElementAt(i + 1).PurchasedID).SellerID) continue;
                    UC_PendingItemsBox ucPendingItemsBox = new UC_PendingItemsBox(order.Skip(k).Take(i - k + 1).ToList(), new Seller_DAO().GetSeller(new PurchasedItem_DAO().GetItem(order.ElementAt(i).PurchasedID).SellerID), _user.Id_user);
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
                    totalPrice += new PurchasedItem_DAO().GetItem(ucPendingItem.Order.PurchasedID).Price;
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
            tbNumberOfItems.Text = $"{CountItems().ToString()} orders";
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
                        new PurchasedItem_DAO().CancelOrder(ucPendingItem.Order.PurchasedID);
                    }
                }
                ((StackPanel)Parent).Children.Remove(this);
            }
        }
    }
}
