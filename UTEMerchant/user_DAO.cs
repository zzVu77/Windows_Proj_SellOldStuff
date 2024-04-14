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
            return  db.LoadData<User>("SELECT * FROM [dbo].[User]");
        }
        public User GetUserByItemID(int id)
        {
            User user = null;
            string sqlStr = "Select Id_user, name, Image_path  From  [dbo].[User] Where @userID = [dbo].[User].Id_user ";
            SqlConnection conn = new SqlConnection(db.connectionString);
            conn.Open();
            SqlCommand command = new SqlCommand(sqlStr, conn);
            command.Parameters.AddWithValue("@userID", id);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                int Id_user = reader.GetInt32(0);
                string name = reader.GetString(1);                
                string imgPath = reader.GetString(2);
                return user = new User(Id_user, name, imgPath);
            }
            return user;
        }
    }
}
