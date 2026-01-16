using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for userInfo
/// </summary>
public class UserInfo
{
	public UserInfo()
	{
		
	}
    public  int ValidateLoginCookie()
    {
        if (HttpContext.Current.Request.Cookies["uckie"] != null)
        {
            string tokenKey = HttpContext.Current.Request.Cookies["uckie"].Value;

            tokenKey = HttpUtility.UrlDecode(tokenKey).Trim().Replace(" ", "+");

            try
            {
                string tokenData = Ultil.Encyption.Decrypt(tokenKey).Replace(" ", "+");
                String[] s = tokenData.Split('|');
                //String s = timeNow + "|" + userId + "|" + userName + "|" + appId + "|" + appToken + "|" + guid + sallOfEncyption.Replace(@"|", string.Empty);
                int UserId = Convert.ToInt32(s[1]);
                string userName = s[2].Replace(@"@", @"(a)").Replace(@"/", String.Empty).Replace(@"?", String.Empty).Replace(@".", String.Empty);
                string userToken = s[4];

                userName = Ultil.StringHelper.ToURLgach(userName);
                String appId = System.Configuration.ConfigurationManager.AppSettings.Get("appId").ToString();
                int saveLoginDay = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings.Get("saveLoginDay"));
                string DomainName = System.Configuration.ConfigurationManager.AppSettings.Get("DomainName");
                Dal.security.apps apps = new Dal.security.apps();
                apps.getAppByUrl(DomainName);

                Models.TokenModel tokenModel = new Models.TokenModel();
                tokenModel.UserId = UserId;
                tokenModel.AppId = appId;
                tokenModel.Token = userToken;
                tokenModel.ExpiresInMinute = 60 * 24 * saveLoginDay;
                tokenModel.TokenType = AEnum.Inactive.TokenType.LoginToken.GetHashCode();
                tokenModel.AppSecrecKey = apps.Pass;

                Dal.security.Token tk = new Dal.security.Token();
                if (tk.Validate(tokenModel))
                {

                    HttpContext.Current.Session["cruid"] = tokenModel.UserId;
                    return tokenModel.UserId;

                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                HttpCookie myCookie = new HttpCookie("uckie");
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                HttpContext.Current.Response.Cookies.Add(myCookie);
                return 0;

            }
        }
        else
        {
            return 0;
        }
    }
    public SqlParameter[] paramlists(String value, String paramName)
    {
        SqlParameter[] paramList = new SqlParameter[1];
        paramList[0] = new SqlParameter(paramName, SqlDbType.VarChar, 100);
        paramList[0].Value = value;
        return paramList;
    }
    public SqlParameter[] paramlists4(String amount, String comemt, String sender, String recip, String paramName, String paramName2, String paramName3, String paramName4)
    {
        SqlParameter[] paramList = new SqlParameter[4];
        paramList[0] = new SqlParameter(paramName,SqlDbType.Int, 100);
        paramList[0].Value = amount;
        paramList[1] = new SqlParameter(paramName2, SqlDbType.NVarChar, 200);
        paramList[1].Value = comemt;
        paramList[2] = new SqlParameter(paramName3, SqlDbType.VarChar, 100);
        paramList[2].Value = sender;
        paramList[3] = new SqlParameter(paramName4, SqlDbType.VarChar, 100);
        paramList[3].Value = recip;
       
        return paramList;
    }
    public DataTable GetDataWithOneVar(String varchar, String procName, String paramName) {
        Dal.DatabaseAccess cn = new Dal.DatabaseAccess();
        SqlConnection scn = cn.OpenConnection();


        SqlDataAdapter sda = new SqlDataAdapter(procName,scn);
        sda.SelectCommand.CommandType = CommandType.StoredProcedure;
        sda.SelectCommand.Parameters.AddRange(paramlists(varchar, paramName));
        DataSet ds = new DataSet();
        sda.Fill(ds);
        scn.Close();
        return ds.Tables[0];
    }
    public int insertBill(String amount, String comment, String sender, String recip, String procName)
    {
        Dal.DatabaseAccess cn = new Dal.DatabaseAccess();
        SqlConnection scn = cn.OpenConnection();
        //SqlDataAdapter sda = new SqlDataAdapter(procName, scn);
        //sda.InsertCommand.CommandType = CommandType.StoredProcedure;
        //sda.SelectCommand.Parameters.AddRange(paramlists4(amount,comment,sender,recip,@"@amount",@"@coment",@"@sender",@"@recip"));

        SqlCommand cmt = new SqlCommand("createBill",scn);
        cmt.CommandType = CommandType.StoredProcedure;
        cmt.Parameters.AddRange(paramlists4(amount, comment, sender, recip, @"@amount", @"@coment", @"@sender", @"@recip"));
        //scn.Open();
        int i = cmt.ExecuteNonQuery();
        scn.Close();
        return i;
    }
}