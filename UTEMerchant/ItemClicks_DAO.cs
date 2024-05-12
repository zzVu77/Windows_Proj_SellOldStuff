using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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
                // Increment the Click_Count
                item.Click_Count += 1;

                // Mark the entity as modified
                db.Entry(item).State = EntityState.Modified;

                try
                {
                    // Save changes to the database
                    db.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    // Handle any exceptions
                    Console.WriteLine($"Error updating item: {ex.Message}");
                }
            }
        }

        public void UpdateSearch(int itemId)
        {
            var item = db.ItemClicks.FirstOrDefault(i => i.Item_Id == itemId);
            if (item != null)
            {
                // Increment the Click_Count
                item.Search_Count += 1;

                // Mark the entity as modified
                db.Entry(item).State = EntityState.Modified;

                try
                {
                    // Save changes to the database
                    db.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    // Handle any exceptions
                    Console.WriteLine($"Error updating item: {ex.Message}");
                }
            }
        }
    }
}
