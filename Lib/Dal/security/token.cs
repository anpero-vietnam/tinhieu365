using Dal.Dapper;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Dal.security
{
    public class Token : ConnectionProxy<TokenModel>
    {
        public Token()
        {
            base.ServerName = "0";
        }

        public String GetTokenKey(int uid, string userName)
        {
            String appId = System.Configuration.ConfigurationManager.AppSettings.Get("appId").ToString();
            String appPass = System.Configuration.ConfigurationManager.AppSettings.Get("appPass").ToString();
            return EncypToken(uid, userName, appId, appPass);
        }

        public string GenerateToken(int userId, String appId, String appPass, int type = 1)
        {
            //@dateCreate varchar(16)          
            String token = Ultil.Encyption.Encrypt(System.Guid.NewGuid().ToString() + "copy by anpero.com, thangtd.hn@gmail.com,+84-0906006580, tran duy thang");
            Dictionary<string, object> ParamList = new Dictionary<string, object>();
            ParamList.Add("@token", token);
            ParamList.Add("@appname", "");
            ParamList.Add("@loginId", userId.ToString());
            ParamList.Add("@appid", appId);
            ParamList.Add("@TokenType", type.ToString());
            ParamList.Add("@appPass", appPass);
            int i = ExecuteProc("GenerateToken",ParamList);
            if (i > 0)
            {
                return token;
            }
            else
            {
                return null;
            }

        }
        public string EncypToken(int userId, String userName, String appId, String appPass, int type = 1)
        {
            String sallOfEncyption = System.Configuration.ConfigurationManager.AppSettings.Get("sallOfEncyption").ToString();
            userName = userName.Replace(@"|", String.Empty);
            String appToken = GenerateToken(userId, appId, appPass, type);
            
            if (appToken != null)
            {
                String timeNow = Ultil.Times.GetYYYYMMDDHHmmssmsNow();
                String guid = Guid.NewGuid().ToString();
                String s = timeNow + "|" + userId + "|" + userName + "|" + appId + "|" + appToken + "|" + guid + sallOfEncyption.Replace(@"|", string.Empty);
                return Ultil.Encyption.Encrypt(s);
            }
            else
            {
                return null;
            }
        }
        public String DecryToken(String input, String pass)
        {
            return Ultil.Encyption.Decrypt(input);
        }
        public bool Validate(TokenModel token,bool refresh =true)
        {
            try
            {
                base.ServerName = "0";
                Dictionary<string, object> ParamList = new Dictionary<string, object>();
                ParamList.Add("@userId", token.UserId);
                ParamList.Add("@appId", token.AppId);
                ParamList.Add("@appPass", token.AppSecrecKey);
                ParamList.Add("@ExpiresInMinute", token.ExpiresInMinute);
                ParamList.Add("@TokenType", token.TokenType.ToString());
                List<TokenModel> tk = Select("getTotenByid",ParamList);

                if (tk != null && tk.Count > 0 && tk[0].Token.Equals(token.Token))
                {
                    if (refresh)
                    {
                        GenerateToken(token.UserId, token.AppId, token.AppSecrecKey, token.TokenType);
                    }
                    
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Dal.MessengerControl ms = new Dal.MessengerControl();
                var x= ms.SendMessagesToRoleAsync("addmin", false, "Dal.Validate Exception : " + ex.Message, ex.Message, "0").Result;
                return false;
            }
        }
    }
    public class apps
    {
        String appid;
        String appName;

        public String AppName
        {
            get { return appName; }
            set { appName = value; }
        }
        public String Appid
        {
            get { return appid; }
            set { appid = value; }
        }
        String url;
        public String Url
        {
            get { return url; }
            set { url = value; }
        }
        String desc;
        String pass;
        public apps()
        {
            Pass = null;
            Url = null;
            Appid = null;
            Desc = null;
            AppName = null;
        }
        public String Pass
        {
            get { return pass; }
            set { pass = value; }
        }
        public String Desc
        {
            get { return desc; }
            set { desc = value; }
        }
        public void getAppByid()
        {

            SqlParameter[] paramList = new SqlParameter[1];
            paramList[0] = new SqlParameter("@Id", SqlDbType.Int);
            paramList[0].Value = Appid;
            Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
            DataTable table = ds.executeSelect("getAppByid", paramList);
            if (table != null && table.Rows.Count > 0)
            {
                Appid = table.Rows[0]["appid"].ToString();
                Url = table.Rows[0]["appUrl"].ToString();
                Desc = table.Rows[0]["appDesc"].ToString();
                pass = table.Rows[0]["appPass"].ToString();
                AppName = table.Rows[0]["appname"].ToString();
            }
        }
        public void getAppByid(String ids)
        {
            SqlParameter[] paramList = new SqlParameter[1];
            paramList[0] = new SqlParameter("@Id", SqlDbType.Int);
            paramList[0].Value = ids;
            Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
            DataTable table = ds.executeSelect("getAppByid", paramList);
            if (table != null && table.Rows.Count > 0)
            {
                Appid = table.Rows[0]["appid"].ToString();
                Url = table.Rows[0]["appUrl"].ToString();
                Desc = table.Rows[0]["appDesc"].ToString();
                pass = table.Rows[0]["appPass"].ToString();
                AppName = table.Rows[0]["appname"].ToString();
            }
        }
        public void getAppByUrl(String url)
        {

            SqlParameter[] paramList = new SqlParameter[1];
            paramList[0] = new SqlParameter("@url", SqlDbType.VarChar, 300);
            paramList[0].Value = url;
            Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
            DataTable table = ds.executeSelect("getAppByUrl", paramList);
            if (table != null && table.Rows.Count > 0)
            {
                Appid = table.Rows[0]["appid"].ToString();
                Url = table.Rows[0]["appUrl"].ToString();
                Desc = table.Rows[0]["appDesc"].ToString();
                pass = table.Rows[0]["appPass"].ToString();
            }

        }
        public int updateApp(int id, String name, String desc, String url, String token)
        {

            SqlParameter[] paramList = new SqlParameter[5];
            paramList[0] = new SqlParameter("@Id", SqlDbType.Int);
            paramList[0].Value = id;
            paramList[1] = new SqlParameter("@appName", SqlDbType.NVarChar, 300);
            paramList[1].Value = name;
            paramList[2] = new SqlParameter("@url", SqlDbType.VarChar, 300);
            paramList[2].Value = url;
            paramList[3] = new SqlParameter("@Desc", SqlDbType.NVarChar, 300);
            paramList[3].Value = desc;
            paramList[4] = new SqlParameter("@Token", SqlDbType.NVarChar, 300);
            paramList[4].Value = token;
            Dal.DatabaseAccess ds = new Dal.DatabaseAccess();

            return ds.executeUpdate("updateApp", paramList);

        }
        public int addApp(String name, String desc, String url, String pass)
        {
            SqlParameter[] paramList = new SqlParameter[4];
            paramList[0] = new SqlParameter("@Name", SqlDbType.NVarChar, 300);
            paramList[0].Value = name;
            paramList[1] = new SqlParameter("@Desc", SqlDbType.NVarChar, 300);
            paramList[1].Value = desc;
            paramList[2] = new SqlParameter("@url", SqlDbType.NVarChar, 300);
            paramList[2].Value = url;
            paramList[3] = new SqlParameter("@pass", SqlDbType.NVarChar, 300);
            paramList[3].Value = pass;
            Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
            return ds.executeUpdate("addApp", paramList);
        }
        public int DelApp(String id)
        {
            SqlParameter[] paramList = new SqlParameter[1];
            paramList[0] = new SqlParameter("@Id", SqlDbType.Int);
            paramList[0].Value = id;
            Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
            return ds.executeUpdate("deltApp", paramList);
        }




    }
}
