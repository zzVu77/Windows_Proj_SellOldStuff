using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTEMerchant
{
    public class User
    {
        public int Id_user { get; set; }
        public string Name { get; set; }
        public string User_name { get; set; }
        public string Password { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Ward { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

       

        public User() { }
        public User(int id, string user_name, string password, string name,
            string city,string district, string ward, string phone, string email)
        {
            Id_user = id;
            Name = name;
            User_name = user_name;
            Password = password;
            City = city;
            District = district;
            Ward = ward;
            Email = email;
            Phone = phone;

        }

    }
}
