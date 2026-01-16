using AModul;
using AModul.Common;
using Dal;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Web.Mvc.Controllers;

namespace admin.Handler
{
    /// <summary>
    /// Summary description for Theme
    /// </summary>
    public class Theme : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            var keyRequest = context.Request["op"];
            var uid = AppSession.CurentProfile.UserId;
            Boolean isAuthen = false;
            MenuControl t = new MenuControl();
            if (UserProfileControl.IsUserInRole(uid, AEnum.UserRole.Admin))
            {
                isAuthen = true;
            }
            String rs = "";
            if (isAuthen)
            {
                switch (keyRequest)
                {
                  
                    case "UpdatePrThumb":
                        {

                            var id = context.Request["imgId"];
                            var txtLinkClick = context.Request["txtLinkClick"];
                            var txtDesc = context.Request["txtDesc"];
                            var prioty = context.Request["prioty"];
                            var referID = string.IsNullOrEmpty(context.Request["referID"]) ? "" : context.Request["referID"];

                            var imgLink = context.Request["imagesLink"];
                            prioty = string.IsNullOrEmpty(prioty) ? "0" : prioty;
                            
                            rs = t.UpdateImgReferer(txtLinkClick, txtDesc, Convert.ToInt32(id), prioty, imgLink, referID).ToString();
                            break;
                        }
                  
                    case "updateMenu":
                        {

                            var id = context.Request["id"];
                            var menuText = context.Request["menuText"];
                            var link = context.Request["link"];
                            var parent = context.Request["parent"];
                            var possition = context.Request["possition"];
                            var prioty = context.Request["prioty"];
                            
                            rs = t.UpdateMenu(menuText, link, prioty, Convert.ToInt32(id), Convert.ToInt32(parent)).ToString();
                            string commontDataKey = AEnum.Cache.Cachingkey.CommonInfo_FontEnd_.ToString() ;
                            Ultil.Cache.CacheHelper.Remove(commontDataKey);
                            break;
                        }
                    case "delMenu":
                        {
                            var id = context.Request["id"];
                            
                            int rsA = t.DelMenu(id);
                            rs = rsA.ToString();
                            
                            break;
                        }

                    case "GetTableMenu":
                        {
                            
                            MenuControl mmenuControl = new MenuControl();
                            int parent = Convert.ToInt32(context.Request["parent"]);

                            string  position = context.Request["position"]==null?"": context.Request["position"].ToString();
                            var table = mmenuControl.GetMenu(parent,false,position);
                            if (table != null && table.Count() > 0)
                            {
                                rs += "<table class='table table-striped table-bordered table-hover'>";
                                rs += "<tr><th>Id</th><th>Chữ hiển thị</th><th>link</th><th>menu Cha</th><th>thứ tự</th><th>Cập nhật</th></tr>";
                                foreach (var item in table)
                                {

                                    rs += "<tr><td>" + item.Id + "</td>";
                                    rs += "<td><input type='text' value='" + item.Tittle + "' id='menuT_" + item.Id + "' class='form-control'></td>";
                                    rs += "<td><input type='text' value='" + item.Link+ "' id='menuL_" + item.Id + "' class='form-control'></td>";
                                    rs += "<td><input type='text' value='" + item.ParentId + "' id='menuP_" + item.Id + "' class='form-control'></td>";
                                    rs += "<td><input type='text' value='" + item.Prioty+ "' id='menuPri_" + item.Id + "' class='form-control'></td>";
                                    rs += "<td><a href='javascript:UpdateMenu(" + item.Id + ");' class='btn btn-success dropdown-toggle'><i class='fa  fa-floppy-o'></i></a> <a href='javascript:delMenu(" + item.Id + ");' class='btn red filter-cancel'><i class='fa fa-trash-o'></i></a></td></tr>";
                                }
                                
                                rs += "</table>";
                            }

                            break;
                        }
                    case "GetSelectMenu":
                        {
                            
                            string position = context.Request["position"] == null ? "master" : context.Request["position"].ToString();

                            var table = t.GetMenu( 0,false, position);
                            if (table != null && table.Count() > 0)
                            {
                                rs += "<select id='menuParent' class='form-control'><option value='0'>- Đặt làm menu cha</option>";

                                foreach (var item in table)
                                {
                                    rs += "<option value='" + item.Id + "'>-- " + item.Tittle+ "</option>";
                                }                               
                                rs += "</select>";
                            }
                            else
                            {
                                rs += "<select id='menuParent' class='form-control'><option value='0'>- Đặt làm menu cha</option></select>";
                            }
                            break;
                        }
                    case "addMenu":
                        {
                            string name = context.Request["name"];
                            string menuLink = context.Request["menuLink"];
                            string menuPrioty = context.Request["menuPrioty"];
                            string menuPosition = context.Request["menuPosition"];
                            string menuParent = context.Request["menuParent"];
                            
                            rs = t.SetMenu(name, menuLink, menuPosition, menuPrioty, Convert.ToInt32(menuParent)).ToString();
                            string commontDataKey = AEnum.Cache.Cachingkey.CommonInfo_FontEnd_.ToString() ;
                            Ultil.Cache.CacheHelper.Remove(commontDataKey);
                            break;
                        }
                }
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