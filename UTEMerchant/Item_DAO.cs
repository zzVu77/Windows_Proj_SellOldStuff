using HandyControl.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace UTEMerchant
{
    public class Item_DAO
    {
        private DB_Connection db = new DB_Connection();

        List<Item> items = new List<Item>();
        public List<Item> Load() // More descriptive method name
        {
            items.Clear();
            using (SqlConnection conn = new SqlConnection(db.connectionString))
            {

                using (SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Item]", conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            items.Add(new Item(
                                Int32.Parse(reader["Item_Id"].ToString()), reader["name"].ToString(), reader["description"].ToString(),
                                float.Parse(reader["original_price"].ToString()), float.Parse(reader["price"].ToString()), reader["image_path"].ToString()
                                , Convert.ToDateTime(reader["bought_date"]), reader["status"].ToString(),
                                reader["type"].ToString(), Int32.Parse(reader["Id_user"].ToString())
                                    )
                                );
                        }
                    }

                }

                conn.Close();
            }

            return items;
        }

        public void AddItem(Item item) // Using PascalCase for method name
        {
            string sqlStr = "INSERT INTO [dbo].[Item] (Item_Id, name, price, original_price, type, bought_date, description, status, image_path, Id_user) " +
                            "VALUES (@ItemId, @Name, @Price, @OriginalPrice, @Type, @BoughtDate, @Description, @Status, @ImagePath, @UserId)";

            db.ExecuteNonQuery(sqlStr,
                new SqlParameter("@ItemId", item.Id),
                new SqlParameter("@Name", item.Name),
                new SqlParameter("@Price", item.Price),
                new SqlParameter("@OriginalPrice", item.OriginalPrice),
                new SqlParameter("@Type", item.Type),
                new SqlParameter("@BoughtDate", item.Bought_date),
                new SqlParameter("@Description", item.Description),
                new SqlParameter("@Status", item.Status),
                new SqlParameter("@ImagePath", item.ImagePath),
                new SqlParameter("@UserId", item.user_id));
        }

        public void RemoveItem(Item item)
        {
            string sqlStr = "DELETE FROM [dbo].[Item] WHERE Item_Id = @ItemId";
            db.ExecuteNonQuery(sqlStr, new SqlParameter("@ItemId", item.Id));
        }
    }
}
