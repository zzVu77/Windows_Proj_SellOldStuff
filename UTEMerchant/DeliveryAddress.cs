using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTEMerchant
{
    public class DeliveryAddress
    {
        public int ID { get; set; }
        public string RecipientName { get; set; }
        public string RecipientPhone { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Ward { get; set; }
        public int UserID { get; set; }
        public bool DefaultAddress { get; set; }

        public DeliveryAddress()
        {

        }

        public DeliveryAddress(int id, string recipientName, string recipientPhone, string streetAddress, string city, string district, string ward, int userID, bool defaultAddress)
        {
            ID = id;
            RecipientName = recipientName;
            RecipientPhone = recipientPhone;
            StreetAddress = streetAddress;
            City = city;
            District = district;
            Ward = ward;
            UserID = userID;
            DefaultAddress = defaultAddress;
        }
    }
}
