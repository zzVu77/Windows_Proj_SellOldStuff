using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTEMerchant
{
    internal class Seller_DAO
    {
        private DB_Connection db = new DB_Connection();
        List<Seller> Users = new List<Seller>();
        public List<Seller> Load()
        {
            return db.LoadData<Seller>("Seller");
        }
    }
}
