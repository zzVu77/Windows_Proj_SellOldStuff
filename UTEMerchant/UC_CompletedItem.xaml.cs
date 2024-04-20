using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for UC_CompletedItem.xaml
    /// </summary>
    public partial class UC_CompletedItem : UserControl
    {
        private  Item Item;
        private  User User;
        private  Seller SellerOfItem;
        private int userID;
        public event EventHandler ReceivedButtonClicked;
        private CustomerReviewDAO CustomerReview_DAO = new CustomerReviewDAO();
        public event EventHandler RateButtonClicked;
        public UC_CompletedItem() 
        {
            InitializeComponent();
            this.Width = 1300;
        }


        public UC_CompletedItem(Item item, Seller seller, int userID) : this()
        {
            this.Item = item;
            this.userID = userID;
            this.SellerOfItem = seller;
            SetData(item, seller);
            this.userID = userID;
            List<CustomerReview> checkList = CustomerReview_DAO.filterReview(this.userID, this.Item.Item_Id);
            if (checkList.Count == 0)
            {
                tbRate.Text = "Rate";
                btnRate.IsEnabled = true;
            }
            else
            {
                var bc = new BrushConverter();
                tbRate.Text = "Rated";
                tbRate.Foreground=Brushes.Green;
                btnRate.Background = Brushes.LightGray;
                btnRate.IsEnabled = false;
                iconRate.Kind = MahApps.Metro.IconPacks.PackIconMaterialKind.CheckBold;
                iconRate.Foreground = Brushes.Green;
            }

        }

        //Binding data to the UI
        private void SetData(Item item, Seller seller)
        {
            //imgToReceiveItem.Source = item.Image;
            //txblShopName.Text = user.Name;
            //txblToReceiveOriginalPrice.Text = $"{item.Original_Price.ToString(CultureInfo.InvariantCulture)}$";
            //txblToReceivePrice.Text = $"{item.Price.ToString(CultureInfo.InvariantCulture)}$";
            //txblToReceiveConditon.Text = $"{item.Condition.ToString(CultureInfo.InvariantCulture)}%";
            //txblToReceiveItemName.Text = item.Name;


            var resourceUri = new Uri(item.Image_Path, UriKind.RelativeOrAbsolute);
            imgToReceiveItem.Source = new BitmapImage(resourceUri);
            txblShopName.Text = SellerOfItem.ShopName;
            txblToReceiveOriginalPrice.Text = $"{Item.Original_Price.ToString(CultureInfo.InvariantCulture)}$";
            txblToReceivePrice.Text = $"{Item.Price.ToString(CultureInfo.InvariantCulture)}$";
            txblToReceiveConditon.Text = $"{Item.Condition.ToString(CultureInfo.InvariantCulture)}%";
            txblToReceiveItemName.Text = Item.Name;
        }

        private void btnRate_Click(object sender, RoutedEventArgs e)
        {
            ReceivedButtonClicked?.Invoke(this, EventArgs.Empty);
           
            List<CustomerReview> checkList = CustomerReview_DAO.filterReview(this.userID,this.Item.Item_Id);
            if (checkList.Count == 0)
            {
                WinRating winRating = new WinRating(this.userID, this.SellerOfItem.SellerID, this.Item.Item_Id);
                winRating.ShowDialog();
                
                if (CustomerReview_DAO.filterReview(this.userID, this.Item.Item_Id).Count() != 0 )
                {
                    tbRate.Text = "Rated";
                    btnRate.IsEnabled = false;
                    RateButtonClicked?.Invoke(this, EventArgs.Empty);
                }
            }
            else
            {
                //kiem tra ngoai le
                tbRate.Text = "Rated";
                btnRate.IsEnabled = false;
            }
           
        }
    }
}
