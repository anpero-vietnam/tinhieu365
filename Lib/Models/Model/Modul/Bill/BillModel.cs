using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Modul.Bill
{
    public class BillModel
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int CreateBy { get; set; }
        public int CreateFor { get; set; }
        public string Detail { get; set; }
        public bool IsSuccess { get; set; }
        public DateTime CreateDate { get; set; }
        public string OrderId { get; set; }
        public string Merchant { get; set; }
        public string CreateByName { get; set; }
        public string CreateForName { get; set; }
        
        public bool  IsCredit { get; set; }
    }
}
