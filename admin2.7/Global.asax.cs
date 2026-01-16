using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace adminv2._4
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);         
         
            System.Web.Optimization.BundleTable.EnableOptimizations = true;
        }
        //void Session_OnStart(object sender, EventArgs e)
        //{

        //}
        protected void Application_EndRequest(Object sender, EventArgs e)
        {
            if (Response.Cookies.Count > 0)
            {
                string authCookie = System.Web.Security.FormsAuthentication.FormsCookieName;
                foreach (string sCookie in Response.Cookies.AllKeys)
                {
                    if (sCookie == authCookie || "asp.net_sessionid".Equals(sCookie, StringComparison.InvariantCultureIgnoreCase) || sCookie.Equals("_tk") || sCookie.Equals("uss"))
                    {
                        //if (System.Environment.Version.Major < 2)
                        //{
                        // Force HttpOnly to be added to the cookie header under 1.x
                        //Response.Cookies[sCookie].Path += ";HttpOnly";
                        Response.Cookies[sCookie].Secure = true;
                        Response.Cookies[sCookie].HttpOnly = true;
                        //}
                    }
                    //Response.Cookies[s].Secure = true;                
                }
            }
        }
        protected void Application_Error(object sender, EventArgs e)
        {
            try
            {
                Exception exception = Server.GetLastError();
                var httpException = exception as System.Web.HttpException;
                var httpCode = httpException.GetHttpCode();
                if (httpCode != 404)
                {
                    string userBrowser = System.Web.HttpContext.Current.Request.Browser.Type;
                    string requestUrl = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
                    Dal.MessengerControl ms = new Dal.MessengerControl();
                    string errMsg = "Có lỗi từ Application_Error<br>";
                    errMsg += "Client Browser: " + userBrowser + "<br>";
                    errMsg += "Request Url: " + requestUrl + "<br>";
                    errMsg += "Message: " + exception.Message + "<br>";
                    errMsg += exception.StackTrace.Replace("\n", @"<br>") + "<br>";

                    ms.SendMsgToAdmin(errMsg);
                }


            }
            catch (Exception ex)
            {
            }
            //  Server.ClearError();

        }    

    }
}