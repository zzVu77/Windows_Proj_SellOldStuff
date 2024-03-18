using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace UTEMerchant
{
    internal class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float OriginalPrice { get; set; }
        public float Price { get; set; }
        public string ImagePath { get; set; }
        public DateTime Bought_date { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        
        public int user_id { get; set; }

        public Item(int id, string name, string description, float originalPrice, float price, string imagePath, DateTime bought_date, string status,string type, int user_id)
        {
            Id = id;
            Name = name;
            Description = description;
            OriginalPrice = originalPrice;
            Price = price;
            ImagePath = imagePath;
            Bought_date = bought_date;
            Status = status;
            Type = type;
            this.user_id = user_id;
        }

        public ImageSource Image
        {
            get
            {
                
                // Chuyển đổi đường dẫn thành ImageSource (BitmapImage)
                return new BitmapImage(new Uri(this.ImagePath));
            }
            
            
        }
    }
}
