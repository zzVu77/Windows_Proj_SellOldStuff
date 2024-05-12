using HandyControl.Controls;
using HandyControl.Themes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Xml;

namespace UTEMerchant
{
    public class Item_DAO : DAO<Item>
    {
        public override List<Item> Load()
        {
            return db.Items.ToList();
        }

        public override void Add(Item item)
        {
            db.Items.Add(item);
            db.SaveChanges();
        }

        public void RemoveItem(Item item)
        {
            db.Items.Remove(item);
            db.SaveChanges();
        }

        public void UpdateStatus(int itemId)
        {
            var item = db.Items.Find(itemId);
            if (item != null)
            {
                item.sale_status = true;
                db.SaveChanges();
            }
        }

        public List<Item> GetItemsBySellerID(int sellerID)
        {
            return db.Items.Where(i => i.SellerID == sellerID).ToList();
        }

        public Item GetItemByItemID(int id)
        {
            return db.Items.FirstOrDefault(i => i.Item_Id == id);
        }

        public Item GetAllInfoItemByItemID(int id)
        {
            return db.Items.FirstOrDefault(i => i.Item_Id == id);
        }

        public List<Item> Search(string text, int sellerID)
        {
            string formattedText = text.ToUpper(); // Format the text to uppercase
            return db.Items.Where(i => i.SellerID != sellerID &&
                                        (i.type.ToUpper().Contains(formattedText) ||
                                         i.name.ToUpper().Contains(formattedText) ||
                                         i.detail_description.ToUpper().Contains(formattedText) ||
                                         i.condition_description.ToUpper().Contains(formattedText))).ToList();
        }

        public List<Item> SearchForWishList(string text, int userID)
        {
            string formattedText = text.ToUpper(); // Format the text to uppercase
            return (from i in db.Items
                    join w in db.Wishlists on i.Item_Id equals w.Item_Id
                    where w.Id_user == userID &&
                          (i.type.ToUpper().Contains(formattedText) ||
                           i.name.ToUpper().Contains(formattedText) ||
                           i.detail_description.ToUpper().Contains(formattedText) ||
                           i.condition_description.ToUpper().Contains(formattedText))
                    select i).ToList();
        }

        public List<Item> SortPrice()
        {
            return db.Items.OrderByDescending(i => i.price).ToList();
        }

        public List<Item> SortRelevance(int userID)
        {
            return (from i in db.Items
                    where (from it in db.Items
                           join pp in db.purchasedProducts on it.Item_Id equals pp.Item_Id
                           where pp.Id_user == userID
                           select it.type).Distinct().Contains(i.type)
                    select i).ToList();
        }

        public int GetTheMaximumItem_ID()
        {
            return db.Items.Max(i => i.Item_Id);
        }

        public void UpdateItem(Item item)
        {
            var existingItem = db.Items.Find(item.Item_Id);
            if (existingItem != null)
            {
                existingItem.name = item.name;
                existingItem.price = item.price;
                existingItem.original_price = item.original_price;
                existingItem.type = item.type;
                existingItem.bought_date = item.bought_date;
                existingItem.condition_description = item.condition_description;
                existingItem.condition = item.condition;
                existingItem.ImgPaths = item.ImgPaths;
                existingItem.detail_description = item.detail_description;
                db.SaveChanges();
            }
        }

        public int CalculateTotalProducts(int sellerID)
        {
            return db.Items.Count(i => i.SellerID == sellerID);
        }
    }
}
