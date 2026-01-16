using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Modul.Product
{
    public class OrderModel
    {
        public OrderModel()
        {
            CustomName = "";
            CusTomPhone = "";
            CustomMail = "";
            CustomAddres = "";
            TotalPrice = 0;
            TotalVat = 0;
            DateCreate = "";
            StaffName = "";
            Status = 0;
            CashRecore = 0;            
            CustomId = 0;
            Id = 0;
            Paided = 0;
            Owe = 0;
            BankName = "";
        }        
        public string Token { get; set; }        

        public string Detail { get; set; }

        public int ShipingMethod { get; set; }

        public int TranferType { get; set; }

        public int ShippingFee { get; set; }        
        
        public string BankName { get; set; }

        public decimal Paided { get; set; }
        public decimal Owe { get; set; }
        public int Id { get; set; }
        

        public int CustomId { get; set; }
        public int ProducQuantity { get; set; }
        public int CashRecore { get; set; }
        
        public int StaffId { get; set; }        
        public int Status { get; set; }        

        public string CustomName { get; set; }       

        public string CusTomPhone { get; set; }        

        public string CustomMail { get; set; }        

        public string CustomAddres { get; set; }        

        public int TotalPrice { get; set; }       

        public int TotalVat { get; set; }        

        public string DateCreate { get; set; }        

        public string StaffName { get; set; }

        public int StoreId { get; set; }
    }
}
