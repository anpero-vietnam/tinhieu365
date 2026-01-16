using AModul.Dapper;
using Models;
using Models.Modul.Product;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Ultil;

namespace AModul.Product
{

    public class UpdateProductControl : ConnectionProxy<ProductItem>
    {
        public int UpdateProductUpdateTime(int prID)
        {

            try
            {
                Dictionary<string, object> paramlist = new Dictionary<string, object>();
                paramlist.Add("@prid", prID);

                return base.ExecuteProc("updatePRTime", paramlist);
            }
            catch (Exception ex)
            {
                Dal.MessengerControl ms = new Dal.MessengerControl();
                ms.SendMsgToAdmin(ex.Message);
                return 0;
            }

        }
        public int UpdateIsInstock(int prID, int status)
        {
            try
            {
                Dictionary<string, object> paramlist = new Dictionary<string, object>();
                paramlist.Add("@id", prID);
                paramlist.Add("@sst", status);
                return base.ExecuteProc("sp_updateIsInstock", paramlist);
            }
            catch (Exception ex)
            {
                Dal.MessengerControl ms = new Dal.MessengerControl();
                ms.SendMsgToAdmin("bug from UpdateIsInstock: " + ex.Message);
                return 0;
            }

        }

        public int DeleteProduct(string id)
        {
            try
            {
                Dictionary<string, object> paramlist = new Dictionary<string, object>();
                paramlist.Add("@Id", id);
                return base.ExecuteProc("DelPr", paramlist);
            }
            catch (Exception)
            {

                return 0;
            }

        }

        public SearchResultModel SearchProduct(SearchProductFilter searchModel)
        {
            int beginRow = (searchModel.Page - 1) * searchModel.ItemPerPage + 1;
            int endRow = (searchModel.Page - 1) * searchModel.ItemPerPage + 1 + searchModel.ItemPerPage;
            int countAll = 0;
            SearchResultModel result = new SearchResultModel();
            try
            {
                Dictionary<string, object> paramlist = new Dictionary<string, object>();
                paramlist.Add("@Id", searchModel.Id);
                paramlist.Add("@Name", searchModel.Title);
                paramlist.Add("@beginRow", beginRow);
                paramlist.Add("@endRow", endRow);
                result.ItemList = base.Select("sp_searchProduct", "@output", out countAll, paramlist);
                result.Count = countAll;
                return result;
            }
            catch (Exception)
            {
                return result;
            }

        }


