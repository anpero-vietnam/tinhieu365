using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class TransactionFilter
    {
        public int? RsiMin;
        public int? RsiMax;
        public int? AdxMin;
        public int? AdxMax;
        public int? MfiMin;
        public int? MfiMax;
        public int? GetBeforeDate;        
    }
    public class TopTransactionFilter
    {
        public int PageSize;
        public string OrderBy;
    }
}
