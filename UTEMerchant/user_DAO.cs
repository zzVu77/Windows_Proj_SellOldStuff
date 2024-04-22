using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTEMerchant
{
    public class user_DAO : DAO<User>
    {
        List<User> Users = new List<User>();
        public override List<User> Load()
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
        public void UpdateUser(User user)
        {
            string updateSql = @"
            UPDATE [dbo].[User]
            SET 
                name = @name,
                City = @city,
                District = @district,
                Ward = @ward,
                phone = @phone,
                email = @email,
                Image_path = @imagePath
            WHERE Id_user = @idUser";
            db.ExecuteNonQuery(updateSql,
                new SqlParameter("@name", user.Name),
                new SqlParameter("@city", user.City),
                new SqlParameter("@district", user.District),
                new SqlParameter("@ward", user.Ward),
                new SqlParameter("@phone", user.Phone),
                new SqlParameter("@email", user.Email),
                new SqlParameter("@imagePath", user.Image_Path),
                new SqlParameter("@idUser", user.Id_user)
                );

        }
    }
}
