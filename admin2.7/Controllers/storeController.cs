using adminv2._4.Filters;
using Dal;
using Models;
using Models.Modul.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Mvc.Controllers
{
    [IsAuthenlication(RoleName = "admin")]
    public class StoreController : BaseController
    {

        UserProfileControl userControl = new UserProfileControl();

        [HttpGet]
        public ActionResult UserInRole()
        {
            SetUpAll();
            List<UserProfile> allUser = new List<UserProfile>();
            if (UserProfileControl.IsUserInRole(AppSession.CurentProfile.UserId, AEnum.UserRole.Admin))
            {
                int count = 1;
                allUser = userControl.GetAllAdmin(string.Empty,1,9999,out count);
            }
            return View(allUser);
        }


        [HttpGet]
        public ActionResult AddUser()
        {
            SetUpAll();
            return View();
        }

        [HttpGet]
        public ActionResult Index()
        {
            //  setUpAll();
            return View();
        }
      

        public ActionResult Turning()
        {
            return View();
        }

        public int TurningRequesAnalyticTable(int connectionId)
        {
            StoreMng.Turning tn = new StoreMng.Turning();
            return tn.TurningRequesAnalyticTable();
        }
    }
}
