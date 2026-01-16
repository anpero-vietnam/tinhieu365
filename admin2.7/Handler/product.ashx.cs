using System;
using System.Web;
using System.Web.SessionState;
using Ultil;
using AModul.Product;
using Dal;

namespace Web.Mvc.Controllers
{
    public class ProductHandler : IHttpHandler, IRequiresSessionState
    {
        [System.Web.Mvc.ValidateInput(false)]
        public void ProcessRequest(HttpContext context)
        {
            var keyRequest = context.Request["op"];

            string captcha = context.Request["captcha"];            
            int UId =  AppSession.CurentProfile.UserId;
            
            String s1 = "";
            if (UserProfileControl.IsUserInRole(UId, AEnum.UserRole.CanSale))
            {
                switch (keyRequest)
                {
                    case "IsInStock":
                        {
                            int IsInStock = Convert.ToInt32(context.Request["IsInStock"]);
                            int id = Convert.ToInt32(context.Request["id"]);
                            UpdateProductControl pr = new UpdateProductControl();
                            s1 =pr.UpdateIsInstock(id, IsInStock).ToString();
                            break;
                        }
                    case "bindPrCat":
                        {
                            Category prc = new Category();
                            var PrcTable = prc.GetAllCategory(0);
                            s1 += "<option value='0'> -- Chọn một mục</option>";
                            if (PrcTable != null)
                            {
                                foreach (var item in PrcTable)
                                {


                                    Category pr = new Category();
                                    UpdateProductControl x = new UpdateProductControl();
                                    var table = pr.GetCategory(item.Id);

                                    s1 += "<option value='" + item.Id.ToString() + "' class='parentOption'>" + item.Name + @"</option>";
                                    if (table != null && table.Count > 0)
                                    {
                                        foreach (var subItem in table)
                                        {

                                            s1 += "<option value='" + subItem.Id.ToString() + "'  class='childOption'>&nbsp&nbsp&nbsp&nbsp-- " + subItem.Name + @"</option>";
                                        }
                                    }
                                }
                            }
                          
                            break;
                        }
                    case "bindPrCatLink":
                        {
                            Category prc = new Category();
                            var PrcTable = prc.GetAllCategory(0);
                            s1 += "<option value='0'> -- Chọn một mục</option>";
                            for (int i = 0; i < PrcTable.Count; i++)
                            {
                                //Dal.Product.Category pr = new Dal.Product.Category();
                                //Dal.Product.product x = new Dal.Product.product();
                                //var table = pr.GetCategory(Convert.ToInt32(PrcTable[i].Id), client);
                                

                                s1 += "<option value='" + UrlHelper.GetParentCategoryLink(PrcTable[i].Name,Convert.ToInt32(PrcTable[i].Id)) + "' class='parentOption'>" + PrcTable[i].Name + @"</option>";
                                foreach (var item in PrcTable[i].ChildCategory)
                                {
                                    s1 += "<option value='" + UrlHelper.GetParentCategoryLink(item.Name, item.Id) + "'  class='childOption'>&nbsp&nbsp&nbsp&nbsp-- " + item.Name + @"</option>";
                                }
                            }
                            break;
                        }
                    case "updatePrTime":
                        {
                            string id = context.Request["id"];                            
                            UpdateProductControl pr = new UpdateProductControl();
                            s1 = pr.UpdateProductUpdateTime(Convert.ToInt32(id)).ToString();
                            break;
                        }
                    case "bindCatTree":
                        {
                            try
                            {

                                if (UserProfileControl.IsUserInRole(UId, AEnum.UserRole.CanSale))
                                {
                                    Category cat = new Category();
                                    var table2 = cat.GetAllCategory(0);
                                    if (table2 != null)
                                    {
                                        s1 += "<ul  data-jstree=\"{'opened':true}\">";
                                        for (int m = 0; m < table2.Count; m++)
                                        {
                                            s1 += "<li data-jstree='{\"opened\":true}' id='cat-"+ table2[m].Id + "'> ";
                                             s1 += "<a class=\"parent-0\" href=\"#\" data-id=\""+ table2[m].Id + "\">" + table2[m].Name + "</a>";
                                            s1 += "<ul>";
                                            if (table2[m].ChildCategory.Count > 0)
                                            {
                                                foreach (var item in table2[m].ChildCategory)
                                                {
                                                    s1 += "<li id='cat-" + item.Id + "'><a href=\"javascript:void(0);\" data-id=\"" + item.Id + "\">" + item.Name + "</a></li>";
                                                }
                                            }
                                            s1 += "</ul>";
                                            s1 += "</li>";
                                        }
                                        s1 += "</ul>";
                                     
                                    }
                                }
                            }
                            catch (Exception)
                            {
                                s1 = "-1";
                            }
                            break;
                        }
                   
                    case "DelOrigin":
                        {
                            if (UserProfileControl.IsUserInRole(UId, AEnum.UserRole.CanUpdateProduct))
                            {
                                string id = context.Request["id"];
                                OriginGroup origin = new OriginGroup();
                                s1 = origin.DelOrigin(Convert.ToInt32(id)).ToString();
                            }
                            

                            break;
                        }                   
                    case "delOd":
                        {
                            if (UserProfileControl.IsUserInRole(UId,AEnum.UserRole.CanSale))
                            {


                                string id = context.Request["id"];
                                OrderControl od = new OrderControl();
                                int rs3 = od.UpdateOD(Convert.ToInt32(id), 0, 1, AppSession.CurentProfile.UserName + " đã xóa order");
                                if (rs3 > 0)
                                {
                                    s1 += "Xóa đơn hàng và " + rs3 + " bản ghi liên quan <br>";
                                }

                            }
                            else
                            {
                                s1 += "Bạn không có quền xóa đơn hàng này, Hãy yêu cầu quản trị cấp quền xóa đơn hàng";
                            }
                            break;
                        }
                    case "searchOrder":
                        {
                            string fromDate = context.Request["fromDate"];
                            string endDate = context.Request["endDate"];
                            string OID = context.Request["OID"].ToUpper().Replace("OD", string.Empty).Replace("DH", string.Empty);
                            string kh = context.Request["kh"].ToUpper().Replace("KH", string.Empty);
                            string sst = context.Request["sst"];
                            string seria = context.Request["seria"];
                            string page = context.Request["page"];
                            if (string.IsNullOrEmpty(page))
                            {
                                page = "1";
                            }

                            if (UserProfileControl.IsUserInRole(UId, AEnum.UserRole.CanSale))
                            {
                                OrderControl orderControl = new OrderControl();
                                int recod;
                                var table = orderControl.GetOderTable(OID,sst, fromDate, endDate, Convert.ToInt32(page), 30, kh, seria,  out recod);
                                 if (table != null && table.Count > 0)
                                {
                                    s1 += "<table class='table table-striped table-bordered table-hover'><tbody>";
                                    s1 += "<tr><th>Id</td><th>Ngày tạo</th><th>Trạng thái</th><th>Thanh toán</th><th>Giá trị</th><th>Ship</th><th title='Đã Thanh Toán'>Đã TT</th><th>Tác vụ</th></tr>";
                                    foreach (var item in table)
                                    {

                                        int statusInt =Convert.ToInt32(item.Status);                                        
                                        string status =Ultil.Static.Oderstatus.GetOrderStatusText(item.Status);
                                        string paid = "0";
                                        
                                        if (!DBNull.Value.Equals(item.Paided))
                                        {
                                            paid = Ultil.StringHelper.ConVertToMoneyFormat(item.Paided.ToString());
                                        }                                     
                                        string tranferType = "";
                                        int tranferTypeInt = item.TranferType;
                                        tranferType = Ultil.Static.PayMenMethod.GetPaymentMethodText(tranferTypeInt);
                                        Boolean defaultStye = true;
                                        if (item.Status==-1 || item.Status >= 77)
                                        {
                                            s1 += "<tr class='gray'>";
                                            defaultStye = false;
                                        }
                                        if (item.Status==5)
                                        {
                                            s1 += "<tr class='red'>";
                                            defaultStye = false;
                                        }
                                        if (item.Status==0)
                                        {
                                            s1 += "<tr class='blue'>";
                                            defaultStye = false;
                                        }
                                        if (defaultStye == true) { s1 += "<tr>"; }
                                        s1 += "<td>#" + item.Id + "</td>";
                                        s1 += "<td>" + Ultil.Times.convertStringToTimeForMat(item.DateCreate.ToString()) + "</td>";
                                        //s1 += "<td>" + u.UserName + "</td>";
                                        s1 += "<td>" + status + "</td>";
                                        s1 += "<td>" + tranferType + "</td>";
                                        s1 += "<td class='red'>" + Ultil.StringHelper.ConVertToMoneyFormatInt(item.TotalPrice.ToString()) + "</td>";
                                        s1 += "<td class='red'>" + Ultil.StringHelper.ConVertToMoneyFormatInt(item.ShippingFee.ToString()) + "</td>";
                                        s1 += "<td class='red'>" + Ultil.StringHelper.ConVertToMoneyFormatInt(item.Paided.ToString()) + "</td>";
                                        s1 += "<td><a href='javascript:delOd(\"" + item.Id.ToString() + "\");' class='btn red filter-cancel'><i class='fa fa-trash-o'></i></a><a href='/pr/order?id=" + item.Id.ToString() + "' title='bán hàng' target='_blank' class='btn btn-success btnAddProduct'><i class='fa fa-shopping-cart'></i></a></td>";
                                        s1 += "</tr>";
                                    }
                                    s1 += "</table>";
                                    
                                    s1 +=Ultil.StringHelper.SetupAjaxPage(Convert.ToInt32(page), 30, recod, 15, "searchOrder");
                                }
                            }
                            break;
                        }
                    case "DelPr":
                        {

                            if (UserProfileControl.IsUserInRole(AppSession.CurentProfile.UserId,AEnum.UserRole.CanUpdateProduct))
                            {
                                try
                                {
                                    String id = context.Request["id"];
                                    UpdateProductControl pr = new UpdateProductControl();
                                    s1 = pr.DeleteProduct(id).ToString();
                                    Dal.SysNotify sn = new Dal.SysNotify();
                                    sn.AddSysNotify("Thành viên " + AppSession.CurentProfile.SureName + " đã xóa sản phẩm có Id \" " + id + "\" vào lúc " + String.Format("{0:g}", DateTime.Now), "");
                                }
                                catch (Exception)
                                {
                                    s1 = "Lỗi";
                                }
                            }
                            else
                            {
                                s1 = "Bạn chưa có quyền xóa sản phẩm";
                            }

                            break;
                        }
                   
                 
                    case "getParentCatSelect":
                        {

                            Category pr = new Category();
                            var table = pr.GetAllCategory(0);
                            if (table != null)
                            {
                                s1 += "<option value='0'> -- Đặt làm danh mục chính</option>";
                                foreach (var item in table)
                                {

                                    s1 += "<option value='" + item.Id + "' data-link='/"+item.DefaultLink + "'>" + item.Name + "</option>";
                                }
                            }

                            break;
                        }
              
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