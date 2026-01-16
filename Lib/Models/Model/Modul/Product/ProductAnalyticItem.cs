using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Modul.Product
{
    public class ProductAnalyticItem
    {        
        public int OrderWaiting { get; set; }
        public int OrderPaider { get; set; }
        public int RequestToday { get; set; }
        
    }
}
