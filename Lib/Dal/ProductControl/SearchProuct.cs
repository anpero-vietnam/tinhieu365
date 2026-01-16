using Dal.ProductProperties;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Product
{
    public class SearchResult
    {
        public SearchResult()
        {
            Item = null;
            ResultCount = 0;
        }

        
        public List<ProductPropertiesValue> PropertiesValue { get; set; }
        int resultCount;

        public List<ProductItem> Item { get; set; }

        public int ResultCount { get; set; }
    }
    

  
}
