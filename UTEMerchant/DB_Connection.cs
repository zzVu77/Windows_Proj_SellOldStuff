using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UTEMerchant
{
    public class UTEMerchantContext: DbContext
    {
        public UTEMerchantContext() : base("name=db_ute_merchantEntities") { }

        public DbSet<Address> Address { get; set; }
        public DbSet<CustomerReview> CustomerReviews { get; set; }
        public DbSet<ImgPath> ImgPaths { get; set; }
        public DbSet<Item> Items { get; set; }
        
        public DbSet<PurchasedProduct> purchasedProducts { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }

        public DbSet<ItemClick> ItemClicks { get; set; }

    }
}
