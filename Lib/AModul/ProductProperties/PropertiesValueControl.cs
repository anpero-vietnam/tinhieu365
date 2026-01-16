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
    public class PropertiesValueControl : ConnectionProxy<AtributeValue>, IPropertiesValue
    {
        string cacheKey = "cache_ProductProperties";
        public int Delete(int id)
        {
            int rs = 0;
            rs = base.DeleteById(id);
            CacheHelper.Remove(cacheKey);
            return rs;
        }
        public AtributeValue GetValueById(int id)
        {
            Dictionary<string, object> paramlist = new Dictionary<string, object>();
            paramlist.Add("@id", id);
            return base.Select("sp_getProductPropertiesValueById", paramlist).FirstOrDefault();
        }
        public List<AtributeValue> GetValueProductCategoryId(int categoryId)
        {
            Dictionary<string, object> paramlist = new Dictionary<string, object>();

            paramlist.Add("@categoryId", categoryId);
            return base.Select("sp_GetValueProductCategoryId", paramlist);
        }

        public int AddProductPropertyValue(AtributeValue models, int productId)
        {
            Dictionary<string, object> paramlist = new Dictionary<string, object>();

            paramlist.Add("@PropertyId", models.Values);
            paramlist.Add("@IsInStock", models.IsInStock);
            paramlist.Add("@Price", models.Price);
            paramlist.Add("@ProductId", productId);
            CacheHelper.Remove(cacheKey);
            return base.ExecuteProc("sp_addProductAtribute", paramlist);
        }
        public int ResetProductPropertyValue(int productId)
        {
            Dictionary<string, object> paramlist = new Dictionary<string, object>();
            CacheHelper.Remove(cacheKey + "_st_");

            paramlist.Add("@ProductId", productId);
            return base.ExecuteProc("sp_ProductPropertyValueReset", paramlist);
        }
        /// <summary>
        /// get GetAllPropertyByProduct
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        public List<AtributeValue> GetAllPropertyByProduct(int productId)
        {
            Dictionary<string, object> paramlist = new Dictionary<string, object>();

            paramlist.Add("@ProductId", productId);
            return base.Select("sp_GetAllPropertyValueByProduct", paramlist);
        }

        //public int UpdateProductAtribute(int atribiteId, int productId,int price,bool isInstock,int agenId)
        //{

        //    base.ParamList.Add("@ST", agenId);
        //    base.ParamList.Add("@ProductId", productId);
        //    return base.ExecuteProc("sp_UpdateProductAtribute", agenId);
        //}

    }
}
