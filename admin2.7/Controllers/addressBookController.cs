using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dal;
using Dal.Dapper;
using Models;
using Ultil;

namespace Web.Mvc.Controllers
{
    [IsAuthenlication(RoleName = "CanViewAllCustomer")]
    public class AddressBookController : BaseController
    {

        Dal.Profile.AddressBook ad = new Dal.Profile.AddressBook();
        //[Authorize]
        //[HttpGet]
        //public ActionResult Index(String orderStatus, String uid, int st)
        //{
        //    bool valid =true;
        //    SetUpAll();            
        //    if (valid)
        //    {
        //        try
        //        {
        //            if (String.IsNullOrEmpty(uid))
        //            {
        //                uid = "%";
        //            }
        //            else
        //            {
        //                uid = uid.ToLower().Replace("kh", string.Empty);
        //            }
        //            string page = Request.QueryString["page"];
        //            if (String.IsNullOrEmpty(page))
        //            {
        //                page = "1";
        //            }
        //            if (String.IsNullOrEmpty(orderStatus))
        //            {
        //                orderStatus = "2";
        //            }
        //            Dal.Analytic ans = new Dal.Analytic();
        //            ans.SetUpMainAnalytic(StoreId);
        //            Dal.Themes t = new Dal.Themes();
        //            ViewBag.paged = Ultil.StringHelper.SetUpPagedV2(Convert.ToInt32(page), 30, allUserBought, 10, "&page=");

        //        }
        //        catch (Exception)
        //        {


        //        }
        //    }

        //    return View();
        //}

        public ActionResult List(string keyword)
        {

            List<Contact> listContact = new List<Contact>();
            SetUpAll();
            try
            {
                if (!String.IsNullOrEmpty(keyword))
                {
                    keyword = keyword.ToLower().Replace("kh", string.Empty);
                }
                string page = Request.QueryString["page"];
                if (String.IsNullOrEmpty(page))
                {
                    page = "1";
                }

                Dal.Profile.AddressBook ad = new Dal.Profile.AddressBook();

                int count = 0;
                listContact = ad.SeachAddressBook(Convert.ToInt32(page), 30, keyword, out count);
                ViewBag.paged = Ultil.StringHelper.SetUpPagedV2(Convert.ToInt32(page), 30, count, 10, "&page=");
            }
            catch (Exception)
            {

            }

            return View(listContact);
        }
        [Authorize]
        public ActionResult GetList(string keyword, int st)
        {
            bool valid = false;
            if (Security.Store.IsInRole(AEnum.UserRole.CanViewAllCustomer))
            {
                valid = true;
            }
            if (valid)
            {
                SetUpAll();
                try
                {
                    if (!String.IsNullOrEmpty(keyword))
                    {
                        keyword = keyword.ToLower().Replace("kh", string.Empty);
                    }

                    string page = Request.QueryString["page"];
                    if (String.IsNullOrEmpty(page))
                    {
                        page = "1";
                    }

                    Dal.Profile.AddressBook ad = new Dal.Profile.AddressBook();

                    int count = 0;
                    ViewData["tableRs"] = ad.SeachAddressBook(Convert.ToInt32(page), 30, keyword, out count);

                    ViewBag.paged = Ultil.StringHelper.SetUpPagedV2(Convert.ToInt32(page), 30, count, 10, "&page=");

                }
                catch (Exception)
                {

                }
            }
            return View("");
        }
        [Authorize]
        public ActionResult Update(int id, int st)
        {
            SetUpAll();
            Models.Contact contact = ad.GetAddressById(id);
            contact.Address = string.IsNullOrEmpty(contact.Address) ? string.Empty : contact.Address.Replace(@"<br/>", "\n");
            return View(contact);
        }

        public string Search(string detail, int st)
        {
            string s1 = "";
            if (UserProfileControl.IsUserInRole(AppSession.CurentProfile.UserId, AEnum.UserRole.CanViewAllCustomer))
            {
                try
                {

                    try
                    {
                        Dal.Profile.AddressBook add = new Dal.Profile.AddressBook();
                        System.Data.DataTable table = add.AdminSeachAddressBook(detail, "0");
                        if (table != null && table.Rows.Count > 0)
                        {
                            s1 += @"<table class='table  table-bordered table-hover'><thead><tr class='heading'><th></th><th>Email</th><th>Phone</th></tr></thead><tbody id='listUserTb'>";
                            for (int i = 0; i < table.Rows.Count; i++)
                            {
                            
                                s1 += "<tr><td><a href='/addressbook/update?id=" + table.Rows[i]["id"] + "&returlurl=/order&st=" + "' taget='_blank' title='cập nhật liên hệ'><i class='icon-pencil'></i> </a> </td>";
                                s1 += "<td><a href='#' class=\"setaddresshng\" data-name='" + table.Rows[i]["name"] + "' data-id='" + table.Rows[i]["id"] + "'>" + table.Rows[i]["mail"] + " (Chọn)</a><br>" + table.Rows[i]["name"] + "</td><td>" + table.Rows[i]["phone"] + "</td></tr>";
                            }
                            s1 += "</tbody></table>";
                        }
                    }
                    catch (Exception ex)
                    {
                        s1 = "Lỗi sổ địa chỉ";
                    }
                }
                catch (Exception)
                {
                    s1 = "0";
                }
                return s1;
            }
            else
            {
                return "Bạn cần cấp quền xem khách hàng để sử dụng chức năng này";
            }


        }


    }
}
