using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace admin.Handler
{
    /// <summary>
    /// Summary description for PaymentConfigHandler
    /// </summary>
    public class PaymentConfigHandler : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            //var keyRequest = context.Request["op"];
            //String rs = "";
            //int uid = WebMatrix.WebData.WebSecurity.CurrentUserId;
            //bool isvalid = false;
            //int storeID = Convert.ToInt32(context.Request["st"].ToString());
            //if (UserProfileControl.IsUserInRole(WebMatrix.WebData.WebSecurity.CurrentUserId, storeID, AEnum.UserRole.Admin))
            //{
            //    isvalid = true;
            //}
          

            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
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