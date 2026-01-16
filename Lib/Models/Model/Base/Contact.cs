using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Contact
    {
        public Contact(){
            Id = 0;
            Phone = string.Empty;
            Name = string.Empty;
            Mail = string.Empty;
            Address = string.Empty;
            UserId = 0;
            TaxCode = string.Empty;
            LocationId = 0;
            IdCard = string.Empty;
            
            Province = string.Empty;
            District = string.Empty;
        }
        
        
        public int Id { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public string Mail { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string Address { get; set; }
        public int UserId { get; set; }
        public string TaxCode { get; set; }
        public string StoreId { get; set; }
        public int LocationId { get; set; }
        public string IdCard { get; set; }
        

    }
}
