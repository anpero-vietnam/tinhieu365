using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Modul.Product
{
    public class ProductGroup
    {
        

        public int Id { get; set; }
        public string Name { get; set; }

        public string Desc { get; set; }
        public string Images { get; set; }
        public int St { get; set; }
        public int Rank { get; set; }        
    }
}
