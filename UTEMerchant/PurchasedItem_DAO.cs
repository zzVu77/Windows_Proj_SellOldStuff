    using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

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

        public List<purchasedItem> LoadOrdersByUser(int userId, string status)
        {
            return db.LoadData<purchasedItem>($"SELECT * FROM [dbo].[PurchasedProducts] WHERE Id_user = {userId} AND Delivery_Status = '{status}'");
        }

        public List<purchasedItem> LoadOrdersBySeller(int sellerId, string status)
        {
            return db.LoadData<purchasedItem>($@"
            SELECT pp.*
            FROM [dbo].[PurchasedProducts] pp
            JOIN [dbo].[Item] i ON pp.Item_Id = i.Item_Id
            WHERE i.SellerID = {sellerId} AND pp.Delivery_Status = '{status}'");
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

        public void RequestItems(List<Item> items, int userId, string deliveryAddress, string recipientName, string recipientPhone) // Using PascalCase for method name
        {
            //DateTime requestDate = DateTime.Now;
            //foreach (var item in items)
            //{
            //    string sqlQuery = @"
            //    INSERT INTO [dbo].[PurchasedProducts] (Id_user, Item_Id, Delivery_Status, PurchaseDate)
            //    VALUES (@userId, @itemId, @deliveryStatus, @requestDate)";
            //    new DB_Connection().ExecuteNonQuery(sqlQuery,
            //        new SqlParameter("@userId", userId),
            //        new SqlParameter("@itemId", item.Item_Id),
            //        new SqlParameter("@deliveryStatus", "pending"),
            //        new SqlParameter("@requestDate", requestDate)
            //    );
            //    new Item_DAO().UpdateStatus(item.Item_Id);
            //} 
            
            DateTime requestDate = DateTime.Now;
            foreach (var item in items)
            {
                string sqlQuery = @"
                INSERT INTO [dbo].[PurchasedProducts] (Id_user, Item_Id, Delivery_Status, PurchaseDate, Delivery_address, Name, Phone)
                VALUES (@userId, @itemId, @deliveryStatus, @requestDate, @deliveryAddress, @recipientName, @recipientPhone)";
                new DB_Connection().ExecuteNonQuery(sqlQuery,
                    new SqlParameter("@userId", userId),
                    new SqlParameter("@itemId", item.Item_Id),
                    new SqlParameter("@deliveryStatus", "pending"),
                    new SqlParameter("@requestDate", requestDate),
                    new SqlParameter("@deliveryAddress", deliveryAddress),
                    new SqlParameter("@recipientName", recipientName),
                    new SqlParameter("@recipientPhone", recipientPhone)
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

        public void UpdateDeliveryStatus(int purchaseId, string newStatus)
        {
            string sqlQuery = @"
            Update  [dbo].[PurchasedProducts] 
            SET Delivery_Status=@newStatus
            WHERE PurchaseID=@purchaseId
            ";
            new DB_Connection().ExecuteNonQuery(sqlQuery,
                new SqlParameter("@purchaseId", purchaseId),
                new SqlParameter("@newStatus", newStatus)
                );
        }

        public void CancelOrder(int purchaseId)
        {
            UpdateDeliveryStatus(purchaseId, "cancelled");

            string sqlQuery = @"UPDATE [dbo].[Item]
            SET sale_status = 0
            WHERE Item_Id = (SELECT Item_Id FROM [dbo].[PurchasedProducts] WHERE PurchaseID=@purchaseId)";
            new DB_Connection().ExecuteNonQuery(sqlQuery,
                new SqlParameter("@purchaseId", purchaseId)
                );
        }

        public Item GetItem(int purchaseId)
        {
            List<purchasedItem> item = db.LoadData<purchasedItem>(@" SELECT * FROM [dbo].[PurchasedProducts] WHERE PurchaseID = @purchaseId",
                new SqlParameter("@purchaseId", purchaseId)
            );
            List<Item> items = db.LoadData<Item>(@"
            SELECT DISTINCT i.*
            FROM [dbo].[Item] i
            JOIN [dbo].[PurchasedProducts] pp ON i.Item_Id = pp.Item_Id
            WHERE pp.[PurchaseID] = @purchaseId",
                new SqlParameter("@purchaseId", purchaseId)
            );
            return items[0];
        }

        public double CalculateTotalPrice(int sellerID)
        {
            double totalPrice = 0;
            string query = @"
        SELECT                     
            SUM(i.price) AS Total
        FROM 
            PurchasedProducts pp, Item i
        WHERE
             pp.Item_Id=i.Item_Id AND i.SellerID=@sellerID  AND pp.Delivery_Status != 'declined'";

            using (SqlConnection conn = new SqlConnection(db.connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@sellerID", sellerID);
                    object result = cmd.ExecuteScalar(); // Thực hiện ExecuteScalar
                    if (result != null && result != DBNull.Value)
                    {
                        totalPrice = Convert.ToDouble(result); // Chuyển đổi giá trị từ object sang double
                    }
                }
            }
            return totalPrice;
        }


        public int CalculateTotalSold(int sellerID)
        {
            int total = 0;
            string query = @"
                SELECT                     
                    COUNT(pp.Item_Id) AS Total
                FROM 
                    PurchasedProducts pp, Item i
                WHERE
                    pp.Item_Id=i.Item_Id AND i.SellerID=@sellerID AND pp.Delivery_Status != 'declined'";
            using (SqlConnection conn = new SqlConnection(db.connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@sellerID", sellerID);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        total = reader.GetInt32(0);
                    }
                    else
                    {
                        return 0;
                    }
                }
                return total;
            }
            
        }

        public User GetUser(int purchaseId)
        {
            List<User> users = db.LoadData<User>(@"
            SELECT DISTINCT u.*
            FROM [dbo].[User] u
            JOIN [dbo].[PurchasedProducts] pp ON u.Id_user = pp.Id_user
            WHERE pp.[PurchaseID] = @purchaseId",
                new SqlParameter("@purchaseId", purchaseId)
            );
            return users[0];
        }
    }
}
