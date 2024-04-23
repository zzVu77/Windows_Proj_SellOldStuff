using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace UTEMerchant
{
    public class User: Person
    {
        public int Id_user { get; set; }
        public string Name { get; set; }
        public string User_name { get; set; }
        public string Password { get; set; }
        
        public string Email { get; set; }

        public string Image_Path { get; set; }


        public User() { }
        public User( string user_name, string password, string name,
            string city,string district, string ward, string phone, string email,string imgPath)
        {
            
            Name = name;
            User_name = user_name;
            Password = password;
            City = city;
            District = district;
            Ward = ward;
            Email = email;
            Phone = phone;
            Image_Path = imgPath;

        }

        public User(int id, string name, string imgPath)
        {
            Id_user = id;
            Name = name;
            Image_Path = imgPath;
        }
        public User(string user_name, string password,string email)
        {
            User_name = user_name;
            Password = password;
            Email = email;
        }
        public ImageSource Image
        {
            get
            {
                // Chuyển đổi đường dẫn thành ImageSource (BitmapImage)
                return new BitmapImage(new Uri(this.Image_Path));
            }

        }
    }
}
