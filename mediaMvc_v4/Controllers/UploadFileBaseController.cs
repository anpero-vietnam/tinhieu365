using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace mediaMvc_v4.Controllers
{
    public class UploadFileBaseController : Controller
    {
        // GET: UploadFileBase
        private static string[] whiteListFileExtenstion = new string[] { ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx", ".jpg", ".jpeg", ".png", ".gif", ".zip", ".pdf", ".svg", ".rar", ".txt", ".ico" };
        private static string[] documentExtenstion = new string[] { ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx", ".zip", ".pdf", ".rar", ".txt" };
        [HttpPost]
        public async Task<JsonResult> Index(List<HttpPostedFileBase> mediaFile, Models.FileUpload modal)
        {


            string savedPath = "/images/noimages.jpg";
            try
            {

                bool valid = true;
                if (string.IsNullOrEmpty(modal.TokenKey) || modal.TokenKey != AEnum.SiteConfig.MediaAPITokenKey)
                {
                    return Json(new { url = "", msg = "Lỗi API token", code = 403 });
                }
                if (valid)
                {

                    string domainNames = System.Configuration.ConfigurationManager.AppSettings.Get("DomainName").ToString();

                    UploadedResult result = new UploadedResult();
                    foreach (var item in mediaFile)
                    {
                        if (whiteListFileExtenstion.Contains(System.IO.Path.GetExtension(item.FileName).ToLower()))
                        {
                            if (documentExtenstion.Contains(System.IO.Path.GetExtension(item.FileName).ToLower()))
                            {
                                modal.Size = "full";
                            }

                            System.IO.Stream stream = item.InputStream;
                            decimal imgLeng = Convert.ToDecimal(stream.Length / 1024);
                            Ultil.StreamBusiness sb = new Ultil.StreamBusiness();
                            string fileName = item.FileName.ToLower();
                            if (GetSizeByText(modal.Size) != null)
                            {
                                savedPath = sb.UploadFiles(stream, "~/images/", modal.AgencyID, fileName, GetSizeByText(modal.Size).Value, 1024 * 1024 * 6);

                            }
                            else if (modal.Size.ToLower().Equals(@"full"))
                            {
                                savedPath = sb.UploadFullFiles(stream, "~/images/", modal.AgencyID, fileName);
                            }
                            UploadedImages uploadedImg = new UploadedImages();
                            stream.Dispose();
                            uploadedImg.Url = domainNames + savedPath;
                            uploadedImg.Name = fileName;
                            result.uploadedImages.Add(uploadedImg);
                            result.code = 200;
                            result.messege = "Tải file thành công";
                        }
                        else
                        {
                            result.code = 405;
                            result.messege = "File extenstion không hợp lệ";
                        }
                    }
                    return Json(result);
                }
                else
                {
                    return Json(new { uploadedImages = "", msg = "Lỗi API token", code = 403 });
                }
            }
            catch (Exception ex)
            {
                Dal.MessengerControl ms = new Dal.MessengerControl();
                await  ms.SendMessagesToRoleAsync("addmin", false, "Lỗi từ trang media fileHandler ", ex.Message, "0");
                return Json(new { uploadedImages = "", msg = ex.Message, code = 403 });
            }
        }
        [HttpPost]
        public JsonResult GetImagesByLink(Models.FileUpload modal)
        {
            
            //Dal.UserProfileControl profileControl = new Dal.UserProfileControl();
            string savedPath = "/images/noimages.jpg";

            Boolean valid = true;
            if (String.IsNullOrEmpty(modal.TokenKey))
            {
                valid = false;
            }

            if (modal.TokenKey != AEnum.SiteConfig.MediaAPITokenKey)
            {
                return Json(new { uploadedImages = "", messege = "Lỗi API token", code = 403 });
            }
            string extenstion = System.IO.Path.GetExtension(modal.ExternalImagesLinkToUpload).ToLower();
            if (!whiteListFileExtenstion.Contains(extenstion))
            {
                valid = false;
                return Json(new { code = 405, messege = "File extenstion không hợp lệ" });
            }
            UploadedResult result = new UploadedResult();
            if (valid && !string.IsNullOrEmpty(modal.ExternalImagesLinkToUpload) && !modal.ExternalImagesLinkToUpload.Equals("undefine"))
            {
                if (!string.IsNullOrEmpty(extenstion))
                {
                    using (System.Net.WebClient webClient = new System.Net.WebClient())
                    {
                        byte[] data = webClient.DownloadData(modal.ExternalImagesLinkToUpload);
                        using (System.IO.Stream stream = new MemoryStream(data))
                        {
                            Ultil.StreamBusiness sb = new Ultil.StreamBusiness();
                            savedPath = sb.UploadFiles(stream, "~/images/", modal.AgencyID, Guid.NewGuid() + extenstion, GetSizeByText(modal.Size).Value, 1024 * 1024 * 6);
                        }
                    }
                }
                string domainNames = System.Configuration.ConfigurationManager.AppSettings.Get("DomainName").ToString();
                UploadedImages uploadedImg = new UploadedImages();
                uploadedImg.Url = domainNames + savedPath;
                result.uploadedImages.Add(uploadedImg);
            }
            result.code = 200;
            result.messege = "Tải file thành công";
            return Json(result);
        }
        [HttpPost]
        public JsonResult Delete(Models.FileDelete modal)
        {
            try
            {
                var valid = true;
                UploadedResult result = new UploadedResult();
                Dal.UserProfileControl userControl = new Dal.UserProfileControl();
                if (String.IsNullOrEmpty(modal.TokenKey) || modal.TokenKey != AEnum.SiteConfig.MediaAPITokenKey || !userControl.CheckTokenUser(Convert.ToInt32(modal.AgencyID), modal.UserToken))
                {
                    return Json(new { url = "", msg = "Lỗi API token", code = 403 });
                }
                if (valid) 
                {
                    var filePath = "/images/"+modal.AgencyID+"/"+ modal.FilePath;
                    var serverPath = Server.MapPath(filePath.Replace(@"//",@"/"));
                    if (System.IO.File.Exists(serverPath))
                    {
                        System.IO.File.Delete(serverPath);
                    }
                    result.code = 200;
                    result.messege = "Tải file thành công";
                    return Json(result);
                }
                else
                {
                    return Json(new { uploadedImages = "", msg = "Lỗi API token", code = 403 });
                }
            }
            catch (Exception ex)
            {
                Dal.MessengerControl ms = new Dal.MessengerControl();
               var x = ms.SendMessagesToRoleAsync("addmin", false, "Lỗi từ trang media fileHandler ", ex.Message, "0").Result;
                return Json(new { uploadedImages = "", msg = ex.Message, code = 403 });
            }
        }
        private int? GetSizeByText(string size)
        {


            switch (size)
            {
                case @"200x200":
                    return 200;
                case @"300x300":
                    return 300;
                case @"400x400":
                    return 400;
                case @"600x600":
                    return 600;
                case @"1024x1024":
                    return 1024;
                case @"2048x2048":
                    return 2048;
                default:
                    return null;
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