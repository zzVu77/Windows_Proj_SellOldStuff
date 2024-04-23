using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTEMerchant
{
    public class DAO<T>
    {
        protected DB_Connection db = new DB_Connection();

        public virtual List<T> Load ()
        {
            return new List<T> ();
        }
        public virtual void Add(T obj)
        {

        }
    }
}
