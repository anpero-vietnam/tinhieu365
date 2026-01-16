using BackendService.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace BackendService.Controllers
{
    [RoutePrefix("api/StoreInfo")]
    public class StoreInfoController : ApiController
    {
        [HttpGet]
        //[IsAuthenlication]
        public JsonResult<List<Models.StoreInfo>> Get(string APITokenKey)
        {
            if (APITokenKey == AEnum.SiteConfig.MasterAPITokenKey)
            {
                StoreMng.Store st = new StoreMng.Store();
                return Json(st.AdminGetAllStoreInfo());
            }
            else
            {
                return Json(new List<Models.StoreInfo>());
            }

        }
    }
}
