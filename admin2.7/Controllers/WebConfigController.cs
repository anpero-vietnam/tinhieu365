using Dal;
using Models.Modul.Common;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Web.Mvc.Controllers
{
    [IsAuthenlication(RoleName = "admin")]
    public class WebConfigController : BaseController
    {
        // GET: WebConfig

        public ActionResult Index()
        {
            StoreMng.WebConfigControl wc = new StoreMng.WebConfigControl();
            SetUpAll();
            return View(wc.AdminGetWebConfig());
        }

        public ActionResult Footer()
        {
            StoreMng.WebConfigControl wc = new StoreMng.WebConfigControl();
            ViewData["webconfig"] = wc.AdminGetWebConfig();
            SetUpAll();
            return View();
        }

        public ActionResult Css()
        {
            StoreMng.WebConfigControl wc = new StoreMng.WebConfigControl();
            ViewData["webconfig"] = wc.AdminGetWebConfig();
            SetUpAll();
            return View();
        }
        [HttpGet]
        public ActionResult UpdateKeyValue()
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            keyValuePairs.Add("Vip1Price", string.Empty);
            keyValuePairs.Add("Vip2Price", string.Empty);
            keyValuePairs.Add("Vip1Day", string.Empty);
            keyValuePairs.Add("Vip2Day", string.Empty);

            KeyValueConfigControl keyValueConfigControl = new KeyValueConfigControl();
            List<KeyValueConfigModel> keyValueList = keyValueConfigControl.GetAllKeyValueConfig();

            var q = from c in keyValuePairs
                    join p in keyValueList on c.Key equals p.Key into ps
                    from p in ps.DefaultIfEmpty()
                    select new KeyValueConfigModel { Key = c.Key,Value=p?.Value,Description=p?.Description};
            var model = q.ToList();                
            return View(model);
        }
        [HttpPost]
        public int UpdateKeyValue(string Key,string description,string value)
        {
            KeyValueConfigControl keyValueConfigControl = new KeyValueConfigControl();
            return keyValueConfigControl.UpdateKeyValueConfig(Key, value, description);
        }

    }
}