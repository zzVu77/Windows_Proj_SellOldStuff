using HandyControl.Themes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace UTEMerchant
{
    internal class CustomerReviewDAO
    {
        private UTEMerchantContext db = new UTEMerchantContext();

        public List<CustomerReview> Load()
        {
            return db.CustomerReviews.ToList();
        }

        public void AddReview(CustomerReview feedback)
        {
            db.CustomerReviews.Add(feedback);
            db.SaveChanges();
        }

        public List<CustomerReview> GetFeedback(int sellerID)
        {
            return db.CustomerReviews.Where(r => r.SellerID == sellerID).ToList();
        }

        public List<CustomerReview> FilterReview(int userId, int itemId)
        {
            return db.CustomerReviews.Where(r => r.Id_user == userId && r.Item_Id == itemId).ToList();
        }

        public double CalculateAverage(int sellerID)
        {
            var average = db.CustomerReviews.Where(r => r.SellerID == sellerID).Average(r => (double?)r.Rating) ?? 0;
            return average;
        }
    }
}
