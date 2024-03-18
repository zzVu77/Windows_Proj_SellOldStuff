using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTEMerchant
{
    internal class DB_Connection
    {
        SqlConnection conn = new
       SqlConnection(@"Data Source=(localdb)\mssqllocaldb;Initial Catalog=DB_Merchant;Integrated Security=True");

        
        
    }
}
