using AModul.ProductProperties;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Web.Mvc.Controllers;
using Models.Modul.Product;
using AModul.Product;
using System.Threading.Tasks;

namespace adminv2._4.Controllers
{
    [IsAuthenlication(RoleName = "CanUpdateProduct")]
    public class PrController : BaseController
    {
        IPropertiesValue property = new PropertiesValueControl();
        UpdateProductControl productControl = new UpdateProductControl();
        [IsAuthenlication]
        public ActionResult priority(string id, int st)
        {
            SetUpAll();
            Category pr = new Category();
            var table = pr.GetAllCategory(0);
            if (table != null && table.Count > 0)
            {
                foreach (var item in table)
                {
                    if (item.Id.ToString() == id)
                    {
                        ViewBag.ParentName = item.Name;
                    }
                }
            }

            return View();

        }

        public string DelPrThumb(string imgId, string productId)
        {
            AModul.Product.Img img = new AModul.Product.Img();
            return img.DelPrThumb(productId, imgId, "%").ToString();
        }

        public string GetProductThumb(string Prod, string contanerElement = "")
        {

            string s1 = string.Empty;
            AModul.Product.Img img = new AModul.Product.Img();
            List<Ads> listAds = img.GetImgOfReferal(Prod);
            if (listAds != null && listAds.Count > 0)
            {
                foreach (var item in listAds)
                {
                    s1 += "<div class='form-body' style='background-color: #f2f6f9;padding: 29px;margin:20px;'>";
                    s1 += "<div class='form-group imgct'>";
                    s1 += "<img src='" + item.ImagesUrl + "' style='max-width:100%;' id='thumb-" + item.Id + "'/>";
                    s1 += "<div class='middle'><div class='text thumb-trigger' id='lbthumb-" + item.Id + "'>Đổi ảnh khác</div></div>";
                    s1 += "<input class='hidden' type='file' id='fileUpload-" + item.Id + "'>";
                    s1 += "</div>";
                    s1 += "<div class='form-group'>";
                    s1 += " <label for= 'form_control_1' > link khi click vào ảnh</label>";
                    s1 += "<input type = 'text' class='form-control' id='txtLinkClick" + item.Id + "' value='" + item.ClickUrl + "'> ";

                    s1 += "</div>";
                    s1 += "<div class='form-group'>";
                    s1 += " <label>Thứ tự hiển thị</label>";
                    s1 += "<input type = 'text' class='form-control' id='prioty_" + item.Id + "' value='" + item.Prioty + "'> ";

                    s1 += "</div>";
                    s1 += "<div class='form-group'>";
                    s1 += " <label>Nội dung </label>";
                    s1 += "<textarea class='form-control a-editor' id='txtDesc_" + item.Id + "' placeholder='Nhập text hoặc HTML' rows='5'>" + item.Description + "</textarea>";

                    s1 += " </div><div class='form-actions noborder'>";
                    s1 += "<a href='javascript:slideControl.delPrThumb(" + item.Id + ",\"" + Prod + "\");' class='btn red'>Xóa</a>";
                    s1 += "<a href='javascript:slideControl.UpdatePrThumb(" + item.Id + ",\"" + Prod + "\",\"" + contanerElement + "\");' class='btn green'>Cập nhật</a>";
                    s1 += " </div></div>";
                }

            }
            else
            {
                s1 = "";
            }
            return s1;
        }

        public string GetThumbByprid(string Prod, string contanerElement = "")
        {
            string s1 = string.Empty;
            AModul.Product.Img img = new AModul.Product.Img();
            List<Ads> adsList = img.GetImgOfReferal(Prod);
            if (adsList != null && adsList.Count > 0)
            {

                foreach (var item in adsList)
                {
                    s1 += "<div class='form-body' style='background-color: #f2f6f9;padding: 29px;margin:20px;'>";
                    s1 += "<div class='form-group imgct'>";
                    s1 += "<img src='" + item.ImagesUrl + "' style='max-width:100%;' id='thumb-" + item.Id + "'/>";
                    s1 += "<div class='middle'><div class='text thumb-trigger' id='lbthumb-" + item.Id + "'>Đổi ảnh khác</div></div>";
                    s1 += "<input class='hidden' type='file' id='fileUpload-" + item.Id + "'>";
                    s1 += "</div>";
                    s1 += "<div class='form-group'>";
                    s1 += " <label for= 'form_control_1' > link khi click vào ảnh</label>";
                    s1 += "<input type = 'text' class='form-control' id='txtLinkClick" + item.Id + "' value='" + item.ClickUrl + "'> ";

                    s1 += "</div>";
                    s1 += "<div class='form-group'>";
                    s1 += " <label>Thứ tự hiển thị</label>";
                    s1 += "<input type = 'text' class='form-control' id='prioty_" + item.Id + "' value='" + item.Prioty + "'> ";

                    s1 += "</div>";
                    s1 += "<div class='form-group'>";
                    s1 += " <label>Nội dung </label>";
                    s1 += "<textarea class='form-control a-editor' id='txtDesc_" + item.Id + "' placeholder='Nhập text hoặc HTML' rows='5'>" + item.Description + "</textarea>";

                    s1 += " </div><div class='form-actions noborder'>";
                    s1 += "<a href='javascript:slideControl.delPrThumb(" + item.Id + ");' class='btn red'>Xóa</a>";

                    s1 += "<a href='javascript:slideControl.UpdatePrThumb(" + item.Id + ",\"" + Prod + "\",\"" + contanerElement + "\");' class='btn green'>Cập nhật</a>";
                    s1 += " </div></div>";
                }

            }
            else
            {
                s1 = "";
            }

            return s1;
        }