        public int AddProduct(ProductItem model)
        {
            int rs = 0;
            if (string.IsNullOrEmpty(model.Images) && model.ImagesSlide != null && model.ImagesSlide.Count > 0)
            {
                model.Images = model.ImagesSlide[0].ImagesUrl;
            }

            var isNeedResizeImages = false;
            if (!string.IsNullOrEmpty(model.Images) && !model.Images.Contains("img.youtube.com"))
            {
                isNeedResizeImages = true;
            }
            if (model.Id > 0)
            {
                AModul.Product.ProductControl productControl = new AModul.Product.ProductControl();
                var modelProject = productControl.GetProductDetail(model.Id, model.CreateBy);
                if (model.Images == modelProject.Images)
                    isNeedResizeImages = false;
            }
            if (isNeedResizeImages)
            {
                string mediaEndPoint = AEnum.SiteConfig.MediaEndPointLink.TrimEnd('/') + "/UploadFileBase/GetImagesByLink";
                FileUpload fileUploadModel = new FileUpload();
                fileUploadModel.TokenKey = AEnum.SiteConfig.MediaAPITokenKey;
                fileUploadModel.AgencyID = model.CreateBy.ToString();
                fileUploadModel.ExternalImagesLinkToUpload = model.Images;
                fileUploadModel.Size = "600x600";
                var updaloadResult = Ultil.HttpRequesHelper<UploadedResult>.Post(mediaEndPoint, fileUploadModel);
                try
                {
                    model.Images = updaloadResult.uploadedImages.FirstOrDefault()?.Url;
                }
                catch (Exception)
                {
                }
            }


            if (!string.IsNullOrEmpty(model.Detail) && model.Detail.Length > 100000)
            {
                model.Detail = StringHelper.SubString(100000, StringHelper.GetSafeHtml(model.Detail));
                model.Detail = model.Detail.Replace("\n", "<br>");
                model.ShortDesc = StringHelper.SubString(300, StringHelper.GetSafeHtml(Ultil.StringHelper.RemoveHtmlTangs(model.Detail)));
                model.ShortDesc = model.ShortDesc.Replace("\n", "<br>");
            }
            try
            {
                if (model.Longitude.HasValue)
                {
                    model.Longitude = Math.Round(model.Longitude.Value, 8);
                }
                if (model.Latitude.HasValue)
                {
                    model.Latitude = Math.Round(model.Latitude.Value, 8);
                }
                Dictionary<string, object> paramlist = new Dictionary<string, object>();
                paramlist.Add("@Title", StringHelper.GetSafeHtml(model.Title));
                paramlist.Add("@ProjectId", model.ProjectId);
                paramlist.Add("@CategoryId", model.CategoryId);
                paramlist.Add("@ParentCatId", model.ParentCatId);
                paramlist.Add("@price", model.Price);
                paramlist.Add("@Unit", StringHelper.GetSafeHtml(model.Unit));
                paramlist.Add("@Images", StringHelper.GetSafeHtml(model.Images));
                paramlist.Add("@Detail", StringHelper.GetSafeHtml(model.Detail));
                paramlist.Add("@Id", model.Id);
                paramlist.Add("@IsPublish", model.IsPublish);
                paramlist.Add("@Rank", model.Rank);
                paramlist.Add("@ShortDesc", StringHelper.GetSafeHtml(model.ShortDesc));
                paramlist.Add("@Script", StringHelper.GetSafeHtml(model.Script));
                paramlist.Add("@KeyWord", StringHelper.GetSafeHtml(model.KeyWord));
                paramlist.Add("@BeginDate", model.BeginDate);
                paramlist.Add("@ClosingDate", model.ClosingDate);
                paramlist.Add("@LaneWidth", model.LaneWidth);
                paramlist.Add("@FacadeLength", model.FacadeLength);
                paramlist.Add("@Orientation", model.Orientation);
                paramlist.Add("@BalconyOrientation", model.BalconyOrientation);
                paramlist.Add("@Floors", model.Floors);
                paramlist.Add("@BedRoom", model.BedRoom);
                paramlist.Add("@BathRoom", model.BathRoom);
                paramlist.Add("@Address", StringHelper.GetSafeHtml(model.Address));
                paramlist.Add("@Area", model.Area);
                paramlist.Add("@DistrictId", model.DistrictId);
                paramlist.Add("@ProvinceId", model.ProvinceId);
                paramlist.Add("@Latitude", model.Latitude);
                paramlist.Add("@Longitude", model.Longitude);
                paramlist.Add("@CreateBy", model.CreateBy);
                base.ExecuteProc("sp_AddProduct", "@output", out rs, paramlist);
                return rs;

            }
            catch (Exception)
            {
                return 0;
            }

        }
        public int UpdateProduct(ProductItem models)
        {
            try
            {


                if (models.Detail != null && models.Detail.Length > 150000)
                {
                    models.Detail = models.Detail.Substring(0, 150000);
                }
                Dictionary<string, object> paramlist = new Dictionary<string, object>();
                paramlist.Add("@PrName", StringHelper.RemoveHexadecimalSymbols(models.Title));
                paramlist.Add("@Images", models.Images ?? "");
                paramlist.Add("@Detail", StringHelper.RemoveHexadecimalSymbols(models.Detail, true));
                paramlist.Add("@price", models.Price);

                //paramlist.Add("@ISSALES", models.IsSales);
                paramlist.Add("@specifications", models.Specifications ?? "");

                paramlist.Add("@CAT", models.CategoryId);
                paramlist.Add("@staffid", models.CreateBy);
                paramlist.Add("@Id", models.Id);


                paramlist.Add("@script", StringHelper.RemoveHexadecimalSymbols(models.Script));
                paramlist.Add("@sortDesc", StringHelper.RemoveHexadecimalSymbols(models.ShortDesc));
                paramlist.Add("@keyword", StringHelper.RemoveHexadecimalSymbols(models.Keywords));
                paramlist.Add("@tag", StringHelper.RemoveHexadecimalSymbols(models.Tag));
                paramlist.Add("@tagLink", Ultil.StringHelper.toURLgachTag(models.Tag));
                return base.ExecuteProc("sp_UpdateProduct", paramlist);
            }
            catch (Exception ex)
            {
                var text = ex.Message;
                return 0;
            }

        }
        public ProductItem GetProductById(int id)
        {
            Dictionary<string, object> paramlist = new Dictionary<string, object>();
            paramlist.Add("@Id", id);

            return base.SelectSingle("sp_GET_PRODUCT_BYID", paramlist);
        }
        private class UploadedResult
        {
            public int code { get; set; }
            public string messege { get; set; }
            public List<UploadedImages> uploadedImages { get; set; }
            public UploadedResult()
            {
                uploadedImages = new List<UploadedImages>();
            }
        }
        private class UploadedImages
        {
            public string Name { get; set; }
            public string Url { get; set; }
        }
    }
}
