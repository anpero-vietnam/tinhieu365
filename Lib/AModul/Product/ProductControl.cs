
using AModul.ProductProperties;
using Models.Modul.Product;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AModul.Product
{
    public class ProductControl : AModul.Dapper.ConnectionProxy<ProductItem>
    {
        public int DeleteProject(int id,int? userid=null)
        {
            Dictionary<string, object> paramlist = new Dictionary<string, object>();
            paramlist.Add("@id", id);
            paramlist.Add("@userId", userid);
            return base.ExecuteProc("sp_DeleteProductById", paramlist);
        }
        public List<ProductItem> GetProductByListId(List<int> productIdList, int agenId)
        {
            try
            {
                string listProduct = string.Join(",", productIdList.Select(x => x.ToString()).ToArray());
                Dictionary<string, object> paramlist = new Dictionary<string, object>();
                paramlist.Add("@listString", listProduct);
                paramlist.Add("@agenId", agenId);
                return base.Select("sp_GetProductByListId", paramlist);
            }
            catch (Exception ex)
            {
                Dal.MessengerControl ms = new Dal.MessengerControl();
                var rs = ms.SendMsgToAdmin(ex.Message).Result;
                return new List<ProductItem>();
            }

        }
        public  SearchResult SearchProject(SearchProductFilter searchFilter)
        {
            Dictionary<string, object> paramlist = new Dictionary<string, object>();
            paramlist.Add("@id", searchFilter.Id);
            paramlist.Add("@title", searchFilter.Title);
            paramlist.Add("@category", searchFilter.CategoryId);
            paramlist.Add("@Province", searchFilter.ProvinceId);
            paramlist.Add("@District", searchFilter.DistrictId);
            paramlist.Add("@Project", searchFilter.ProjectId);
            paramlist.Add("@Orientation", searchFilter.Orientation);
            paramlist.Add("@AreaFrom", searchFilter.AreaFrom);
            paramlist.Add("@AreaTo", searchFilter.AreaTo);
            paramlist.Add("@PriceFrom", searchFilter.PriceFrom);
            paramlist.Add("@PriceTo", searchFilter.PriceTo);
            paramlist.Add("@bathRoom", searchFilter.BathRoom);
            paramlist.Add("@bedRoom", searchFilter.BedRoom);
            paramlist.Add("@minRank", searchFilter.Rank);
            paramlist.Add("@Floors", searchFilter.Floors);
            paramlist.Add("@BalconyOrientation", searchFilter.BalconyOrientation);
            paramlist.Add("@FacadeLength", searchFilter.FacadeLength);
            paramlist.Add("@laneWidth", searchFilter.LaneWidth);
            paramlist.Add("@Advantage", searchFilter.Advantage);
            paramlist.Add("@IsPublisth", searchFilter.IsPublish);
            paramlist.Add("@CreateBy", searchFilter.CreateBy);
            paramlist.Add("@getClosedProject", searchFilter.GetClosedProject);
            paramlist.Add("@page", searchFilter.Page);
            paramlist.Add("@itemPerPage", searchFilter.ItemPerPage);
            
            SearchResult result = new SearchResult();
            result.CurrentPage = searchFilter.Page;
            //await Task.Run(()=> {
            //    int count = 0;
            //    var productList = base.Select("sp_searchProject", "@output", out count, paramlist);                
            //    result.Item = productList;
            //    result.ItemCount = count;
            //});
            //Task.WaitAll();
            int count = 0;
            var productList = base.Select("sp_searchProject", "@output", out count, paramlist);
            result.Item = productList;
            result.PageSize = searchFilter.ItemPerPage;
            result.ItemCount = count;
            return result;
        }
        public int UpdateProductRank(int productId,int rank,int? addDay)
        {
            Dictionary<string, object> paramlist = new Dictionary<string, object>();
            paramlist.Add("@rank", rank);
            paramlist.Add("@dayAdd", addDay);
            paramlist.Add("@productId", productId);
            return base.ExecuteProc("sp_UpdatePrRank", paramlist);
           

        }
        public ProductItem GetProductDetail(int id, int agenId=0)
        {
            Dictionary<string, object> paramlist = new Dictionary<string, object>();
            paramlist.Add("@id", id);
            paramlist.Add("@UserId", agenId);
            ProductItem productItem = base.SelectSingle("GET_PRODUCT_DETAIL", paramlist);
            if (productItem != null && productItem.Id > 0)
            {
                AModul.ProductProperties.IProperties property = new AModul.ProductProperties.PropertiesControl();
                productItem.Atribute = property.GetDataOfProductOnly(id);

                AModul.Product.Img img = new AModul.Product.Img();
                productItem.ImagesSlide = img.GetImgOfReferal("pr" + productItem.Id);
                return productItem;
            }
            else
            {
                return new ProductItem();
            }

        }

        public SearchResult SearchProductFrontEnd(string catId, string parentCatId, string originId, int priceFrom, int priceTo, int curentPage, int pageSite, string detail, string order, int agenID, int minPrioty, string properties, bool getFullData = false)
        {

            if (string.IsNullOrEmpty(catId))
            {
                catId = "%";
            }
            if (string.IsNullOrEmpty(parentCatId))
            {
                parentCatId = "%";
            }
            if (string.IsNullOrEmpty(originId))
            {
                originId = "%";
            }
            if (string.IsNullOrEmpty(detail))
            {
                detail = "%";
            }

            SearchResult result = new SearchResult();
            int beginRow = (curentPage - 1) * pageSite + 1;
            int endRow = (curentPage - 1) * pageSite + 1 + pageSite;
            List<ProductItem> ListProduct = new List<ProductItem>();
            int countAll = 0;
            try
            {
                SqlParameter[] param = new SqlParameter[12];
                param[0] = new SqlParameter("@cat", SqlDbType.VarChar, 300);
                param[0].Value = catId;
                param[1] = new SqlParameter("@parentCat", SqlDbType.VarChar, 300);
                param[1].Value = parentCatId.Trim();
                param[2] = new SqlParameter("@origin", SqlDbType.VarChar, 300);
                param[2].Value = originId;

                param[3] = new SqlParameter("@priceFrom", SqlDbType.Decimal);
                param[3].Value = priceFrom;
                param[4] = new SqlParameter("@priceTo", SqlDbType.Decimal);
                param[4].Value = priceTo;

                param[5] = new SqlParameter("@beginRow", SqlDbType.Int);
                param[5].Value = beginRow;
                param[6] = new SqlParameter("@endRow", SqlDbType.Int);
                param[6].Value = endRow;
                param[7] = new SqlParameter("@detail", SqlDbType.NVarChar, 100);
                param[7].Value = detail;
                param[8] = new SqlParameter("@order", SqlDbType.VarChar, 15);
                param[8].Value = order;
                param[9] = new SqlParameter("@st", SqlDbType.Int);
                param[9].Value = agenID;
                param[10] = new SqlParameter("@minPrioty", SqlDbType.Int);
                param[10].Value = minPrioty;
                param[11] = new SqlParameter("@properties", SqlDbType.NVarChar, 300);
                param[11].Value = string.IsNullOrEmpty(properties) ? string.Empty : properties.TrimEnd(',');

                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                DataTable prTable = ds.executeSelect("searchProduct_v2", param, out countAll);                
                result.Item = ListProduct;
                result.ItemCount = countAll;
                return result;
            }
            catch (Exception)
            {
                return null;
            }

        }
        //public SearchResult SearchProductByLocation(string catId, string parentCatId, Double priceFrom, Double priceTo, int curentPage, int pageSite, int Location, int city, string origin, int acreageFrom, int acreageTo, int agenId)
        //{

        //    if (string.IsNullOrEmpty(origin) || origin == "0")
        //    {
        //        origin = "%";
        //    }
        //    if (string.IsNullOrEmpty(catId) || catId == "0")
        //    {
        //        catId = "%";
        //    }
        //    if (string.IsNullOrEmpty(parentCatId) || parentCatId == "0")
        //    {
        //        parentCatId = "%";
        //    }
        //    SearchResult result = new SearchResult();
        //    int beginRow = (curentPage - 1) * pageSite + 1;
        //    int endRow = (curentPage - 1) * pageSite + 1 + pageSite;
        //    List<ProductItem> ListProduct = new List<ProductItem>();
        //    int countAll = 0;
        //    try
        //    {
        //        SqlParameter[] param = new SqlParameter[12];
        //        param[0] = new SqlParameter("@cat", SqlDbType.VarChar, 5);
        //        param[0].Value = catId;
        //        param[1] = new SqlParameter("@parentCat", SqlDbType.VarChar, 5);
        //        param[1].Value = parentCatId.Trim();

        //        param[2] = new SqlParameter("@st", SqlDbType.Int);
        //        param[2].Value = agenId;
        //        param[3] = new SqlParameter("@priceFrom", SqlDbType.Decimal);
        //        param[3].Value = priceFrom;
        //        param[4] = new SqlParameter("@priceTo", SqlDbType.Decimal);
        //        param[4].Value = priceTo;

        //        param[5] = new SqlParameter("@beginRow", SqlDbType.Int);
        //        param[5].Value = beginRow;
        //        param[6] = new SqlParameter("@endRow", SqlDbType.Int);
        //        param[6].Value = endRow;
        //        param[7] = new SqlParameter("@location", SqlDbType.Int);
        //        param[7].Value = Location;

        //        param[8] = new SqlParameter("@origin", SqlDbType.VarChar, 5);
        //        param[8].Value = origin;

        //        param[9] = new SqlParameter("@City", SqlDbType.Int);
        //        param[9].Value = city;

        //        param[10] = new SqlParameter("@acreageFrom", SqlDbType.Int);
        //        param[10].Value = acreageFrom;

        //        param[11] = new SqlParameter("@acreageTo", SqlDbType.Int);
        //        param[11].Value = acreageTo;

        //        Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
        //        DataTable prTable = ds.executeSelect("searchProduct_v3", param, out countAll);

        //        if (prTable != null && prTable.Rows.Count > 0)
        //        {
        //            foreach (DataRow item in prTable.Rows)
        //            {
        //                ListProduct.Add(GetProductItem(item, agenId));
        //            }
        //        }
        //        result.Item = ListProduct;
        //        result.ResultCount = countAll;
        //        return result;
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }

        //}
        //public ProductItem GetProductItem(DataRow row, int agenId, Boolean isGetAllData = false)
        //{
        //    ProductItem item = new ProductItem();
        //    item.ProjectId = Convert.ToInt32(row["ProjectId"]);

        //    item.CategoryId = Convert.ToInt32(row["CategoryId"]);
        //    item.Id = Convert.ToInt32(row["Id"]);
        //    item.Price = Convert.ToDecimal(row["Price"]);


        //    item.Images = row["images"].ToString();

        //    item.UpdateTime = row["UpdateTime"].ToString();
        //    item.ParentCatName = row["ParentName"].ToString();
        //    item.Detail = row["detail"].ToString();
        //    item.Script = row["Script"].ToString();

        //    item.ShortDesc = row["shortDesc"].ToString();
        //    item.KeyWord = row["keyWord"].ToString();

        //    try
        //    {
        //        item.Tag = row["tag"].ToString();
        //        item.TagLink = row["tagLink"].ToString();
        //        item.Specifications = row["Specifications"].ToString();
        //    }
        //    catch (Exception)
        //    {
        //    }


        //    if (isGetAllData)
        //    {
        //        AModul.Product.Img img = new AModul.Product.Img();
        //        var listAds = img.GetImgOfReferal("sp" + item.Id);

        //        if (listAds != null && listAds.Count > 0)
        //        {
        //            foreach (var items in listAds)
        //            {
        //                item.ImagesSlide.Add(items.ImagesUrl);
        //            }
        //        }
        //        var listAds2 = img.GetImgOfReferal("ps" + item.Id);

        //        if (listAds2 != null && listAds2.Count > 0)
        //        {
        //            item.ImagesSlide2 = listAds2;
        //        }
        //    }

        //    return item;
        //}
    }

}
