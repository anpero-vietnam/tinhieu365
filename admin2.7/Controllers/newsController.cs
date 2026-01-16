using System;
using System.Web.Mvc;
using AModul.Article;
namespace Web.Mvc.Controllers
{
    [IsAuthenlication(RoleName = "CanUpdateAndAddNew")]
    public class NewsController : BaseController
    {
        ArticleCategoryControl categoryControl = new ArticleCategoryControl();
        ArticleItemControl articleControl = new ArticleItemControl();        

        public ActionResult Update( int id)
        {
            try
            {
                ViewBag.data = CreateSelectCateGory();
            }
            catch (Exception)
            {

            }
            var model = articleControl.GetArticleById(id);
            ViewBag.Title = model.Tittle;
            SetUpAll();
            return View(model);
        }
        public ActionResult AddNews()
        {
            try
            {
                ViewBag.data = CreateSelectCateGory();
            }
            catch (Exception)
            {

            }
            SetUpAll();
            return View();
        }

        public ActionResult Category()
        {
            SetUpAll();
            return View();
        }
        string CreateSelectCateGory()
        {
            string rs = "";

            ArticleCategoryControl cat = new ArticleCategoryControl();
            var itemList = cat.GetArticleCateGory();
            rs += "<div class='form-group form-md-radios'><div class=\"md-radio-list\">";
            rs += "<label>Blogs</label>";
            if (itemList.Count > 0)
            {
                foreach (var item in itemList)
                {
                    rs += "<div class=\"md-radio\">";
                    rs += "<input id='check_" + item.Id + "' type = \"radio\"  name='radiocat' value='" + item.Id + "' class='md-radiobtn'>";
                    rs += "<label for='check_" + item.Id + "'>";
                    rs += "<span></span>";
                    rs += "<span class='check'></span>";
                    rs += "<span class='box'></span>" + item.Description + "</label>";
                    rs += "</div>";
                }
            }

            rs += "</div></div>";
            return rs;
        }

        public ActionResult search()
        {
            ViewBag.data = CreateSelectCateGory();
            SetUpAll();
            return View();
        }


        [OutputCache(Duration = 600, VaryByParam = "*")]
        public ActionResult addCategory()
        {
            SetUpAll();
            return View();
        }
        //[HttpPost]
        //[IsAuthenlication]
        //public ActionResult AddNewCatagory()
        //{
        //    String name = Request["catName"];
        //    String st = Request["st"];            
        //    categoryControl.add(name, Convert.ToInt32(st));
        //    Response.Redirect("/news/addCategory?st=" + st);
        //    return View();
        //}
        //[IsAuthenlication]
        //public ActionResult tag()
        //{
        //    String id = Request.QueryString["id"].ToString();
        //    try
        //    {
        //        getTagByTag();
        //        GetNewsByTag(id);

        //    }
        //    catch (Exception)
        //    {

        //    }

        //    return View();
        //}

        //public ActionResult Index(int st, int id)
        //{

        //    String trang = "1";
        //    if (Request.QueryString["trang"] != null)
        //    {
        //        trang = Request.QueryString["trang"].ToString();
        //    }
        //    try
        //    {
        //        Dal.Themes theme = new Dal.Themes();
        //        Dal.Analytic analitic = new Dal.Analytic();

        //        int rs = tin.GetNewsByUID(id, st);
        //        if (rs > 0)
        //        {


        //        }
        //    }
        //    catch (Exception)
        //    {

        //    }

        //    return View();
        //}



    }
}
