using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTEMerchant
{
    public class user_DAO
    {
        private DB_Connection db = new DB_Connection();
        List<User> Users = new List<User>();
        public List<User> Load()
        {
            Users.Clear();
            using (SqlConnection conn = new SqlConnection(db.connectionString))
            {

                using (SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[User]", conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            bool isseller = reader["seller"].ToString() == "True";
                            Users.Add(new User(
                                Int32.Parse(reader["Id_user"].ToString()), reader["User_name"].ToString(), reader["Password"].ToString(),
                                reader["name"].ToString(), reader["address"].ToString(), reader["phone"].ToString(), reader["email"].ToString(),
                                isseller, reader["name_shop"].ToString(), reader["store_address"].ToString()
                                )
                            );
                        }
                    }

                }

                conn.Close();
            }

            return Users;
        }
    }
}
