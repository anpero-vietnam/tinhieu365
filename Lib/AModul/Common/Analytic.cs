using System;
using System.Collections.Generic;
using System.Data;
using AModul.Dapper;
using Models;
using Models.Modul.Common;
using Ultil.Cache;

/// <summary>
/// Summary description for analytic
/// </summary>
/// 
namespace AModul.Common
{
    public class Analytic : ConnectionProxy<RequesAnalyticModel>
    {      
        public List<RequesAnalyticModel> GetRequesAnalytic( int Year, int month, int dayOfMonth, string type = null)
        {

            List<RequesAnalyticModel> model = new List<RequesAnalyticModel>();
            string cacheKey = "_GetRequesAnalytic_admin_st_" + Year + "_" + month;
            if(!CacheHelper.TryGet(cacheKey,out model))
            {
                Dictionary<string, object> paramlist = new Dictionary<string, object>();
                
                paramlist.Add("@yyyy", Year);
                paramlist.Add("@MM", month);
                paramlist.Add("@DD", dayOfMonth);
                paramlist.Add("@type", type);
                model = base.Select("sp_GetRequesAnalytic", paramlist);
                CacheHelper.Set(cacheKey, model, 15);                
            }            
            return model;
        }
        public List<RequesAnalyticModel> GetMonthRequesAnalyticAPI(int Year, int month)
        {
            try
            {
                Dictionary<string, object> paramlist = new Dictionary<string, object>();
                
                paramlist.Add("@yyyy", Year);
                paramlist.Add("@MM", month);                
                return base.Select("GetMonthRequesAnalytic", paramlist);
            }
            catch (Exception)
            {
                return null;
            }
        }
      
    }

}