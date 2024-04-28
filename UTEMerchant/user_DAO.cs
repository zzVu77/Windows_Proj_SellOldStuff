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
        public void UpdateUserThroughSeller (User user)
        {
            string updateSellerSql = @"
                        UPDATE [dbo].[User]
                        SET email = @email,
                            Image_path = @imagePath
                        WHERE Id_user = @Id_user
                    ";
            db.ExecuteNonQuery(updateSellerSql,
                new SqlParameter("@email", user.Email),
                new SqlParameter("@imagePath", user.Image_Path),
                new SqlParameter("@Id_user", user.Id_user));

        }
        public User GetUserByItemEmail(string Email)
        {
            User user = null;
            string sqlStr = "Select User_name, Password, email  From  [dbo].[User] Where @email = [dbo].[User].email ";
            SqlConnection conn = new SqlConnection(db.connectionString);
            conn.Open();
            SqlCommand command = new SqlCommand(sqlStr, conn);
            command.Parameters.AddWithValue("@email", Email);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                user = new User(reader.GetString(0),reader.GetString(1), reader.GetString(2));
            }
            return user;
        }
        public override void Add(User user) // Using PascalCase for method name
        {
            string sqlStr = "INSERT INTO [dbo].[User] (User_name, Password, name, City, District, Ward, phone, email, Image_path) " +
                            "VALUES (@UserName, @Password, @name, @City, @District, @Ward, @phone, @email, @Image_path)";

            db.ExecuteNonQuery(sqlStr,
                new SqlParameter("@UserName", user.User_name),
                new SqlParameter("@Password", user.Password),
                new SqlParameter("@name", user.Name),
                new SqlParameter("@City", user.City),
                new SqlParameter("@District", user.District),
                new SqlParameter("@Ward", user.Ward),
                new SqlParameter("@phone", user.Phone),
                new SqlParameter("@email", user.Email),
                new SqlParameter("@Image_path", (object)user.Image_Path ?? DBNull.Value)
                );
        }

        public User GetUserByID (int id)
        {
            return db.LoadData<User>($"SELECT * FROM [dbo].[User] WHERE Id_user = {id}").FirstOrDefault();
        }
        public void updateUser(string newpassword, string email)
        {
            string sqlStr = "UPDATE [dbo].[User] SET Password = @NewPassword WHERE Email = @GmailAddress";
            db.ExecuteNonQuery(sqlStr,
                new SqlParameter("@NewPassword", newpassword),
                new SqlParameter("@GmailAddress", email));

        }

        public User GetUserByUserName(string username)
        {
            return db.LoadData<User>($"SELECT * FROM [dbo].[User] WHERE User_name = '{username}'").FirstOrDefault();
        }
    }
}
