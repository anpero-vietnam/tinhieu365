using AModul.Common;
using Models;
using Models.Modul.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;
using Ultil.Cache;


//using System.Diagnostics;
namespace Web.Mvc.Controllers
{
   

    public class HomeController : BaseController
    {

        [IsAuthenlication(RoleName = "CanSale")]
        public async Task<ActionResult> Index()
        {           
            try
             {
                System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                 sw.Start();
                string s = "";
                if (!Ultil.Cache.CacheHelper.TryGet("keyTestAdminHome", out s))
                {
                    Ultil.Cache.CacheHelper.Set<string>("keyTestAdminHome", "RedisRunninginAdmin last time" + DateTime.Now.ToString(), 30);
                }
                sw.Stop();
                var redisElapsedTicks =sw.ElapsedMilliseconds;
                if (redisElapsedTicks >= 500)
                {
                    Dal.MessengerControl Massage = new Dal.MessengerControl();
                    await Massage.SendMsgToAdminAsync("Redis server đang hoạt động bất thường : Hiện tại lấy dữ liệu từ redis server đang lớn hơn 500ms vui lòng kiểm tra cấu hình kết nối");
                }

            }
            catch (Exception ex)
            {
                Dal.MessengerControl Massage = new Dal.MessengerControl();
                await Massage.SendMsgToAdminAsync("Hệ thống tự động phát hiện lỗi tại Ultil.Cache.CacheHelper.Set backend code : " + ex.Message);

            }
            
            ViewBag.key = "<meta name=\"keywords\" content=\"Phầm mềm quản lý bán hàng đa chức năng \" >";
            ViewBag.des = "<meta name=\"description\" content=\"Phầm mềm quản lý bán hàng đa chức năng \" >";
            SetUpAll();
            
            return View();
        }


        [IsAuthenlication(RoleName = "CanViewAnalytic")]
        [OutputCache(Duration = 900, VaryByParam = "st", Location = OutputCacheLocation.Any)]
        public string GetDashBoad()
        {
            HomeDashboard dashboard = new HomeDashboard();
            try
            {
               
                string cacheKey = AEnum.Cache.Cachingkey.MainAnalytic.ToString();
                if (!CacheHelper.TryGet(cacheKey, out dashboard))
                {

                    AModul.Common.MainAnalytic mainAnalytic = new AModul.Common.MainAnalytic();
                    dashboard = mainAnalytic.SetUpMainAnalytic();

                    if (AppSession.CurentProfile.UserId > 0)
                    {
                        Dal.MessengerControl ms = new Dal.MessengerControl();
                        dashboard.UserMessengeList = ms.GetMassage(AppSession.CurentProfile.UserId.ToString(), "%", "0", "%", 1, 4);
                        ms.analyticMassage(AppSession.CurentProfile.UserId);
                        dashboard.WaitingMessage = ms.WaitingMassageCount;
                    }
                    CacheHelper.Set<HomeDashboard>(cacheKey, dashboard, 15);
                }
            }
            catch (Exception ex)
            {
                

            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(dashboard);
        }
        
        [IsAuthenlication(RoleName = "CanViewAnalytic")]
        [OutputCache(Duration = 72000, Location = OutputCacheLocation.Any)]
        public JsonResult GetReqestDashBoad()
        {
            string cacheKey = AEnum.Cache.Cachingkey.RequestAnalytic.ToString();

            List<PlotCharModel> rs = new List<PlotCharModel>();
            if (!CacheHelper.TryGet(cacheKey, out rs))
            {
                rs = new List<PlotCharModel>();
                Analytic an = new Analytic();
                List<RequesAnalyticModel> rs1 = an.GetMonthRequesAnalyticAPI(DateTime.Now.Year, DateTime.Now.Month);
                int lastMonth = DateTime.Now.Month - 1;
                int lastYear = DateTime.Now.Year;
                if (lastMonth == 0)
                {
                    lastMonth = 12;
                    lastYear = lastYear - 1;
                }
                PlotCharModel pc1 = new PlotCharModel();
                pc1.label = "Tháng này";
                if (rs1.Count > 0)
                {
                    for (int i = 0; i < rs1.Count; i++)
                    {
                        pc1.data.Add(new int[] { rs1[i].Dd, rs1[i].RequestCount });
                    }
                }
                rs.Add(pc1);
                List<RequesAnalyticModel> rs2 = an.GetMonthRequesAnalyticAPI(lastYear, lastMonth);
                PlotCharModel pc2 = new PlotCharModel();
                pc2.label = "Tháng trước";
                if (rs2.Count > 0)
                {
                    for (int i = 0; i < rs2.Count; i++)
                    {
                        pc2.data.Add(new int[] { rs2[i].Dd, rs2[i].RequestCount });
                    }
                }
                rs.Add(pc2);
                CacheHelper.Set<List<PlotCharModel>>(cacheKey, rs, 15);
            }
            return Json(rs);
        }
    }
    

}

