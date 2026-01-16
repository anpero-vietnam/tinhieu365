using AModul.ProductProperties;
using Dal;
using Models;
using Models.Modul.Product;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static FrontEnd.Controllers.BaseController;

namespace FrontEnd.Controllers
{

    public class AjaxController : Controller
    {
        public PartialViewResult SearchProject(SearchProductFilter filter)
        {
            AModul.Product.ProductControl productControl = new AModul.Product.ProductControl();            
            filter.IsPublish = true;
            var model = productControl.SearchProject(filter);
            return PartialView(model);
        }
        [IsAuthenlication]
        public PartialViewResult SearchProjectAuth(bool IsPublish, bool closed=false,int Page=1)
        {
            AModul.Product.ProductControl productControl = new AModul.Product.ProductControl();
            SearchProductFilter filter = new SearchProductFilter();

            filter.IsPublish = IsPublish;            
            filter.CreateBy = AppSession.CurentProfile.UserId;
            filter.Page = Page;
            filter.GetClosedProject= closed;
            var model = productControl.SearchProject(filter);            
            return PartialView("SearchProjectAjax",model);
        }
   
        [IsAuthenlication(RoleName = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public int Deleteproject(int Id)
        {
            
            AModul.Product.ProductControl productControl = new AModul.Product.ProductControl();
            var productItem = productControl.GetProductDetail(Id);
            if (string.IsNullOrEmpty(productItem.Images))
            {
                AdminDeleteImages(productItem.Images, productItem.CreateBy);
            }
            if (productItem.ImagesSlide.Count > 0)
            {
                foreach (var item in productItem.ImagesSlide)
                {
                    AdminDeleteImages(item.ImagesUrl, productItem.CreateBy);
                }
            }
            return productControl.DeleteProject(Id);
        }
        private int AdminDeleteImages(string filePath, int userId)
        {
            try
            {
                if (CheckAuthenLicationLink(filePath))
                {
                    Dal.UserProfileControl userProfile = new Dal.UserProfileControl();
                    var pathArray = filePath.Split('/');
                    FileDelete model = new FileDelete();
                    model.TokenKey = AEnum.SiteConfig.MediaAPITokenKey;
                    model.AgencyID = AppSession.CurentProfile.UserId.ToString();
                    model.UserToken = userProfile.UpdateUserToken(userId);
                    string mediaEndPoint = AEnum.SiteConfig.MediaEndPointLink.TrimEnd('/') + "/UploadFileBase/Delete";
                    model.FilePath = "/" + pathArray[pathArray.Length - 2] + "/" + Path.GetFileName(filePath);
                    Ultil.HttpRequesHelper<UploadedResult>.PostAndReturnJson(mediaEndPoint, model);
                }
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        private bool CheckAuthenLicationLink(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                var pathArray = filePath.Split('/');
                string fileName = Path.GetFileName(filePath);
                string userUrl = AppSession.CurentProfile.UserId + "/" + pathArray[pathArray.Length - 2] + "/" + fileName;
                string queryUrl = pathArray[pathArray.Length - 3] + "/" + pathArray[pathArray.Length - 2] + "/" + pathArray[pathArray.Length - 1];
                if (userUrl == queryUrl && AppSession.CurentProfile.IsAuthenlication)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }
    }
}