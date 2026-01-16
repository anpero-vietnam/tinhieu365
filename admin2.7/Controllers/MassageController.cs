using adminv2._4.Filters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Mvc.Controllers
{
    public class MassageController : BaseController
    {
        //
        // GET: /Massage/

        [InitializeSimpleMembership]
        [Authorize(Roles = "addmin,mod,accounting,maketing")]
        public ActionResult Index()
        {
            //setUpAll();
            return View();
        }
        [InitializeSimpleMembership]
        //[Authorize(Roles = "addmin,mod,accounting,maketing")]
        public ActionResult InBox()
        {
            try
            {
                SetUpAll();
                string uid = AppSession.CurentProfile.UserId.ToString();
                string id = Request.QueryString["ms"];
                if (id !=null)
                {

                    Dal.MessengerControl ms = new Dal.MessengerControl();
                    try
                    {


                        if (uid != null && ms.From.Equals(uid) || ms.To.Equals(uid))
                        {
                            ViewData["msg"] = ms.GetMessengerByID(id, AppSession.CurentProfile.UserId); ;
                            
                            ViewData["msgWaiting"] = ms.GetMassage(uid, "%", "0", "%", 1, 12);
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            catch (Exception)
            {

            }
          

            return View();
        }
        [InitializeSimpleMembership]
        [Authorize(Roles = "addmin,mod,accounting,maketing")]
        public ActionResult InBoxs()
        {
            SetUpAll();
            String uid = AppSession.CurentProfile.UserId.ToString();
            String page = Request.QueryString["page"];
            if (String.IsNullOrEmpty(page))
            {
                page = "1";
            }
            if (!String.IsNullOrEmpty(uid))
            {


                Dal.MessengerControl ms = new Dal.MessengerControl();           
                ViewData["msg"] = ms.GetMassage(uid, "%", "%", "%", Convert.ToInt32(page), 30);
                ms.analyticMassage(AppSession.CurentProfile.UserId);
                
                ViewBag.isSender = "0";
                ViewBag.page = Ultil.StringHelper.SetUpPagedV2(Convert.ToInt32(page), 35, ms.ReaderMassageCount + ms.WaitingMassageCount, 10, "?page=");
           
            }

            return View();
        }
        [InitializeSimpleMembership]
        [Authorize(Roles = "addmin,mod,accounting,maketing")]
        public ActionResult Sender()
        {
            SetUpAll();
            String uid = AppSession.CurentProfile.UserId.ToString();
            String page = Request.QueryString["page"];
            if (String.IsNullOrEmpty(page))
            {
                page = "1";
            }
            if (!String.IsNullOrEmpty(uid))
            {


                Dal.MessengerControl ms = new Dal.MessengerControl();
                ViewData["msg"] = ms.GetMassage("%",uid, "%", "%", Convert.ToInt32(page), 30);
                ms.analyticMassage(AppSession.CurentProfile.UserId);
                
                ViewBag.isSender = "1";
                ViewBag.page =  Ultil.StringHelper.SetUpPagedV2(Convert.ToInt32(page), 35, ms.SendMessageCount, 10, "?page=");

            }

            return View();
        }
        [InitializeSimpleMembership]
        [Authorize(Roles = "addmin,mod,accounting,maketing")]
        public ActionResult SendMail()
        {

            SetUpAll();
            return View();
        }
    }
}
