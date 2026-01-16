using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Mvc.Controllers;

namespace admin.Controllers
{
    [IsAuthenlication(RoleName = "CanUpdateAndAddNew")]
    public class PolicyController : BaseController
    {
        
        public ActionResult Index()
        {
            SetUpAll();
            return View();
        }
    }
}