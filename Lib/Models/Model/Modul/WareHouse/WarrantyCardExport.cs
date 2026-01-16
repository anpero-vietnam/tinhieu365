using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Modul.WareHouse
{
    public class WarrantyCardExport
    {
        public WarrantyCardExport()
        {
            
            Seria = string.Empty;            
            BeginDate = DateTime.Now;
            CreateDate = DateTime.Now;
            EndDate = DateTime.Now;            
            
            Warranty = 0;
            Id=string.Empty;
            ProductId = 0;
            Reseller = string.Empty;
            Note = string.Empty;
        }
        public string Id { get; set; }
        public int ProductId { get; set; }
        public string Seria { get; set; }
        public string Reseller { get; set; }
        public string Note { get; set; }
        public int Warranty { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }        
        public string Mail { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string Address { get; set; }
        public int UserId { get; set; }
        public string TaxCode { get; set; }        
        
        public string IdCard { get; set; }
        
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
