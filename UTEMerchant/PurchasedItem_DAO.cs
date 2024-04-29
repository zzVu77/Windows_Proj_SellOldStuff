    using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTEMerchant
{
    public class PurchasedItem_DAO : DAO<purchasedItem>
    {
        
        public override List<purchasedItem> Load() // More descriptive method name
        {
            return db.LoadData<purchasedItem>("SELECT * FROM [dbo].[PurchasedProducts]");
        }

        public List<purchasedItem> Load(string status)
        {
            return db.LoadData<purchasedItem>($"SELECT * FROM [dbo].[PurchasedProducts] WHERE Delivery_Status = '{status}'");
        }

        public List<purchasedItem> LoadItemsByUser(int userId, string status)
        {
            return db.LoadData<purchasedItem>($"SELECT * FROM [dbo].[PurchasedProducts] WHERE Id_user = {userId} AND Delivery_Status = '{status}'");
        }

        public List<Item> Load(int Id_user, string deliverStatus)       
        {
            List<Item> purchasedItems = db.LoadData<Item>($"SELECT i.* FROM [dbo].[Item] i INNER JOIN [dbo].[PurchasedProducts] pi ON pi.Item_Id = i.Item_Id WHERE pi.Id_user = {Id_user} AND pi.Delivery_Status='{deliverStatus}'");
            return purchasedItems;
        }

        public void AddItem(purchasedItem item) // Using PascalCase for method name
        {
            string sqlQuery = @"
            INSERT INTO [dbo].[PurchasedProducts] 
                (Id_user, Item_Id, Delivery_Status, PurchaseDate)
            VALUES
                (@userId, @itemId, @deliveryStatus, dbo.GETDATE())";
            new DB_Connection().ExecuteNonQuery(sqlQuery,
                new SqlParameter("@userId", item.Id_user),
                new SqlParameter("@itemId", item.Item_Id),
                new SqlParameter("@deliveryStatus", "pending")
                );
        }

        public void RequestItems(List<Item> items, int userId, string deliveryAddress) // Using PascalCase for method name
        {
            DateTime requestDate = DateTime.Now;
            foreach (var item in items)
            {
                string sqlQuery = @"
                INSERT INTO [dbo].[PurchasedProducts] (Id_user, Item_Id, Delivery_Status, PurchaseDate)
                VALUES (@userId, @itemId, @deliveryStatus, @requestDate)";
                new DB_Connection().ExecuteNonQuery(sqlQuery,
                    new SqlParameter("@userId", userId),
                    new SqlParameter("@itemId", item.Item_Id),
                    new SqlParameter("@deliveryStatus", "pending"),
                    new SqlParameter("@requestDate", requestDate)
                );
                new Item_DAO().UpdateStatus(item.Item_Id);
            }

        }



        //public string GetPurchasedProductStatus(int itemID, int userID)
        //{
        //    //string sqlStr = @"select Delivery_Status from PurchasedProducts pp, Item i Where pp.Id_user=@userID and pp.Item_Id = i.Item_Id ";
        //    string status = "";

        //    using (SqlConnection connection = new SqlConnection(db.connectionString))
        //    {

        //        string sqlStr = @"select Delivery_Status from PurchasedProducts pp, Item i Where pp.Id_user=@userID and pp.Item_Id = i.Item_Id ";

        //        connection.Open();


        //        using (SqlCommand command = new SqlCommand(sqlStr, connection))
        //        {

        //            command.Parameters.AddWithValue("@userID", userID);
        //            command.Parameters.AddWithValue("@itemID", itemID);

        //            using (SqlDataReader reader = command.ExecuteReader())
        //            {
        //                if (reader.Read())
        //                {
        //                    status = reader["Delivery_Status"].ToString();
        //                }
        //            }
        //        }
        //    }
        //    return status;
        //}

        public void UpdateDeliveryStatus(int itemID, int userID, string newStatus)
        {
            string sqlQuery = @"
            Update  [dbo].[PurchasedProducts] 
            SET Delivery_Status=@newStatus
            WHERE Id_user=@userID AND Item_Id=@itemID
            ";
            new DB_Connection().ExecuteNonQuery(sqlQuery,
                new SqlParameter("@userId", userID),
                new SqlParameter("@itemId", itemID),
                new SqlParameter("@newStatus", newStatus)
                );
        }
    }
}
