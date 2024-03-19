using HandyControl.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
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
           

            using (SqlCommand command = new SqlCommand("select * from Item", conn))
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
            }

        }
    }
}
