using AModul.Dapper;
using Models.Modul.Product;
using System.Collections.Generic;

namespace AModul.Product
{
    public class ProductAnalyticControl : ConnectionProxy<ProductAnalyticItem>
    {
        public ProductAnalyticItem GetProductAnalytic()
        {   
            return base.SelectSingle("[sp_getAllAnalytic]");
        }
    }
}
