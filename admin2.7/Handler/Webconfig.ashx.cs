using Dal;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Web.Mvc.Controllers;

namespace admin.Handler
{
    /// <summary>
    /// Summary description for Webconfig
    /// </summary>
    public class Webconfig : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            var keyRequest = context.Request["op"];
            
            int uid = AppSession.CurentProfile.UserId;
            String rs = "0";
            Boolean isAuthen = false;
            if (UserProfileControl.IsUserInRole(uid, AEnum.UserRole.CanSale))
            {
                isAuthen = true;
            }
            if (isAuthen)
            {
                switch (keyRequest)
                {
                    case "update":
                        {

                            Models.Webconfig config= new Models.Webconfig();
                            config.OtherPhone = context.Request["otherPhone"];
                            config.Footer = context.Request["footerText"];
                            config.PageScript = context.Request["pageScript"];
                            config.HotLine = context.Request["hotline"];
                            config.Skype = context.Request["Skype"];
                            config.Email = context.Request["email"];
                            config.Address = context.Request["address"];
                            config.FaceBookLink = context.Request["facebookLink"];
                            config.ShortDesc = context.Request["shortDesc"];
                            config.Logo = context.Request["Logo"];
                            config.Favicon = context.Request["favicon"];
                            config.Title = context.Request["Title"];
                            StoreMng.WebConfigControl wc = new StoreMng.WebConfigControl();
                            if (!string.IsNullOrEmpty(config.HotLine))
                            {
                                config.HotLine = config.HotLine.Replace(@".", string.Empty).Replace(@",", string.Empty);
                            }
                            rs = wc.UpdateWebConfig(config).ToString();
                            break;
                        }
                    case "updateJavascript":
                        {
                            StoreMng.WebConfigControl wc = new StoreMng.WebConfigControl();
                            rs = wc.UpdateJavaScript(context.Request["script"]).ToString();
                            break;
                        }
                        
                    //case "updatePaymentConfig":
                    //    {
                    //        string paymentType = context.Request["paymentType"];
                    //        string merchantId = context.Request["merchantId"];
                    //        string merchantPass = context.Request["merchantPass"];
                    //        int isDefault =Convert.ToInt32(context.Request["isDefault"]);
                    //        StoreMng.Config.PaymentConfigControl wc = new StoreMng.Config.PaymentConfigControl();
                    //        StoreMng.Config.PaymentConfig pc = new StoreMng.Config.PaymentConfig();
                    //        pc.MerchantId = merchantId;
                    //        pc.Email = context.Request["email"];
                    //        pc.MerchantPassword = merchantPass;
                    //        pc.Isdefault = isDefault==1?true:false;
                    //        pc.Name = paymentType;
                    //        pc.PaymentCode = paymentType;
                    //        pc.Token = "";
                    //        pc.St = st;
                    //        pc.PaymentFee =Convert.ToInt32(context.Request["paymentFee"]);
                    //        // footerText: _footerText,pageScript: _pageScript,hotline: _hotline,Skype: _Skype,facebookLink: _facebookLink},
                    //        rs = wc.UpdatePaymentConfig(pc).ToString();
                    //        break;
                    //    }
                    case "getPaymentConfig":
                        {
                            StoreMng.Inteface.IPaymentConfig wc = new StoreMng.Config.PaymentConfigControl();
                            string paymentType = context.Request["paymentType"];
                            List<PaymentConfig> list = wc.GetPaymentConfigList();
                            if(list!=null && list.Count > 0)
                            {
                                for (int i = 0; i < list.Count; i++)
                                {
                                    if(list[i].PaymentCode== paymentType)
                                    {
                                        rs=  Newtonsoft.Json.JsonConvert.SerializeObject(list[0]);
                                    }
                                }
                            }
                            

                            break;
                        }

                    default:
                        break;
                }

            }
            else
            {
                rs = "-1";
            }
            context.Response.ContentType = "text/plain";
            context.Response.Write(rs);
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