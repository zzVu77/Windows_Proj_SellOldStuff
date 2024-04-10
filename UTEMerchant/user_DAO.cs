using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTEMerchant
{
    public class user_DAO : DAO<User>
    {
        List<User> Users = new List<User>();
        public override List<User> Load()
        {
            return  db.LoadData<User>("SELECT * FROM [dbo].[User]");
        }
    }
}
