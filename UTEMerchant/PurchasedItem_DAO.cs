    using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTEMerchant
{
    public class PurchasedItem_DAO
    {
        new DB_Connection db = new DB_Connection();
        public List<purchasedItem> Load() // More descriptive method name
        {
            return db.LoadData<purchasedItem>("SELECT * FROM [dbo].[PurchasedProducts]");
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
                (Id_user, Item_Id, Delivery_Status)
            VALUES
                (@userId, @itemId, @deliveryStatus)";
            new DB_Connection().ExecuteNonQuery(sqlQuery,
                new SqlParameter("@userId", item.Id_user),
                new SqlParameter("@itemId", item.Item_Id),
                new SqlParameter("@deliveryStatus", "delivering")
                );
        }
    }
}
