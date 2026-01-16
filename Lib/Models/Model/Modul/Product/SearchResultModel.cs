using Models.Modul.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Modul.Product
{
    public class SearchResultModel
    {
       public List<ProductItem> ItemList{ get; set; }
        public int Count { get; set; }
        public SearchResultModel()
        {
            ItemList =new List<ProductItem>();
            Count = 0;
        }
    }
}
