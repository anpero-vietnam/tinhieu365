using Models.Modul.Article;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Ultil;

namespace AModul.Article
{
    public class ArticleItemControl : AModul.Dapper.ConnectionProxy<ArticleItem>
    {
        /// <summary>
        /// trả về một bảng chứa 1 tin theo id nhập vào
        /// Hàm này đồng thời khởi tạo luôn đối tượng Tin có giá trị tương đương
        /// </summary>
        public ArticleItem GetArticleById(int id)
        {
            try
            {
                Dictionary<string, object> paramList = new Dictionary<string, object>();
                paramList.Add("@Id", id);                
                return base.SelectSingle("sp_AdminGetArticleById", paramList);
            }
            catch (Exception ex)
            {
                string s = ex.StackTrace;
                return new ArticleItem();
            }
        }

        //public DataTable TimKiem(String detail)
        //{
        //    try
        //    {
        //        SqlParameter[] paramList = new SqlParameter[1];
        //        paramList[0] = new SqlParameter("@Detail", SqlDbType.NVarChar, 150);
        //        paramList[0].Value = detail;
        //        Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
        //        return ds.executeSelect("seachs", paramList);
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}

        public List<ArticleItem> GetNewTable(int subCat, string publish, int minPrioty, string author, int currentPage, int pageSite, out int count)
        {
            try
            {
                Dictionary<string, object> paramList = new Dictionary<string, object>();
                int beginRow = (currentPage - 1) * pageSite + currentPage;
                int endRow = (currentPage - 1) * pageSite + currentPage + pageSite;

                
                paramList.Add("@subcat", subCat);
                paramList.Add("@publish", publish);
                paramList.Add("@author", author);
                paramList.Add("@beginRow", beginRow);
                paramList.Add("@EndRow", endRow);
                paramList.Add("@Prioty", minPrioty);
                return base.Select("sp_getNewTable", "@output", out count, paramList);
            }
            catch (Exception ex)
            {
                count = 0;
                return new List<ArticleItem>();
            }
        }
        public DataTable getNewTableFrontEnd(int subCat, int minPrioty, int currentPage, int pageSite, int st, out int count)
        {
            try
            {
                if (currentPage == 0) { currentPage = 1; }
                int beginRow = (currentPage - 1) * pageSite + currentPage;
                int endRow = (currentPage - 1) * pageSite + currentPage + pageSite;

                SqlParameter[] paramList = new SqlParameter[4];
                
                paramList[1] = new SqlParameter("@subcat", SqlDbType.VarChar, 10);
                paramList[1].Value = subCat;
                paramList[2] = new SqlParameter("@beginRow", SqlDbType.Int, 32);
                paramList[2].Value = beginRow;
                paramList[3] = new SqlParameter("@EndRow", SqlDbType.Int, 32);
                paramList[3].Value = endRow;
                paramList[0] = new SqlParameter("@Prioty", SqlDbType.Int, 32);
                paramList[0].Value = minPrioty;
                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                DataTable tables = ds.executeSelect("sp_getNewTable_2", paramList, out count);
                return tables;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataRow GetNewsByIdFrontEnd(int id)
        {
            try
            {
                Dictionary<string, object> paramList = new Dictionary<string, object>();
                paramList.Add("@Id", id);
                
                DataTable tables = base.SelectDataTable("GetNewById_2", paramList);
                if (tables != null && tables.Rows.Count > 0)
                {
                    return tables.Rows[0];
                }
                else { return null; }


            }
            catch (Exception)
            {
                return null;
                // throw;
            }
        }
      
        public int Addnews(string Subcategorys, string tittle, string _shortDesc, string newcontent, string imggesThumb,
            string publish, string prioty, string lang, string tag, string linkTag, int userID, int viewTime)
        {

            tittle = Ultil.StringHelper.RemoveHtmlTangs(tittle);
            newcontent = Ultil.StringHelper.RemoveScript(newcontent);


            if (newcontent.Length > 50000)
            {
                newcontent = newcontent.Substring(0, 49999);
            }
            try
            {
                Dictionary<string, object> paramList = new Dictionary<string, object>();
                int loaitins = Convert.ToInt32(Subcategorys);

                paramList.Add("@CategoryId", Subcategorys);
                paramList.Add("@Tittle", tittle);
                paramList.Add("@content", newcontent);
                paramList.Add("@Thumb", imggesThumb);                
                paramList.Add("@author", userID);
                paramList.Add("@isPublish", publish);
                paramList.Add("@prioty", prioty);
                paramList.Add("@ShortDesc", _shortDesc);
                paramList.Add("@viewTime", viewTime);
                
                paramList.Add("@lang", lang);                
                paramList.Add("@tag", tag);
                paramList.Add("@linktag", linkTag);

                return base.ExecuteProc("sp_CreateaArticle",  paramList);

            }
            catch (Exception)
            {
                throw;
                //return 0;
            }
        }

        public int UpdateNews(ArticleItem model)
        {
            model.Tittle = Ultil.StringHelper.RemoveHtmlTangs(model.Tittle);
            model.NewsDesc = Ultil.StringHelper.RemoveScript(model.NewsDesc);
            if (model.NewsDesc.Length > 50000)
            {
                model.NewsDesc = model.NewsDesc.Substring(0, 49999);
            }
            try
            {
                Dictionary<string, object> paramList = new Dictionary<string, object>();
                paramList.Add("@SubCatID", model.CategoryId);
                paramList.Add("@Tittle", model.Tittle);
                paramList.Add("@content", model.NewsDesc);
                paramList.Add("@thumb", model.Thumb);
                paramList.Add("@author", model.Author); ;
                paramList.Add("@publish", model.Publish);
                paramList.Add("@prioty", model.Prioty);
                paramList.Add("@viewTime", model.ViewTime);
                paramList.Add("@lang", model.Lang);
                paramList.Add("@ShortDesc", model.ShortDesc);
                paramList.Add("@tag", model.Tag);
                paramList.Add("@linktag", string.Empty);
                paramList.Add("@id", model.Id);
                
                return base.ExecuteProc("[sp_AdminUpdateNews]", paramList);

            }
            catch (Exception)
            {
                throw;
                //return 0;
            }
        }

        public int DeleteNew(int id)
        {
            try
            {
                Dictionary<string, object> paramList = new Dictionary<string, object>();
                paramList.Add("@Id", id);
                
                return base.ExecuteProc("delNewsById", paramList);
                
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }

}