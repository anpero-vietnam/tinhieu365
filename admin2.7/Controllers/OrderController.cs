using adminv2._4.Filters;
using AModul.Product;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Mvc.Controllers;

namespace mvcAdminV2.Controllers
{
    public class OrderController : BaseController
    {


        [IsAuthenlication]
        public string UpdateOrder(string odSST, string orderDetail, int orID)
        {
            if (string.IsNullOrEmpty(odSST)) {
                odSST = "0";
            }
             OrderControl od = new OrderControl();
             return od.UpdateOD(Convert.ToInt32(orID), Convert.ToInt32(odSST), 0, orderDetail).ToString();
        }
        [InitializeSimpleMembership]
        [Authorize]
        [Authorize(Roles = "addmin,mod,accounting,maketing")]

        public ActionResult printOrder()
        {

            //int id = Convert.ToInt32(Request["id"]);
            //Dal.orderMng.order or = new Dal.orderMng.order();
            //or.GetOrderById(id);
            //ViewData["order"] = or;
            return View();
        }
        //
        // GET: /Order/
        [Authorize]
        [Authorize(Roles = "addmin,mod,accounting,maketing")]
        [InitializeSimpleMembership]
        public ActionResult updateProduct()
        {
            //setUpAll();
            //int id = Convert.ToInt32(Request.QueryString["id"]);
            //Dal.orderMng.orderProduct op = new Dal.orderMng.orderProduct();
            //op.getProductbyId(id);          
            //ViewData["op"] = op;
            //Dal.authen.AddressBook add = new Dal.authen.AddressBook();
            //if (!String.IsNullOrEmpty(op.AddressId.ToString()) && op.AddressId != 0)
            //{
            //    add.GetAddressByIdAndUid(0,op.AddressId);
            //    ViewData["recip"] = add;
            //}

            //Dal.UserProfile sender = new Dal.UserProfile();
            //sender.getUserProfileById(op.UserId.ToString(),"0");
            //ViewData["sender"] = sender;
            return View();

        }
        [Authorize]
        [Authorize(Roles = "addmin,mod,accounting,maketing")]
        [InitializeSimpleMembership]
        public ActionResult Index()
        {
            SetUpAll();

            return View();

        }
        [HttpGet]
        [Authorize]
        [Authorize(Roles = "addmin,accounting,maketing")]
        [InitializeSimpleMembership]
        public ActionResult Index(String page)
        {
            try
            {
                SetUpAll();
                String KhId = Request["KhId"];
                if (String.IsNullOrEmpty(KhId))
                {
                    KhId = "%";
                }
                else
                {
                    KhId = KhId.ToUpper().Replace("KH", string.Empty);
                }
                String orderId = Request["product_id"];
                if (String.IsNullOrEmpty(orderId))
                {
                    orderId = "%";
                }
                else
                {
                    orderId = orderId.ToUpper().Replace("SH", String.Empty);
                }
                String createDateFrom = Request["product_created_from"];
                if (String.IsNullOrEmpty(createDateFrom))
                {
                    createDateFrom = "0";
                }
                else
                {
                    try
                    {
                        createDateFrom = Ultil.Times.GetyyyyMMddhhmm(createDateFrom, true);
                    }
                    catch (Exception)
                    {

                        createDateFrom = "0";
                    }
                }
                String createDateTo = Request["product_created_to"];
                if (String.IsNullOrEmpty(createDateTo))
                {
                    createDateTo = "999999999999";
                }
                else
                {
                    try
                    {
                        createDateTo = Ultil.Times.GetyyyyMMddhhmm(createDateTo);
                    }
                    catch (Exception)
                    {

                        createDateTo = "299999999999";
                    }
                }
                String customerName = Request["customerName"];
                if (String.IsNullOrEmpty(customerName))
                {
                    customerName = "%";
                }
                else
                {
                    customerName = customerName.ToUpper().Replace("KH", String.Empty);
                }
                String orderType = Request["orderType"];
                if (orderType.Equals("0"))
                {
                    orderType = "%";
                }
                String productQuantityFrom = Request["product_quantity_from"];
                if (String.IsNullOrEmpty(productQuantityFrom))
                {
                    productQuantityFrom = "0";
                }
                String productQuantityTo = Request["product_quantity_to"];
                if (String.IsNullOrEmpty(productQuantityTo))
                {
                    productQuantityTo = "999999999";
                }

                String orderStatus = Request["orderStatus"];
                if (String.IsNullOrEmpty(orderStatus) || orderStatus.Equals("-2"))
                {
                    orderStatus = "%";
                }
                String pages = Request["page"];
                if (String.IsNullOrEmpty(page))
                {
                    pages = "1";
                }
                //Dal.orderMng.order or = new Dal.orderMng.order();
                //ViewData["resoult"] = or.spTB_orderSeach(Convert.ToInt32(pages), 60, orderId, createDateFrom, createDateTo, customerName, orderType, productQuantityFrom, productQuantityTo, orderStatus,KhId);
                //int rs = or.spTB_orderSeachRs(Convert.ToInt32(pages), 60, orderId, createDateFrom, createDateTo, customerName, orderType, productQuantityFrom, productQuantityTo, orderStatus,KhId);
                // Dal.Themes t = new Dal.Themes();

                // ViewBag.page = t.setUpPagedV2(Convert.ToInt32(pages), 18, rs, 5, @"&page=");
            }
            catch (Exception)
            {


            }
            return View();

        }
        [Authorize]
        [Authorize(Roles = "addmin,mod,accounting,maketing")]
        [InitializeSimpleMembership]
        [Authorize]
        [Authorize(Roles = "addmin,mod,accounting,maketing")]
        public ActionResult Detail()
        {
            //setUpAll();
            //int id = Convert.ToInt32(Request["id"]);
            //Dal.orderMng.order or = new Dal.orderMng.order();
            //or.GetOrderById(id);
            //ViewData["order"] = or;
            return View();
        }
        [InitializeSimpleMembership]
        [Authorize]
        [Authorize(Roles = "addmin,mod,accounting,maketing")]
        public ActionResult product()
        {

            return View();
        }
        [Authorize(Roles = "addmin,accounting")]
        public ActionResult ProductCateGory()
        {
            return View();
        }




    }
}
