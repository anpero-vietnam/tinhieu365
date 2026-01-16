using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontEnd.Controllers
{
    public class LocationController : Controller
    {
        // GET: Location
        public JsonResult GetLocation(int ParentLocationId = 0)
        {
            Dal.LocationControl locationControl = new Dal.LocationControl();
            return Json(locationControl.GetLocation(ParentLocationId));
        }        

    }
}