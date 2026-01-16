// using Facebook;
using Dal;
using GemBox.Spreadsheet;
using Microsoft.Ajax.Utilities;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ultil.Cache;
namespace Web.Mvc.Controllers
{
    public class BaseController : Controller
    {
        public bool IsInRole(int role)
        {
            return UserProfileControl.IsUserInRole(AppSession.CurentProfile.UserId, role);
        }
        //AEnum.UserRole.CanViewAllCustomer

        public void ClearUserCookieUser()
        {
            Session.Remove("cruid");
            HttpCookie myCookie = new HttpCookie("uckie");
            myCookie.Expires = DateTime.Now.AddDays(-1d);
            Response.Cookies.Add(myCookie);
        }
        //
        // GET: /base/
        public string GetTokenKey()
        {
            Dal.security.Token tk = new Dal.security.Token();
            //int uid = AppSession.CurentProfile.UserId;
            string appId = System.Configuration.ConfigurationManager.AppSettings.Get("appId").ToString();
            string appPass = System.Configuration.ConfigurationManager.AppSettings.Get("appPass").ToString();
            //string currentUser = AppSession.CurentProfile.UserId.ToString();
            Dal.security.Token token = new Dal.security.Token();
            string tokenKey = token.EncypToken(AppSession.CurentProfile.UserId, AppSession.CurentProfile.UserName, appId, appPass);
            return HttpUtility.UrlEncode(tokenKey);
        }

        public void SetUpAll()
        {

        }


        public Boolean ValidateCaptcha(string captcha)
        {
            String[] s = (String[])Session["capcha"];

            if (s != null && captcha.Equals(s[1]))
            {
                return true;
            }
            else
            {

                return false;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ReportName"></param>
        /// <param name="dataTable"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="ignoreColunm"></param>
        /// <param name="mapingColunm">oldName2>newName2,oldName>newName</param>
        public void Export(string ReportName, DataTable dataTable, DateTime fromDate, DateTime toDate, List<string> ignoreColunm, string mapingColunm)
        {
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                ReportName = ReportName.Replace("\r\n", "");

                string exportFileName = ReportName.Replace("&", "").Replace(" ", "-").Replace("_", "").Replace("'", "");
                
                string xlsName = Guid.NewGuid().ToString() + ".xlsx";
                string forderPath = Server.MapPath(@"~/Content/exportexcel");
                string fileName = forderPath + xlsName;

                if (!System.IO.Directory.Exists(forderPath))
                {
                    System.IO.Directory.CreateDirectory(forderPath);
                }
                Ultil.NpoiExcelHelper.DataTableToExcel(dataTable, fileName, ReportName, mapingColunm, ignoreColunm);                
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("content-disposition", "attachment; filename=" + exportFileName + ".xlsx");
                FileStream sourceFile = new System.IO.FileStream(fileName, FileMode.Open);
                long FileSize;
                FileSize = sourceFile.Length;
                byte[] getContent = new byte[(int)FileSize];
                System.Text.Encoding enc = System.Text.Encoding.ASCII;
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                sourceFile.Read(getContent, 0, (int)sourceFile.Length);

                Response.BinaryWrite(getContent);

                Response.Flush();
                Response.End();
                sourceFile.Close();

                //delete file after use
                System.IO.File.Delete(fileName);
            }
            else
            {
                Response.Write("Không có dữ liệu export.");
            }

        }
    }
    public class IsAuthenlication : ActionFilterAttribute
    {
        /// <summary>
        /// Admin,CanUpdateProduct,CanUpdateAndAddNew,CanSale,CanAddScriptToWeb,CanViewAllCustomer,CanUpdateTheme,CanViewAnalytic
        /// </summary>
        /// <param name="filterContext"></param>
        public string RoleName { get; set; }
        
        UserProfileControl userProfileControl = new UserProfileControl();
        
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var currentProfile = userProfileControl.GetUserProfileFromCookie();
            if (currentProfile != null && currentProfile.IsAuthenlication)
            {
                
                if (!string.IsNullOrEmpty(RoleName) && !UserProfileControl.IsUserInRole(currentProfile.UserId,AEnum.UserRole.GetRoleIdByName(RoleName)))
                {
                    filterContext.Result = new HttpUnauthorizedResult();
                }
            }
            else
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
            base.OnActionExecuting(filterContext);
        }

      
    }

}
