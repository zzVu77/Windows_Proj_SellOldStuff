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
            rbDelivering.IsChecked = true;
            

        }
        public void Load()
        {
            
            var matchedItems = dao.Load(Id_user, "delivering");         
            spDeliveringStatus.Children.Clear();
            foreach (var item in matchedItems)
            {                
                UC_DeliveringItemsBox uc_item = new UC_DeliveringItemsBox(item, SellerDao.GetSeller(item.SellerID),this.Id_user);
                uc_item.ReceivedButtonClicked += UCToReceiveItem_ReceivedButtonClicked;
                spDeliveringStatus.Children.Add(uc_item);
            }
            rbDelivering.Content = $"Delivering ({matchedItems.Count()})";
            

            matchedItems = dao.Load(Id_user, "delivered");
            //sort item
            List<CustomerReview> ListCompare = new CustomerReviewDAO().Load();
            matchedItems.Sort((item1, item2) =>
            {
                bool hasReview1 = ListCompare.Any(review => review.ID_User == this.Id_user && review.Item_ID == item1.Item_Id);
                bool hasReview2 = ListCompare.Any(review => review.ID_User == this.Id_user && review.Item_ID == item2.Item_Id);

                return hasReview1.CompareTo(hasReview2);
            });

            spDeliveredStatus.Children.Clear();
            foreach (var item in matchedItems)
            {
                UC_CompletedItem uc_item = new UC_CompletedItem(item, SellerDao.GetSeller(item.SellerID), this.Id_user);
                uc_item.RateButtonClicked += UCCompletedItem_RateButtonClicked;
                spDeliveredStatus.Children.Add(uc_item);
            }
            rbDelivered.Content = $"Delivered ({matchedItems.Count()})";
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
        private void UCToReceiveItem_ReceivedButtonClicked(object sender, EventArgs e)
        {
            if (sender is UC_DeliveringItemsBox clickedItemView)
            {
                Load();
            }
            
        }

        private void UCCompletedItem_RateButtonClicked(object sender, EventArgs e)
        {
            if (sender is UC_CompletedItem clickedItemView)
            {
                Load();
            }

        }

    }
}
