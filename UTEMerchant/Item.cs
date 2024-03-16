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
        public decimal OriginalPrice { get; set; }
        public decimal Price { get; set; }
        public string ImagePath { get; set; }
        public DateTime PostedDate { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }

        public Item(int id, string name, string description, decimal originalPrice, decimal price, string imagePath, DateTime postedDate, string status,string type)
        {
            Id = id;
            Name = name;
            Description = description;
            OriginalPrice = originalPrice;
            Price = price;
            ImagePath = imagePath;
            PostedDate = postedDate;
            Status = status;
            Type = type;
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
