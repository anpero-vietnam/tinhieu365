using AModul;
using Models;
using Models.Modul.Product;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontEnd.Controllers
{
    
    
    public class ProjectController : BaseController
    {
        Lazy<TransactionControl> transactionLazy = new Lazy<TransactionControl>();
        public ActionResult Index(string keyword)
        {
            var model = transactionLazy.Value.SearchBussiness(keyword);
            ViewBag.SearhcTitle = "Tìm kiếm từ khóa " + keyword;
            ViewBag.Title = "Tìm kiếm từ khóa " + keyword;
            return View("Project/List", model);
        }
        public ActionResult Filter()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        public JsonResult AjaxSearch(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                var model = transactionLazy.Value.SearchBussiness(keyword);
                return Json(model);
            }
            else
            {
                return Json(new List<TransactionModel>());
            }
        }
        [IsAuthenlication(RoleName = "admin")]
        public ActionResult CreateNew(string ticker)
        {            
            var model = transactionLazy.Value.GetBussinessFromKiker(ticker)??new TransactionModel();
            if (string.IsNullOrEmpty(model.Ticker))
                model.Ticker = ticker;
            return View(model);
        }
        [ValidateInput(false)]
        [IsAuthenlication(RoleName = "admin")]
        public int Update(TransactionModel modal)
        {
            return transactionLazy.Value.UpdateBussiness(modal);
            
        }
        
    }
}