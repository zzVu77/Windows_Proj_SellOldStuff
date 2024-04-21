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
        public List<User> GetUserByItemEmail(string Email)
        {
           string sqlStr = $"Select * From [dbo].[User] Where [dbo].[User].email={Email}";
           return db.LoadData<User>(sqlStr);
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
    }
}
