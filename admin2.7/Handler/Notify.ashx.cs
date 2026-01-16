using Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Mvc.Controllers;

namespace admin.Handler
{
    /// <summary>
    /// Summary description for Notify
    /// </summary>
    public class Notify : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            var keyRequest = context.Request["op"];
            String s1 = "0";
            try
            {

                Boolean isAuthen = false;
                if (UserProfileControl.IsUserInRole(AppSession.CurentProfile.UserId,  AEnum.UserRole.CanSale))
                {
                    isAuthen = true;
                }
                switch (keyRequest)
                {
                    case "getNotify":
                        {
                            if (isAuthen)
                            {
                                try
                                {
                                    int id = AppSession.CurentProfile.UserId;
                                    Dal.MessengerControl ms = new Dal.MessengerControl();

                                    Dal.SysNotify sn = new Dal.SysNotify();

                                    List<Models.Notify.SysNotify> sysNotifyList = sn.GetSysNotify("0", "%", AppSession.CurentProfile.UserId.ToString());
                                    if (sysNotifyList.Count > 0)
                                    {
                                        s1 = sysNotifyList[0].Title;
                                        foreach (var item in sysNotifyList)
                                        {
                                            sn.updateSystemNotify(item.Id, 1, id);
                                        }
                                    }
                                    else
                                    {
                                        Dal.Notify n = new Dal.Notify();
                                        n.getUserNotify(Convert.ToInt32(id), 0);
                                        if (n.IsLock == 0)
                                        {
                                            s1 = n.Content;
                                            n.updateUserNotify(id, 1, "", "");
                                        }
                                    }

                                }
                                catch (Exception)
                                {
                                    s1 = "0";
                                }
                            }
                            break;
                        }
                }
            }
            catch (Exception)
            {


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