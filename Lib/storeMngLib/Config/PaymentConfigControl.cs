using System;
using System.Collections.Generic;
using System.Data;
using AModul.Dapper;
using Models;

namespace StoreMng.Config
{
    public class PaymentConfigControl: ConnectionProxy<PaymentConfig>,Inteface.IPaymentConfig
    {
        public int UpdatePaymentConfig(PaymentConfig config)
        {   
            try
            {
                Dictionary<string, object> paramList = new Dictionary<string, object>();
                paramList.Add("@Name", config.Name);
                paramList.Add("@MerchantId", config.MerchantId);
                paramList.Add("@MerchantPassword", config.MerchantPassword);
                paramList.Add("@Token", config.Token);
                paramList.Add("@Email", config.Email);
                paramList.Add("@isdefault", config.Isdefault ? 1 : 0);
                paramList.Add("@PaymentCode", config.PaymentCode);                
                paramList.Add("@PaymentFee", config.PaymentFee);
                paramList.Add("@currency", config.Currency);
                return ExecuteProc("Update_PAYMENT_CONFIG", paramList);
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public List<PaymentConfig> GetPaymentConfigList()
        {
            List<PaymentConfig> returnList = new List<PaymentConfig>();
            try
            {
                

                DatabaseAccess ds = new DatabaseAccess();
                DataTable table=   ds.executeSelect("GET_PAYMENT_CONFIG");
                if(table!=null && table.Rows.Count > 0)
                {
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        PaymentConfig pc = new PaymentConfig();
                        
                        pc.Name = table.Rows[i]["Name"].ToString();
                        pc.MerchantId = table.Rows[i]["MerchantId"].ToString();
                        pc.MerchantPassword = table.Rows[i]["MerchantPassword"].ToString();
                        pc.Token = table.Rows[i]["Token"].ToString();
                        pc.Email = table.Rows[i]["Email"].ToString();
                        pc.Isdefault = Convert.ToBoolean(table.Rows[i]["isdefault"]);
                        pc.UpdateTime = table.Rows[i]["updateTime"].ToString();
                        pc.PaymentCode = table.Rows[i]["PaymentCode"].ToString();
                        pc.PaymentFee = Convert.ToInt32(table.Rows[i]["PaymentFee"]);
                        returnList.Add(pc);
                    }
                    return returnList;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
 
}
