using AEnum;
using AModul;
using AModul.Article;
using AModul.Common;
using Models;
using Models.Modul.Common;
using Models.Modul.Product;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FrontEnd.Controllers
{
    public class HomeController : BaseController
    {
        public async Task<ActionResult> Index()
        {
            
            Dal.MessengerControl Massage2 = new Dal.MessengerControl();
            AModul.Product.ProductControl productControl = new AModul.Product.ProductControl();
            //SearchProductFilter filter = new SearchProductFilter();
            //filter.IsPublish = true;
            //filter.CreateBy = 0;
            //filter.Page = 1;
            //filter.ItemPerPage = 15;
            //filter.Rank = 2;
            SearchResult model = new SearchResult();
            Task task1 = SetUpSlideAds();
           
            //Task task2 = Task.Run(() =>
            //{
            //    model = productControl.SearchProject(filter);
            //});
            Task task3 = Task.Run(() =>
            {
                GetFeatureArticle();
            });
            Task task4 = Task.Run(() =>
            {
                GetOversold();
            });
            Task task5 = Task.Run(() =>
            {
                GetSurfSignal();
            });
            
            await Task.WhenAll(task1, task3, task4, task5);

            return View(model);
        }
        public ActionResult Search(TransactionFilter filter)
        {            
            var model = (List<TransactionModel>)ViewData["Top50Modal"];
            ViewBag.SearhcTitle = "Top 50 khối lượng giao dịch: ngày " + model[0].CreateDate.ToString("dd/MM/yyyy");
            return View("Project/list", model);
        }
        private void GetOversold()
        {
            TransactionControl transaction = new TransactionControl();
            
            List<TransactionModel> model = new List<TransactionModel>();
            if (!Ultil.Cache.CacheHelper.TryGet("GetOversold", out model))
            {
                model = transaction.GetOversold();
                Ultil.Cache.CacheHelper.Set("GetOversold", model, 30);
            }
            ViewData["Oversold"] = model;
        }
        private void GetSurfSignal()
        {
            TransactionControl transaction = new TransactionControl();

            List<TransactionModel> model = new List<TransactionModel>();
            if (!Ultil.Cache.CacheHelper.TryGet("GetSurfSignal", out model))
            {   
                model = transaction.GetSurfSignal();
                Ultil.Cache.CacheHelper.Set("GetSurfSignal", model, 30);
            }
            ViewData["SurfSignal"] = model;
        }
        private async Task<bool> SetUpSlideAds()
        {

            int shortCacheTime = 5;
            AdsControl ac = new AdsControl();
            string SlideCache = "LandSlide";
            //string Ads1Cache = "LandAds1";
            List<Ads> Slide = new List<Ads>();
            List<Ads> ads1 = new List<Ads>();
            Slide = await Ultil.Cache.CacheHelper.TryGetAsync<List<Ads>>(SlideCache);
            Task task1 = Task.Run(() =>
            {
                if (Slide == null)
                {
                    Slide = ac.GetSlide(PageContent.Slide);
                    Ultil.Cache.CacheHelper.Set(SlideCache, Slide, shortCacheTime);
                }
            });
            //ads1 = await Ultil.Cache.CacheHelper.TryGetAsync<List<Ads>>(Ads1Cache);
            //var task2 = Task.Run(() =>
            //{
            //    if (ads1 == null)
            //    {
            //        ads1 = ac.GetSlide(PageContent.Ads1);
            //        Ultil.Cache.CacheHelper.Set(Ads1Cache, ads1, shortCacheTime);
            //    }
            //});
            await task1;
            //await task2;

            ViewBag.Slide = Slide;
            ViewBag.Ads1 = ads1;
            Response.Cache.SetCacheability(HttpCacheability.Public);
            return true;
        }
        private void GetFeatureArticle()
        {
            AModul.Article.ArticleItemControl articleControl = new AModul.Article.ArticleItemControl();
            int count = 0;
            ViewData["featureArticle"] = articleControl.GetNewTable(0, "1", 2, "%", 1, 6, out count);
        }
        public JsonResult GetCategory(int parentCategory)
        {
            var commonInfo = (Models.Webconfig)ViewData["CommonData"];
            if (commonInfo != null)
            {
                var childCategory = commonInfo.ProductCategoryList.Find(x => x.Id == parentCategory).ChildCategory;
                return Json(childCategory);
            }
            else
            {
                return null;
            }
        }

        public ActionResult Policy(int type)
        {
            try
            {
                WebContentControl service = new WebContentControl();
                ViewBag.HtmlContent = service.GetWebContent(type).TextContent;
                ViewBag.Title = WebContentTitle.GetTitle(type);
            }
            catch (Exception)
            {
                ViewBag.HtmlContent = "Nội dung đang được cập nhật";
            }
            return View();
        }

        public ActionResult Contact()
        {

            return View();
        }
        
        public int  CreateContact(ContactItem item, string captcha)
        {
            if (!Ultil.CheckValid.ValidateGoogleCaptcha(captcha))
            {              
                return 0;
            }
            else
            {   
                ContactControl control = new ContactControl();                
                return control.AddContact(item);
            }
            
        }
    }
}