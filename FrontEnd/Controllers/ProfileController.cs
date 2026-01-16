using Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontEnd.Controllers
{
    public class ProfileController : BaseController
    {
        Lazy<UserProfileControl> lazyUserControl = new Lazy<UserProfileControl>();
        UserProfileControl userControl { get { return lazyUserControl.Value; } }
        Dal.UserProfileControl profileControl = new Dal.UserProfileControl();
   
        [IsAuthenlication]
        [ValidateAntiForgeryToken]
        public JsonResult UpdateBusiness(Models.UserProfile model)
        {
            var currentProfile = profileControl.GetUserProfileById(AppSession.CurentProfile.UserId);
            currentProfile.BusinessName = Ultil.StringHelper.GetSafeHtml(model.BusinessName);
            currentProfile.WebSite = Ultil.StringHelper.GetSafeHtml(model.WebSite);
            currentProfile.IdentityCardNumber =Ultil.StringHelper.GetSafeHtml(model.IdentityCardNumber);
            currentProfile.IntroduceBusiness = Ultil.StringHelper.GetSafeHtml(model.IntroduceBusiness);                    
            return UpdateProfileAndRelogin(currentProfile);
        }
        [IsAuthenlication]
        [ValidateAntiForgeryToken]
        public JsonResult UpdateAvata(string Avata)
        {
            var currentProfile = profileControl.GetUserProfileById(AppSession.CurentProfile.UserId);
            currentProfile.Avata = Ultil.StringHelper.GetSafeHtml(Avata);             
            return UpdateProfileAndRelogin(currentProfile);
        }
        [IsAuthenlication]
        [ValidateAntiForgeryToken]
        public JsonResult UpdateContact(Models.UserProfile model)
        {
            var currentProfile = profileControl.GetUserProfileById(AppSession.CurentProfile.UserId);
            currentProfile.Email = Ultil.StringHelper.GetSafeHtml(model.Email);
            currentProfile.Phone = Ultil.StringHelper.GetSafeHtml(model.Phone);
            currentProfile.ProvinceId = model.ProvinceId==0?null: model.ProvinceId;
            currentProfile.LocationId = model.LocationId==0?null: model.LocationId;
            currentProfile.Address = Ultil.StringHelper.GetSafeHtml(model.Address);
            return UpdateProfileAndRelogin(currentProfile);
        }
        [IsAuthenlication]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public JsonResult UpdateNickName(string IntroduceYourself,string SureName)
        {
        
            var currentProfile = profileControl.GetUserProfileById(AppSession.CurentProfile.UserId);
            currentProfile.IntroduceYourself =IntroduceYourself;
            currentProfile.SureName=SureName;
            var result = profileControl.UpdateProfile(currentProfile);
            if (result)
            {
                int cacheMinute = AEnum.SiteConfig.SaveLoginDay * 60 * 24;
                var newToken = Ultil.JwtManagers.GenerateToken(currentProfile.UserName, currentProfile.UserId.ToString(), currentProfile.SureName, currentProfile.Avata, currentProfile.Address, currentProfile.Credit.ToString(), cacheMinute);
                Ultil.CookieHelper.SetCookie("_tk", newToken, cacheMinute);
                return Json(new { code = 200, message = "Profile updated" });
            }
            else
            {
                return Json(new { code = 500, message = "Profile updated error" });
            }
        }
        private JsonResult UpdateProfileAndRelogin(Models.UserProfile currentProfile)
        {
            var result = profileControl.UpdateProfile(currentProfile);
            if (result)
            {
                int cacheMinute = AEnum.SiteConfig.SaveLoginDay * 60 * 24;
                var newToken = Ultil.JwtManagers.GenerateToken(currentProfile.UserName, currentProfile.UserId.ToString(), currentProfile.SureName, currentProfile.Avata, currentProfile.Address, currentProfile.Credit.ToString(), cacheMinute);
                Ultil.CookieHelper.SetCookie("_tk", newToken, cacheMinute);
                return Json(new { code = 200, message = "Profile updated" });
            }
            else
            {
                return Json(new { code = 500, message = "Profile updated error" });
            }
        }
    }
}