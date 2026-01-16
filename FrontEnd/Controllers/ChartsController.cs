using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontEnd.Controllers
{
    public class ChartsController : Controller
    {
        // GET: Chars
        public ActionResult Index()
        {
            return View();
        }
    }
}