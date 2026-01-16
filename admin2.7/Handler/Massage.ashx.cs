using Models.Notify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Mvc.Controllers;

namespace admin.Handler
{
    /// <summary>
    /// Summary description for Massage
    /// </summary>
    public class Massage : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var keyRequest = context.Request["op"];
            string senderList = context.Request["senderList"];
            string tittle = Ultil.StringHelper.RemoveHtmlTangs(context.Request["tittle"]);
            string content = Ultil.StringHelper.RemoveScript(context.Request["content"]);
            string sendMail = context.Request["sendMail"];
            string sendCustomer = context.Request["sendCustomer"];
            string sendAddmin = context.Request["sendAddmin"];
            string sendMaketing = context.Request["sendMaketing"];
            string sendMod = context.Request["sendMod"];
            string sendAccounting = context.Request["sendAccounting"];


            string s1 = "";

            bool isAuthen = AppSession.CurentProfile.IsAuthenlication;

            switch (keyRequest)
            {
                #region admin add msg

                #endregion admin add msg
               
                case "sendMail":
                    {
                        if (isAuthen)
                        {
                            try
                            {
                                String[] senderLists = senderList.Split(',');
                                Dal.MessengerControl ms = new Dal.MessengerControl();
                                for (int i = 0; i < senderLists.Length; i++)
                                {
                                    if (!String.IsNullOrEmpty(senderLists[i]))
                                    {
                                        Ultil.SendMail mails = new Ultil.SendMail();
                                        String mailTo = senderLists[i];
                                        if (Ultil.StringHelper.isEmail(mailTo))
                                        {
                                            mails.sendMail("Hệ thống chăm sóc khách hàng", mailTo, "", "", tittle, content, @"/theme/html/lienHeTemplate.html");
                                        }

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
                case "findMs":
                    {
                        if (isAuthen)
                        {
                            try
                            {
                                string id = AppSession.CurentProfile.UserId.ToString();
                                Dal.MessengerControl ms = new Dal.MessengerControl();
                                List<UserMessenge> table = ms.GetMassage(id, "%", "%", tittle, 1, 20);
                                foreach (var item in table)
                                {
                                    s1 += @"<label><i class='icon-search'></i> <a href='/messages/inbox?ms=" + item.Id + "'>" + item.Title + @"</a></label>";
                                }
                            }
                            catch (Exception)
                            {
                                s1 = "0";
                            }
                        }
                        break;
                    }
                case "delMsg":
                    {
                        if (isAuthen)
                        {

                            try
                            {
                                var msgId = context.Request["id"];
                                String id = AppSession.CurentProfile.UserId.ToString();
                                Dal.MessengerControl ms = new Dal.MessengerControl();
                                ms.DelMassageByID(msgId, id);
                                s1 = "1";

                            }
                            catch (Exception)
                            {
                                s1 = "0";
                            }
                        }
                        break;
                    }
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