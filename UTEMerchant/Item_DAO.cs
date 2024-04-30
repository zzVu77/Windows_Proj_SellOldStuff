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
            string sqlStr = "INSERT INTO [dbo].[Item] (name, price, original_price, type, bought_date, condition_description, condition, image_path, sale_status, detail_description, SellerID, PostedDate) " +
                            "VALUES (@Name, @Price, @OriginalPrice, @Type, @BoughtDate, @Condition_description, @Condition, @ImagePath, @Sale_status, @Detail_description, @SellerID, @PostedDate)";

            db.ExecuteNonQuery(sqlStr,
               
                new SqlParameter("@Name" +
                "" +
                "", item.Name),
                new SqlParameter("@Price", item.Price),
                new SqlParameter("@OriginalPrice", item.Original_Price),
                new SqlParameter("@Type", item.Type),
                new SqlParameter("@BoughtDate", item.Bought_date),
                new SqlParameter("@Condition_description", item.Condition_Description),
                new SqlParameter("@Condition", item.Condition),
                new SqlParameter("@ImagePath", item.Image_Path),
                new SqlParameter("@Sale_status", item.Sale_Status),
                new SqlParameter("@Detail_description", item.Detail_description),
                new SqlParameter("@SellerID", item.SellerID),
                new SqlParameter("@PostedDate", item.PostedDate));
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
            conn.Close();
            return item;
        }
        public List<Item> Search(string text)
        {
            string formattedText = text.ToUpper(); // Format the text to uppercase
            string query = @"
    SELECT *
    FROM [dbo].[Item]
    WHERE (UPPER(type) LIKE '%' + @Text + '%' OR 
          UPPER(name) LIKE '%' + @Text + '%' OR 
          UPPER(detail_description) LIKE '%' + @Text + '%' OR 
          UPPER(condition_description) LIKE '%' + @Text + '%') ";   // AND [sale_status] = 0
            return db.LoadData<Item>(query, new SqlParameter("@Text", formattedText));
        }
        public List<Item> SortPrice()
        {
            return db.LoadData<Item>(@"
            SELECT *
            FROM [dbo].[Item]
            ORDER BY [price] DESC");
        }
        public List<Item> SortRevelance(int userID)
        {
            return db.LoadData<Item>(@"SELECT i.* FROM [dbo].[Item] i 
            WHERE i.[type] IN (
                SELECT DISTINCT [type]
                FROM [dbo].[Item] it
                INNER JOIN [dbo].[PurchasedProducts] pp ON it.Item_Id = pp.Item_Id
                WHERE pp.[Id_user] = @UserId
            )", new SqlParameter("@UserId", userID));
        }

        public int GetTheMaximumItem_ID()
        {
            int maxValue = 0;
            using (SqlConnection connection = new SqlConnection(db.connectionString))
            {
                // Mở kết nối
                connection.Open();

                // Tạo câu lệnh SQL               
                string sqlString = $"SELECT MAX(Item_Id) FROM [dbo].[Item]";

                // Tạo đối tượng SqlCommand
                using (SqlCommand command = new SqlCommand(sqlString, connection))
                {
                    // Thực thi câu truy vấn và lấy giá trị trả về
                    object result = command.ExecuteScalar();

                    // Kiểm tra và ép kiểu kết quả về kiểu int
                    if (result != DBNull.Value)
                    {
                        maxValue = Convert.ToInt32(result);
                    }
                }
            }
            return maxValue;
        }

        public void UpdateItem(Item item)
        {
            string updateSql = @"
            UPDATE [dbo].[Item]
            SET 
                name = @Name,
                price = @price,
                original_price = @original_price,
                type = @type,
                bought_date = @bought_date,
                condition_description = @condition_description,
                condition = @condition,
                image_path = @mainImgPath,
                detail_description = @detail_description
            WHERE Item_Id = @Item_Id";
            db.ExecuteNonQuery(updateSql,

               new SqlParameter("@Name" , item.Name),
               new SqlParameter("@price", item.Price),
               new SqlParameter("@original_price", item.Original_Price),
               new SqlParameter("@type", item.Type),
               new SqlParameter("@bought_date", item.Bought_date),
               new SqlParameter("@condition_description", item.Condition_Description),
               new SqlParameter("@condition", item.Condition),
               new SqlParameter("@mainImgPath", item.Image_Path),
               new SqlParameter("@Item_Id", item.Item_Id),
               new SqlParameter("@detail_description", item.Detail_description));
        }


        
    }
}
