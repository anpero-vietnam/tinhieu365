using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Mvc.Controllers;

namespace admin.Controllers
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
            //model.Size = full,"1024x1024","600x600","400x400",200x200,300x300,20x20
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