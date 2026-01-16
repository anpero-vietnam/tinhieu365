using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class TransactionModel : CompanyModel
    {
        public TransactionModel(){
            Open = 0;
            High = 0;
            Low = 0;
            Close = 0;
            Volume = 0;
            RSI = 0;
            ADX = 0;
            MFI = 0;
            CreateDateText = string.Empty;
            SMA = 0;
            SMA50 = 0;
            AVGValue20 = 0;
        }
        public DateTime CreateDate { get; set; }
        public float Open { get; set; }
        public float High { get; set; }
        
        public float Low { get; set; }
        public float Close { get; set; }
        public float Volume { get; set; }        
        public float RSI { get; set; }
        public float ADX { get; set; }
        public float MFI { get; set; }
        public string CreateDateText { get; set; }
        public float SMA { get; set; }
        public float SMA50 { get; set; }
        public decimal AVGValue20 { get; set; }        
        
    }
}

