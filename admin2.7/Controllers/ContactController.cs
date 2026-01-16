using adminv2._4.Filters;
using AModul.Common;
using Models.Modul.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Mvc.Controllers;

namespace mvcAdminV2.Controllers
{
    [IsAuthenlication(RoleName = "admin")]
    public class ContactController : BaseController
    {
         ContactControl contactControl = new ContactControl();
        //
        // GET: /Contact/

        public ActionResult Index(int page=1)
        {
            SetUpAll();
            var model = new List<ContactItem>();
            if (IsInRole(AEnum.UserRole.CanViewAllCustomer))
            {
                try
                {
                    int count = 0;
                    
                    int st = Convert.ToInt32(Request.QueryString["st"]);
                    model = contactControl.GetContact(-1, "%", page, 50,out count);
                    ViewBag.page = Ultil.StringHelper.SetUpPagedV2(page, 50, count, 10, @"?page=");
                }
                catch
                {

                }
            }
            return View(model);
        }
        public ActionResult Detail()
        {
            SetUpAll();
            var model = new ContactItem();
            if (IsInRole(AEnum.UserRole.CanViewAllCustomer))
            {
                try
                {
                    string id = Request.QueryString["id"];
                    int st = Convert.ToInt32(Request.QueryString["st"]);
                    model = contactControl.GetConTactByID(Convert.ToInt32(id), st); 
                }
                catch (Exception)
                {

                }
            }
            return View(model);
        }
        public ActionResult delmsg(int id)
        {
            if (IsInRole(AEnum.UserRole.CanViewAllCustomer))
            {

                try
                {   
                    contactControl.DelContact(Convert.ToInt32(id));
                    Response.Redirect("~/contact");
                }
                catch (Exception)
                {

                }
            }
            return View();

        }

    }
}
