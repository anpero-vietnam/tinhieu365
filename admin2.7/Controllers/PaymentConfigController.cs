
using Models;
using StoreMng;
using StoreMng.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Mvc.Controllers
{
    [IsAuthenlication]
    public class PaymentConfigController : BaseController
    {
        StoreMng.Inteface.IPaymentConfig paymentControl = new PaymentConfigControl();
        // GET: PaymentConfig
        public ActionResult Index()
        {
            SetUpAll();
            return View();
        }
        public string UpdatePaymentConfig(PaymentConfig model)
        {
            return paymentControl.UpdatePaymentConfig(model).ToString();
        }
       
    }
}