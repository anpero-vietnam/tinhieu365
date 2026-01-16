using AModul.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Mvc.Controllers;

namespace admin.Controllers
{
    [IsAuthenlication(RoleName = "CanUpdateProduct")]
    public class BranchController : BaseController
    {
        OriginGroup origin = new OriginGroup();
        
        
        [OutputCache(Duration = 300, VaryByParam = "*")]
        public ActionResult Index()
        {
            SetUpAll();
            return View();
        }
        [HttpPost]
        
        public int AddBranch(int id,string name, string st, string images, string desc,int rank)
        {
            
            Dal.SysNotify sn = new Dal.SysNotify();
            sn.AddSysNotify("Thành viên " + AppSession.CurentProfile.SureName + " thêm thương hiệu \" " + name + "\" vào lúc " + String.Format("{0:g}", DateTime.Now), "");
            return origin.AddOrigin(id,name, desc, images, rank);
        }
        [HttpPost]
        
        public string BindSelectOriginLink(string st, string id)
        {
            string rs = "";
            
            var model = origin.GetOrigin();

            rs += "<option value='0'>Chọn một</option>";
            if (model != null)
            {
                foreach (var item in model)
                {
                    rs += "<option value='" + Ultil.UrlHelper.GetProductGroupLink(item.Name, item.Id) + "'>" + item.Name + "</option>";
                }

            }
            return rs;
        }
        
        public string BindSelectOrigin(string st)
        {
            string s1 = "";            
            var model = origin.GetOrigin();
            s1 += "<option value='0'>Chọn một</option>";
            if (model != null)
            {
                foreach (var item in model)
                {
                    s1 += "<option value='" + item.Id + "'>" + item.Name + "</option>";
                }
            }
            return s1;
        }
       
        public PartialViewResult GetAllOriginHtml()
        {            
            var model = origin.GetOrigin();            
            return PartialView(model);
        }
       
        public JsonResult GetByID(int id)
        {
            var model = origin.GetOrigin().Where(x=>x.Id== id).FirstOrDefault();
            return Json(model);
        }
    }
}