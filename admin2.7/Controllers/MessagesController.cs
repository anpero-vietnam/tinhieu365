using adminv2._4.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Mvc.Controllers;

namespace admin.Controllers
{
    public class MessagesController : BaseController
    {
        Dal.MessengerControl ms = new Dal.MessengerControl();
        [IsAuthenlication]
        public ActionResult Index()
        {
            SetUpAll();
            return View();
        }
        [IsAuthenlication]
        public ActionResult InBox()
        {
            try
            {
                SetUpAll();
                string id = Request.QueryString["ms"];
                Dal.MessengerControl ms = new Dal.MessengerControl();
                ViewData["msg"] = ms.GetMessengerByID(id, AppSession.CurentProfile.UserId);
                ViewData["msgWaiting"] = ms.GetMassage(AppSession.CurentProfile.UserId.ToString(), "%", "0", "%", 1, 100);
            }
            catch (Exception)
            {
                throw;
            }
            return View();
        }
        [IsAuthenlication]
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
                ViewData["msg"] = ms.GetMassage("%", uid, "%", "%", Convert.ToInt32(page), 30);
                ms.analyticMassage(AppSession.CurentProfile.UserId);
                
                ViewBag.isSender = "1";
                ViewBag.page = Ultil.StringHelper.SetUpPagedV2(Convert.ToInt32(page), 35, ms.SendMessageCount, 10, "?page=");

            }

            return View();
        }        
        
        [IsAuthenlication(RoleName ="admin")]
        [ValidateInput(false)]
        public int AdminSendMessage(string sendMail, string senderList, string tittle, string content, string sendCustomer = "0", string sendAddmin = "0", string sendMod = "0", string sendMaketing = "0", string sendAccounting = "0")
        {
            int s1 = 0;
            try
            {

                string id = AppSession.CurentProfile.UserId.ToString();
                String[] senderLists = senderList.Split('|');
                Dal.MessengerControl ms = new Dal.MessengerControl();
                if (sendCustomer.Equals("1"))
                {
                    ms.SendMessagesToRole("0", sendMail.Equals("1"), tittle, content, id);
                }
                if (sendAddmin.Equals("1"))
                {
                    ms.SendMessagesToRole("addmin", sendMail.Equals("1"), tittle, content, id);
                }
                if (sendMaketing.Equals("1"))
                {
                    ms.SendMessagesToRole("maketing", sendMail.Equals("1"), tittle, content, id);
                }
                if (sendMod.Equals("1"))
                {
                    ms.SendMessagesToRole("mod", sendMail.Equals("1"), tittle, content, id);
                }
                if (sendAccounting.Equals("1"))
                {
                    ms.SendMessagesToRole("accounting", sendMail.Equals("1"), tittle, content, id);
                }
                Dal.Interactive.WaitingNotifyControl interactive = new Dal.Interactive.WaitingNotifyControl();

                for (int i = 0; i < senderLists.Length; i++)
                {
                    if (!String.IsNullOrEmpty(senderLists[i]))
                    {
                        ms.CreateMassage(tittle, content, id, Convert.ToInt32(senderLists[i]));
                        Dal.Notify n = new Dal.Notify();
                        n.updateUserNotify(Convert.ToInt32(senderLists[i]), 0, "Tin nhắn mới", @"Bạn có thư mới <a href='/Messages/inboxs'>click vào đây để tới hòm thư</a>");

                    }
                }
                if (sendMail.Equals("1"))
                {
                    for (int i = 0; i < senderLists.Length; i++)
                    {
                        if (!String.IsNullOrEmpty(senderLists[i]))
                        {

                            using (System.IO.StreamReader reader = System.IO.File.OpenText(Server.MapPath("~/theme/html/EmptyWithFooter.html")))
                            {
                                string Htmlbody = reader.ReadToEnd();
                                Htmlbody = Htmlbody.Replace(@"{body}", content);
                                Dal.UserProfileControl u = new Dal.UserProfileControl();
                                var userInfo = u.GetUserProfileById(Convert.ToInt32(senderLists[i]));
                                String mailTo = userInfo.UserName;
                                interactive.AddWaitingNotify(1, "Anpero", mailTo, tittle, Htmlbody);

                            }


                        }
                    }
                }
            }
            catch (Exception)
            {
                s1 = 0;
            }
            return s1;
        }
        [InitializeSimpleMembership]
        [Authorize(Roles = "addmin,mod,accounting,maketing")]
        public ActionResult SendMail()
        {
            SetUpAll();

            return View();
        }
        [IsAuthenlication]
        [ValidateInput(false)]
        public int SendMessage(string senderList,string tittle,string content)
        {

            try
            {
                content = Ultil.StringHelper.GetSafeHtml(content);
                tittle = Ultil.StringHelper.GetSafeHtml(tittle);
                string[] senderLists = senderList.Split('|');
                
                StoreMng.UseControl storeControl = new StoreMng.UseControl();                
                for (int i = 0; i < senderLists.Length; i++)
                {
                    
                    //if (!String.IsNullOrEmpty(senderLists[i]) && userInThisAgen.Where(x=>x.UserId==Convert.ToInt32(senderLists[i])).Count()>0)
                    //{
                    //    ms.CreateMassage(tittle, content,senderLists[i]);
                    //    Dal.Notify n = new Dal.Notify();
                    //    n.updateUserNotify(Convert.ToInt32(senderLists[i]), 0, "Tin nhắn mới", @"Bạn có thư mới từ " + AppSession.CurentUserName);
                    //}
                }
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }


        }
    }
}