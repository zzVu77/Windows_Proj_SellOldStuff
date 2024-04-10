using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTEMerchant
{
    public class CustomerReview
    {
        public int ReviewID { get; set; }
        public int ID_User { get; set; }
        public int SellerID { get; set; }   
        public int Item_ID { get; set; }
        public double Rating { get; set; }
        public string ReviewText { get; set; }  
        public DateTime ReviewDate { get; set; }

        public CustomerReview() { }
        public CustomerReview( int iD_User, int sellerID, int item_ID, double rating, string reviewText, DateTime ReviewDate)
        {
            //ReviewID = reviewID;
            ID_User = iD_User;
            SellerID = sellerID;
            Item_ID = item_ID;
            Rating = rating;
            ReviewText = reviewText;
            this.ReviewDate=ReviewDate;
            //ReviewDate = reviewDate;
        }

    }
}
