using AModul;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontEnd.Controllers
{
    public class SymbolsController : BaseController
    {
        // GET: Symbols
        public ActionResult Index(string ticker)
        {
            TransactionControl transaction = new TransactionControl();
            var model = transaction.GetBussinessFromKiker(ticker)??new TransactionModel();
            ViewBag.Action = "theo dõi";
            if (model.RSI <= 30 && model.ADX > 25 && model.MFI <= 20)
            {
                ViewBag.Action = "Xem xét mua";
            }
            if (model.RSI >= 70 && model.ADX > 25 && model.MFI >= 80)
            {
                ViewBag.Action = "Xem xét bán";
            }
            if (string.IsNullOrEmpty(model.Ticker))
            {
                model.Ticker = ticker;
            }
            return View(model);
        }
        
    
    }
}