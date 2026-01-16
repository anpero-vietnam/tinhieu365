using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dal.Dapper;
namespace Dal.Interactive
{
   public class WaitingNotifyControl: ConnectionProxy<WaitingNotify>
    {
        /// <summary>
        /// thangtd.hn@gmail.com 14/7/2017
        /// </summary>
        /// <param name="type">1 is mail,2 is msg</param>
        /// <returns></returns>
        public List<WaitingNotify> GetWaitingNotify(int type)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@type", SqlDbType.Int);
                param[0].Value = type;
                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                List<WaitingNotify> WaitingNotifyList = new List<WaitingNotify>();
                DataTable table = ds.executeSelect("GetWaitingNotify", param);
                if (table != null && table.Rows.Count > 0)
                {
                    foreach (DataRow item in table.Rows)
                    {
                        WaitingNotify wn = new WaitingNotify();
                        wn.From = item["sendName"].ToString();
                        wn.To = item["sendto"].ToString();
                        wn.Subject = item["subject"].ToString();
                        wn.Body = item["body"].ToString();
                        wn.Types = Convert.ToInt32(item["type"].ToString());
                        WaitingNotifyList.Add(wn);
                    }
                }
                return WaitingNotifyList;
            }
            catch (Exception ex)
            {
                Dal.MessengerControl ms = new Dal.MessengerControl();
                var x= ms.SendMessagesToRoleAsync("addmin", false, "Lỗi từ GetWaitingNotify() của mail service console", ex.Message, "0").Result;
                return null;
            }
           
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">1 : sendMail, 2 : sendSms</param>
        /// <param name="senderName"></param>
        /// <param name="sendTo"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public int AddWaitingNotify(int type,string senderName,String sendTo,string subject,string body)
        {
            Dictionary<string, object> ParamList = new Dictionary<string, object>();
            ParamList.Add("@type", type);
            ParamList.Add("@sendName", senderName);
            ParamList.Add("@SendTo", sendTo);
            ParamList.Add("@subject", subject);
            ParamList.Add("@body", body);
            return base.ExecuteProc("AddWaitingNotify_proc",ParamList);            
        }
        public DataTable DeleteSenderWaitingNotify(int type)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@type", SqlDbType.NVarChar, 100);
            param[0].Value = type;
            Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
            return ds.executeSelect("DeleteSenderWaitingNotify", param);
        }
    }
      public  class WaitingNotify
      {
        public WaitingNotify()
        {
            Body = "";
            From = "";
            Subject = "";
            To = "";
            Types = 0;
        }
        string from, to, subject, body;
        int types;

        public string Body
        {
            get
            {
                return body;
            }

            set
            {
                body = value;
            }
        }

        public string From
        {
            get
            {
                return from;
            }

            set
            {
                from = value;
            }
        }

        public string Subject
        {
            get
            {
                return subject;
            }

            set
            {
                subject = value;
            }
        }

        public string To
        {
            get
            {
                return to;
            }

            set
            {
                to = value;
            }
        }

        public int Types
        {
            get
            {
                return types;
            }

            set
            {
                types = value;
            }
        }
    }
}
