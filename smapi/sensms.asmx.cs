using eSMSAPI_Demo.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace eSMSAPI_Demo
{
    /// <summary>
    /// Summary description for sensms
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class sensms : System.Web.Services.WebService
    {
        string pws = "tmtshippingthangtd79nothing";
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        public string send(String pw, String to, String content,String type)
        {
              if(pw.Equals(pws)){              
              
            sendSms Send = new sendSms();
                ///=4 là 19001534
                ///=6 là 8755
            Send.Send(to, content,type);
            return "1";
              }else{
                      return "1";
              }

            
        }
        [WebMethod]
        public string SendBrandname(String pw, String to, String content)
        {
              if(pw.Equals(pws)){              
              
                    sendSms Send = new sendSms();
                    Send.SendBrandname(to, content);
                    return "1";
                  }else{
                    return "1";
              }

            
        }

    }
}