        public int UpdateOrder(int statusId, string orderDetail, int id, string tranferType)
        {
            OrderControl orderControl = new OrderControl();
            return orderControl.UpdateOD(id, statusId, 0, orderDetail);
        }

        //[IsAuthenlication]
        //public ActionResult updateParentCat(string id, int st)
        //{
        //    ProductCategory category = new ProductCategory();
        //    if (Security.Store.IsInRole(AEnum.UserRole.CanUpdateProductCat))
        //    {
        //        SetUpAll();
        //        Dal.Product.Category pr = new Dal.Product.Category();
        //        Dal.Product.Category cat = new Dal.Product.Category();
        //        var table = pr.GetAllCategory(base.AnperoClient, 0);
        //        if (table != null && table.Count > 0)
        //        {
        //            foreach (var item in table)
        //            {
        //                if (item.Id.ToString() == id)
        //                {
        //                    category = item;
        //                }

        //            }

        //        }

        //    }
        //    return View(category);

        //}
        //[IsAuthenlication]
        //public ActionResult printWhTranfer(string id)
        //{
        //    Dal.Product.WareHowseHistory whh = new Dal.Product.WareHowseHistory();
        //    whh.GetWhHistoryById(id);
        //    ViewData["whh"] = whh;
        //    SetUpAll();
        //    return View();
        //}

        [OutputCache(Duration = 600, VaryByParam = "*")]
        public ActionResult UpdateCat()
        {
            if (Security.Store.IsInRole(AEnum.UserRole.CanSale))
            {
                SetUpAll();

            }
            return View();
        }



        public ActionResult AddPr()
        {
            SetUpAll();          
            return View();

        }
        public async Task<ActionResult> PrintOrder(String id)
        {
            if (Security.Store.IsInRole(AEnum.UserRole.CanSale))
            {
                SetUpAll();
                try
                {
                    OrderControl od = new OrderControl();
                    var model = od.GetOdById(Convert.ToInt32(id));
                    return View(model);
                }
                catch (Exception ex)
                {
                    Dal.MessengerControl ms = new Dal.MessengerControl();
                   await  ms.SendMessagesToRoleAsync("addmin", false, "Lỗi thu thập từ sp/update", ex.Message, "0");
                }
            }
            return View();
        }

        [OutputCache(Duration = 600, VaryByParam = "*")]
        public ActionResult orderList(String id, int st)
        {
            if (Security.Store.IsInRole(AEnum.UserRole.CanSale))
            {
                SetUpAll();
            }

            return View();
        }

        public ActionResult Order(string id)
        {
            if (Security.Store.IsInRole(AEnum.UserRole.CanSale))
            {
                SetUpAll();
                try
                {
                    OrderControl od = new OrderControl();
                    var model = od.GetOdById(Convert.ToInt32(id));
                    return View(model);
                }
                catch (Exception ex)
                {
                    Dal.MessengerControl ms = new Dal.MessengerControl();
                    var x = ms.SendMsgToAdmin("Lỗi thu thập từ order view: " + ex.Message);
                }
            }
            return View();
        }

        public async Task<ActionResult> Update(string sp)
        {   
                SetUpAll();
                try
                {
                    var model = productControl.GetProductById(Convert.ToInt32(sp.Replace("sp", string.Empty)));
                    return View(model);
                }
                catch (Exception ex)
                {
                    Dal.MessengerControl ms = new Dal.MessengerControl();
                  await  ms.SendMessagesToRoleAsync("addmin", false, "Lỗi thu thập từ sp/update", ex.Message, "0");
                }
            
            return View();
        }

