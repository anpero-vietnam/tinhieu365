using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendMailWinService
{
    public partial class MainComonent : Component
    {
        public MainComonent()
        {
            InitializeComponent();
        }

        public MainComonent(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

     
        //public static void SendMail()
        //{

        //    int sendMailTime = 0;
        //    Dal.Massage ms = new Dal.Massage();
        //    string mailList = "";
        //    int timeInterval = Convert.ToInt32(ConfigurationManager.AppSettings["timerIntervalTime"]);
        //    List<Dal.Interactive.WaitingNotify> waitingMailList = new List<Dal.Interactive.WaitingNotify>();
        //    Dal.Interactive.WaitingNotifyControl c = new Dal.Interactive.WaitingNotifyControl();

            
        //        int SenderMailCount = 0;

        //        try
        //        {


        //            Ultil.SendMailConsole mails = new Ultil.SendMailConsole();
        //            waitingMailList = c.GetWaitingNotify(1);
        //            foreach (Dal.Interactive.WaitingNotify item in waitingMailList)
        //            {
        //                try
        //                {
        //                    mails.SendMail(item.From, item.To, item.Subject, item.Body, "/html/orderTemplate.html",true);
        //                    SenderMailCount += 1;
        //                    mailList += "To: " + item.To + "<br />";
        //                    mailList += "Subject: " + item.Subject + "<br />";
        //                    mailList += "-------------------------------------------------------------<br />";
        //                }
        //                catch (Exception ex)
        //                {
        //                    ms.sendMessagesToRole("addmin", false, "Lỗi từ send mail service", ex.Message, "");
        //                }
        //            }
        //            sendMailTime += 1;
        //            if (SenderMailCount > 0)
        //            {
        //                ms.sendMessagesToRole("addmin", false, "Service đã gửi thành công " + SenderMailCount + " mail", mailList, "");
        //            }
        //        }
        //        catch (Exception ex)
        //        {                
        //            ms.sendMessagesToRole("addmin", false, "Lỗi từ service gửi mail " + ex.Message, mailList, "");
        //        }
        //        waitingMailList = null;
        //        c.DeleteSenderWaitingNotify(1);                
        //    }
        
    }
}
