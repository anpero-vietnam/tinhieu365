using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public partial class CompanyModel
    {
        public CompanyModel()
        {
            Ticker = string.Empty;
            BusinessName = string.Empty;
            EngName = string.Empty;
            Description = string.Empty;
            StockExchange = string.Empty;
            Logo = string.Empty;
            IsWatchList =false;
            WebSite = string.Empty;
            Phone = string.Empty;
            Address = string.Empty;
            Email = string.Empty;
            MaxValue52Week =0;
            MinValue52Week = 0;
        }
        public string Ticker { get; set; }
        public string BusinessName { get; set; }
        public DateTime LastUpdate { get; set; }
        
        public string EngName { get; set; }
        public string Description { get; set; }
        public string StockExchange { get; set; }
        public string Logo { get; set; }
        public bool IsWatchList { get; set; }
        public string WebSite { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public float MaxValue52Week { get; set; }
        public float MinValue52Week { get; set; }

    }
}
