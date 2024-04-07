using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTEMerchant
{
    public class Seller
    {
        public int SellerID { get; set; }
        public int Id_user { get; set; }

        public string ShopName { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Ward { get; set; }

        public float Average_rating { get; set; }

    }
}
