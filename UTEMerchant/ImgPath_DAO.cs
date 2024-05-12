using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTEMerchant
{
    internal class ImgPath_DAO : DAO<ImgPath>
    {
        public override List<ImgPath> Load()
        {
            return db.ImgPaths.ToList();
        }

        public List<ImgPath> GetListImagePathByItemID(int itemId)
        {
            return db.ImgPaths.Where(i => i.Item_Id == itemId).ToList();
        }

        public override void Add(ImgPath imgPath)
        {
            db.ImgPaths.Add(imgPath);
            db.SaveChanges();
        }

        public void DeleteImgPaths(int itemId)
        {
            var imgPaths = db.ImgPaths.Where(i => i.Item_Id == itemId);
            db.ImgPaths.RemoveRange(imgPaths);
            db.SaveChanges();
        }
    }
}
