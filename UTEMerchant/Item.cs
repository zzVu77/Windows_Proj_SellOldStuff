using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace UTEMerchant
{
    public class Item
    {
        public int Item_Id { get; set; }
        public string Name { get; set; }
        public string Condition_Description { get; set; }
        public int Condition { get; set; }
        public string Detail_description { get; set; }
        public float Original_Price { get; set; }
        public float Price { get; set; }
        public string Image_Path { get; set; }
        public DateTime Bought_date { get; set; }
        public bool Sale_Status { get; set; }
        public string Type { get; set; }

        public int SellerID { get; set; }
        public DateTime PostedDate { get; set; }

        public Item()
        {

        }
        public Item(int id, string name, float price, float originalPrice, string type, DateTime bought_date, 
            string condition_description, int condition, string imagePath, bool sale_status ,
            string detail_description, int sellerId,DateTime postedID)
        {
            Item_Id = id;
            Name = name;
            Condition_Description = condition_description;
            Original_Price = originalPrice;
            Price = price;
            Image_Path = imagePath;
            Bought_date = bought_date;
            Sale_Status = sale_status;
            Type = type;
            Detail_description = detail_description;
            SellerID = sellerId;
            Condition = condition;
            PostedDate = postedID;
        }

        public Item(int itemID, string name, float price, string imagePath)
        {
            Item_Id = itemID;
            Name = name;
            Price = price;
            Image_Path = imagePath;
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
