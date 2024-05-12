    using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace UTEMerchant
{
    public class PurchasedItem_DAO : DAO<PurchasedProduct>
    {
        public override List<PurchasedProduct> Load()
        {
            return db.purchasedProducts.ToList();
        }

        public List<PurchasedProduct> Load(string status)
        {
            return db.purchasedProducts.Where(pi => pi.Delivery_Status == status).ToList();
        }

        public List<PurchasedProduct> LoadOrdersByUser(int userId, string status)
        {
            return db.purchasedProducts.Where(pi => pi.Id_user == userId && pi.Delivery_Status == status).ToList();
        }

        public List<PurchasedProduct> LoadOrdersBySeller(int sellerId, string status)
        {
            return db.purchasedProducts.Where(pi => pi.Item.SellerID == sellerId && pi.Delivery_Status == status).ToList();
        }

        public void AddItem(PurchasedProduct item)
        {
            item.PurchaseDate = DateTime.Now;
            item.Delivery_Status = "pending";
            db.purchasedProducts.Add(item);
            db.SaveChanges();
        }

        public void RequestItems(List<Item> items, int userId, string deliveryAddress, string name, string phone, string email)
        {
            DateTime requestDate = DateTime.Now;
            foreach (var item in items)
            {
                PurchasedProduct purchasedItem = new PurchasedProduct
                {
                    Id_user = userId,
                    Item_Id = item.Item_Id,
                    PurchaseDate = requestDate,
                    Delivery_Status = "pending",
                    Name = name,
                    Phone = phone,
                    Email = email,
                    Delivery_Address = deliveryAddress
                };
                db.purchasedProducts.Add(purchasedItem);
                db.SaveChanges();
                new Item_DAO().UpdateStatus(item.Item_Id);
            }
        }

        public void UpdateDeliveryStatus(int purchaseId, string newStatus)
        {
            var purchasedItem = db.purchasedProducts.Find(purchaseId);
            if (purchasedItem != null)
            {
                purchasedItem.Delivery_Status = newStatus;
                db.SaveChanges();
            }
        }

        public void CancelOrder(int purchaseId)
        {
            UpdateDeliveryStatus(purchaseId, "cancelled");
            var purchasedItem = db.purchasedProducts.Find(purchaseId);
            if (purchasedItem != null)
            {
                var item = purchasedItem.Item;
                if (item != null)
                {
                    item.sale_status = false;
                    db.SaveChanges();
                }
            }
        }

        public Item GetItem(int purchaseId)
        {
            var purchasedItem = db.purchasedProducts.Find(purchaseId);
            return purchasedItem != null ? purchasedItem.Item : null;
        }

        public double CalculateTotalPrice(int sellerID)
        {
            double totalPrice = db.purchasedProducts
        .Where(pi => pi.Item.SellerID == sellerID && pi.Delivery_Status != "declined")
        .Sum(pi => (double?)pi.Item.price) ?? 0.0; // Use null-coalescing operator to handle null values
            return totalPrice;
        }

        public int CalculateTotalSold(int sellerID)
        {
            return db.purchasedProducts
                     .Count(pi => pi.Item.SellerID == sellerID && pi.Delivery_Status != "declined");
        }
    }
}
