using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTEMerchant
{
    internal class CustomerReviewDAO
    {
        private DB_Connection db = new DB_Connection();
        List<CustomerReview> Users = new List<CustomerReview>();
        public List<CustomerReview> Load()
        {
            return db.LoadData<CustomerReview>("SELECT * FROM [dbo].[CustomerReviews]");
        }

        public void AddReview(CustomerReview feedBack) // Using PascalCase for method name
        {
            string sqlQuery = @"
            INSERT INTO [dbo].[CustomerReviews] 
                (Id_user, Item_Id, SellerID,ReviewText,ReviewDate,Rating)
            VALUES
                (@userId, @itemId, @sellerID, @ReviewText, @ReviewDate, @rating)";
            new DB_Connection().ExecuteNonQuery(sqlQuery,
                new SqlParameter("@userId", feedBack.ID_User),
                new SqlParameter("@itemId", feedBack.Item_ID),
                new SqlParameter("@sellerID", feedBack.SellerID),
                new SqlParameter("@ReviewText", feedBack.ReviewText),
                new SqlParameter("@ReviewDate", feedBack.ReviewDate),
                new SqlParameter("@rating", feedBack.Rating)
                );
        }

        public List<CustomerReview> GetFeedBack( int sellerID)
        {
             List < CustomerReview >  list=db.LoadData<CustomerReview>($"SELECT * FROM [dbo].[CustomerReviews] WHERE SellerID={sellerID}");
            return list;
        }
    }
}
