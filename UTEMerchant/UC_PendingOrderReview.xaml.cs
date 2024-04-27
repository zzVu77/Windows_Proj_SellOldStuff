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
    /// Interaction logic for UC_PendingOrderReview.xaml
    /// </summary>
    public partial class UC_PendingOrderReview : UserControl
    {
        public UC_PendingOrderReview()
        {
            InitializeComponent();
        }

        //public UC_PendingOrderReview(List<Order> pendingOrders) : this()
        //{
        //    foreach (Item order in pendingOrders)
        //    {
        //        productGrid.Items.Add(order);
        //    }
        //}

        private void ProductGrid_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BtnApprove_OnClick(object sender, RoutedEventArgs e)
        {
            // Get the clicked row in the data grid
            var row = (DataGridRow)productGrid.ItemContainerGenerator.ContainerFromItem(((Button)sender).DataContext);
            // Get the index of the clicked row
            var index = productGrid.ItemContainerGenerator.IndexFromContainer(row);
            // Remove the clicked row from the data grid
            productGrid.Items.RemoveAt(index);
            // Update the status of the clicked row in the database
            // ...
        }

        private void BtnDecline_OnClick(object sender, RoutedEventArgs e)
        {
            // Get the clicked row in the data grid
            var row = (DataGridRow)productGrid.ItemContainerGenerator.ContainerFromItem(((Button)sender).DataContext);
            // Get the index of the clicked row
            var index = productGrid.ItemContainerGenerator.IndexFromContainer(row);
            // Remove the clicked row from the data grid
            productGrid.Items.RemoveAt(index);
            // Remove the clicked row from the database
            // ...
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
    }
}
