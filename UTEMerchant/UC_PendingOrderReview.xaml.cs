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
           _pendingOrders = new PurchasedItem_DAO().Load("pending");
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
            var purchaseId = ((dynamic)productGrid.Items[index]).PurchasedID;
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
            var id = ((dynamic)productGrid.Items[index]).PurchasedID;
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
            
        }

        public void SetSeller (Seller seller)
        {
            _seller = seller;

            UserControl_Loaded(this, new RoutedEventArgs());
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (IsVisible) UserControl_Loaded(this, new RoutedEventArgs());
        }

        private void productGrid_Loaded(object sender, RoutedEventArgs e)
        {
            if (_seller != null && _pendingOrders != null)
            {
                productGrid.Items.Clear();
                List<User> users = new user_DAO().Load();
                List<Item> items = new Item_DAO().Load();

                int len = _pendingOrders.Count();
                // Create a new row for each pending order
                foreach (var item in _pendingOrders)
                {
                    string DeliveryAddress = item.Delivery_address;
                    productGrid.Items.Add
                    (new
                        {
                            item.PurchaseID,
                            item.Item_Id,
                            items.FirstOrDefault(i => i.Item_Id == item.Item_Id)?.Name,
                            item.PurchaseDate,
                            item.name,
                            items.FirstOrDefault(i => i.Item_Id == item.Item_Id)?.Price,
                            items.FirstOrDefault(i => i.Item_Id == item.Item_Id)?.Image_Path,
                            items.FirstOrDefault(i => i.Item_Id == item.Item_Id)?.PostedDate,
                            users.FirstOrDefault(user => user.Id_user == item.Id_user)?.User_name,
                            item.Phone,
                            DeliveryAddress
                        }
                    );
                }
            }
        }

        // create a method to select a bunch of rows when click on the checkbox

    }
}
