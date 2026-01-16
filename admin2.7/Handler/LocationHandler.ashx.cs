using Models.Modul.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace admin.Handler
{
    /// <summary>
    /// Summary description for LocationHandler
    /// </summary>
    public class LocationHandler : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            List<Location> LocationList = null;
            string LocationListHtml = "";
            Dal.LocationControl sv = new Dal.LocationControl();
            try
            {
                int ParentLocationId = Convert.ToInt32(context.Request["ParentLocationId"]);
                LocationList = sv.GetLocation(ParentLocationId);
                if (ParentLocationId == 0)
                {
                    LocationListHtml += "<option value='0'>Tỉnh / thành phố</option>";
                }
                else
                {
                    LocationListHtml += "<option value='0'>Quận / Huyện</option>";
                }
            }
            catch (Exception)
            {
                LocationList = sv.GetLocation(0);
                throw;
            }
            if (LocationList != null && LocationList.Count > 0)
            {
                foreach (Location item in LocationList)
                {
                    LocationListHtml += "<option value='" + item.Id + "'>" + item.Name + "</option>";
                }
            }
            context.Response.ContentType = "text/plain";
            context.Response.Write(LocationListHtml);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}