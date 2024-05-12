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
        private PurchasedProduct _order;
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


        public UC_CompletedItem(PurchasedProduct order, Seller seller, int userID) : this()
        {
            this._order = order;
            this.userID = userID;
            this.SellerOfItem = seller;
            Item item = new PurchasedItem_DAO().GetItem(order.PurchaseID);
            SetData(item, seller);
            this.userID = userID;
            List<CustomerReview> checkList = CustomerReview_DAO.FilterReview(this.userID, this._order.Item_Id);
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
            //imgToReceiveItem.Source = order.Image;
            //txblShopName.Text = user.name;
            //txblToReceiveOriginalPrice.Text = $"{order.Original_Price.ToString(CultureInfo.InvariantCulture)}$";
            //txblToReceivePrice.Text = $"{order.Price.ToString(CultureInfo.InvariantCulture)}$";
            //txblToReceiveConditon.Text = $"{order.Condition.ToString(CultureInfo.InvariantCulture)}%";
            //txblToReceiveItemName.Text = order.name;


            var resourceUri = new Uri(item.image_path, UriKind.RelativeOrAbsolute);
            imgToReceiveItem.Source = new BitmapImage(resourceUri);
            txblShopName.Text = SellerOfItem.ShopName;
            txblToReceiveOriginalPrice.Text = $"{item.original_price}$";
            txblToReceivePrice.Text = $"{item.price}$";
            txblToReceiveConditon.Text = $"{item.condition}%";
            txblToReceiveItemName.Text = item.name;
        }

        private void btnRate_Click(object sender, RoutedEventArgs e)
        {
            ReceivedButtonClicked?.Invoke(this, EventArgs.Empty);
           
            List<CustomerReview> checkList = CustomerReview_DAO.FilterReview(this.userID,this._order.Item_Id);
            if (checkList.Count == 0)
            {
                WinRating winRating = new WinRating(this.userID, this.SellerOfItem.SellerID, this._order.Item_Id);
                winRating.ShowDialog();
                
                if (CustomerReview_DAO.FilterReview(this.userID, this._order.Item_Id).Count() != 0 )
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
