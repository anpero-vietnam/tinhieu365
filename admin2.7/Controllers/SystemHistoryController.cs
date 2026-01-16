using System;
using System.Web.Mvc;
using Web.Mvc.Controllers;

namespace adminv2._4.Controllers
{
    [IsAuthenlication(RoleName="cansale")]
    public class SystemHistoryController : BaseController
    {
       
        public ActionResult Detail(string id)
        {
            SetUpAll();
            try
            {
                
                    string userName = AppSession.CurentProfile.SureName;
                    Dal.SysNotify sn = new Dal.SysNotify();
                    var notifyItem= sn.GetNotifyById(Convert.ToInt32(id));
                    if (notifyItem.Id > 0)
                    {
                        sn.updateSysNotify(Convert.ToInt32(id), 1, notifyItem.Title, notifyItem.Content + "<br> " + userName + " đã đọc lúc " + DateTime.Now.ToLocalTime());
                    }
                    ViewData["sn"] = notifyItem;
                
                
            }
            catch (Exception)
            {

                throw;
            }
            return View();
        }
        
        public ActionResult Index()
        {
            SetUpAll();
            try
            {   
                    Dal.SysNotify sn = new Dal.SysNotify();
                    ViewData["asllNotify"] = sn.GetSysNotify("%", "%", "-777");
            }
            catch (Exception)
            {
            }
            return View();
        }

    }
}
