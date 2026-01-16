using Dal;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Ultil;
using Ultil.Cache;
using Models.Modul.Common;
namespace FrontEnd.Controllers
{
    public class AccountController : BaseController
    {
        Lazy<UserProfileControl> lazyUserControl = new Lazy<UserProfileControl>();
        UserProfileControl userControl { get { return lazyUserControl.Value; } }
        Dal.UserProfileControl profileControl = new Dal.UserProfileControl();
        [IsAuthenlication]
        public ActionResult Notification(int page = 1)
        {
            Dal.MessengerControl msg = new Dal.MessengerControl();
            var msgs = msg.GetMassage(AppSession.CurentProfile.UserId.ToString(), "%", "0", "%", page, 100);
            return View(msgs);
        }
        [IsAuthenlication]
        [HttpGet]
        public ActionResult ChangePassword()
        {

            return View();
        }
        [IsAuthenlication]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public JsonResult ChangePassword(string OldPass, string NewPass, string ConfirmNewPass)
        {
            bool valid = true;
            if (string.IsNullOrEmpty(OldPass) || string.IsNullOrEmpty(ConfirmNewPass) || string.IsNullOrEmpty(NewPass))
            {
                valid = false;
                return Json(new { code = 403, messenger = "Password can not be empty" });
            }
            if (valid && userControl.Login(AppSession.CurentProfile.UserName, OldPass, false))
            {
                userControl.ResetPassword(AppSession.CurentProfile.UserId, NewPass);
                return Json(new { code = 200, messenger = "Change password successful" });
            }
            else
            {
                return Json(new { code = 403, messenger = "Old password do not match" });
            }
        }
        [IsAuthenlication]
        public ActionResult MyProfile()
        {

            return View();

        }
        public ActionResult ForgetPassword()
        {

            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ForgetPasswords(string mail, string captcha)
        {

            Models.UserProfile userModel = new Models.UserProfile();
            bool valids = true;
            if (!CheckValid.ValidateGoogleCaptcha(captcha))
            {
                valids = false;
                return Json(new Models.JsonResultModel() { Code = 403, Messenger = "Please click reCapcha check box" });
            }
            try
            {

                if (!StringHelper.isEmail(mail))
                {
                    valids = false;
                    return Json(new Models.JsonResultModel() { Code = 403, Messenger = "Invalid email address" });
                }
                else
                {
                    userModel = profileControl.GetUserProfile(mail);

                }

                if (userModel == null || userModel.UserId == 0)
                {
                    valids = false;
                    return Json(new Models.JsonResultModel() { Code = 403, Messenger = "User does not exist" });
                }
                if (valids)
                {

                    string token = userControl.UpdateUserToken(userModel.UserId);

                    string DomainName = System.Configuration.ConfigurationManager.AppSettings["DomainName"];
                    string AppName = System.Configuration.ConfigurationManager.AppSettings["AppName"];
                    SendMail mails = new SendMail();
                    string resetUrl = DomainName.TrimEnd('/') + "/account/checkMail?token=" + token + @"&id=" + mail;
                    string username = string.IsNullOrEmpty(userModel.SureName) ? userModel.UserName : userModel.SureName;
                    using (StreamReader reader = System.IO.File.OpenText(Server.MapPath("~/theme/html/ChangePass.html")))
                    {
                        string Htmlbody = reader.ReadToEnd();
                        Htmlbody = Htmlbody.Replace(@"{userName}", username);
                        Htmlbody = Htmlbody.Replace(@"{resetLink}", resetUrl);
                        Htmlbody = Htmlbody.Replace(@"{email}", mail);

                        mails.sendMail(AppName + " - Confirm password change request", mail, "", "", "password recovery for mail: " + mail, Htmlbody, string.Empty);
                    }
                    return Json(new Models.JsonResultModel() { Code = 200, Messenger = "The system has sent a link to change password to your email, please check your spam mailbox if you don't see the incoming mail" });

                }
                else
                {
                    return Json(new Models.JsonResultModel() { Code = 403, Messenger = "User does not exist" });
                }

            }
            catch (Exception ex)
            {
                return Json(new Models.JsonResultModel() { Code = 403, Messenger = "User does not exist" });
            }
        }
        public ActionResult CheckMail(string token, string id)
        {
            var mail = id;
            Models.UserProfile userModel = new Models.UserProfile();
            var model = new Models.JsonResultModel();
            if (String.IsNullOrEmpty(mail))
            {
                model = new Models.JsonResultModel() { Code = 403, Messenger = "User does not exist" };
            }
            bool valids = true;
            try
            {

                if (!Ultil.StringHelper.isEmail(mail))
                {
                    valids = false;
                    model = new Models.JsonResultModel() { Code = 403, Messenger = "Invalid email" };
                }
                else
                {
                    userModel = profileControl.GetUserProfile(mail);
                }

                if (userModel.UserId <= 0)
                {

                    valids = false;
                    model = new Models.JsonResultModel() { Code = 403, Messenger = "User does not exist" };
                }
                if (valids && userControl.CheckTokenUser(userModel.UserId, token))
                {
                    SendMail mails = new SendMail();

                    string newPass = Ultil.StringHelper.GetRandomString(8, false);
                    StringBuilder body = new StringBuilder("<div>New password is here : <h2 style='color:red;'>New password: ");
                    body.Append(newPass + "</h2></div>");
                    body.Append("<div>For your safety, please change your password right after logging in.");
                    body.Append(". And don't hesitate to contact us for prompt assistance.</div>");
                    body.Append("<div>Regards.</div>");
                    mails.sendMail("New password", mail, "", "", "password recovery for mail : " + mail, body.ToString(), @"/theme/html/lienHeTemplate.html");

                    try
                    {
                        var result = userControl.ResetPassword(userModel.UserId, newPass);
                        userControl.UpdateUserToken(userModel.UserId);
                    }
                    catch (Exception)
                    {
                        //người dùng đăng nhập bằng openid không có pass
                        model = new Models.JsonResultModel() { Code = 403, Messenger = "User does not exist" };
                    }

                    model = new Models.JsonResultModel() { Code = 200, Messenger = "Email has been sent to your inbox" };
                }
                else
                {
                    model = new Models.JsonResultModel() { Code = 200, Messenger = "The confirmation link has expired,please try again" };
                }

            }
            catch (Exception ex)
            {
                model = new Models.JsonResultModel() { Code = 200, Messenger = "The confirmation link has expired,please try again" };
            }

            return View(model);

        }
        [HttpGet]
        public ActionResult mailNotice()
        {
            return View();
        }
        public ActionResult Login(string returnUrl)
        {
            AppSession.CurentProfile.UserId = 0;
            ClearUserCookieUser();
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public async Task<JsonResult> Login(string UserName, string Password, string captcha, string returnUrl = "/")
        {
            try
            {
                bool valid = true;
                if (!Ultil.CheckValid.ValidateGoogleCaptcha(captcha))
                {
                    valid = false;
                    return Json(new { code = 403, message = "Please click reCaptcha check box" });
                }

                if (valid && userControl.Login(UserName, Password, true))
                {
                    userControl.UpdateLastLogin(AppSession.CurentProfile.UserId);
                    return Json(new { code = 200, message = "Login successful" });
                }
                else
                {
                    return Json(new { code = 403, message = "Wrong username or password.." });
                }

            }
            catch (Exception ex)
            {
                Dal.MessengerControl ms = new Dal.MessengerControl();
                await ms.SendMsgToAdmin("Error from Login function : " + UserName + "<br>" + ex.StackTrace);
            }

            return Json(new { code = 403, message = "Wrong username or password.." });
        }

        [HttpPost]
        public void LogOff(string stid)
        {
            int uid = AppSession.CurentProfile.UserId;
            try
            {
                string cacheKey = AEnum.Cache.Cachingkey.StoreListOfUser.ToString() + uid.ToString();
                CacheHelper.Remove("ROLE_OF_USER_" + uid.ToString());
                CacheHelper.Remove(cacheKey);

                ClearUserCookieUser();
            }
            catch (Exception)
            {


            }
        }

        //
        // GET: /Account/Register
        #region register

        public ActionResult Register()
        {
            if (AppSession.CurentProfile.IsAuthenlication)
            {
                Ultil.CookieHelper.RemoveCookie("_tk");
            }
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        public ActionResult Register(Models.UserProfile model, string Password, string confirmPassword)
        {
            string captcha = Request["captcha"];
            if (Ultil.CheckValid.ValidateGoogleCaptcha(captcha))
            {

                if (Ultil.StringHelper.isEmail(model.UserName) || Ultil.StringHelper.isVnPhone(model.UserName))
                {
                    Dal.UserProfileControl userControl = new Dal.UserProfileControl();
                    if (userControl.Register(model, Password))
                    {
                        userControl.Login(model.UserName, Password, true);
                        Dal.MessengerControl msg = new Dal.MessengerControl();
                        msg.CreateMassage("Welcome to tinhieu365", "Updating your profile information for project posting by clicking this <a href=\"/account/myprofile\">link</a> ", string.Empty, AppSession.CurentProfile.UserId);
                        return Json(new { code = 200, message = "Sign Up Success" });
                    }
                    else
                    {
                        return Json(new { code = 201, message = "This username already exists" });
                    }
                }
                else
                {

                    return Json(new { code = 403, message = "Invalid Emails." });
                }

            }
            else
            {
                return Json(new { code = 403, message = "Please click reCaptcha check box." });

            }

        }
        #endregion
        public ActionResult CreateAccount()
        {
            return View();
        }


        [IsAuthenlication(RoleName = "admin")]


        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }


        #endregion
        #region google login
        [HttpPost]
        public string GetGoogleLoginPage(string returnUrl)
        {
            GoogleClient client = new GoogleClient();
            string clientid = client.client_id;
            var thisUrl =Request.Url.GetLeftPart(UriPartial.Authority) + "/Account/GoogleLogincallback";
            thisUrl = thisUrl.ToLower().Replace("http://","https://");
            
            client.redirect_uri = string.IsNullOrEmpty(returnUrl) ? thisUrl : thisUrl + "?returnUrl=" + returnUrl;
            return "https://accounts.google.com/o/oauth2/v2/auth?scope=profile%20email&include_granted_scopes=true&redirect_uri=" + client.redirect_uri + "&response_type=code&client_id=" + clientid + "";

        }
        public ActionResult GoogleLogincallback(string code, string scope)
        {
            GoogleClient param = new GoogleClient();
            var thisUrl = Request.Url.GetLeftPart(UriPartial.Authority) + "/Account/GoogleLogincallback";
            thisUrl = thisUrl.ToLower().Replace("http://", "https://");
            param.redirect_uri = thisUrl;
            param.code = code;
            var token = Ultil.HttpRequesHelper<GoogleTokenCode>.Post("https://oauth2.googleapis.com/token", param);
            GoogleProfile googleProfile = GetuserProfile(token.access_token);

            if (!string.IsNullOrEmpty(googleProfile.Email))
            {
                UserProfileControl profileControl = new UserProfileControl();
                var localUser = profileControl.GetUserProfile(googleProfile.Email);
                if (localUser != null && localUser.UserId > 0)
                {
                    var needUpdate = false;
                    if (string.IsNullOrEmpty(localUser.SureName) && (!string.IsNullOrEmpty(googleProfile.Name) || !string.IsNullOrEmpty(googleProfile.Given_name) || !string.IsNullOrEmpty(googleProfile.Family_name)))
                    {
                        localUser.SureName = string.IsNullOrEmpty(googleProfile.Name) ? (googleProfile.Family_name ?? googleProfile.Given_name) : googleProfile.Name;
                        localUser.LastVisit = DateTime.Now;
                        needUpdate = true;
                    }
                    if (string.IsNullOrEmpty(localUser.Avata) || localUser.Avata == "https://media.whereviet.com/images/1/112020/12020112717042683.png")
                    {
                        needUpdate = true;
                        localUser.Avata = googleProfile.Picture;
                    }
                    if (needUpdate)
                    {
                        profileControl.UpdateProfile(localUser);
                    }
                    profileControl.Login(localUser.UserName, true);
                }
                else
                {
                    localUser = new Models.UserProfile();
                    localUser.UserName = googleProfile.Email;
                    localUser.BusinessName = string.IsNullOrEmpty(googleProfile.Name) ? (googleProfile.Family_name ?? googleProfile.Given_name) : googleProfile.Name;
                    localUser.SureName = string.IsNullOrEmpty(googleProfile.Name) ? (googleProfile.Family_name ?? googleProfile.Given_name) : googleProfile.Name;
                    localUser.Avata = googleProfile.Picture;
                    localUser.Email = googleProfile.Email;
                    profileControl.Register(localUser, Ultil.StringHelper.GetRandomString(50, false));

                    profileControl.Login(localUser.UserName, true);
                    Dal.MessengerControl msg = new Dal.MessengerControl();
                    msg.CreateMassage("Welcome to Tinhieu365.com", "Chào mừng bạn đến với hệ thống phân tích tín hiệu chứng khoán, vui lòng click vào link bên cạnh để cập nhật thông tin <a href=\"/account/myprofile\">link</a> ", string.Empty, AppSession.CurentProfile.UserId);
                    ViewBag.message = "Sign Up Success";
                }
                
            }
            return RedirectToLocal("/");
        }

            private GoogleProfile GetuserProfile(string accesstoken)
            {
                string url = "https://www.googleapis.com/oauth2/v3/userinfo?access_token=" + accesstoken + "";
                return Ultil.HttpRequesHelper<GoogleProfile>.Get(url, null);
            }
        public class GoogleClient
        {
            public string code { get; set; }
            public string client_id
            {
                get
                {
                    return AppConfig.GoogleClientid;
                }
            }
            public string client_secret
            {
                get
                {
                    return AppConfig.GoogleClientSecret;
                }
            }
            public string redirect_uri { get; set; }            
            public string grant_type
            {
                get
                {
                    return "authorization_code";
                }
            }
        }
        #endregion google login

    }

}