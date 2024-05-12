using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTEMerchant
{
    internal class ItemClick_DAO : DAO<ItemClick>
    {
        public override List<ItemClick> Load()
        {
            return db.ItemClicks.ToList();
        }

        public void UpdateClick(int itemId)
        {
            var item = db.ItemClicks.FirstOrDefault(i => i.Item_Id == itemId);
            if (item != null)
            {
                item.Click_Count++;
                db.SaveChanges();
            }
        }

        public void UpdateSearch(int itemId)
        {
            var item = db.ItemClicks.Find(itemId);
            if (item != null)
            {
                item.Search_Count++;
                db.SaveChanges();
            }
        }
    }
}
