using AModul.Article;
using AModul.Product;
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
    /// Summary description for CategoryHandler
    /// </summary>
    public class CategoryHandler : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            var keyRequest = context.Request["op"];
            
            String s1 = "";
            
            
            Boolean isvalid = false;
            ArticleCategoryControl categoryControl = new ArticleCategoryControl();
            if (UserProfileControl.IsUserInRole(AppSession.CurentProfile.UserId,  AEnum.UserRole.CanUpdateAndAddNew))
            {
                isvalid = true;
            }
            switch (keyRequest)
            {
                case "BindCatTable":
                    {
                        try
                        {
                            int id = Convert.ToInt32(context.Request["parentId"]);
                            Category cat = new Category();

                            if (id > 0)
                            {

                                var table = cat.GetCategory(Convert.ToInt32(id));
                                if (table != null && table.Count > 0)
                                {
                                    s1 += "<table class='table table-striped table-bordered table-hover table-scrollable'>";
                                    s1 += "<tr><th>Xóa</th><th>Tên danh mục</th><th>Link</th><th>Cập nhật</th><th colspan='2'>Thứ tự</th></tr>";
                                    foreach (var item in table)
                                    {

                                        s1 += "<tr>";
                                        s1 += "<td><button onclick=\"Category.DeleteCat(" + item.Id + ");\" class=\"btn red\"><i class=\"fa fa-trash-o\"></i></button></td>";
                                        s1 += "<td>" + item.Name + "</td>";
                                        s1 += @"<td>" + Ultil.UrlHelper.GetCategoryLink(item.Name, item.Id) + "</td>";
                                        s1 += "<td><a href=\"/pr/updateCat?id=" + item.Id +  "\" class=\"btn blue\" target='_blank'><i class=\"glyphicon icon-pencil\"></i> Sửa</a></td>";
                                        s1 += "<td>" + item.Rank.ToString() + "</td>";
                                        s1 += "<td><a href='javascript:Category.UpdateRank(" + item.Id + ",0);'><i class='fa fa-arrow-up'></i></a><a href='javascript:Category.UpdateRank(" + item.Id + ",1);'><i class='fa fa-arrow-down'></i></a></td>";
                                        s1 += "</tr>";
                                    }
                                    s1 += "</table>";

                                }
                            }
                            else
                            {
                                Category pr = new Category();
                                var parentTable = pr.GetAllCategory(0);
                                if (parentTable != null && parentTable.Count > 0)
                                {
                                    s1 += "<table class='table table-striped table-bordered table-hover table-scrollable'>";
                                    s1 += "<tr><th>Xóa</th><th>Tên danh mục</th><th>Link</th><th>Cập nhật</th><th colspan='2'>Thứ tự</th></tr>";
                                    foreach (var item in parentTable)
                                    {


                                        s1 += "<tr>";
                                        s1 += "<td><button onclick=\"DeleteParentCat(" + item.Id + ");\" class=\"btn red\"><i class=\"fa fa-trash-o\"></i></button></td>";
                                        s1 += "<td><input type=\"text\" id=\"updateCat\" class=\"form-control has-warning\" value=\"" + item.Name + "\"/></td>";
                                        s1 += @"<td>/" + item.DefaultLink + "</td>";
                                        s1 += "<td><button onclick=\"Category.UpdateParentCat2(" + item.Id + ");\" class=\"btn blue\"><i class=\"glyphicon icon-pencil\"></i>Chỉnh sửa</button></td>";
                                        s1 += "<td>" + item.Rank + "</td>";
                                        s1 += "<td><a href='javascript:Category.UpdateParentRank(" + item.Id + ",1);'><i class='fa fa-arrow-up'></i></a><a href='javascript:Category.UpdateParentRank(" + item.Id + ",0);'><i class='fa fa-arrow-down'></i></a></td>";
                                        s1 += "</tr>";
                                    }
                                    s1 += "</table>";
                                }
                            }
                        }
                        catch
                        {
                            s1 = "Không có danh mục con";
                        }

                        break;
                    }
                case "UpdateRank":
                    {
                        int isPlus = Convert.ToInt32(context.Request["isPlus"]);
                        int id = Convert.ToInt32(context.Request["id"]);
                        Category cat = new Category();
                        s1 = cat.UpdateCategoryRank(id, isPlus).ToString();

                    }
                    break;            
                case "delCat":
                    {
                        if (isvalid)
                        {
                            try
                            {
                                int catId = Convert.ToInt32(context.Request["id"]);
                                int i = categoryControl.DelCategory(catId);
                                if (i > 0)
                                {
                                    s1 = "Xóa thành công";
                                }
                                else
                                {
                                    s1 = "Chủ đề có chứa chủ đề con, không xóa được";
                                }
                            }
                            catch (Exception e)
                            {
                                //tin không có
                                
                                s1 = "Chủ đề không thể xóa được nếu có chủ đề con trong đó";
                            }
                        }
                        else
                        {

                            s1 = "Vui lòng đăng nhập";
                        }

                    }
                    break;
                case "addCategory":
                    {
                        if (isvalid)
                        {
                            try
                            {
                                var SubName = context.Request["title"];                                
                                int rs = categoryControl.UpdateCategory(SubName);
                                s1 = rs>0?"thêm danh mục con thành công": "Có lỗi, hệ thống đã lưu lỗi này và chờ xử lý";
                            }
                            catch (Exception e)
                            {
                                //tin không có
                                
                                s1 = "Có lỗi, hệ thống đã lưu lỗi này và chờ xử lý";
                            }
                        }
                        else
                        {
                            s1 = "Vui lòng đăng nhập";
                        }

                    }
                    break;
                case "getArticleCategory":
                    {
                        if (isvalid)
                        {
                            try
                            {
                                var catId = context.Request["catId"];

                                var itemList = categoryControl.GetArticleCateGory();
                                if (itemList!=null && itemList.Count() > 0)
                                {
                                    s1 += @"<table class='table table-hover table-bordered table-striped'><thead><tr><th>ID</th><th>Tên Chủ đề con</th><th>Link chủ đề</th><th>Cập nhật</th><th>Xóa</th></tr></thead>";
                                    foreach (var item in itemList)
                                    {   
                                            s1 += @"<tr><td>" + item.Id + "</td><td><input type='text' id='txt" + item.Id + "' value='" + item.Description + "' class=\"form-control\"/></td><td><input type='text' value='/" + Ultil.StringHelper.ToSplitURLgach(item.Description.ToString().ToLower()) + @"-b" + item.Id + @"' disabled  class='form-control'></td><td><a href='javascript:updateSubCat(" + item.Id + @");'>cập nhật</a></td><td><a href='javascript:delSubCat(" + item.Id + ")'>xóa</a></td></tr>";
                                    }
                                    s1 += @"</table>";
                                }
                                else
                                {
                                    s1 = "Chưa có chủ đề con nào";
                                }



                            }
                            catch (Exception e)
                            {
                               
                                s1 = "Có lỗi, hệ thống đã lưu lỗi này và chờ xử lý";
                            }
                        }
                        else
                        {

                            s1 = "Vui lòng đăng nhập";
                        }

                    }
                    break;
                case "bindSelectArticleCategory":
                    {
                        if (isvalid)
                        {
                            try
                            {
                                var catId = context.Request["catId"];
                                
                                var model = categoryControl.GetArticleCateGory();
                                if (model != null && model.Count() > 0)
                                {
                                    s1 += @"<select id='scat' name='scat'>";
                                    foreach (var item in model)
                                    {
                                        s1 += @"<option value='" + item.Id + "'>" + item.Description + "</option>";
                                    }
                                    s1 += @"</select>";
                                }
                                else
                                {
                                    s1 += @"<select><option value=1>Chưa có chủ đề cons</option></select>";
                                }


                            }
                            catch (Exception e)
                            {
                                s1 = "-1";
                            }
                        }
                        else
                        {

                            s1 = "Vui lòng đăng nhập";
                        }

                    }
                    break;
                case "updateCat":
                    {
                        if (isvalid)
                        {
                            try
                            {
                                string id = context.Request["id"];
                                string desc = context.Request["desc"];                                
                                s1 = categoryControl.UpdateCategory(desc,Convert.ToInt32(id)).ToString();
                                if (s1.Equals("1"))
                                {
                                    s1 = "Cập nhật thành công";
                                }


                            }
                            catch (Exception e)
                            {
                                //tin không có
                                s1 = "-1";
                            }
                        }
                        else
                        {

                            s1 = "Vui lòng đăng nhập";
                        }

                    }
                    break;
                case "updateSubCat":
                    {
                        if (isvalid)
                        {
                            try
                            {
                                string id = context.Request["id"];
                                string desc = context.Request["desc"];
                                int i = categoryControl.UpdateCategory(desc,Convert.ToInt32(id));
                                s1 = "Đã cập nhật";

                            }
                            catch (Exception e)
                            {
                                //tin không có
                                s1 = "Có lỗi, hệ thống đã lưu lỗi này và chờ xử lý";
                            }
                        }
                        else
                        {

                            s1 = "Vui lòng đăng nhập";
                        }

                    }
                    break;
                case "delSubCat":
                    {
                        if (isvalid)
                        {
                            try
                            {
                                string id = context.Request["id"];                                
                                int i = categoryControl.DelCategory(Convert.ToInt32(id));
                                s1 = "Xóa thành công chủ đề con";
                            }
                            catch (Exception e)
                            {
                                //tin không có
                                s1 = "Chủ đề con có tin liên quan, không xóa được";
                            }
                        }
                        else
                        {
                            s1 = "Vui lòng đăng nhập";
                        }

                    }
                    break;

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