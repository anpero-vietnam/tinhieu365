using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AModul.Dapper;
using Models.Modul.Article;

namespace AModul.Article
{
    public class ArticleCategoryControl:ConnectionProxy<ArticleCategoryModel>
    { 
        public int DelCategory(int id)
        {
            try
            {
                Dictionary<string, object> paramList = new Dictionary<string, object>();
                paramList.Add("@id", id);
                
                return base.ExecuteProc("[sp_DelArticleCategory]", paramList);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public int UpdateCategory(string tittle, int? id=null)
        {
            try
            {
                Dictionary<string, object> paramList = new Dictionary<string, object>();
                paramList.Add("@title", tittle);
                paramList.Add("@Id", id);
                
                return base.ExecuteProc("[sp_AddOrUpdateArticleCategory]",  paramList);
            }
            catch (Exception)
            {
                throw;
            }
        }    
        public List<ArticleCategoryModel> GetArticleCateGory()
        {
            try
            {
                Dictionary<string, object> paramList = new Dictionary<string, object>();
                
                return base.Select("sp_GetArticeCategory", paramList);
            }
            catch (Exception)
            {
                return new List<ArticleCategoryModel>();
            }
        }
        
    }
}
