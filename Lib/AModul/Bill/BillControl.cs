using AModul.Dapper;
using Models.Modul.Bill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AModul.Bill
{
    public class BillControl: ConnectionProxy<BillModel>
    {
        public int AddBill(BillModel model)
        {
            Dictionary<string, object> paramlist = new Dictionary<string, object>();
            paramlist.Add("@id", model.Id);
            paramlist.Add("@Amount", model.Amount);            
            paramlist.Add("@CreateBy", model.CreateBy);
            paramlist.Add("@CreateFor", model.CreateFor);
            paramlist.Add("@Detail", model.Detail);
            paramlist.Add("@IsSuccess", model.IsSuccess);
            paramlist.Add("@OrderId", model.OrderId);
            paramlist.Add("@Merchant", model.Merchant);
            paramlist.Add("@IsCredit", model.IsCredit);
            
            return Convert.ToInt32(base.ExecuteScalar("sp_CreateBill", paramlist));
        }
        public BillModel GetBillById(int id,int CreteBy)
        {
            Dictionary<string, object> paramlist = new Dictionary<string, object>();
            paramlist.Add("@id",id);
            paramlist.Add("@CreateBy",CreteBy);            
            return base.SelectSingle("sp_GetBillById", paramlist);
        }
        //public BillModel AdminGetBill(int id, int CreteBy)
        //{
        //    Dictionary<string, object> paramlist = new Dictionary<string, object>();
        //    return base.SelectSingle("sp_AdminGetBill", paramlist);
        //}
        public List<BillModel> AdminGetBill(bool isCredit)
        {
            Dictionary<string, object> paramlist = new Dictionary<string, object>();
            paramlist.Add("@isCredit", isCredit?1:0);
            return base.Select("sp_AdminGetBill", paramlist);
        }
    }
}
