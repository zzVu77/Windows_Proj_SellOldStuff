using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTEMerchant
{
    public class Seller_DAO : DAO<Seller>
    {
        public override List<Seller> Load()
        {
            return db.Sellers.ToList();
        }

        public Seller GetSeller(int sellerID)
        {
            return db.Sellers.FirstOrDefault(s => s.SellerID == sellerID);
        }

        public Seller GetSellerByUserID(int userID)
        {
            return db.Sellers.FirstOrDefault(s => s.Id_user == userID);
        }

        public void UpdateSeller(Seller seller)
        {
            var existingSeller = db.Sellers.Find(seller.SellerID);
            if (existingSeller != null)
            {
                existingSeller.ShopName = seller.ShopName;
                existingSeller.City = seller.City;
                existingSeller.District = seller.District;
                existingSeller.Ward = seller.Ward;
                existingSeller.phone = seller.phone;
                db.SaveChanges();
            }
        }

        public override void Add(Seller seller)
        {
            db.Sellers.Add(seller);
            db.SaveChanges();
        }
    }
}
