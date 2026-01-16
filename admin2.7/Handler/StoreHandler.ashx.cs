using Dal;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Mvc.Controllers;

namespace admin.Handler
{
    /// <summary>
    /// Summary description for StoreHandler
    /// </summary>
    public class StoreHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            var keyRequest = context.Request["op"];
            string rs = "";
            UserProfileControl userProfileControl = new UserProfileControl();
            Boolean isvalid = false;
            if (UserProfileControl.IsUserInRole(AppSession.CurentProfile.UserId, AEnum.UserRole.Admin))
            {
                isvalid = true;
            }
            if (isvalid)
            {
                switch (keyRequest)
                {

                    case "CheckUser":
                        var captchas = context.Request["captcha"];
                        var userName = context.Request["userName"];

                        if (Ultil.CheckValid.ValidateCaptcha(captchas))
                        {
                            int userId = userProfileControl.GetUserProfile(userName).UserId;
                            if (userId > 0)
                            {
                                rs = userId.ToString();

                            }
                            else
                            {
                                rs = "0";
                            }
                        }
                        else
                        {
                            rs = "-1";
                        }
                        break;
                    case "GetU":
                        {
                            var userId = context.Request["userID"];

                            if (!string.IsNullOrEmpty(userId))
                            {

                                UserProfile u =userProfileControl.GetUserProfileById(Convert.ToInt32(userId));
                                rs = Newtonsoft.Json.JsonConvert.SerializeObject(u);


                            }
                            break;
                        }
                    case "addUser":
                        {
                            try
                            {
                                var _userName = context.Request["userName"];
                                var RoleList = context.Request["RoleList"];
                                var userIdParam = context.Request["userId"];
                                int userId = 0;
                                if (Convert.ToInt32(userIdParam) > 0)
                                {
                                    userId = Convert.ToInt32(userIdParam);
                                }
                                else
                                {
                                    userId = userProfileControl.GetUserProfile(_userName).UserId;
                                }
                                if (userId > 0 && !string.IsNullOrEmpty(RoleList))
                                {


                                    rs = userProfileControl.AddUserToMultiRole(RoleList, userId).ToString();

                                }
                            }
                            catch (Exception)
                            {

                                throw;
                            }

                            break;
                        }
                    case "DelUser":
                        {
                            try
                            {
                                var userIdParam = context.Request["userId"];
                                rs = userProfileControl.DeleteAllRoleOfUser(Convert.ToInt32(userIdParam)).ToString();
                            }
                            catch (Exception)
                            {

                                throw;
                            }

                            break;
                        }


                    default:
                        break;
                }
            }
            context.Response.ContentType = "text/plain";
            context.Response.Write(rs);

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