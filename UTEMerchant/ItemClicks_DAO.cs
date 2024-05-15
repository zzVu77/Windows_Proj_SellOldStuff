using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTEMerchant
{
    internal class ItemClicks_DAO : DAO<ItemClicks>
    {
        public override List<ItemClicks> Load()
        {
            return db.LoadData<ItemClicks>("Select * from [dbo].[ItemClicks] ");
        }
        public override void Add(ItemClicks obj)
        {
            base.Add(obj);
        }
        public void UpdateClick(int Item_id)
        {
            db.ExecuteNonQuery(@"UPDATE [dbo].[ItemClicks] SET Click_Count = Click_Count + 1 WHERE Item_Id = @item_id", new SqlParameter("@item_id", Item_id));
        }

    }
}
