using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AModul.Dapper;
using Models;
using Models.Modul.Product;
using Ultil.Cache;

namespace AModul.ProductProperties
{
    public class PropertiesControl : ConnectionProxy<AtributeModel>, IProperties
    {
        string cacheKey = "cache_ProductProperties";
        public int Delete(int id)
        {
            int rs = 0;
            rs = base.ExecQuery(" delete ProductProperties where id=" + id);
            CacheHelper.Remove(cacheKey);
            return rs;
        }
        public int UpdatePriotyRank(int rank, int id)
        {
            Dictionary<string, object> paramlist = new Dictionary<string, object>();
            paramlist.Add("@Id", id);
            paramlist.Add("@Rank", rank);
            
            CacheHelper.Remove(cacheKey);
            return base.ExecuteProc("sp_UpdateProductPropertiesRank",  paramlist);
        }
        public int UpdatePriotyValueRank(int rank, int id)
        {
            Dictionary<string, object> paramlist = new Dictionary<string, object>();
            paramlist.Add("@Id", id);
            paramlist.Add("@Rank", rank);
            
            CacheHelper.Remove(cacheKey);

            return base.ExecuteProc("sp_UpdateProductPropertiesValueRank",  paramlist);
        }

        public int AddOrUpdate(AtributeModel model)
        {
            Dictionary<string, object> paramlist = new Dictionary<string, object>();
            paramlist.Add("@Id", model.Id);
            paramlist.Add("@Images", model.Images);
            paramlist.Add("@Rank", model.Rank);
            paramlist.Add("@Description", model.Description);
            paramlist.Add("@keywords", model.Keywords);
            paramlist.Add("@Name", model.Name);
            
            CacheHelper.Remove(cacheKey);
            return base.ExecuteProc("sp_AddOrUpdateProductProperties",  paramlist);
        }
        public int AddOrUpdateValue(AtributeValue model)
        {
            Dictionary<string, object> paramlist = new Dictionary<string, object>();
            paramlist.Add("@Id", model.Id);
            paramlist.Add("@Images", model.Images);
            paramlist.Add("@SmallThumb", model.SmallThumb);
            paramlist.Add("@Rank", model.Rank);
            paramlist.Add("@Values", model.Values);
            
            paramlist.Add("@PropertiesId", model.PropertiesId);
            CacheHelper.Remove(cacheKey );

            return base.ExecuteProc("sp_AddOrUpdateProductPropertiesValue", paramlist);
        }
    
        public int DeleteValById(int id)
        {
            int rs = 0;
            Dictionary<string, object> paramlist = new Dictionary<string, object>();
            paramlist.Add("@Id", id);            
            rs = base.ExecuteProc("sp_delPriotyValueById",paramlist);
            CacheHelper.Remove(cacheKey);
            return rs;
        }
        /// <summary>
        /// get All property and value 
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public List<AtributeModel> GetAll()
        {
            List<AtributeModel> rs = new List<AtributeModel>();
            if (!CacheHelper.TryGet(cacheKey, out rs))
            {
                Dictionary<string, object> paramlist = new Dictionary<string, object>();                
                rs = base.Select("sp_getProductProperties", paramlist);
                CacheHelper.Set<List<AtributeModel>>(cacheKey, rs, 60 * 24 * 5);
            }
            return rs;
        }
        /// <summary>
        /// Get Only Prioty of this product
        /// </summary>
        /// <param name="client"></param>
        /// <returns>All value and property having price and isIsInstock</returns>
        public List<AtributeModel> GetDataOfProductOnly(int productId)
        {
            Dictionary<string, object> paramlist = new Dictionary<string, object>();            
            paramlist.Add("@ProductId", productId);
            return base.Select("sp_getProductProperties_ByProductID",  paramlist).Where(x=>x.AtributeValueJson!=null).ToList();
        }
    
    }
}
