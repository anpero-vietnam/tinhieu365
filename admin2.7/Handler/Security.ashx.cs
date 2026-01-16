using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Mvc.Controllers;

namespace admin.Handler
{
    /// <summary>
    /// Summary description for Security
    /// </summary>
    public class Security : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {

            var keyRequest = context.Request["op"];
            String s1 = "";
            switch (keyRequest)
            {
                case "GetTokenData":
                    {
                        if (HttpContext.Current.User.Identity.IsAuthenticated)
                        {

                            String appId = System.Configuration.ConfigurationManager.AppSettings.Get("appId").ToString();
                            String appPass = System.Configuration.ConfigurationManager.AppSettings.Get("appPass").ToString();
                            Dal.security.Token token = new Dal.security.Token();
                            s1 = token.EncypToken(AppSession.CurentProfile.UserId, AppSession.CurentProfile.UserName, appId, appPass);
                            s1 = HttpUtility.UrlEncode(s1);
                        }
                    }
                    break;
                case "GetTokenKey":
                    {
                        if (HttpContext.Current.User.Identity.IsAuthenticated)
                        {
                            s1 = Ultil.Encyption.Encrypt(new Guid() + DateTime.Now.ToString()).Replace(@"/", "-").Replace(@"=", "-");

                        }
                    }
                    break;
            }
            context.Response.ContentType = "text/plain";
            context.Response.Write(s1);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}