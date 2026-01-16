using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class PaymentConfig
    {

        public int St { get; set; }

        public string Name { get; set; }
        public string MerchantId { get; set; }

        public string MerchantPassword { get; set; }

        public string Token { get; set; }

        public string Email { get; set; }
        public string PaymentCode { get; set; }

        public string UpdateTime { get; set; }
        public bool Isdefault { get; set; }

        public int PaymentFee { get; set; }

        public string Currency { get; set; }
    }
}
