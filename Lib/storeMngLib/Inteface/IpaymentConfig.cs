using Models;
using StoreMng.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMng.Inteface
{
   public interface IPaymentConfig
    {
        int UpdatePaymentConfig(PaymentConfig config);
        List<PaymentConfig> GetPaymentConfigList();
       
    }
  
}
