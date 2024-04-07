using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTEMerchant
{
    internal class Address
    {
        public int Id {  get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Ward { get; set; }
        public Address() { }
    }
}