        public ActionResult Index()
        {
            SetUpAll();
            return View();
        }

        [OutputCache(Duration = 600, VaryByParam = "*")]
        public ActionResult List()
        {
            SetUpAll();
            return View();
        }

        [OutputCache(Duration = 600, VaryByParam = "*")]
        public ActionResult analytic()
        {
            SetUpAll();
            return View();
        }
        //[IsAuthenlication]
        //public ActionResult tranferHistory()
        //{
        //    SetUpAll();
        //    return View();
        //}


        public ActionResult CategoryManager()
        {
            SetUpAll();
            return View();
        }

        public ActionResult GetCategoryTable(int ParentID)
        {

            Category pr = new Category();
            List<ProductCategory> parentCatTable = pr.GetAllCategory(ParentID);
            return PartialView("_partial_categoryTable", parentCatTable);
        }

    

        [HttpPost]
        [ValidateInput(false)]
        public int AjaxUpdateProduct(ProductItem model)
        {
            int rs = 0;
            try
            {
                AModul.Product.UpdateProductControl pr = new AModul.Product.UpdateProductControl();

                model.CreateBy = AppSession.CurentProfile.UserId;
                rs = pr.UpdateProduct(model);

                property.ResetProductPropertyValue(model.Id);
                if (model.Property.Count > 0)
                {
                    foreach (var item in model.Property)
                    {
                        property.AddProductPropertyValue(item, model.Id);
                    }
                }
                Dal.SysNotify sn = new Dal.SysNotify();
                sn.AddSysNotify("Thành viên " + AppSession.CurentProfile.SureName + " đã sửa sản phẩm \" <span class=\"lb-nf\">" + model.Title + "\"</span> vào lúc " + String.Format("{0:g}", DateTime.Now), "");
                return 1;
            }
            catch (Exception)
            {

                return 0;
            }


        }
        [HttpPost]

        public string SearchProduct(SearchProductFilter searchModel)
        {
            string s1 = "";
            UpdateProductControl pr = new UpdateProductControl();
            SearchResultModel searchResult = pr.SearchProduct(searchModel);
            s1 += "<table class=\"table table-striped table-bordered table-hover table-product\">";
            s1 += @"<thead><tr><th>Mã SP</th><th style='width:120px;'>Ảnh</th><th>Tên</th><th>Công khai</th><th class='c1'>Trạng thái</th><th class='c1'>Giá</th><th class='c1'>Giá KM</th><th class='c1' title='bảo hành'>Bảo hành</th><th class='c1'>Lượt xem</th><th>Tác vụ</th></tr></thead><tbody>";
            if (searchResult != null && searchResult.ItemList != null && searchResult.ItemList.Count > 0)
            {
                foreach (var item in searchResult.ItemList)
                {
                    String tipContent = "";

                    s1 += @"<tr><td>" + item.Id + "</td>";
                    s1 += "<td class='img-it'><img src='" + item.Images + "' /></td>";
                    s1 += "<td class='qtc-productname'>" + item.Title + "<div class='qtc-tooltip'><span class='col-md-12'>" + tipContent + "</span></div></td>";
                    s1 += @"<td>" + (item.IsPublish ? "Công khai" : "Ẩn") + "</td>";
                    

                    s1 += @"<td class='red c1'>" + Ultil.StringHelper.ConVertToMoneyFormat(item.Price.ToString()) + "</td>";
                    
                    
                    s1 += @"<td class='c1' title='bảo hành'>" + item.ViewTime + "</td>";
                    s1 += @"<td class='atc'>";

                    //s1 += @"<a href='javascript:sale(" + item.Id+ ");' title='bán hàng, thêm vào giỏ' class='btn btn-success btnAddProduct'><i class='fa fa-shopping-cart'></i></a> ";
                    s1 += @"<a href='/pr/update?sp=" + item.Id + "&st=" + "' title='sửa' target='_blank' class='btn red filter-cancel'><i class='fa fa-pencil'></i></a></td>";
                }
            }

            s1 += @"</tbody></table>";
            if (searchResult.Count > 0)
            {

                s1 += Ultil.StringHelper.SetupAjaxPage(searchModel.Page, 30, searchResult.Count, 10, "Product.seachProduct");
            }
            return s1;
        }
        public JsonResult SearchProductByName(SearchProductFilter searchModel)
        {   
            searchModel.ItemPerPage = 7;
            UpdateProductControl pr = new UpdateProductControl();
            SearchResultModel searchResult = pr.SearchProduct(searchModel);
            return Json(searchResult);
        }
    }
}
