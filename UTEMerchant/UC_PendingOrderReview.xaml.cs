using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
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
    /// Interaction logic for UC_PendingOrderReview.xaml
    /// </summary>
    public partial class UC_PendingOrderReview : UserControl
    {
        private Seller _seller;
        private List<purchasedItem> _pendingOrders;

        public UC_PendingOrderReview()
        {
            InitializeComponent();
        }

        public UC_PendingOrderReview(Seller seller) : this()
        {
            _seller = seller;
        }

        private void ProductGrid_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BtnApprove_OnClick(object sender, RoutedEventArgs e)
        {
            // Get the clicked row in the data grid
            var row = (DataGridRow)productGrid.ItemContainerGenerator.ContainerFromItem(((Button)sender).DataContext);
            // Get the index of the clicked row
            var index = productGrid.ItemContainerGenerator.IndexFromContainer(row);
            // Update the status of the clicked row in the database
            var purchaseId = ((dynamic)productGrid.Items[index]).PurchaseID;
            User user = new user_DAO().GetUserByUserName((string)((dynamic)productGrid.Items[index]).User_name);
            new PurchasedItem_DAO().UpdateDeliveryStatus(purchaseId, "delivering");
            // Remove the clicked row from the data grid
            productGrid.Items.RemoveAt(index);

        }

        private void BtnDecline_OnClick(object sender, RoutedEventArgs e)
        {
            // Get the clicked row in the data grid
            var row = (DataGridRow)productGrid.ItemContainerGenerator.ContainerFromItem(((Button)sender).DataContext);
            // Get the index of the clicked row
            var index = productGrid.ItemContainerGenerator.IndexFromContainer(row);
            // Update the status of the clicked row in the database
            var id = ((dynamic)productGrid.Items[index]).PurchaseID;
            User user = new user_DAO().GetUserByUserName((string)((dynamic)productGrid.Items[index]).User_name);
            new PurchasedItem_DAO().UpdateDeliveryStatus(id, "declined");
            // Remove the clicked row from the data grid
            productGrid.Items.RemoveAt(index);
        }

        private void DeliveryAddress_OnHandler(object sender, MouseButtonEventArgs e)
        {
            // Display the address on message box when the user click on the delivery address
            MessageBox.Show(((TextBlock)sender).Text, "Delivery Address", MessageBoxButton.OK,
                MessageBoxImage.Information);

        }

        private void imgItem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Image viewer
            WinImageZoom winImageZoom = new WinImageZoom((Image)sender);
            winImageZoom.ShowDialog();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (StaticValue.SELLER != null)
            {
                _pendingOrders = new PurchasedItem_DAO().LoadOrdersBySeller(StaticValue.SELLER.SellerID, "pending");
            }
        }

        public void SetSeller()
        {
            //_seller = StaticValue.SELLER;
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (IsVisible) UserControl_Loaded(this, new RoutedEventArgs());
        }

        private void productGrid_Loaded(object sender, RoutedEventArgs e)
        {
            if (StaticValue.SELLER != null)
            {
                if (_pendingOrders != null && _pendingOrders.Count != 0)
                {
                    productGrid.Items.Clear();

                    // Create a new row for each pending order
                    foreach (var item in _pendingOrders)
                    {
                        string DeliveryAddress = item.Delivery_address;
                        User user = new PurchasedItem_DAO().GetUser(item.PurchaseID);
                        productGrid.Items.Add
                        (new
                            {
                                item.PurchaseID,
                                item.Item_Id,
                                new PurchasedItem_DAO().GetItem(item.PurchaseID).Name,
                                item.PurchaseDate,
                                new PurchasedItem_DAO().GetItem(item.PurchaseID).Price,
                                new PurchasedItem_DAO().GetItem(item.PurchaseID).Image_Path,
                                new PurchasedItem_DAO().GetItem(item.PurchaseID).PostedDate,
                                user.User_name,
                                user.Phone,
                                DeliveryAddress
                            }
                        );
                    }
                }
            }
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_pendingOrders != null && _pendingOrders.Count == 0)
            {
                MessageBox.Show("You have no items that need to be confirmed", "Information", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
        }
    }
}
