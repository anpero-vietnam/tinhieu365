using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SendMailWinService
{
    public partial class SendMailService : ServiceBase
    {
        
        private static int SenderMailCount = 0;
        
        public SendMailService()
        {
            InitializeComponent();
        }
        public SendMailService(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }
        private static int ScanStateInterval
        {
            get
            {
                int i = 0;
                if (int.TryParse(ConfigurationManager.AppSettings["ScanStateIntervalInMinute"], out i))
                {
                    i = i *1000 * 60;
                    return i;
                }
                else
                {
                    return (10 * 60 * 1000);
                }
            }
        }
        protected override void OnStart(string[] args)
        {
            Dal.Massage ms = new Dal.Massage();
            ms.sendMessagesToRole("addmin", false, "Email Service start at " + DateTime.Now.ToLongDateString(), "", "");
            
            Thread sendMailThread = new Thread(new ThreadStart(SendMail));
            sendMailThread.Start();
        }
        protected override void OnStop()
        {
            Dal.Massage ms = new Dal.Massage();
            ms.sendMessagesToRole("addmin", false, "Email Service Stop at " + DateTime.Now.ToLongDateString(), "", "");
        }

        public static void SendMail()
        {
            Ultil.SendMailConsole mails = new Ultil.SendMailConsole();

            int sendMailTime = 0;
            Dal.Massage ms = new Dal.Massage();

            string mailList = "";
            List<Dal.Interactive.WaitingNotify> waitingMailList = new List<Dal.Interactive.WaitingNotify>();
            Dal.Interactive.WaitingNotifyControl c = new Dal.Interactive.WaitingNotifyControl();

            while (true)
            {
                
                try
                {

                    waitingMailList = c.GetWaitingNotify(1);
                    foreach (Dal.Interactive.WaitingNotify item in waitingMailList)
                    {
                        try
                        {
                            mails.SendMail(item.From, item.To, item.Subject, item.Body, "/html/orderTemplate.html",true);
                            SenderMailCount += 1;
                            mailList += "To: " + item.To + "<br />";
                            mailList += "Subject: " + item.Subject + "<br />";
                            mailList += "-------------------------------------------------------------<br />";
                            sendMailTime += 1;
                        }
                        catch (Exception ex)
                        {
                            Thread.Sleep(2 * 60 * 1000);
                            ms.sendMessagesToRole("addmin", false, "Lỗi từ send mail service", ex.Message, "");
                        }
                    }                
                    if (SenderMailCount > 0)
                    {
                        ms.SendMsgToAdmin("Service đã gửi thành công " + SenderMailCount + " mail <br /> lần thứ "+ sendMailTime +"<br />" + mailList);
                        c.DeleteSenderWaitingNotify(1);
                        SenderMailCount = 0;
                    }
                }
                catch (Exception ex)
                {
                    Thread.Sleep(2 * 60 * 1000);
                    ms.SendMsgToAdmin("Lỗi từ service gửi mail " + ex.Message);
                }
                waitingMailList = null;
               
                Thread.Sleep(ScanStateInterval);
            }
        }
    }
}
