using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Modul.WareHouse
{
    public class WarrantyCard
    {
        public WarrantyCard()
        {
            SeriaId = string.Empty;
            Seria = string.Empty;
            
            BeginDate = DateTime.Now;
            CreateDate = DateTime.Now;
            EndDate = DateTime.Now;
            UserAddressId = 0;
            AgenId = 0;
            contact = new Contact();
            Warranty = 0;
            Id=string.Empty;
            ProductId = 0;
            Reseller = string.Empty;
            Note = string.Empty;
        }
        public string Id { get; set; }
        public string Seria { get; set; }
        public string Reseller { get; set; }
        public string Note { get; set; }
        public string SeriaId { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public int UserAddressId { get; set; }
        public DateTime CreateDate { get; set; }
        public int AgenId { get; set; }
        public Contact contact { get; set; }
        public int Warranty { get; set; }
        public int ProductId { get; set; }

        

    }
}
