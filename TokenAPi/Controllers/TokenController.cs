using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web.Http;
using System.Web.Http.Results;
using TokenAPi.Bussiness;

namespace TokenAPi.Controllers
{
    public class TokenController : ApiController
    {
        public JsonResult<string> Get(string username, string password)
        {
            // need check userName
            //var membership = (WebMatrix.WebData.SimpleMembershipProvider)Membership.Provider;
            //var x = membership.ValidateUser(model.UserName, model.Password);
            return Json(JwtManager.GenerateToken(username, "userId", "sureName", 1));
        }      
    }
}
