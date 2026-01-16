using AModul.Article;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontEnd.Controllers
{
    public class StaticController : Controller
    {
        [OutputCache(Duration = 300, VaryByParam = "none")]
        public ContentResult Css()
        {
            Response.AppendHeader("Cache-Control", "max-age=1200,stale-while-revalidate=3600"); // HTTP 1.1.
            Response.AppendHeader("Pragma", "no-cache"); // HTTP 1.0.
            Response.AppendHeader("Cache-Control", "public");
            Response.AppendHeader("Content-Type", "text/css");

            WebContentControl webContentControl = new WebContentControl();
            return Content(webContentControl.GetWebContent(7).TextContent, "text/css");
        }
    }
}