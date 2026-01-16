using adminv2._4.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace admin.Controllers
{
    public class SuperAdminController : Controller
    {
        


        // GET: SuperAdmin        
        [Authorize]
        [Authorize(Roles = "addmin")]
        [InitializeSimpleMembership]
        public ActionResult Index()
        {
            ViewBag.SMSBlank = GetSmsBlank();
            return View();
        }
        private string GetSmsBlank()
        {
            try
            {
                String SmsAPIKey = System.Configuration.ConfigurationManager.AppSettings["SmsAPIKey"];
                string SmsSecretKey = System.Configuration.ConfigurationManager.AppSettings["SmsSecretKey"];
                String rs = "http://api.esms.vn/MainService.svc/xml/GetBalance/" + SmsAPIKey + "/" + SmsSecretKey;
                System.Xml.XmlTextReader reader = new System.Xml.XmlTextReader(rs);

                reader.WhitespaceHandling = System.Xml.WhitespaceHandling.None;
                System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
                //Load the file into the XmlDocument
                xmlDoc.Load(reader);
                //Close off the connection to the file.
                System.Xml.XmlNodeList xnList = xmlDoc.SelectNodes("/MemberModel ");

                reader.Close();
                //foreach (XmlNode xn in xnList)
                //{
                string Balance = xnList[0]["Balance"].InnerText;
                string CodeResponse = xnList[0]["CodeResponse"].InnerText;

                return Ultil.StringHelper.ConVertToMoneyFormatInt(Balance);
            }
            catch (Exception)
            {

                return "0";
            }

            //}


        }
    }
}