using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.IO;
using adminv2._4.Models;
using Ultil.Cache;
using System.Text;
using Admin.Bussiness;
using Dal;
using Ultil;
using AModul.Bill;

namespace Web.Mvc.Controllers
{

    public class AccountController : Base2Controller
    {
        Lazy<UserProfileControl> lazyUserControl = new Lazy<UserProfileControl>();
        UserProfileControl userControl { get { return lazyUserControl.Value; } }
        Dal.UserProfileControl profileControl = new Dal.UserProfileControl();
        
        [IsAuthenlication(RoleName = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public string AddCash(string email,string Amount)
        {
            var valid = true;
            var rs = string.Empty;
            UserProfileControl userProfileControl = new UserProfileControl();
            int userId = userProfileControl.GetUserProfile(email).UserId;
            try
            {
                decimal _amount = Convert.ToDecimal(Amount);
                if (_amount <= 0)
                {
                    valid = false;
                    rs = "Số tiền nhập vào không hợp lệ";
                }
                if (userId <= 0)
                {
                    valid = false;
                    rs = "Người dùng không hợp lệ";
                }
                if (valid)
                {
                    BillControl billControl = new BillControl();
                  
                    var insertId = billControl.AddBill(new Models.Modul.Bill.BillModel
                    {
                        Amount = _amount,
                        CreateBy = AppSession.CurentProfile.UserId,
                        CreateFor = userId,
                        Detail = "Admin cộng tiền từ admin [tinhieu365 credit]",
                        IsSuccess = true,
                        OrderId = string.Empty,
                        IsCredit = true,
                        Merchant = "WV"
                    });
                    rs += "Thêm thành công tiền vào tài khoản :"+ email+", transaction Id="+insertId+" đã được tạo";
                    Dal.SysNotify sn = new Dal.SysNotify();
                    sn.AddSysNotify("Thành viên " + AppSession.CurentProfile.SureName + " đã thêm tiền vào tài khoản \" <span class=\"lb-nf\">" + email + "\"</span> vào lúc " + String.Format("{0:g}", DateTime.Now)+", với số tiền: "+ Amount, "");
                }
            }
            catch (Exception ex)
            {
                rs = ex.Message;
            }
          
          
            return rs;
        }
        [IsAuthenlication(RoleName = "admin")]
        public ActionResult AddCredit()
        {

            return View();

        }
        public ActionResult ForgetPassword()
        {

            return View();

        }
        [HttpPost]
        public ActionResult ForgetPasswords(string mail, string captcha)
        {

            Models.UserProfile userModel = new Models.UserProfile();
            bool valids = true;
            if (!CheckValid.ValidateGoogleCaptcha(captcha))
            {
                valids = false;
                return Redirect("/account/forgetPassword?rs=2");
            }
            try
            {

                if (!StringHelper.isEmail(mail))
                {
                    valids = false;
                    return Redirect("/account/forgetPassword?rs=3");
                }
                else
                {
                    userModel = profileControl.GetUserProfile(mail);

                }

                if (userModel == null || userModel.UserId == 0)
                {
                    valids = false;
                    return Redirect("/account/forgetPassword?rs=1");
                }
                if (valids)
                {

                    string token = userControl.UpdateUserToken(userModel.UserId);


                    string DomainName = System.Configuration.ConfigurationManager.AppSettings["DomainName"];
                    string AppName = System.Configuration.ConfigurationManager.AppSettings["AppName"];
                    SendMail mails = new SendMail();
                    String mailUrl = mail.Replace(@"@", @"(at)");
                    string resetUrl = DomainName.TrimEnd('/') + "/account/checkMail?token=" + token + @"&id=" + mailUrl;
                    string username = string.IsNullOrEmpty(userModel.SureName) ? userModel.UserName : userModel.SureName;
                    using (StreamReader reader = System.IO.File.OpenText(Server.MapPath("~/theme/html/ChangePass.html")))
                    {
                        string Htmlbody = reader.ReadToEnd();
                        Htmlbody = Htmlbody.Replace(@"{userName}", username);
                        Htmlbody = Htmlbody.Replace(@"{resetLink}", resetUrl);
                        Htmlbody = Htmlbody.Replace(@"{email}", mail);

                        mails.sendMail(AppName + " - xác nhận yêu cầu đổi mật khẩu", mail, "", "", "khôi phục mật khẩu cho mail : " + mail, Htmlbody, string.Empty);
                    }
                    return Redirect("/account/forgetPassword?rs=0");

                }
                else
                {
                    return Redirect("/account/forgetPassword?rs=2");
                }

            }
            catch (Exception ex)
            {   
                return View("forgetPassword");
            }
        }
        public ActionResult CheckMail()
        {
            Models.UserProfile userModel = new Models.UserProfile();
            string mail = Request["id"];            
            if (String.IsNullOrEmpty(mail))
            {
                return Redirect("/account/mailNotice?rs=1");
            }
            else
            {
                mail = mail.Replace(@"(at)", @"@");
            }

            string token = Request["token"];
          
          
            bool valids = true;
            try
            {

                if (!StringHelper.isEmail(mail))
                {
                    valids = false;
                    return Redirect("/account/mailNotice?rs=3");
                }
                else
                {
                    userModel = profileControl.GetUserProfile(mail);
                }

                if (userModel.UserId <= 0)
                {

                    valids = false;
                    return Redirect("/account/mailNotice?rs=1");
                }
                if (valids && userControl.CheckTokenUser(userModel.UserId, token))
                {
                    SendMail mails = new SendMail();

                    string newPass = Ultil.StringHelper.GetRandomString(8, false);
                    StringBuilder body = new StringBuilder("<div>Mật khẩu mới đây rồi : <h2 style='color:red;'>Mật khẩu mới: ");
                    body.Append(newPass + "</h2></div>");
                    body.Append("<div>Để đảm bảo an toàn vui lòng đổi mật khẩu mới ngay sau khi đăng nhập.");
                    body.Append(". Và đừng ngần ngại liên hệ với chúng tôi để được hỗ trợ nhanh chóng.</div>");
                    body.Append("<div>Trân trọng cảm ơn.</div>");                    
                    mails.sendMail("Mật khẩu mới", mail, "", "", "khôi phục mật khẩu cho mail : " + mail, body.ToString(), @"/theme/html/lienHeTemplate.html");
                    
                    try
                    {   
                        var result = userControl.ResetPassword(userModel.UserId,newPass);
                        userControl.UpdateUserToken(userModel.UserId);
                    }
                    catch (Exception)
                    {
                        //người dùng đăng nhập bằng openid không có pass
                        return Redirect("/account/mailNotice?rs=4");
                    }

                    return Redirect("/account/mailNotice?rs=0");
                }
                else
                {
                    return Redirect("/account/mailNotice?rs=2");
                }

            }
            catch (Exception ex)
            {
             

            }

            return View();

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
        public ActionResult Login(LoginModel model, string captcha, string returnUrl = "/")
        {
            Security.Store.ReGenerateSessionId();
            try
            {

                bool valid = true;
                if (!Ultil.CheckValid.ValidateGoogleCaptcha(captcha))
                {
                    valid = false;
                    ModelState.AddModelError("", "Vui lòng click vào ô kiểm tra.");
                    return View(model);
                }

                if (valid && ModelState.IsValid && userControl.Login(model.UserName, model.Password, true))
                {
                    userControl.UpdateLastLogin(AppSession.CurentProfile.UserId);
                    return RedirectToLocal("/");
                }
                ModelState.AddModelError("", "Sai tên đăng nhập hoặc mật khẩu.");
            }
            catch (Exception ex)
            {
                Dal.MessengerControl ms = new Dal.MessengerControl();
                ms.SendMsgToAdmin("Error from Login function : " + model.UserName + "<br>" + ex.StackTrace);
                
                return View(model);
            }

            return View(model);
        }

        void ClearUserCookieUser()
        {
            Ultil.CookieHelper.RemoveCookie("_uk");
        }
        [IsAuthenlication(RoleName = "admin")]
        public ActionResult Alluser(int page = 1,string userName=null)
        {
            int count = 0;
            UserProfileControl prifileControl = new UserProfileControl();
            var model = prifileControl.GetAllUser(null, page, 50, out count,userName);
            ViewBag.paged = StringHelper.SetUpPagedV2(page, 30, count, 10, "?page");
            return View(model);
        }

        [HttpPost]
        public ActionResult LogOff()
        {
            int uid = AppSession.CurentProfile.UserId;
            try
            {   
                CacheHelper.Remove("ROLE_OF_USER_" + uid.ToString());
                ClearUserCookieUser();
            }
            catch (Exception)
            {


            }
            return RedirectToAction("Login", "account");
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
                try
                {
                    if (Ultil.StringHelper.isEmail(model.UserName) || Ultil.StringHelper.isVnPhone(model.UserName))
                    {
                        Dal.UserProfileControl userControl = new Dal.UserProfileControl();
                        if (userControl.Register(model, Password))
                        {
                            userControl.Login(model.UserName, Password, true);
                        }
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("validateName", "Tên đăng nhập phải là e-mail hoặc số điện thoại.");
                    }
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }
            else
            {
                ModelState.AddModelError("capcha", "Vui lòng ckick vào nút kiểm tra.");
            }
            return View(model);
        }
        #endregion


        public ActionResult CreateAccount()
        {
            return View();
        }


        [IsAuthenlication(RoleName = "admin")]

        public ActionResult SuperAdmin()
        {
            // SimpleMembershipProvider provider = (SimpleMembershipProvider)Membership.Provider;
            // string username = provider.GetUserNameFromId(userId);
            return View();
        }
        //[Authorize]
        //[Authorize(Roles = "addmin")]
        //[InitializeSimpleMembership]
        //public ActionResult AddUserTorole()
        //{
        //    Dal.UserProfile ui = new Dal.UserProfile();
        //    String url = Request.Url.AbsolutePath;
        //    Models.UserInStore model = new Models.UserInStore();
        //    if (Request.QueryString["uid"] != null && Request.QueryString["uid"].ToString().Trim() != "")
        //    {
        //        Dal.UserProfile profile = new Dal.UserProfile();
        //        model = profile.GetUserProfileById(Convert.ToInt32(Request.QueryString["uid"]));
        //    }


        //    if (Request.QueryString["userName"] != null)
        //    {
        //        if (Request.QueryString["rs"] != null && Request.QueryString["rs"].Equals("1"))
        //        {
        //            ViewBag.resoult = "1";
        //        }
        //        Dal.UserProfile u = new Dal.UserProfile();
        //        model = u.GetUserProfileById(Convert.ToInt32(Request.QueryString["userName"]));
        //        Dal.oauthProvider[] oauth = u.PrivoderList;
        //        if (oauth != null && oauth.Length > 0)
        //        {
        //            ViewData["provideList"] = oauth;
        //        }



        //    }
        //    SetUpAll();
        //    return View(model);
        //}

        //[HttpPost]
        //[InitializeSimpleMembership]
        //[Authorize(Roles = "addmin")]
        //public void AddUserToroles(string userName, string role)
        //{

        //    String block = Request["block"].ToString().Trim();
        //    String phone = Request["phone"].ToString().Trim().ToLower();
        //    String web = Request["web"].ToString().Trim().ToLower();
        //    String add = Request["add"].ToString().Trim().ToLower();
        //    String sureName = Request["sureName"].ToString().Trim().ToLower();

        //    SimpleMembershipProvider provider = (SimpleMembershipProvider)Membership.Provider;
        //    int id = provider.GetUserId(userName);
        //    Dal.UserProfileControl u = new Dal.UserProfileControl();

        //    String dtmNow = Ultil.Times.GetyyyyMMddhhmmNow();
        //    u.AddUserToRole(id.ToString(), role);
        //    u.UpDateProfile(id.ToString(), "0", "0", phone, web, "0", "addmin", add, sureName);
        //    var roles = (SimpleRoleProvider)System.Web.Security.Roles.Provider;
        //    var membership = (SimpleMembershipProvider)System.Web.Security.Membership.Provider;
        //    if (!roles.RoleExists(role))
        //    {
        //        roles.CreateRole(role);
        //    }
        //    try
        //    {
        //        if (Roles.FindUsersInRole("addmin", userName).Length > 0)
        //        {
        //            Roles.RemoveUserFromRole(userName, "addmin");
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        Dal.MessengerControl ms = new Dal.MessengerControl();
        //        ms.SendMessagesToRole("addmin", false, "Lỗi từ addUserTorole", ex.Message, "0");

        //    }
        //    try
        //    {
        //        if (Roles.FindUsersInRole("mod", userName).Length > 0)
        //        {
        //            Roles.RemoveUserFromRole(userName, "mod");
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        Dal.MessengerControl ms = new Dal.MessengerControl();
        //        ms.SendMessagesToRole("addmin", false, "Lỗi từ addUserTorole", ex.Message, "0");

        //    }

        //    try
        //    {
        //        if (Roles.FindUsersInRole("0", userName).Length > 0)
        //        {
        //            Roles.RemoveUserFromRole(userName, "0");
        //        }
        //    }
        //    catch (Exception)
        //    {


        //    }

        //    try
        //    {
        //        if (Roles.FindUsersInRole("maketing", userName).Length > 0)
        //        {
        //            Roles.RemoveUserFromRole(userName, "maketing");
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        Dal.MessengerControl ms = new Dal.MessengerControl();
        //        ms.SendMessagesToRole("addmin", false, "Lỗi từ addUserTorole", ex.Message, "0");

        //    }
        //    try
        //    {
        //        if (Roles.FindUsersInRole("accounting", userName).Length > 0)
        //        {
        //            Roles.RemoveUserFromRole(userName, "accounting");
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        Dal.MessengerControl ms = new Dal.MessengerControl();
        //        ms.SendMessagesToRole("addmin", false, "Lỗi từ addUserTorole", ex.Message, "0");

        //    }
        //    try
        //    {
        //        if (Roles.FindUsersInRole("store", userName).Length > 0)
        //        {
        //            Roles.RemoveUserFromRole(userName, "store");
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        Dal.MessengerControl ms = new Dal.MessengerControl();
        //        ms.SendMessagesToRole("addmin", false, "Lỗi từ addUserTorole", ex.Message, "0");

        //    }
        //    try
        //    {
        //        if (Roles.FindUsersInRole("tech", userName).Length > 0)
        //        {
        //            Roles.RemoveUserFromRole(userName, "tech");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Dal.MessengerControl ms = new Dal.MessengerControl();
        //        ms.SendMessagesToRole("addmin", false, "Lỗi từ addUserTorole", ex.Message, "0");

        //    }
        //    Roles.AddUserToRole(userName, role);
        //    u.AddUserToRole(id.ToString(), role);
        //    Response.Redirect("/account/addusertorole?userName=" + id.ToString() + @"&rs=1");

        //}

        //[Authorize]
        //[Authorize(Roles = "addmin")]
        //[InitializeSimpleMembership]
        //public ActionResult listAdmin()
        //{
        //    Dal.UserProfile ui = new Dal.UserProfile();
        //    UserInStore model = new UserInStore();
        //    String url = Request.Url.AbsolutePath;
        //    if (url.Contains(@"user/curentuser") || url.Contains(@"account/UpdateProfile") || url.Contains(@"Account/Manage"))
        //    {

        //        if (User.Identity.IsAuthenticated)
        //        {
        //            Dal.UserProfileControl profile = new Dal.UserProfileControl();
        //            model = profile.GetUserProfileById(AppSession.CurentProfile.UserId);
        //        }
        //    }
        //    else
        //    {
        //        if (Request.QueryString["uid"] != null && Request.QueryString["uid"].ToString().Trim() != "")
        //        {
        //            Dal.UserProfileControl profile = new Dal.UserProfileControl();
        //            model = profile.GetUserProfileById(Convert.ToInt32(Request.QueryString["uid"]));
        //        }
        //    }
        //    return View(model);
        //}
        //[HttpGet]
        //[Authorize]
        //[Authorize(Roles = "addmin")]
        //[InitializeSimpleMembership]
        //public ActionResult ListAdmin(string role, string userName, int page = 1)
        //{
        //    SetUpAll();
        //    List<Models.UserInStore> listUser = new List<Models.UserInStore>();
        //    Dal.UserProfile u = new Dal.UserProfile();
        //    int count = 0;
        //    listUser = u.GetAllUser(role, userName, page, 100, out count);
        //    ViewBag.page = Ultil.StringHelper.SetUpPagedV2(page, 100, count, 50, "?page=");
        //    return View(listUser);
        //}

        //[Authorize]
        //[Authorize(Roles = "addmin")]
        //[InitializeSimpleMembership]
        //public ActionResult listOauth(String role)
        //{
        //    int page = 1;


        //    if (Request.QueryString["page"] != null)
        //    {
        //        page = Convert.ToInt32(Request.QueryString["page"].ToString());
        //    }
        //    String[] allUser = null;
        //    Dal.UserProfile u = new Dal.UserProfile();
        //    DataTable tableUser = u.GetListOauthMemberShip(page, 30);

        //    if (tableUser != null)
        //    {
        //        allUser = new String[tableUser.Rows.Count];
        //        for (int i = 0; i < tableUser.Rows.Count; i++)
        //        {
        //            allUser[i] = tableUser.Rows[i]["UserName"].ToString();
        //        }
        //    }
        //    else { allUser = null; }
        //    List<Models.UserInStore> listUser = new List<Models.UserInStore>();
        //    if (allUser != null && allUser.Length > 0)
        //    {
        //        for (int i = 0; i < allUser.Length; i++)
        //        {
        //            Dal.UserProfile user = new Dal.UserProfile();
        //            String uname = allUser[i];
        //            SimpleMembershipProvider provider = (SimpleMembershipProvider)Membership.Provider;
        //            //String id = provider.GetUserId(Name.ToLower()).ToString();

        //            listUser.Add(user.GetUserProfileByName(provider.GetUserId(uname.ToLower()), @"-1"));
        //        }
        //        ViewData["ulist"] = listUser;
        //    }
        //    setUpAll();
        //    Dal.Themes t = new Dal.Themes();
        //    Dal.Analytic an = new Dal.Analytic();
        //    an.SetUpAnalyticMemberShip();
        //    ViewBag.page = Ultil.StringHelper.SetUpPagedV2(page, 20, an.AllOauthUserCount, 100, "?page=");
        //    return View();
        //}
        //[Authorize]
        //[Authorize(Roles = "addmin")]
        //[InitializeSimpleMembership]
        //public string AdminGetUsers(string UserName = "")
        //{
        //    string rs = "";
        //    if (String.IsNullOrEmpty(UserName) || UserName == "0")
        //    {
        //        UserName = "%";
        //    }
        //    try
        //    {
        //        Dal.UserProfile u = new Dal.UserProfile();
        //        int count = 0;
        //        var userList = u.GetAllUser(string.Empty, UserName, 1, 20, out count);
        //        foreach (var item in userList)
        //        {
        //            rs += @"<label class='checkbox'><input type='checkbox' id='" + item.UserId + "' value='" + item.UserId + "'>" + item.UserName + @"</label>";
        //        }

        //    }
        //    catch (Exception)
        //    {
        //    }
        //    return rs;
        //}
        //[IsAuthenlication]
        //public string GetUsers(int st, string UserName = "")
        //{
        //    string rs = "";

        //    try
        //    {
        //        //if (UserProfileControl.IsUserInRole(AppSession.CurentProfile.UserId,  AEnum.UserRole.Admin))
        //        //{
        //        //    StoreMng.UseControl useControl = new StoreMng.UseControl();
        //        //    List<Models.UserInStore> allUser = useControl.GetAllUserByStoreId();
        //        //    foreach (var item in allUser)
        //        //    {
        //        //        rs += @"<label class='checkbox'><input type='checkbox' id='" + item.UserId + "' value='" + item.UserId + "'>" + item.UserName ?? item.SureName + @"</label>";
        //        //    }
        //        //}

        //    }
        //    catch (Exception)
        //    {
        //        rs = "0";
        //    }

        //    return rs;
        //}
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

    

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "Tên đăng nhập đã tồn tại, vui lòng đăng ký tên khác.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "Tên đăng nhập đã tồn tại, vui lòng đăng ký tên khác.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }


        #endregion
        public string GetGoogleLoginPage()
        {
            var googleClient = new GoogleClient();
            return "https://accounts.google.com/o/oauth2/v2/auth?scope=profile%20email&include_granted_scopes=true&redirect_uri=" + googleClient.client_secret + "&response_type=code&client_id=" + googleClient.client_id + "";
        }
        public void GoogleLogincallback(string code, string scope)
        {
            GoogleClient param = new GoogleClient();
            param.code = code;

            var token = Ultil.HttpRequesHelper<TokenCode>.Post("https://oauth2.googleapis.com/token", param);
            GoogleProfile googleProfile = GetuserProfile(token.access_token);
            //do something with  GoogleProfile
        }

        public GoogleProfile GetuserProfile(string accesstoken)
        {
            string url = "https://www.googleapis.com/oauth2/v3/userinfo?access_token=" + accesstoken + "";
            GoogleProfile googleProfile = Ultil.HttpRequesHelper<GoogleProfile>.Get(url, null);
            return googleProfile;
        }
    }
    public class GoogleProfile
    {
        public string Sub { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Given_name { get; set; }
        public string Family_name { get; set; }
        public string Picture { get; set; }
        public string Locale { get; set; }
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
        public string redirect_uri
        {
            get
            {
                return  HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "/Account/GoogleLogincallback";                
            }
        }
        public string grant_type
        {
            get
            {
                return "authorization_code";
            }
        }
    }
    public class TokenCode
    {
        public string access_token { get; set; }
        public string expires_in { get; set; }
        public string token_type { get; set; }
        public string scope { get; set; }
        public string refresh_token { get; set; }
    }
}
