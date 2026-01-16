using AModul.Bill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace admin.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment
        BillControl billControl = new BillControl();
        public ActionResult Index(bool isCredit)
        {
            
            var model = billControl.AdminGetBill(isCredit);
            return View(model);
        }
        public ActionResult Transaction(bool isCredit)
        {            
            var model = billControl.AdminGetBill(isCredit);
            return View(model);
        }

    }
}