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
        public List<Item> Load(int Id_user) 
        {
            
    
            List<Item> purchasedItems = db.LoadData<Item>($"SELECT i.* FROM [dbo].[Item] i INNER JOIN [dbo].[PurchasedProducts] pi ON pi.Item_Id = i.Item_Id WHERE pi.Id_user = {Id_user}");
            /*purchasedItems = purchasedItems.Where(item => item.Id_user == Id_user).ToList();
            List<Item> matchedItems =
                (from purchasedItem in purchasedItems
                 join item in items on purchasedItem.Item_Id equals item.Item_Id
                 select item).ToList();*/
            return purchasedItems;
        }
        public void AddItem(purchasedItem item) // Using PascalCase for method name
        {
            string sqlQuery = @"
            INSERT INTO [dbo].[PurchasedProducts] 
                (Id_user, Item_Id)
            VALUES
                (@userId, @itemId)";
            new DB_Connection().ExecuteNonQuery(sqlQuery,
                new SqlParameter("@userId", item.Id_user),
                new SqlParameter("@itemId", item.Item_Id)
                );
        }
    }
}
