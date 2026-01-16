using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Modul.Product
{
    public class SearchResult
    {
        public SearchResult()
        {
            Item = null;
            ItemCount = 0;
            CurrentPage = 1;
            PageSize = 30;
        }
        public List<AtributeValue> PropertiesValue { get; set; }
        public List<ProductItem> Item { get; set; }

        public int ItemCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }

}
