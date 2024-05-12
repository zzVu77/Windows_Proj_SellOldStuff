using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTEMerchant
{
    public class DAO<T> where T : class
    {
        protected UTEMerchantContext db = new UTEMerchantContext();

        public virtual List<T> Load()
        {
            return new List<T>();
        }

        public virtual void Add(T obj)
        {
            db.Set<T>().Add(obj);
            db.SaveChanges();
        }
    }
}
