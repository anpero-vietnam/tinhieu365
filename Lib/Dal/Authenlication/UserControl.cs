using Dal.Dapper;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Claims;
using System.Web;
using Ultil.Cache;

namespace Dal
{
    public class UserProfileControl : ConnectionProxy<UserProfile>
    {
       
        public List<UserProfile> GetAllAdmin(string roleName, int currentPage, int pageSite, out int count)
        {
            try
            {
                Dictionary<string, object> ParamList = new Dictionary<string, object>();
                ParamList.Add("@role", null);
                ParamList.Add("@currentPage", currentPage);
                ParamList.Add("@pageSite", pageSite);
                return base.Select("sp_getAllAdmin",ParamList, "@count", out count);
            }
            catch (Exception e)
            {

                count = 0;
                return new List<UserProfile>();
            }


        }
        public List<UserProfile> GetAllUser(string roleName,  int currentPage, int pageSite, out int count,string userName = null)
        {
            try
            {
                Dictionary<string, object> ParamList = new Dictionary<string, object>();
                ParamList.Add("@role", roleName);                
                ParamList.Add("@currentPage", currentPage);
                ParamList.Add("@pageSite", pageSite);
                ParamList.Add("@userName", userName);
                return base.Select("[sp_getAllUser]",ParamList, "@count", out count);
            }
            catch (Exception e)
            {

                count = 0;
                return new List<UserProfile>();
            }


        }
        public int ResetPassword(int userId, string newPass)
        {
            string hashPasword = Ultil.MD5Helper.GetMD5Hash(newPass.Trim());
            Dictionary<string, object> ParamList = new Dictionary<string, object>();
            ParamList.Add("@userId", userId);
            ParamList.Add("@hashPasword", hashPasword);            
            return base.ExecuteProc("sp_ResetPassword",ParamList);
        }
        public bool Register(Models.UserProfile profile, string password)
        {
            string hashPasword = Ultil.MD5Helper.GetMD5Hash(password.Trim());
            try
            {
                Dictionary<string, object> ParamList = new Dictionary<string, object>();
                ParamList.Add("@UserName", profile.UserName.Trim());
                ParamList.Add("@Avata", profile.Avata);
                ParamList.Add("@Phone", profile.Phone);
                ParamList.Add("@WebSite", profile.WebSite);
                ParamList.Add("@Address", profile.Address);
                ParamList.Add("@SureName", profile.SureName);
                ParamList.Add("@LocationID", profile.LocationId);
                ParamList.Add("@IdentityCardNumber", profile.IdentityCardNumber);
                ParamList.Add("@hashPasword", hashPasword);
                int userId = 0;
                base.ExecuteProc("sp_CreateUserProfile",ParamList, "@output", out userId);
                return userId > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool UpdateProfile(Models.UserProfile profile)
        {   
            try
            {
                Dictionary<string, object> ParamList = new Dictionary<string, object>();
                ParamList.Add("@Email", profile.Email);
                ParamList.Add("@Avata", profile.Avata);
                ParamList.Add("@Phone", profile.Phone);
                ParamList.Add("@WebSite", profile.WebSite);
                ParamList.Add("@Address", profile.Address);
                ParamList.Add("@SureName", profile.SureName);
                ParamList.Add("@BusinessName", profile.BusinessName);
                ParamList.Add("@LocationID", profile.LocationId);
                ParamList.Add("@IdentityCardNumber", profile.IdentityCardNumber);
                ParamList.Add("@UserId", profile.UserId);
                ParamList.Add("@IntroduceYourself", profile.IntroduceYourself);
                ParamList.Add("@IntroduceBusiness", profile.IntroduceBusiness);
                ParamList.Add("@ProvinceId", profile.ProvinceId);

                base.ExecuteProc("sp_UpdateUserProfile", ParamList);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Login(string userName, string passWord, bool persistCookie = true)
        {
            var profile = GetUserProfile(userName, passWord);            
            if (profile != null && profile.UserId > 0)
            {
                if (persistCookie)
                {
                    int cacheMinute = AEnum.SiteConfig.SaveLoginDay * 60 * 24;
                    var token = Ultil.JwtManagers.GenerateToken(profile.UserName, profile.UserId.ToString(), profile.SureName, profile.Avata,profile.Address, profile.Credit.ToString(), cacheMinute);
                    Ultil.CookieHelper.SetCookie("_tk", token, cacheMinute);
                }
                
                return true;
            }
            else
            {
                return false;
            }
            
        }
        public bool Login(string userName, bool persistCookie = true)
        {
            var profile = GetUserProfile(userName);
            if (profile != null && profile.UserId > 0)
            {
                if (persistCookie)
                {
                    int cacheMinute = AEnum.SiteConfig.SaveLoginDay * 60 * 24;
                    var token = Ultil.JwtManagers.GenerateToken(profile.UserName, profile.UserId.ToString(), profile.SureName, profile.Avata, profile.Address, profile.Credit.ToString(), cacheMinute);
                    Ultil.CookieHelper.SetCookie("_tk", token, cacheMinute);
                }

                return true;
            }
            else
            {
                return false;
            }

        }
        public UserProfile GetUserProfileFromCookie()
        {
            var result = new UserProfile { IsAuthenlication = false };
            try
            {
                var token = Ultil.CookieHelper.GetCookie("_tk");
                if (!string.IsNullOrEmpty(token))
                {
                    var principal = Ultil.JwtManagers.GetClaimsPrincipal(token);
                    if (principal != null)
                    {
                        var identity = principal.Identity as ClaimsIdentity;
                        if (identity == null)
                            return new UserProfile() { IsAuthenlication = false };

                        if (identity.IsAuthenticated && principal.FindFirst(ClaimTypes.Name)?.Value != null)
                        {
                            return new UserProfile
                            {
                                UserId = Convert.ToInt32(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                                UserName = identity.FindFirst(ClaimTypes.Name)?.Value,
                                SureName = identity.FindFirst(ClaimTypes.Surname)?.Value,
                                Avata = identity.FindFirst("avata")?.Value,
                                Address = identity.FindFirst("address")?.Value,
                                Credit =Convert.ToDouble(identity.FindFirst("Credit")?.Value),
                                IsAuthenlication = identity.IsAuthenticated
                            };

                        }
                    }
                 
                }
            }
            catch (Exception)
            {
            }
         
            return result;
        }
        public Models.UserProfile GetUserProfile(string userName, string passWord)
        {
            try
            {
                string hashPasword = Ultil.MD5Helper.GetMD5Hash(passWord.Trim());
                Dictionary<string, object> ParamList = new Dictionary<string, object>();
                ParamList.Add("@email", userName);
                ParamList.Add("@password", hashPasword);
                return base.SelectSingle("[sp_getUseProfileByUserName]",ParamList);
            }
            catch (Exception)
            {
                return null;
            }

        }
        public Models.UserProfile GetUserProfile(string userName)
        {
            try
            {
                Dictionary<string, object> ParamList = new Dictionary<string, object>();
                ParamList.Add("@email", userName);                
                return base.SelectSingle("[sp_adminGetUseProfileByUserName]",ParamList);
            }
            catch (Exception)
            {
                return new Models.UserProfile { UserId=0};
            }

        }
        public int AddUserToMultiRole(string roleList, int userId)
        {
            
            int roleAdded = 0;            
            string[] roleArray = roleList.Split(',');
            if (roleArray != null && roleArray.Length > 0)
            {
                try
                {
                    DeleteAllRoleOfUser(userId);
                    for (int i = 0; i < roleArray.Length; i++)
                    {
                        if (roleArray[i] != "" && roleArray[i] != ",")
                        {

                            roleAdded += AddUserToStoreRole(userId, Convert.ToInt32(roleArray[i]));
                        }
                    }
                }
                catch (Exception)
                {


                }
            }
            return roleAdded;
        }
        public int DeleteAllRoleOfUser(int uid)
        {
            try
            {
                Dictionary<string, object> ParamList = new Dictionary<string, object>();
                ParamList.Add("@userId", uid);                
                return base.ExecuteProc("[sp_DeleteAllRoleOfUser]",ParamList);
            }
            catch (Exception ex)
            {
                Dal.MessengerControl msg = new Dal.MessengerControl();
                var x = msg.SendMsgToAdminAsync(ex.StackTrace).Result;
                return 0;
            }

        }
        public int AddUserToStoreRole(int uid, int role)
        {
            try
            {
                Dictionary<string, object> ParamList = new Dictionary<string, object>();
                ParamList.Add("@UID", uid);
                ParamList.Add("@ROLE", role);
                return base.ExecuteProc("[ADD_U_2_ROLE]",ParamList);
            }
            catch (Exception ex)
            {
                Dal.MessengerControl msg = new Dal.MessengerControl();
                var x = msg.SendMsgToAdminAsync(ex.StackTrace).Result;
                return 0;
            }

        }
        public DataTable GetUserInRole(string role)
        {
            try
            {
                SqlParameter[] paramList = new SqlParameter[1];
                paramList[0] = new SqlParameter("@roleName", SqlDbType.NVarChar, 50);
                paramList[0].Value = role;
                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();                
                return ds.executeSelect("sp_GET_USER_IN_ROLE", paramList);
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// update and return token just update
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string UpdateUserToken(int userId)
        {
            try
            {

                string token = Ultil.StringHelper.GetRandomString(100, true);
                SqlParameter[] paramList = new SqlParameter[2];
                paramList[0] = new SqlParameter("@Id", SqlDbType.Int, 32);
                paramList[0].Value = userId;
                paramList[1] = new SqlParameter("@Token", SqlDbType.VarChar, 300);
                paramList[1].Value = token;
                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                if (ds.executeUpdate("[sp_updateTokenUser]", paramList) > 0)
                {
                    return token;
                }
                else { return "0"; }

            }
            catch (Exception e)
            {

                return "0";
            }


        }
        public bool CheckTokenUser(int userId, string token)
        {
            try
            {

                SqlParameter[] paramList = new SqlParameter[2];
                paramList[0] = new SqlParameter("@UserId", SqlDbType.Int, 32);
                paramList[0].Value = userId;
                paramList[1] = new SqlParameter("@Token", SqlDbType.VarChar, 300);
                paramList[1].Value = token;
                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                return ds.executeScalary("sp_CheckUserToken", paramList) > 0 ? true : false;

            }
            catch (Exception e)
            {

                return false;
            }


        }

        public Models.UserProfile GetUserProfileById(int UserId)
        {
            try
            {
                Dictionary<string, object> ParamList = new Dictionary<string, object>();
                ParamList.Add("@userId", UserId);
                return base.SelectSingle("[sp_getUseProfile]",ParamList);
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public static bool IsUserInRole(int uid, int role)
        {
            bool rs = false;
            List<int> allRole = GetAllUserRoles(uid);
            for (int i = 0; i < allRole.Count; i++)
            {
                if (allRole[i] == AEnum.UserRole.Admin)
                {
                    rs = true;
                    break;
                }
                else if (allRole[i] == role)
                {
                    rs = true;
                    break;
                }
            }
            return rs;
        }

        public static List<int> GetAllUserRoles(int uid)
        {
            string cacheKey = "ROLE_OF_USER_" + uid.ToString() + "_";
            List<int> rs = new List<int>();
            if (!CacheHelper.TryGet<List<int>>(cacheKey, out rs))
            {
                rs = new List<int>();
                SqlParameter[] paramList = new SqlParameter[1];
                paramList[0] = new SqlParameter("@userId", SqlDbType.Int);
                paramList[0].Value = uid;
                DatabaseAccess ds = new DatabaseAccess();
                DataTable table = ds.executeSelect("sp_GetUserRoldes", paramList);
                if (table != null && table.Rows.Count > 0)
                {
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        rs.Add(Convert.ToInt32(table.Rows[i]["Role"]));
                    }
                }
                CacheHelper.Set<List<int>>(cacheKey, rs, 10);
            }
            return rs;
        }
        public bool UpdateLastLogin(int uid)
        {
            Dictionary<string, object> ParamList = new Dictionary<string, object>();
            ParamList.Add("@userId", uid);
            return base.ExecuteProc("sp_UpdateLastLogin",ParamList)>0?true:false;
        }
   
    }
    public class oauthProvider
    {
        String providerName;
        String userId;
        String provoderUserId;
        public String ProviderName
        {
            get { return providerName; }
            set { providerName = value; }
        }
        public String ProvoderUserId
        {
            get { return provoderUserId; }
            set { provoderUserId = value; }
        }
        public String UserId
        {
            get { return userId; }
            set { userId = value; }
        }
    }

}
