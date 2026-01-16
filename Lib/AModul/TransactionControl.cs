using AModul.Dapper;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AModul
{
    public class TransactionControl : ConnectionProxy<TransactionModel>
    {
        public List<TransactionModel> SearchTransaction(TransactionFilter filter)
        {
            DateTime dt = DateTime.Now;
            Dictionary<string, object> paramlist = new Dictionary<string, object>();
            int getBeforeDate = -1;
            if (dt.DayOfWeek == DayOfWeek.Sunday)
            {
                getBeforeDate = -3;
            }
            if (dt.DayOfWeek == DayOfWeek.Saturday)
            {
                getBeforeDate = -2;
            }
            paramlist.Add("@RsiMin", filter.RsiMin);
            paramlist.Add("@RsiMax", filter.RsiMax);
            paramlist.Add("@AdxMin", filter.AdxMin);
            paramlist.Add("@AdxMax", filter.AdxMax);
            paramlist.Add("@MfiMin", filter.MfiMin);
            paramlist.Add("@MfiMax", filter.MfiMax);
            paramlist.Add("@GetBeforeDate", getBeforeDate);

            return base.Select("sp_FilterTransaction", paramlist);
        }
        
        public List<TransactionModel> GetOversold()
        {
            return base.Select("sp_GetOversold");
        }
        public List<TransactionModel> GetSurfSignal()
        {
            return base.Select("sp_BuyPointShortTerm");
        }
        public TransactionModel GetBussinessFromKiker(string tickerName)
        {
            if (tickerName.Contains("-")  && !tickerName.Equals(@"VNAll-INDEX",StringComparison.OrdinalIgnoreCase))
            {
                tickerName = tickerName.Split('-').LastOrDefault();
            }
            Dictionary<string, object> paramlist = new Dictionary<string, object>();
            paramlist.Add("@ticker", tickerName);
            return base.SelectSingle("[sp_GetBussinessFromticker]", paramlist);
        }
        public List<TransactionModel> SearchBussiness(string keyword)
        {
            Dictionary<string, object> paramlist = new Dictionary<string, object>();
            paramlist.Add("@ticker", keyword);
            return base.Select("[sp_SearchBussiness]", paramlist) ?? new List<TransactionModel>();
        }
        public int UpdateBussiness(TransactionModel model)
        {
            Dictionary<string, object> paramlist = new Dictionary<string, object>();
            paramlist.Add("@Ticker", model.Ticker.Trim());
            paramlist.Add("@BusinessName", model.BusinessName.Trim());
            paramlist.Add("@StockExchange", string.IsNullOrEmpty(model.StockExchange)?string.Empty: model.StockExchange.ToUpper().Trim());
            paramlist.Add("@IsWatchList", model.IsWatchList);
            paramlist.Add("@Description", model.Description?.Trim());
            paramlist.Add("@logo", model.Logo?.Trim());
            paramlist.Add("@website", model.WebSite?.Trim());
            paramlist.Add("@Phone", model.Phone?.Trim());
            paramlist.Add("@Address", model.Address);
            paramlist.Add("@EngName", model.EngName);
            paramlist.Add("@Email", model.Email?.Trim());
            return base.ExecuteProc("[sp_UpdateBussinessByticker]", paramlist);
        }
    }
}
