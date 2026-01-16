using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using TokenAPi.Bussiness;

namespace TokenAPi.Controllers
{
    public class ProfileController : ApiController
    {
        public JsonResult<string> Get(string token)
        {
            string username;
            var x = JwtManager.ValidateToken(token, out username);
            return Json(username);
        }
    }
}
