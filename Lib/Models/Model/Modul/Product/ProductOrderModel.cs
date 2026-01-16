using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Modul.Product
{
    public class ProductOrderModel
    {
        public int Id { get; set; }

        public int OrderId { get; set; }


        public int PrId { get; set; }

        public string Seria { get; set; }
        public decimal Price { get; set; }

        public int Quantyti { get; set; }


        public ProductOrderModel()
        {
            Quantyti = 0;
            Price = 0;
            Seria = "";
            PrId = 0;
            OrderId = 0; Id = 0;
        }
    }

}
