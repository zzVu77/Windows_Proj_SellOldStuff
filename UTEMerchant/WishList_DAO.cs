using HandyControl.Controls;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTEMerchant
{
    internal class WishList_DAO : DAO<WishList>
    {
        public override List<WishList> Load() // More descriptive method name
        {
            return db.LoadData<WishList>("SELECT * FROM [dbo].[WishList]");
        }

        public List<WishList> GetItemsByUserID(int userID)
        {
            return db.LoadData<WishList>($"SELECT * FROM [dbo].[WishList] WHERE Id_user = '{userID}'");
        }

        public override void Add(WishList item) // Using PascalCase for method name
        {
            string sqlStr = "INSERT INTO [dbo].[Wishlist] (Id_user, Item_Id, AddedDate) " +
                            "VALUES (@Id_user, @Item_Id, @AddedDate)";

            db.ExecuteNonQuery(sqlStr,

                new SqlParameter("@Id_user", item.Id_user),
                new SqlParameter("@Item_Id", item.Item_Id),
                new SqlParameter("@AddedDate", item.AddedDate));
        }

        public bool CheckAddedItemInWishList(int itemID, int userID)
        {
            string sqlStr = "SELECT * FROM [dbo].[WishList] WHERE Id_user = @userID AND Item_Id = @itemID";
            using (SqlConnection conn = new SqlConnection(db.connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
                {
                    cmd.Parameters.AddWithValue("@userID", userID);
                    cmd.Parameters.AddWithValue("@itemID", itemID);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {                      
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                
            }
        }

        public void RemoveItem(int itemID, int userID)
        {
            string sqlStr = "DELETE FROM [dbo].[WishList] WHERE Id_user = @userID AND Item_Id = @itemID";
            db.ExecuteNonQuery(sqlStr, new SqlParameter("@userID", userID),
                                       new SqlParameter("@itemID", itemID));
        }





    }
}
