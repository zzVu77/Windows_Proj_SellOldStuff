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

        List<purchasedItem> purchasedItems = new List<purchasedItem>();
        List<Seller> sellers = new List<Seller>();
        List<Item> items = new List<Item>();
        PurchasedItem_DAO dao = new PurchasedItem_DAO();
        Seller_DAO SellerDao = new Seller_DAO();
        Item_DAO item_DAO = new Item_DAO();
        private User _user;

        public UC_DeliveryUI()
        {
            InitializeComponent();
            rbDelivering.IsChecked = true;
        }

        public UC_DeliveryUI(User user) : this()
        {
            _user = user;
        }

        public void SetUser(User user)
        {
            _user = user;
            UserControl_Loaded(this, new RoutedEventArgs());
        }


        private void rbPending_Checked(object sender, RoutedEventArgs e)
        {
            this.grdDeliveredStatus.Visibility = Visibility.Collapsed;
            this.grdDeliveringStatus.Visibility = Visibility.Collapsed;
            grdPendingStatus.Visibility = Visibility.Visible;
            //grdCancelledStatus.Visibility = Visibility.Collapsed;
        }

        private void rbDelivering_Checked(object sender, RoutedEventArgs e)
        {
            grdDeliveredStatus.Visibility = Visibility.Collapsed;
            grdPendingStatus.Visibility = Visibility.Collapsed;
            grdDeliveringStatus.Visibility = Visibility.Visible;
            //grdCancelledStatus.Visibility = Visibility.Collapsed;
        }

        private void rbDelivered_Checked(object sender, RoutedEventArgs e)
        {
            grdPendingStatus.Visibility = Visibility.Collapsed;
            grdDeliveringStatus.Visibility = Visibility.Collapsed;
            grdDeliveredStatus.Visibility = Visibility.Visible;
            //grdCancelledStatus.Visibility = Visibility.Collapsed;
        }

        private void rbCancelled_Checked(object sender, RoutedEventArgs e)
        {
            grdDeliveredStatus.Visibility = Visibility.Collapsed;
            grdDeliveringStatus.Visibility = Visibility.Collapsed;
            grdPendingStatus.Visibility = Visibility.Collapsed;
            //grdCancelledStatus.Visibility = Visibility.Visible;
        }

        private void svDeliveryStatus_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }

        private void UCToReceiveItem_ReceivedButtonClicked(object sender, EventArgs e)
        {
            if (sender is UC_DeliveringItemsBox box)
            {
                UserControl_Loaded(this, new RoutedEventArgs());
            }

        }

        private void UCCompletedItem_RateButtonClicked(object sender, EventArgs e)
        {
            if (sender is UC_CompletedItem clickedItemView)
            {
                UserControl_Loaded(this, new RoutedEventArgs());
            }
        }

        private void PendingStatus_Load(object sender, RoutedEventArgs e)
        {
            // Filter items that are pending
            List<Item> matchedItems = dao.Load(_user.Id_user, "pending");

            // Filter items that are pending
            List<purchasedItem> purchasedItems = new PurchasedItem_DAO().LoadItemsByUser(_user.Id_user, "pending");

            // Group items by the date they were purchased
            IEnumerable<IGrouping<DateTime, Item>> groups = matchedItems.GroupBy(item =>
                purchasedItems.First(purchasedItem => purchasedItem.Item_Id == item.Item_Id).PurchaseDate);

            // Clear the list of items that are being delivered
            spPendingStatus.Children.Clear();

            // Add items to the list
            foreach (var group in groups)
            {
                AddItemsInPending(group);
            }

            rbPending.Content = $"Pending ({matchedItems.Count()})";
        }

        private void DeliveringStatus_Load(object sender, RoutedEventArgs e)
        {
            // Filter items that are being delivered
            var matchedItems = dao.Load(_user.Id_user, "delivering");

            // Clear the list of items that are being delivered
            spDeliveringStatus.Children.Clear();

            // Sort items that has the same seller
            IEnumerable<IGrouping<int, Item>> groupBySeller = matchedItems.GroupBy(item => item.SellerID);
            foreach (var group in groupBySeller)
            {
                AddItemsInDelivering(group);
            }

            rbDelivering.Content = $"Delivering ({matchedItems.Count()})";
        }

        private void DeliveredStatus_Load(object sender, RoutedEventArgs e)
        {
            // Filter items that are delivered
            List<Item> matchedItems = dao.Load(_user.Id_user, "delivered");

            // Sort the matchedItems list. Items that have been reviewed by the current user are placed at the beginning of the list.
            List<CustomerReview> ListCompare = new CustomerReviewDAO().Load();
            matchedItems = matchedItems.OrderBy(item =>
                ListCompare.Any(review => review.ID_User == this._user.Id_user && review.Item_ID == item.Item_Id)).ToList();

            // Clear the list of items that are delivered
            spDeliveredStatus.Children.Clear();

            foreach (var item in matchedItems)
            {
                UC_CompletedItem uc_item = new UC_CompletedItem(item, SellerDao.GetSeller(item.SellerID), this._user.Id_user);
                uc_item.RateButtonClicked += UCCompletedItem_RateButtonClicked;
                spDeliveredStatus.Children.Add(uc_item);
            }

            rbDelivered.Content = $"Delivered ({matchedItems.Count()})";

        }

        

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            PendingStatus_Load(sender, e);
            DeliveringStatus_Load(sender, e);
            DeliveredStatus_Load(sender, e);
        }

        public void Reload()
        {
            UserControl_Loaded(this, new RoutedEventArgs());
        }

        private void AddItemsInPending(IGrouping<DateTime, Item> items)
        {

            UC_PendingOrderBox ucOrderBox = new UC_PendingOrderBox(items, _user);

            int len = spPendingStatus.Children.Count;
            if (len == 0)
            {
                spPendingStatus.Children.Add(ucOrderBox);
                return;

            }

            ucOrderBox.Margin = new Thickness(0, 5, 0, 0);

            if (len == 1)
            {
                var first = (UC_PendingOrderBox)spPendingStatus.Children[0];
                first.Margin = new Thickness(0, 0, 0, 5);
            }
            else if (spPendingStatus.Children.Count > 1)
            {
                for (int i = 1; i < len; i++)
                {
                    var box = (UC_PendingOrderBox)spPendingStatus.Children[i];
                    box.Margin = new Thickness(0, 5, 0, 5);
                }
            }

            spPendingStatus.Children.Add(ucOrderBox);

        }
        
        private void AddItemsInDelivering(IGrouping<int, Item> group)
        {
            UC_DeliveringItemsBox ucItemBox =
                new UC_DeliveringItemsBox(group.ToList(), SellerDao.GetSeller(group.Key), this._user.Id_user);
            ucItemBox.ReceivedButtonClicked += UCToReceiveItem_ReceivedButtonClicked;

            int len = spDeliveringStatus.Children.Count;
            if (len == 0)
            {
                spDeliveringStatus.Children.Add(ucItemBox);
                return;

            }

            ucItemBox.Margin = new Thickness(0, 5, 0, 0);

            if (len == 1)
            {
                var first = (UC_DeliveringItemsBox)spDeliveringStatus.Children[0];
                first.Margin = new Thickness(0, 0, 0, 5);
            }
            else if (spDeliveringStatus.Children.Count > 1)
            {
                for (int i = 1; i < len; i++)
                {
                    var box = (UC_DeliveringItemsBox)spDeliveringStatus.Children[i];
                    box.Margin = new Thickness(0, 5, 0, 5);
                }
            }

            spDeliveringStatus.Children.Add(ucItemBox);
        }
    }
}
