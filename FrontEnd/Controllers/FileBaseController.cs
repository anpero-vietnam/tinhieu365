using Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace FrontEnd.Controllers
{
    public class FileBaseController : BaseController
    {
        // GET: FileBase
        private static string[] whiteListFileExtenstion = new string[] { ".jpg", ".jpeg", ".png", ".gif", ".ico", ".doc", ".docx", ".pdf", ".zip", ".rar", ".xls", ".xlsx" };        
        [ValidateAntiForgeryToken]
        [IsAuthenlication]
        [ValidateInput(false)]
        public async Task<JsonResult> Index(FileUpload model, List<HttpPostedFileBase> mediaFile)
        {
            string rs = string.Empty;
            foreach (var item in mediaFile)
            {
                if (!whiteListFileExtenstion.Contains(System.IO.Path.GetExtension(item.FileName).ToLower()))
                {

                    return Json(new UploadedResult { code = 405, messege = "Có file ảnh không hợp lệ." });
                }
            }
            if (mediaFile.Count > 5)
            {
                return Json(new UploadedResult { code = 405, messege = "Tải tối đa 5 ảnh 1 lần" });
            }
            if (mediaFile.Count > 0 && mediaFile.Count < 10)
            {
                string mediaEndPoint = AEnum.SiteConfig.MediaEndPointLink.TrimEnd('/') + "/UploadFileBase";
                model.TokenKey = AEnum.SiteConfig.MediaAPITokenKey;
                model.AgencyID = AppSession.CurentProfile.UserId.ToString();
                rs = await Ultil.HttpRequesHelper<UploadedResult>.PostImage(mediaEndPoint, mediaFile, model);
            }
            return Json(JsonConvert.DeserializeObject<UploadedResult>(rs));
        }
        [IsAuthenlication]
        [ValidateAntiForgeryToken]
        public  JsonResult Delete(string filePath)
        {
            if (CheckAuthenLicationLink(filePath))
            {
                Dal.UserProfileControl userProfile = new Dal.UserProfileControl();
                var pathArray = filePath.Split('/');
                FileDelete model = new FileDelete();
                model.TokenKey = AEnum.SiteConfig.MediaAPITokenKey;
                model.AgencyID = AppSession.CurentProfile.UserId.ToString();
                model.UserToken= userProfile.UpdateUserToken(AppSession.CurentProfile.UserId);
                string mediaEndPoint = AEnum.SiteConfig.MediaEndPointLink.TrimEnd('/') + "/UploadFileBase/Delete";
                model.FilePath = "/" + pathArray[pathArray.Length - 2] + "/" + Path.GetFileName(filePath);
                Ultil.HttpRequesHelper<UploadedResult>.PostAndReturnJson(mediaEndPoint, model);
            }

            return null;
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
    public class UploadedResult
    {
        public int code { get; set; }
        public string messege { get; set; }
        public List<UploadedImages> uploadedImages { get; set; }
        public UploadedResult()
        {
            uploadedImages = new List<UploadedImages>();
        }
    }
    public class UploadedImages
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
}