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
        public decimal Price { get; set; }
        public string ImagePath { get; set; }
        public int Quantity { get; set; }
        public Item(int id, string name, string description, decimal price, string imagePath, int quantity)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            ImagePath = imagePath;
            Quantity = quantity;
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
