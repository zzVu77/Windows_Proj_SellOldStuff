using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTEMerchant
{
    public class User
    {
        public int Id_user {  get; set; }
        public string Name { get; set; }
        public string User_name {get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        
        public bool IsSeller { get; set; }
        public string Name_shop { get; set; }
        public string Store_address { get; set; }
       
        public User(int id, string user_name, string password, string name, 
            string address, string phone, string email, bool isSeller, string name_shop, string store_address) 
        { 
            Id_user = id;
            Name = name;
            User_name = user_name;
            Password = password;
            Address = address;
            Email = email;
            Phone = phone;
            IsSeller = isSeller;
            Name_shop = name_shop;
            Store_address = store_address;
        }
    }
}
