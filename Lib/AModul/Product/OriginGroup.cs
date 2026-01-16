using AModul.Dapper;
using Models;
using Models.Modul.Product;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Ultil;

namespace AModul.Product
{
    public class OriginGroup : ConnectionProxy<ProductGroup>
    {
        public int AddOrigin(int id, string name, string desc, string images, int rank)
        {
            Dictionary<string, object> paramlist = new Dictionary<string, object>();
            paramlist.Add("@Name", name);
            
            paramlist.Add("@desc", desc);
            paramlist.Add("@images", images);
            paramlist.Add("@rank", rank);
            if (id == 0)
            {

                return base.ExecuteProc("[sp_addOrigin]", paramlist);
            }
            else
            {
                paramlist.Add("@id", id);
                return base.ExecuteProc("[sp_UpdateOrigin]", paramlist);
            }

        }
        public List<ProductGroup> GetOrigin()
        {
            Dictionary<string, object> paramlist = new Dictionary<string, object>();
            
            return base.Select("[sp_getOrigin]", paramlist);
        }

        public int DelOrigin(int id)
        {
            Dictionary<string, object> paramlist = new Dictionary<string, object>();
            paramlist.Add("@Id", id);            
            return ExecuteProc("DelOrigin", paramlist);
        }
    }

    public class Category : ConnectionProxy<ProductCategory>
    {
        public List<ProductCategory> GetAllCategory(int parentId)
        {
            try
            {
                Category categoryControl = new Category();
                List<ProductCategory> parentCat = new List<ProductCategory>();
                string cacheKey = AEnum.Cache.Cachingkey.AllCategoryCache.ToString() ;
                if (!Ultil.Cache.CacheHelper.TryGet<List<ProductCategory>>(cacheKey, out parentCat))
                {
                    parentCat = categoryControl.GetCategory(0);
                    foreach (var item in parentCat)
                    {
                        Category childcCategory = new Category();
                        List<ProductCategory> childTable = childcCategory.GetCategory(item.Id);
                        item.ChildCategory = childTable;
                    }
                    Ultil.Cache.CacheHelper.Set<List<ProductCategory>>(cacheKey, parentCat, 100);
                }
                if (parentId == 0)
                {
                    return parentCat;
                }
                else
                {
                    return parentCat.Where(x => x.Id == parentId).FirstOrDefault().ChildCategory;
                }
            }
            catch (Exception)
            {
                return null;
            }

        }
        public void RemoceCategoryCache()
        {
            try
            {
                string cacheKey = AEnum.Cache.Cachingkey.AllCategoryCache.ToString() ;
                Ultil.Cache.CacheHelper.Remove(cacheKey);
                string commontDataKey = AEnum.Cache.Cachingkey.CommonInfo_FontEnd_.ToString();
                Ultil.Cache.CacheHelper.Remove(commontDataKey);
            }
            catch (Exception)
            {


            }

        }
        //public int UpdateParentCategoryRank(int CategoryId, int isPlush, int st)
        //{
        //    SqlParameter[] param = new SqlParameter[3];
        //    param[0] = new SqlParameter("@isPlush", SqlDbType.Int);
        //    param[0].Value = isPlush;
        //    param[1] = new SqlParameter("@CategoryId", SqlDbType.Int);
        //    param[1].Value = CategoryId;
        //    param[2] = new SqlParameter("@st", SqlDbType.Int);
        //    param[2].Value = st;
        //    Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
        //    return ds.executeUpdate("UPDATE_PAPENT_CAT_RANK", param);
        //}
        public int UpdateCategoryRank(int CategoryId, int isPlush)
        {
            RemoceCategoryCache();
            Dictionary<string, object> paramlist = new Dictionary<string, object>();
            paramlist.Add("@isPlush", isPlush);
            paramlist.Add("@CategoryId", CategoryId);            
            return base.ExecuteProc("[sp_UpdateArticleCategoryRank]", paramlist);
        }
        public ProductCategory GetCatById(int id)
        {
            ProductCategory rs = new ProductCategory();
            Dictionary<string, object> paramlist = new Dictionary<string, object>();
            paramlist.Add("@Id", id);
            return base.SelectSingle("getCatById", paramlist);
        }

        public int DeleteCategory(int id)
        {
            try
            {
                RemoceCategoryCache();
                Dictionary<string, object> paramlist = new Dictionary<string, object>();
                paramlist.Add("@ID", id);
                return ExecuteProc("sp_DeleteProductCategory", paramlist);
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public int UpdateCategory(int id, String name, int prioty, string shortDesc, string keyword, string thumb, int parentId)
        {

            try
            {
                RemoceCategoryCache();
                Dictionary<string, object> paramlist = new Dictionary<string, object>();                
                paramlist.Add("@ID", id);
                paramlist.Add("@CatName", name);
                paramlist.Add("@Prioty", prioty);
                paramlist.Add("@ShortDesc", shortDesc);
                paramlist.Add("@keywords", keyword);
                paramlist.Add("@thumb", thumb);
                paramlist.Add("@parentId", parentId);                

                return base.ExecuteProc("[sp_UpdateProductCategory]", paramlist);

            }
            catch (Exception)
            {
                return 0;
            }
        }
        public List<ProductCategory> GetCategory(int ParentId)
        {
            try
            {
                Dictionary<string, object> paramlist = new Dictionary<string, object>();
                paramlist.Add("@parentId", ParentId);
                

                return Select("sp_GET_CAT_PROC", paramlist);
            }
            catch (Exception)
            {

                return null;
            }

        }



    }
}