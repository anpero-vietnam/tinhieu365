using Dal.Dapper;
using Models.Notify;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Ultil;

namespace Dal
{
    public class MessengerControl : ConnectionProxy<UserMessenge>
    {
        public int SendMessagesToRole(string role, bool isSendMail, string title, string content, string from)
        {
            return SendMessagesToRoleAsync(role, isSendMail, title, content,from).Result;
        }
        public async Task<int> SendMessagesToRoleAsync(string role, bool isSendMail, string title, string content, string from)
        {
            int j = 0;
            try
            {
                UserProfileControl u = new UserProfileControl();
                DataTable allUserInrole = u.GetUserInRole(role);
                Dal.Notify n = new Dal.Notify();
                Dal.Interactive.WaitingNotifyControl interactive = new Dal.Interactive.WaitingNotifyControl();
                if (allUserInrole != null && allUserInrole.Rows.Count > 0)
                {
                    for (int i = 0; i < allUserInrole.Rows.Count; i++)
                    {
                        Dal.MessengerControl ms = new Dal.MessengerControl();
                        var task1 = Task.Run(() =>
                        {
                            ms.CreateMassage(title, content, from, Convert.ToInt32(allUserInrole.Rows[i]["userid"]));
                        });
                        var task2 = Task.Run(() =>
                        {
                            n.updateUserNotify(Convert.ToInt32(allUserInrole.Rows[i]["userid"]), 0, "Tin nhắn mới", @"Bạn có thư mới <a href='/Messages/inboxs'>click vào đây để tới hòm thư</a>");
                        });
                        if (isSendMail)
                        {
                            if (StringHelper.isEmail(allUserInrole.Rows[i]["USERname"].ToString()))
                            {
                                
                                await Task.Run(() =>
                                {
                                    interactive.AddWaitingNotify(1, "Anpero", allUserInrole.Rows[i]["USERname"].ToString(), title, content);
                                });                                
                            }
                        }
                        j += 1;
                        Task.WaitAll(task1,task2);   
                    }
                }
               
                return j;
            }
            catch (Exception ex)
            {
                Dal.MessengerControl ms = new Dal.MessengerControl();
                await ms.SendMessagesToRoleAsync("addmin", false, "Error from function sendMessagesToRole", ex.Message, "");
                return j;
            }
        }
        
        public async Task<int> SendMsgToAdmin(string content)
        {
            return await SendMessagesToRoleAsync("addmin", false, "Thông báo dành cho admin", content, "");
        }
        public async Task<int> SendMsgToAdminAsync(string content)
        {
            return await SendMessagesToRoleAsync("addmin", false, "Thông báo dành cho admin", content, "");
        }
        public int CreateMassage(string tittle, string content, string from, int to)
        {


            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@content", SqlDbType.NVarChar);
            param[0].Value = content;
            param[1] = new SqlParameter("@from", SqlDbType.VarChar, 100);
            param[1].Value = from;
            param[2] = new SqlParameter("@to", SqlDbType.VarChar, 100);
            param[2].Value = to;
            param[3] = new SqlParameter("@reader", SqlDbType.VarChar, 2);
            param[3].Value = "0";
            param[4] = new SqlParameter("@datesend", SqlDbType.VarChar, 300);
            param[4].Value = Ultil.Times.GetyyyyMMddhhmmNow();
            param[5] = new SqlParameter("@tittle", SqlDbType.NVarChar, 500);
            param[5].Value = tittle;
            Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
            return ds.executeUpdate("createMassage", param);
        }
        public int createMassageV2(String tittle, String content, String from, String to, string senderName, String reveiceName)
        {


            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@content", SqlDbType.NVarChar);
            param[0].Value = content;
            param[1] = new SqlParameter("@from", SqlDbType.VarChar, 100);
            param[1].Value = from;
            param[2] = new SqlParameter("@to", SqlDbType.VarChar, 100);
            param[2].Value = to;
            param[3] = new SqlParameter("@reader", SqlDbType.VarChar, 2);
            param[3].Value = "0";
            param[4] = new SqlParameter("@datesend", SqlDbType.VarChar, 300);
            param[4].Value = Ultil.Times.GetyyyyMMddhhmmNow();
            param[5] = new SqlParameter("@tittle", SqlDbType.NVarChar, 500);
            param[5].Value = tittle;
            Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
            return ds.executeUpdate("CreateMassage", param);
        }
        //public int UpdateMassage(int id, String isReader)
        //{


        //    SqlParameter[] param = new SqlParameter[3];
        //    param[0] = new SqlParameter("@Id", SqlDbType.Int, 32);
        //    param[0].Value = id;
        //    param[1] = new SqlParameter("@dateRead", SqlDbType.VarChar, 100);

        //    param[1].Value = Ultil.Times.GetyyyyMMddhhmmNow();
        //    param[2] = new SqlParameter("@reader", SqlDbType.VarChar, 2);

        //    param[2].Value = isReader;

        //    Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
        //    return ds.executeUpdate("updateMassage", param);
        //}
        public List<UserMessenge> GetMassage(string to, string From, string reader, string tittle, int currentPage, int pageSite)
        {
            int beginRow = (currentPage - 1) * pageSite + currentPage;
            int endRow = (currentPage - 1) * pageSite + currentPage + pageSite;
            Dictionary<string, object> ParamList = new Dictionary<string, object>();
            ParamList.Add("@to", to);
            ParamList.Add("@from", From);
            ParamList.Add("@reader", reader);
            ParamList.Add("@Title", tittle);
            ParamList.Add("@beginRow", beginRow);
            ParamList.Add("@EndRow", endRow);
            return Select("getMassge",ParamList);
        }
        public void analyticMassage(int to)
        {

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@to", SqlDbType.NVarChar);
            param[0].Value = to;
            Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
            DataTable table = ds.executeSelect("analyticMassage", param);
            ReaderMassageCount = Convert.ToInt32(table.Rows[0]["readerCount"]);
            WaitingMassageCount = Convert.ToInt32(table.Rows[0]["waitingCount"]);
            SendMessageCount = Convert.ToInt32(table.Rows[0]["senderCount"]);
        }
        public Models.Notify.UserMessenge GetMessengerByID(string id, int uid)
        {
            try
            {
                Dictionary<string, object> ParamList = new Dictionary<string, object>();
                ParamList.Add("Id", id);
                ParamList.Add("uid", uid);
                return base.SelectSingle("getMassageByID",ParamList);
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public int DelMassageByID(String id, String uid)
        {
            Dictionary<string, object> ParamList = new Dictionary<string, object>();
            ParamList.Add("Id", id);
            ParamList.Add("uid", uid);
            return base.ExecuteProc("DelMassageByID",ParamList);
        }


        public int SendMessageCount { get; set; }
        public int ReaderMassageCount { get; set; }

        public int WaitingMassageCount { get; set; }
        public int Id { get; set; }

        public string Tittle { get; set; }

        public string Content { get; set; }

        public string From { get; set; }
        public string To { get; set; }

        public int Reader { get; set; }

        public string Datesend { get; set; }

        public string DateRead { get; set; }
    }
}
