using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTEMerchant
{
    public class ImgPath_DAO : DAO<ImgPath>
    {
        public override List<ImgPath> Load() // More descriptive method name
        {
            return db.LoadData<ImgPath>("SELECT * FROM [dbo].[ImgPath]");
        }

        public List<ImgPath> GetListImagePathByItemID(int id)
        {
            return db.LoadData<ImgPath>($"SELECT * FROM [dbo].[ImgPath] WHERE Item_Id = '{id}'");
        }

        public override void Add(ImgPath imgPath) // Using PascalCase for method name
        {
            string sqlStr = "INSERT INTO [dbo].[ImgPath] (Item_Id, Path) " +
                            "VALUES (@Item_Id, @Path)";

            db.ExecuteNonQuery(sqlStr,

                new SqlParameter("@Item_Id" , imgPath.Item_Id),
                new SqlParameter("@Path", imgPath.Path));
        }
    }
}
