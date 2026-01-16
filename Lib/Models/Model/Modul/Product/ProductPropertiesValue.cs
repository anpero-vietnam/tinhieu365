using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Modul.Product
{
    public class AtributeValue
    {
        
        
        
        public int Id { get; set; }
        public int PropertiesId { get; set; }
        public string PropertiesName { get; set; }
        public int Rank { get; set; }

        public string Values { get; set; }
        public string Images { get; set; }
        public string SmallThumb { get; set; }
        public bool IsInStock { get; set; }
        public decimal Price { get; set; }

        public AtributeValue()
        {
            Id = 0;
            PropertiesId = 0;
            Values = string.Empty;
            Images = string.Empty;
            Rank = 0;
            PropertiesName = string.Empty;
            IsInStock = true;
            Price = -1;
        }
    }
}
