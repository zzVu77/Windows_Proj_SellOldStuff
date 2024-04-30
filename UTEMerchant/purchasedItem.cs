using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTEMerchant
{
    public class purchasedItem
    {
        public int PurchasedID {  get; set; }
        public int Id_user { get; set; }
        public int Item_Id { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Delivery_address { get; set; }
        public purchasedItem() { }
        public purchasedItem(int purchasedID, int id_user,int item_id, DateTime purchaseDate, string name,string phone
            ,string email, string city,string district, string delivery_address)
        {
            PurchasedID = purchasedID;
            Id_user = id_user;
            Item_Id = item_id;
            PurchaseDate = purchaseDate;
            this.name = name;
            Phone = phone;
            Email = email;
            City = city;
            District = district;
            Delivery_address = delivery_address;
        }
    }
}
