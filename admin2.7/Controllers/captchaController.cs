using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Web;
using System.Web.Mvc;
using mvcAdminV2.Controllers;
using Dal;

namespace Web.Mvc.Controllers
{
    public class captchaController : Controller
    {
        //
        // GET: /captcha/
        private string[] CreateCapcha()
        {
            makeRandom rd = new makeRandom();
            return rd.RanDomTextVi();
        }
        //public static String captchaNum = "";

        [OutputCache(NoStore = true, Duration = 0)]
        public void Index()
        {

            Session["capcha"] = CreateCapcha();
            string[] cc = (string[])Session["capcha"];
            // String capchaText = cc[0];
            //captchaNum = cc[1];
            string[] ss = (string[])Session["capcha"];

            RandomImage ci = new RandomImage(cc[1], 200, 50);
            // Change the response headers to output a JPEG image.
            this.Response.Clear();
            this.Response.ContentType = "image/jpeg";
            // Write the image to the response stream in JPEG format.
            ci.Image.Save(this.Response.OutputStream, ImageFormat.Jpeg);
            // Dispose of the CAPTCHA image object.
            ci.Disposes();
            //return View();
        }



    }

}
