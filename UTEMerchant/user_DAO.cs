using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTEMerchant
{
    internal class user_DAO
    {
        SqlConnection conn = new
       SqlConnection(@"Data Source=(localdb)\mssqllocaldb;Initial Catalog=DB_Merchant;Integrated Security=True");

        List<User> Users = new List<User>();
        public void Load()
        {


            using (SqlCommand command = new SqlCommand("select * from User", conn))
            {
                conn.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Users.Add(new User(
                            Int32.Parse(reader["Id_user"].ToString()), reader["User_name"].ToString(), reader["Password"].ToString(),
                            reader["name"].ToString(), reader["address"].ToString(), reader["phone"].ToString(), reader["email"].ToString(),
                            Int32.Parse(reader["seller"].ToString()) ==1, reader["name_shop"].ToString(), reader["store_addesss"].ToString()
                                )
                            );
                    }
                }
            }

        }
    }
}
