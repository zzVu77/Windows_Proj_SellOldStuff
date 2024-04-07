using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTEMerchant
{
    public class user_DAO
    {
        private DB_Connection db = new DB_Connection();
        List<User> Users = new List<User>();
        public List<User> Load()
        {
            return db.LoadData<User>("User");
        }
    }
}
