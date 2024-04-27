using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTEMerchant
{
    public class ImgPath
    {
        public int Item_Id { get; set; }
        public string Path { get; set; }    

        public ImgPath() { }
        public ImgPath(int item_Id, string path)
        {
            Item_Id = item_Id;
            Path = path;
        }   
    }
}
