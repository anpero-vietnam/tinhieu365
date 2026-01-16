using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Modul.Product
{
    public class PriotyModel
    {  

        public string Prid { get; set; }
        public string Value { get; set; }        
        public string Images { get; set; }
        public string CatName { get; set; }        

        public String ParentCatName { get; set; }       

        public string PrName { get; set; }
        public string Price { get; set; }
        public PriotyModel()
        {
            CatName = "";
            ParentCatName = "";
            Prid = "";
            Value = "";
            PrName = "";
            Images = "";
            Price = "0";
        }
    }
}
