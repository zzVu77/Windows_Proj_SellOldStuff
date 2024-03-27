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
        SqlConnection conn = new
       SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\db_ute_merchant.mdf;Integrated Security=True");

        public List<Item> items = new List<Item>();
        public void Load()
        {

            items.Clear();
            using (SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Item]", conn))
            {
                conn.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                       
                        items.Add( new Item(
                            Int32.Parse(reader["Item_Id"].ToString()), reader["name"].ToString(), reader["description"].ToString(),
                            float.Parse(reader["original_price"].ToString()), float.Parse(reader["price"].ToString()), reader["image_path"].ToString()
                            ,Convert.ToDateTime(reader["bought_date"]), reader["status"].ToString(),
                            reader["type"].ToString(), Int32.Parse(reader["Id_user"].ToString())
                                )
                            );
                    }
                }
                conn.Close();
            }

        }
        public void add(Item item )
        {
            try
            {

                conn.Open();

                string sqlStr = "INSERT INTO [dbo].[Item] (Item_Id, name, price, original_price, type, bought_date, description, status, image_path, Id_user) " +
                                "VALUES (@ItemId, @Name, @Price, @OriginalPrice, @Type, @BoughtDate, @Description, @Status, @ImagePath, @UserId)";

                SqlCommand cmd = new SqlCommand(sqlStr, conn);
                cmd.Parameters.AddWithValue("@ItemId", item.Id);
                cmd.Parameters.AddWithValue("@Name", item.Name);
                cmd.Parameters.AddWithValue("@Price", item.Price);
                cmd.Parameters.AddWithValue("@OriginalPrice", item.OriginalPrice);
                cmd.Parameters.AddWithValue("@Type", item.Type);
                cmd.Parameters.AddWithValue("@BoughtDate", item.Bought_date);
                cmd.Parameters.AddWithValue("@Description", item.Description);
                cmd.Parameters.AddWithValue("@Status", item.Status);
                cmd.Parameters.AddWithValue("@ImagePath", item.ImagePath);
                cmd.Parameters.AddWithValue("@UserId", item.user_id);


                if (cmd.ExecuteNonQuery() > 0)
                {
                    //MessageBox.Show("Thêm thành công");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        public void remove(Item item)
        {
            try
            {
                conn.Open();
                string sqlStr = "DELETE FROM [dbo].[Item] WHERE Item_Id = @ItemId";
                SqlCommand cmd = new SqlCommand(sqlStr, conn);
                cmd.Parameters.AddWithValue("@ItemId", item.Id);
                cmd.ExecuteNonQuery();
            }
            catch(Exception ex) {
                //MessageBox.Show("that bai" + ex);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
