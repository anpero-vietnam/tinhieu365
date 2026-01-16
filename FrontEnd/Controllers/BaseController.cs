//*******************************************************************************
//
//          Anpero Confidential 
//          © Copyright Anpero - 2020.
//-------------------------------------------------------------------------------
//  Initiator: Tran Duy Thang.
//*******************************************************************************
using AModul;
using Dal;
using Models;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace FrontEnd.Controllers
{
    public class BaseController : Controller
    {
        TransactionControl transaction = new TransactionControl();
        // GET: Base
        protected override void OnActionExecuting(ActionExecutingContext context)
        {
            
            StoreMng.WebConfigControl webConfigControl = new StoreMng.WebConfigControl();
            ViewBag.NotificationCount = 0;
            if (AppSession.CurentProfile.IsAuthenlication)
            {                
                Dal.MessengerControl msg = new Dal.MessengerControl();
                var msgs = msg.GetMassage(AppSession.CurentProfile.UserId.ToString(), "%", "0", "%", 1, 10);
                ViewBag.NotificationCount = msgs==null?0: msgs.Count;
                ViewData["nofify"] = msgs;
            }
            ViewData["CommonData"] = webConfigControl.GetCommonConfig();
            List<TransactionModel> top50 = new List<TransactionModel>();
            if (!Ultil.Cache.CacheHelper.TryGet("top50",out top50)){
                top50 = transaction.SearchTransaction(new TransactionFilter());
                Ultil.Cache.CacheHelper.Set("top50", top50,30);
            }
            ViewData["Top50Modal"] = top50;
            base.OnActionExecuting(context);
        }
        public bool IsInRole(int role)
        {
            return UserProfileControl.IsUserInRole(AppSession.CurentProfile.UserId, role);
        }
        public bool ValidateCaptcha(string captcha)
        {
            string[] s = (string[])Session["capcha"];
            if (s != null && captcha.Equals(s[1]))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public class IsAuthenlication : ActionFilterAttribute
        {
            /// <summary>
            /// Admin,CanUpdateProduct,CanUpdateAndAddNew,CanSale,CanAddScriptToWeb,CanViewAllCustomer,CanUpdateTheme,CanViewAnalytic
            /// </summary>
            /// <param name="filterContext"></param>
            public string RoleName { get; set; }

            UserProfileControl userProfileControl = new UserProfileControl();

            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                var currentProfile = userProfileControl.GetUserProfileFromCookie();
                if (currentProfile != null && currentProfile.IsAuthenlication)
                {

                    if (!string.IsNullOrEmpty(RoleName) && !UserProfileControl.IsUserInRole(currentProfile.UserId, AEnum.UserRole.GetRoleIdByName(RoleName)))
                    {
                        filterContext.Result = new HttpUnauthorizedResult();
                    }
                }
                else
                {
                    filterContext.Result = new HttpUnauthorizedResult();
                }
                base.OnActionExecuting(filterContext);
            }


        }
        public string GetTokenKey()
        {
            Dal.security.Token tk = new Dal.security.Token();
            //int uid = AppSession.CurentProfile.UserId;
            string appId = System.Configuration.ConfigurationManager.AppSettings.Get("appId").ToString();
            string appPass = System.Configuration.ConfigurationManager.AppSettings.Get("appPass").ToString();
            //string currentUser = AppSession.CurentProfile.UserId.ToString();
            Dal.security.Token token = new Dal.security.Token();
            string tokenKey = token.EncypToken(AppSession.CurentProfile.UserId, AppSession.CurentProfile.UserName, appId, appPass);
            return HttpUtility.UrlEncode(tokenKey);
        }
        public void ClearUserCookieUser()
        {
            Ultil.CookieHelper.RemoveCookie("_tk");
        }
        public static class PageContent
        {
            public static string Slide = "slide";
            public static string Ads1 = "ads1";
            public static string Ads2 = "ads2";
            public static string Ads3 = "ads3";
            public static string Ads4 = "ads4";
            public static string Ads5 = "ads5";
            public static string Ads6 = "ads6";

        }
    }
}