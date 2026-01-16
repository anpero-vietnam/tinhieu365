using Models.Modul.Article;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontEnd.Controllers
{
    public class ArticleController : BaseController
    {
        AModul.Article.ArticleItemControl itemControl = new AModul.Article.ArticleItemControl();
        public ActionResult Index(int id)
        {


            var model = itemControl.GetArticleById(id);
            ViewBag.Title = model.Tittle;
            ViewBag.WebsiteUrl = Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host + Ultil.UrlHelper.GetArticleLink(model.Tittle, model.Id);
            GetFeatureArticle();
            GetRelateArticle(model.CategoryId);
            return View(model);
        }
        private void GetFeatureArticle()
        {
            string cacheKey = "FeatureArticlekey";
            int count = 0;
            var rs = new List<ArticleItem>();
            if (!Ultil.Cache.CacheHelper.TryGet(cacheKey, out rs))
            {
                rs = itemControl.GetNewTable(0, "1", 1, "%", 1, 12, out count);
            }
            ViewData["featureArticle"] = rs;
        }
        private void GetRelateArticle(int categoryID)
        {
            string cacheKey = "FeatureArticlekey";
            int count = 0;
            var rs = new List<ArticleItem>();
            if (!Ultil.Cache.CacheHelper.TryGet(cacheKey, out rs))
            {
                rs = itemControl.GetNewTable(categoryID, "1", 1, "%", 1, 6, out count);
            }
            ViewData["RelateArticle"] = rs;
        }
        public ActionResult Category(int id=0, int page = 1)
        {

            int count = 0;
            var model = itemControl.GetNewTable(id, "1", 0, "%", page, 16, out count);
            ViewBag.page = Ultil.StringHelper.SetUpPagedV2(page, 16, count, 10, "?page=");            
            if (id ==0)
            {             
                ViewBag.Title = "Article";
            }
            else if (model.Count > 0)
            {
                ViewBag.Title = model[0].CateGoryName;
                ViewBag.CategoryName = model[0].CateGoryName;
            }
            return View(model);
        }
    }
}
