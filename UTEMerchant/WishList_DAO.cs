using HandyControl.Controls;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTEMerchant
{
    internal class WishList_DAO : DAO<Wishlist>
    {
        public override List<Wishlist> Load()
        {
            return db.Wishlists.ToList();
        }

        public List<Wishlist> GetItemsByUserID(int userID)
        {
            return db.Wishlists.Where(wl => wl.Id_user == userID).ToList();
        }

        public override void Add(Wishlist item)
        {
            db.Wishlists.Add(item);
            db.SaveChanges();
        }

        public bool CheckAddedItemInWishList(int itemID, int userID)
        {
            return db.Wishlists.Any(wl => wl.Id_user == userID && wl.Item_Id == itemID);
        }

        public void RemoveItem(int itemID, int userID)
        {
            var wishListItem = db.Wishlists.FirstOrDefault(wl => wl.Id_user == userID && wl.Item_Id == itemID);
            if (wishListItem != null)
            {
                db.Wishlists.Remove(wishListItem);
                db.SaveChanges();
            }
        }
    }
}
