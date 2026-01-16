using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Modul.Product
{
    public class SearchProductFilter: ProductItem
    {
        public SearchProductFilter()
        {          
            Page = 1;
            ItemPerPage = 30;
            IsPublish = true;
        }
        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }
        public decimal? AreaFrom { get; set; }
        public decimal? AreaTo { get; set; }

        public int Page { get;set; }
        public int ItemPerPage { get; set; }
        public bool? GetClosedProject{ get; set; }
        public string Advantage { get; set; }
        
        
    }
}
