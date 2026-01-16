using Models;
using StoreMng;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace BackendService.Controllers
{
    public class CommontInfo : ApiController
    {
   
        // GET api/<controller>/5
        //public JsonResult<Webconfig> Get([FromUri]int st)
        //{
        //        Webconfig wc = new Webconfig();
        //        WebConfigControl config = new WebConfigControl();
        //        AnperoClient client = StoreMng.ClientControl.GetClientByAgencyId(st);
        //        wc = config.GetCommonConfig(client);
            
        //   return Json(wc).ToString();            
        //}

        // POST api/<controller>
        public void Post([FromBody]string value)
        {

        }
    }
}