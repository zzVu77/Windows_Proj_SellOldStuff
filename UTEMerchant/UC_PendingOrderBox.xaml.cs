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
        public EventHandler CancelButtonClicked;

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
                IOrderedEnumerable<purchasedItem> order = _orders.OrderBy(x => new PurchasedItem_DAO().GetItem(x.PurchaseID).SellerID);
                int k = 0;
                for (int i = 0; i < order.Count(); i++)
                {
                    if (i == order.Count() - 1) ;
                    else if (new PurchasedItem_DAO().GetItem(order.ElementAt(i).PurchaseID).SellerID == new PurchasedItem_DAO().GetItem(order.ElementAt(i + 1).PurchaseID).SellerID) continue;
                    UC_PendingItemsBox ucPendingItemsBox = new UC_PendingItemsBox(order.Skip(k).Take(i - k + 1).ToList(), new Seller_DAO().GetSeller(new PurchasedItem_DAO().GetItem(order.ElementAt(i).PurchaseID).SellerID), _user.Id_user);
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
                    totalPrice += new PurchasedItem_DAO().GetItem(ucPendingItem.Order.PurchaseID).Price;
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
                        new PurchasedItem_DAO().CancelOrder(ucPendingItem.Order.PurchaseID);
                    }
                }
                ((StackPanel)Parent).Children.Remove(this);

                // Show a message box to inform the user that the order has been cancelled
                MessageBox.Show("The order has been cancelled successfully.", "Order Cancelled", MessageBoxButton.OK, MessageBoxImage.Information);

                // Refresh the parent window
                CancelButtonClicked?.Invoke(this, EventArgs.Empty);
            }
        }

        public void DeclinedOrderCheck()
        {
            var list = new List<purchasedItem>();
            foreach (UC_PendingItemsBox itemBox in spItems.Children)
            {
                foreach (UC_PendingItem item in itemBox.spItems.Children)
                {
                    if (item.Order.Delivery_Status == "declined")
                    {
                        list.Add(item.Order);
                    }
                }
            }

            if (list.Count > 0)
            {
                // Create a message box to confirm the cancellation
                var result = MessageBox.Show(
                    "Seller has declined an item or more in your order.\nDo you want to cancel the whole order?\nIf no, only the declined items will be cancelled which includes:\n" +
                    string.Join("\n- ", list.Select(item => new PurchasedItem_DAO().GetItem(item.Item_Id).Name)), "Cancel Order", MessageBoxButton.YesNoCancel,
                    MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    foreach (UC_PendingItemsBox itemBox in spItems.Children)
                    {
                        foreach (UC_PendingItem item in itemBox.spItems.Children)
                        {
                            new PurchasedItem_DAO().CancelOrder(item.Order.PurchaseID);
                        }
                    }
                    // Inform the user that the order has been cancelled
                    MessageBox.Show("The order has been cancelled successfully.", "Order Cancelled", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
                else if (result == MessageBoxResult.No)
                {
                    foreach (var item in list)
                    {
                        new PurchasedItem_DAO().CancelOrder(item.PurchaseID);
                    }
                    // Inform the user that the declined item has been cancelled
                    MessageBox.Show("The declined item has been cancelled successfully.", "Order Cancelled",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (result == MessageBoxResult.Cancel)
                {
                    MessageBox.Show("The declined item needs to be cancelled or the delivering cannot proceed",
                        "Warning",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }
    }
}
