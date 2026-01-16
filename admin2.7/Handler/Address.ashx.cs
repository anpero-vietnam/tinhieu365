using Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Web.Mvc.Controllers;

namespace admin.Handler
{
    /// <summary>
    /// Summary description for Address
    /// </summary>
    public class Address : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            var keyRequest = context.Request["op"];

            string captcha = context.Request["captcha"];
            
            int uid = AppSession.CurentProfile.UserId;
            String s1 = "";

            Boolean isAuthen = false;
            if (uid > 0)
            {
                isAuthen = true;

            }
            switch (keyRequest)
            {

                case "seachAddressBook":
                    {
                        if (isAuthen && UserProfileControl.IsUserInRole(uid,  AEnum.UserRole.CanSale))
                        {
                            try
                            {

                                try
                                {
                                    Dal.Profile.AddressBook add = new Dal.Profile.AddressBook();
                                    String detail = context.Request["detail"];
                                    System.Data.DataTable table = add.AdminSeachAddressBook(detail, "0");
                                    if (table != null && table.Rows.Count > 0)
                                    {
                                        s1 += @"<table class='table  table-bordered table-hover'><thead><tr class='heading'><th></th><th>Email</th><th>Phone</th></tr></thead><tbody id='listUserTb'>";
                                        for (int i = 0; i < table.Rows.Count; i++)
                                        {
                                            s1 += "<tr><td><a href='/addressbook/update?id=" + table.Rows[i]["id"] +  "' taget='_blank' title='cập nhật liên hệ'><i class='icon-pencil'></i> </a> <a href='/cashbook?cash_id=&amp;productId=&amp;cash_created_from=&amp;cash_created_to=&amp;staffName=&amp;customerName=" + table.Rows[i]["id"] + "&amp;departMents=0&amp;subsection=0&amp;cashType=1&amp;keyw=&amp;bankId=bank_0&amp;ischecked=%25&amp;returlurl=/addressbook' title='Lịch sử giao dịch'><i class='fa icon-bar-chart'></i></a></td>";
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
                        }
                        break;
                    }
                case "bindAdd":
                    {

                        if (isAuthen && UserProfileControl.IsUserInRole(uid,  AEnum.UserRole.CanSale))
                        {
                            try
                            {

                                try
                                {
                                    Dal.Profile.AddressBook ad = new Dal.Profile.AddressBook();
                                    String detail = context.Request["detail"];

                                    System.Data.DataTable table = ad.AdminSeachAddressBook(detail, "0");
                                    if (table != null && table.Rows.Count > 0)
                                    {
                                        for (int i = 0; i < table.Rows.Count; i++)
                                        {
                                            s1 += @"<ul class='widgetList'>";
                                            s1 += @"<li> Tên LH: " + table.Rows[i]["name"].ToString() + "</li>";
                                            s1 += @"<li> Điện thoại: " + table.Rows[i]["phone"].ToString() + "</li>";
                                            s1 += @"<li> Email: " + table.Rows[i]["mail"].ToString() + "</li>";
                                            s1 += @"<li> Địa chỉ: " + table.Rows[i]["address"].ToString() + "</li>";
                                            s1 += @"<li class='adc'><a href='javascript:setAddress(" + table.Rows[i]["id"].ToString() + @");' id='adr" + table.Rows[i]["id"].ToString() + @"'><i class='fa fa-sign-in'></i> Đặt làm địa chỉ nhận</a></li>";
                                            s1 += "<li class='ads'> <a href='javascript:setSender(" + table.Rows[i]["id"].ToString() + @");' id='ads" + table.Rows[i]["id"].ToString() + @"'><i class='fa fa-sign-out'></i> Đặt làm địa chỉ gửi</a></li>";

                                            if (DBNull.Value != table.Rows[i]["IsActive"] && Convert.ToInt32(table.Rows[i]["IsActive"]) == 1)
                                            {
                                                s1 += "<li><a href='/addressbook/active?id=" + table.Rows[i]["id"].ToString() + @"'  style='color:red;'><i class='fa fa-check'></i>Mã KH: " + table.Rows[i]["userCode"].ToString() + "</a></li>";
                                            }
                                            else
                                            {
                                                s1 += "<li><a href='/addressbook/active?id=" + table.Rows[i]["id"].ToString() + @"'><i class='fa fa-sign-out'></i> Yêu cầu cấp mã</a></li>";
                                            }

                                            s1 += @"</ul>";
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    
                                    s1 = "Đăng nhập để cập nhật sổ địa chi";
                                }


                            }
                            catch (Exception)
                            {
                                s1 = "0";
                            }
                        }
                        break;
                    }
                case "updateAdd":
                    {
                        if (isAuthen && UserProfileControl.IsUserInRole(uid, AEnum.UserRole.CanSale))
                        {
                            try
                            {
                                Models.Contact contact = new Models.Contact();
                                contact.Id = Convert.ToInt32(context.Request["adid"]);
                                contact.Phone = context.Request["phone"];
                                contact.Mail = context.Request["mail"];
                                contact.Name= context.Request["name"];
                                contact.Address = context.Request["address"];
                                contact.TaxCode = context.Request["taxcode"];
                                contact.Province = context.Request["Province"];
                                contact.District = context.Request["District"];
                                Dal.Profile.AddressBook ad = new Dal.Profile.AddressBook();                                
                                s1 = ad.UpdateAddress(contact).ToString();

                            }
                            catch (Exception)
                            {
                                s1 = "0";
                            }
                        }
                        break;
                    }
           
                case "addAdress":
                    {
                        if (isAuthen && UserProfileControl.IsUserInRole(uid, AEnum.UserRole.CanSale))
                        {
                            try
                            {

                                String name = context.Request["name"];
                                String phone = context.Request["phone"];
                                String mail = context.Request["mail"];
                                String address = context.Request["address"];
                                String taxcode = context.Request["taxcode"];
                                Boolean valid = true;
                                if (valid && Ultil.StringHelper.isEmail(mail) && Ultil.StringHelper.isVnPhone(phone))
                                {
                                    Dal.Profile.AddressBook Address = new Dal.Profile.AddressBook();
                                    s1 = Address.AddAddress(uid, Ultil.StringHelper.RemoveHtmlTangs(name), mail, phone, Ultil.StringHelper.SubString(500, Ultil.StringHelper.RemoveHtmlTangs(address)), taxcode).ToString();
                                }
                              ;

                            }
                            catch (Exception)
                            {
                                s1 = "0";
                            }
                        }
                        break;
                    }
                case "adminAddAdress":
                    {
                        if (isAuthen && UserProfileControl.IsUserInRole(uid, AEnum.UserRole.CanSale))
                        {
                            try
                            {

                                String name = context.Request["name"];
                                String phone = context.Request["phone"];
                                String mail = context.Request["mail"];
                                String address = context.Request["address"];
                                String taxcode = context.Request["taxcode"];
                                if (string.IsNullOrEmpty(mail))
                                {

                                    mail = "";
                                }
                                Boolean valid = true;
                                if (valid && Ultil.StringHelper.isVnPhone(phone))
                                {
                                    Dal.Profile.AddressBook Address = new Dal.Profile.AddressBook();
                                    s1 = Address.AddAddress(0, Ultil.StringHelper.RemoveHtmlTangs(name), mail, phone, Ultil.StringHelper.SubString(500, Ultil.StringHelper.RemoveHtmlTangs(address)), taxcode).ToString();
                                }


                            }
                            catch (Exception)
                            {
                                s1 = "0";
                            }
                        }
                        break;
                    }
            }
            context.Response.ContentType = "text/plain";
            context.Response.Write(s1);
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