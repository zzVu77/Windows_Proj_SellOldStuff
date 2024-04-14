using HandyControl.Controls;
using HandyControl.Themes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Xml;

namespace UTEMerchant
{
    public class Item_DAO: DAO<Item>
    {
    

        public override List<Item> Load() // More descriptive method name
        {
            return db.LoadData<Item>("SELECT * FROM [dbo].[Item]");
        }

        public override void Add(Item item) // Using PascalCase for method name
        {
            string sqlStr = "INSERT INTO [dbo].[Item] (Item_Id, name, price, original_price, type, bought_date, condition_description, condition, image_path, sale_status, detail_description, SellerID) " +
                            "VALUES (@ItemId, @Name, @Price, @OriginalPrice, @Type, @BoughtDate, @Condition_description, @Condition, @ImagePath, @Sale_status, @Detail_description, @SellerID)";

            db.ExecuteNonQuery(sqlStr,
                new SqlParameter("@ItemId", item.Item_Id),
                new SqlParameter("@Name", item.Name),
                new SqlParameter("@Price", item.Price),
                new SqlParameter("@OriginalPrice", item.Original_Price),
                new SqlParameter("@Type", item.Type),
                new SqlParameter("@BoughtDate", item.Bought_date),
                new SqlParameter("@Condition_description", item.Condition_Description),
                new SqlParameter("@Condition", item.Condition),
                new SqlParameter("@ImagePath", item.Image_Path),
                new SqlParameter("@Sale_status", item.Sale_Status),
                new SqlParameter("@Detail_description", item.Detail_description),
                new SqlParameter("@SellerID", item.SellerID));
        }

        public void RemoveItem(Item item)
        {
            string sqlStr = "DELETE FROM [dbo].[Item] WHERE Item_Id = @ItemId";
            db.ExecuteNonQuery(sqlStr, new SqlParameter("@ItemId", item.Item_Id));
        }
        public void UpdateStatus(int Item_Id)
        {
            bool status = true;
            string sqlStr = "UPDATE[dbo].[Item] SET sale_status = @NewSaleStatus WHERE Item_Id = @ItemId";
            db.ExecuteNonQuery(sqlStr, new SqlParameter("@ItemId", Item_Id), new SqlParameter("@NewSaleStatus", status));
        }

        public List<Item> GetItemsBySellerID(int SellerID)
        {
            return db.LoadData<Item>($"SELECT * FROM [dbo].[Item] WHERE SellerID = '{SellerID}'");
        }

        public Item GetItemByItemID(int id)
        {
            Item item = null;
            string sqlStr = "Select  Item_Id, name, price, image_path From  [dbo].[Item] Where @itemID = [dbo].[Item].Item_Id ";
            SqlConnection conn = new SqlConnection(db.connectionString);
            conn.Open();
            SqlCommand command = new SqlCommand(sqlStr, conn);
            command.Parameters.AddWithValue("@itemID", id);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                int itemID = reader.GetInt32(0);
                string name = reader.GetString(1);
                double price = reader.GetDouble(2);             
                string imgPath = reader.GetString(3);                
                return item = new Item(itemID,name,(float)price,imgPath);    
                
            }
            return item;
        }
        
    }
}
