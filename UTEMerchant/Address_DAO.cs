using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace UTEMerchant
{
    internal class Address_DAO : DAO<Address>
    {
        public Address_DAO() { }
        public override List<Address> Load() // More descriptive method name
        {
            return db.LoadData<Address>("SELECT * FROM [dbo].[Address]");
        }
        public List<string> getdistrict(string city)
        {

            return db.LoadData<Address>("SELECT * FROM [dbo].[Address]").Where(a => a.City == city)
                   .Select(a => a.District)
                   .Distinct().ToList();

            //return db.LoadData<Address>($"SELECT * FROM [dbo].[Address] WHERE City={city}").Select(a=>a.District).Distinct().ToList();
        }
    }
}
