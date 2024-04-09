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
    /// Interaction logic for UC_DeliveryUI.xaml
    /// </summary>
    public partial class UC_DeliveryUI : UserControl
    {

        List <purchasedItem> purchasedItems = new List<purchasedItem> ();
        List <Seller> sellers = new List<Seller> ();
        List <Item> items = new List<Item> ();
        PurchasedItem_DAO dao = new PurchasedItem_DAO();
        Seller_DAO SellerDao = new Seller_DAO();
        Item_DAO item_DAO = new Item_DAO();
        public int Id_user;
        public UC_DeliveryUI()
        {
            InitializeComponent();
            rbPending.IsChecked = true;

        }
        public void Load()
        {
            
            var matchedItems = dao.Load(Id_user, "delivering");         
            spDeliveringStatus.Children.Clear();
            foreach (var item in matchedItems)
            {
                
                UC_ToReceiveItem uc_item = new UC_ToReceiveItem(item, SellerDao.GetSeller(item.SellerID));
                spDeliveringStatus.Children.Add(uc_item);
            }
            
        }

        private void rbPending_Checked(object sender, RoutedEventArgs e)
        {
            this.grdDeliveredStatus.Visibility = Visibility.Collapsed;
            this.grdDeliveringStatus.Visibility = Visibility.Collapsed;
            //grdPendingStatus.Visibility = Visibility.Visible;
            //grdCancelledStatus.Visibility = Visibility.Collapsed;
        }

        private void rbDelivering_Checked(object sender, RoutedEventArgs e)
        {
            grdDeliveredStatus.Visibility = Visibility.Collapsed;
            //grdPendingStatus.Visibility = Visibility.Collapsed;
            grdDeliveringStatus.Visibility = Visibility.Visible;
            //grdCancelledStatus.Visibility = Visibility.Collapsed;
        }

        private void rbDelivered_Checked(object sender, RoutedEventArgs e)
        {
            //grdPendingStatus.Visibility = Visibility.Collapsed;
            grdDeliveringStatus.Visibility = Visibility.Collapsed;
            grdDeliveredStatus.Visibility = Visibility.Visible;
            //grdCancelledStatus.Visibility = Visibility.Collapsed;
        }

        private void rbCancelled_Checked(object sender, RoutedEventArgs e)
        {
            grdDeliveredStatus.Visibility = Visibility.Collapsed;
            grdDeliveringStatus.Visibility = Visibility.Collapsed;
            //grdPendingStatus.Visibility = Visibility.Collapsed;
            //grdCancelledStatus.Visibility = Visibility.Visible;
        }

        private void svDeliveryStatus_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }
    }
}
