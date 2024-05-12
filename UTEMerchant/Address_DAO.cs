using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace UTEMerchant
{
    public class Address_DAO : DAO<Address>
    {
        public Address_DAO() { }

        public override List<Address> Load()
        {
            return db.Address.ToList();
        }

        public List<string> GetDistricts(string city)
        {
            return db.Address
                .Where(a => a.City == city)
                .Select(a => a.District)
                .Distinct()
                .ToList();
        }
    }
}
