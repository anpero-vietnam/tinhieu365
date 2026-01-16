using Models;
using Models.Modul.Product;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AModul.Product
{
    public class ProductGroupControl
    {
        public List<ProductGroup> GetProductGroup()
        {
            OriginGroup or = new OriginGroup();
            return or.GetOrigin();
        }
    }
}
