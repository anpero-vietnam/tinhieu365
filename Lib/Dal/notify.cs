using Dal.Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Models.Notify;
namespace Dal
{
    public class Notify : ConnectionProxy<SysNotify>
    {

        int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        String userId;

        public String UserId
        {
            get { return userId; }
            set { userId = value; }
        }
        String notifyTime;

        public String NotifyTime
        {
            get { return notifyTime; }
            set { notifyTime = value; }
        }
        String tittle;

        public String Tittle
        {
            get { return tittle; }
            set { tittle = value; }
        }
        String content;

        public String Content
        {
            get { return content; }
            set { content = value; }
        }
        int isLock;

        public int IsLock
        {
            get { return isLock; }
            set { isLock = value; }
        }
        public Notify()
        {
            id = 0;
            userId = null;
            notifyTime = null;
            tittle = null;
            content = null;
            isLock = 1;
        }
        public void getUserNotify(int Uid, int islocked)
        {
            SqlParameter[] paramList = new SqlParameter[2];
            paramList[0] = new SqlParameter("@userId", SqlDbType.VarChar, 10);
            paramList[0].Value = Uid;
            paramList[1] = new SqlParameter("@isblock", SqlDbType.Int, 32);
            paramList[1].Value = islocked;
            Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
            DataTable tables = ds.executeSelect("getUserNotify", paramList);
            if (tables != null && tables.Rows.Count > 0)
            {
                Id = Convert.ToInt32(tables.Rows[0]["Id"]);
                Tittle = tables.Rows[0]["tittle"].ToString();
                Content = tables.Rows[0]["content"].ToString();
                NotifyTime = tables.Rows[0]["notifyTime"].ToString();
                IsLock = Convert.ToInt32(tables.Rows[0]["isLock"]);
            }

        }
        public void updateUserNotify(int Uid, int islocked, String tittle, String content)
        {
            try
            {
                SqlParameter[] paramList = new SqlParameter[4];
                paramList[0] = new SqlParameter("@userId", SqlDbType.VarChar, 10);
                paramList[0].Value = Uid;
                paramList[1] = new SqlParameter("@tittle", SqlDbType.NVarChar, 100);
                paramList[1].Value = tittle;
                paramList[2] = new SqlParameter("@content", SqlDbType.NVarChar, 500);
                paramList[2].Value = content;
                paramList[3] = new SqlParameter("@isblock", SqlDbType.Int, 32);
                paramList[3].Value = islocked;
                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                ds.executeUpdate("createUserNotify", paramList);
            }
            catch (Exception ex)
            {
                
            }
        }
    }
    public class SysNotify: ConnectionProxy<Models.Notify.SysNotify>
    {
      
        public Models.Notify.SysNotify GetNotifyById(int id)
        {
            try
            {
                Dictionary<string, object> ParamList = new Dictionary<string, object>();
                ParamList.Add("@Id", id);             
                return base.SelectSingle("sp_getNotifyById",ParamList);

            }
            catch (Exception)
            {

                return null;
            }


        }
        public List<Models.Notify.SysNotify> GetSysNotify(string islocked, string status, string userTriggerNotLike)
        {
            Dictionary<string, object> ParamList = new Dictionary<string, object>();
            ParamList.Add("@islocked", islocked);
            ParamList.Add("@status", status);
            ParamList.Add("@userTriger", userTriggerNotLike);            
            List<Models.Notify.SysNotify> rsList =base.Select("getSysNotify",ParamList);            
            return rsList;
        }
        public int AddSysNotify(String tittle, String content)
        {
            int uid = 0;
            try
            {

                SqlParameter[] paramList = new SqlParameter[3];
                paramList[0] = new SqlParameter("@tittle", SqlDbType.NVarChar, 300);
                paramList[0].Value = tittle;
                paramList[1] = new SqlParameter("@content", SqlDbType.NVarChar, 4000);
                paramList[1].Value = content;
                paramList[2] = new SqlParameter("@userTriger", SqlDbType.Int);
                paramList[2].Value = uid;
                
                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                return ds.executeUpdate("addSysNotify", paramList);

            }
            catch (Exception ex)
            {
                
                return 0;
            }


        }
        public void updateSysNotify(int id, int islocked, String tittle, String content)
        {

            try
            {

                SqlParameter[] paramList = new SqlParameter[4];
                paramList[0] = new SqlParameter("@Id", SqlDbType.Int);
                paramList[0].Value = id;
                paramList[1] = new SqlParameter("@tittle", SqlDbType.NVarChar, 300);
                paramList[1].Value = tittle;
                paramList[2] = new SqlParameter("@content", SqlDbType.NVarChar, 4000);
                paramList[2].Value = content;
                paramList[3] = new SqlParameter("@isblock", SqlDbType.Int, 32);
                paramList[3].Value = islocked;
                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                ds.executeUpdate("updateSysNotify", paramList);

            }
            catch (Exception ex)
            {
                

            }
        }
        public int updateSystemNotify(int id, int isLock, int uid)
        {

            SqlParameter[] paramList = new SqlParameter[3];
            paramList[0] = new SqlParameter("@Id", SqlDbType.Int);
            paramList[0].Value = id;
            paramList[1] = new SqlParameter("@islocked", SqlDbType.Int);
            paramList[1].Value = isLock;
            paramList[2] = new SqlParameter("@uid", SqlDbType.Int);
            paramList[2].Value = uid;
            Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
            return ds.executeUpdate("updateSystemNotify", paramList);
        }

    }
}
