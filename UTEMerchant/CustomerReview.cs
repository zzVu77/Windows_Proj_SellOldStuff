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
        public string ReviewText { get; set; }  
        public DateTime ReviewDate { get; set; }
        public float RatePoint { get; set; }

        public CustomerReview() { }
        public CustomerReview( int iD_User, int sellerID, int item_ID, string reviewText, DateTime ReviewDate, float ratePoint)
        {
           
            ID_User = iD_User;
            SellerID = sellerID;
            Item_ID = item_ID;
            ReviewText = reviewText;
            this.ReviewDate = ReviewDate;
            this.RatePoint = ratePoint;
           
        }

    }
}
