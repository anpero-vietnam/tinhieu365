using adminv2._4.Filters;
using Dal.Authenlication;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;


namespace Web.Mvc.Controllers
{
    public class Base2Controller : Controller
    {
        Dal.UserProfileControl profile = new Dal.UserProfileControl();
        public void SetUpAll()
        {
            try
            {
              
                string url = Request.Url.AbsolutePath;
                if (url.Contains(@"user/curentuser") || url.Contains(@"account/UpdateProfile") || url.Contains(@"Account/Manage"))
                {
                    if (User.Identity.IsAuthenticated)
                    {
                        Dal.UserProfileControl profile = new Dal.UserProfileControl();

                        ViewData["CurrentPofile"] = profile.GetUserProfileById(AppSession.CurentProfile.UserId);
                    }
                }
                else if (System.Web.Security.Roles.IsUserInRole("addmin") && Request.QueryString["uid"] != null && Request.QueryString["uid"].ToString().Trim() != "")
                {
                    ViewData["CurrentPofile"] = profile.GetUserProfileById(Convert.ToInt32(Request.QueryString["uid"]));
                }

                if (System.Web.Security.Roles.IsUserInRole("addmin"))
                {
                    AnalyticMemberShip an = new AnalyticMemberShip();
                    an.SetUpAnalyticMemberShip();
                    ViewBag.allUserCount = an.AllUserCount;
                    ViewBag.allBlockUserUsercount = an.AllBlockUserUsercount;
                    ViewBag.allOauthUserCount = an.AllOauthUserCount;
                 

                }
                
            }
            catch (Exception)
            {
            }
        }
      
      
        public int StoreId
        {
            get
            {
                int storeId = 0;
                int.TryParse(Request.QueryString["st"], out storeId);
                if (storeId > 0)
                {
                    return storeId;
                }
                else
                {
                    int.TryParse(Request.Form["st"], out storeId);
                }
                if (storeId == 0)
                {
                    var headers = Request.Headers;
                    if (headers != null)
                    {
                        var stHearder = headers["st"];
                        if (!string.IsNullOrEmpty(stHearder))
                        {
                            int.TryParse(stHearder, out storeId);
                        }
                    }
                }
                return storeId;
            }
            set { }
        }
        // GET: Base2

    }
}