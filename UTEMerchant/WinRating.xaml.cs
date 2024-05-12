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
using System.Windows.Shapes;

namespace UTEMerchant
{
    /// <summary>
    /// Interaction logic for WinRating.xaml
    /// </summary>
    public partial class WinRating : Window
    {
        private int Id_User;
        private int Seller_ID;
        private int Item_ID;
        CustomerReviewDAO customerReviewDAO = new CustomerReviewDAO();
        
        public WinRating()
        {
            InitializeComponent();
        }

        public WinRating(int id_User, int seller_ID, int item_ID):this()
        {
            this.Id_User = id_User;
            this.Seller_ID = seller_ID;
            this.Item_ID = item_ID;
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            string text_detail = new TextRange(rtbCommentFeedback.Document.ContentStart, rtbCommentFeedback.Document.ContentEnd).Text;
            DateTime currentDate = DateTime.Now;
            CustomerReview newCustomerReview = new CustomerReview
            {
                Id_user = this.Id_User,
                SellerID = this.Seller_ID,
                Item_Id = this.Item_ID,
                ReviewText = text_detail,
                ReviewDate = currentDate,
                Rating = float.Parse(ucStarRating.GetRating().ToString())
            };
            customerReviewDAO.AddReview(newCustomerReview);
            this.Close();

        }
    }
}
