using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace FrontEnd
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            System.Web.Optimization.BundleTable.EnableOptimizations = true;
        }
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
    }
}
