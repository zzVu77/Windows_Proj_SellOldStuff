using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTEMerchant
{
    public class WishList
    {
        public int WishListID { get; set; }
        public int Id_user { get; set; }
        public int Item_Id { get; set; }    
        public DateTime AddedDate { get; set; }

        public WishList()
        { 
        
        }
        public WishList(int wishListID, int Id_user, int Item_ID, DateTime addedDate)
        {
            this.WishListID = wishListID;
            this.Id_user = Id_user;
            this.Item_Id = Item_ID;
            this.AddedDate = addedDate;
        }
        public WishList ( int Id_user, int Item_ID, DateTime addedDate)
        {
            this.Id_user = Id_user;
            this.Item_Id = Item_ID;
            this.AddedDate = addedDate;
        }
    }
}
