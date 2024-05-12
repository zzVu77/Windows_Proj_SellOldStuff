using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTEMerchant
{
    public class User_DAO : DAO<User>
    {
        public override List<User> Load()
        {
            return db.Users.ToList();
        }

        public User GetUserByItemID(int id)
        {
            var userProjection = db.Users
                             .Where(u => u.Id_user == id)
                             .Select(u => new
                             {
                                 u.Id_user,
                                 u.name,
                                 u.Image_path
                             })
                             .FirstOrDefault();

            if (userProjection != null)
            {
                return new User
                {
                    Id_user = userProjection.Id_user,
                    name = userProjection.name,
                    Image_path = userProjection.Image_path
                };
            }

            return null;
        }

        public void UpdateUser(User user)
        {
            var existingUser = db.Users.Find(user.Id_user);
            if (existingUser != null)
            {
                existingUser.name = user.name;
                existingUser.City = user.City;
                existingUser.District = user.District;
                existingUser.Ward = user.Ward;
                existingUser.phone = user.phone;
                existingUser.email = user.email;
                existingUser.Image_path = user.Image_path;
                db.SaveChanges();
            }
        }

        public void UpdateUserThroughSeller(User user)
        {
            var existingUser = db.Users.Find(user.Id_user);
            if (existingUser != null)
            {
                existingUser.email = user.email;
                existingUser.Image_path = user.Image_path;
                db.SaveChanges();
            }
        }

        public User GetUserByItemEmail(string Email)
        {
            return db.Users.FirstOrDefault(u => u.email == Email);
        }

        public override void Add(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
        }

        public User GetUserByID(int id)
        {
            return db.Users.FirstOrDefault(u => u.Id_user == id);
        }

        public void updateUser(string newpassword, string email)
        {
            var existingUser = db.Users.FirstOrDefault(u => u.email == email);
            if (existingUser != null)
            {
                existingUser.Password = newpassword;
                db.SaveChanges();
            }
        }

        public User GetUserByUserName(string username)
        {
            return db.Users.FirstOrDefault(u => u.User_name == username);
        }
    }
}
