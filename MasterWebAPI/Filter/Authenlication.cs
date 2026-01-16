using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace BackendService.Filter
{
    public class IsAuthenlication : ActionFilterAttribute
    {
        public override  void OnActionExecuting(HttpActionContext filterContext)
        {
            try
            {
                
                int st = Convert.ToInt32(HttpContext.Current.Request.QueryString["AgenId"]);
                string tokenkey = HttpContext.Current.Request.QueryString["token"];
                if (st == 0)
                {
                    int.TryParse(HttpContext.Current.Request.Form["st"], out st);
                }
                if (string.IsNullOrEmpty(tokenkey))
                {
                    tokenkey = HttpContext.Current.Request.Form["token"];
                }
                tokenkey = HttpUtility.UrlDecode(tokenkey);
                if (StoreMng.Security.CheckStoreAuthenlication(st, tokenkey))
                {
                    base.OnActionExecuting(filterContext);
                    return;
                }
                else
                {
                    filterContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden) { Content = new StringContent("Token key not valid") };
                }
            }
            catch (Exception)
            {
                filterContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden) { Content = new StringContent("Token key not valid") };
            }
        }
    }
}