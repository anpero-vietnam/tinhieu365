using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Ultil
{
    public static class ImagesResize
    {
      
        public static System.IO.Stream ScaleImage(System.IO.Stream stream, int maxWidth, int maxHeight)
        {
            Image image = System.Drawing.Image.FromStream(stream);
            var width = image.Width;
            var height = image.Height;
            var newWidth = 0;
            var newHeight = 0;
            var divisor = 0;
            if (width > height)
            {
                if (width > maxWidth)
                {
                    divisor = (width * 1000) / maxWidth;
                    if (divisor == 0)
                    {
                        divisor = 1;
                    }
                    newHeight = Convert.ToInt32(((height * 100000) / divisor) / 100);
                    newWidth = Convert.ToInt32(((width * 100000) / divisor) / 100);
                }
                else
                {
                    newHeight = height;
                    newWidth = width;
                }
            }
            else
            {
                if (height > maxHeight)
                {
                    divisor = (height * 1000) / maxHeight;
                    if (divisor == 0)
                    {
                        divisor = 1;
                    }
                    newWidth = Convert.ToInt32(((width * 100000) / divisor) / 100);
                    newHeight = Convert.ToInt32(((height * 100000) / divisor) / 100);
                }
                else
                {
                    newHeight = height;
                    newWidth = width;
                }
            }
            // not yet tested
            var newImage = new Bitmap(newWidth, newHeight, image.PixelFormat);
            using (Graphics g = Graphics.FromImage(newImage))
            {
                g.Clear(Color.Black);
                g.SmoothingMode = SmoothingMode.AntiAlias;                
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;                
                g.DrawImage(image, 0, 0, newWidth, newHeight);         
                
            }
            
            image.Dispose();
            return ToStream(newImage);

        }
        public static Stream ToStream(this System.Drawing.Image image)
        {
            var stream = new System.IO.MemoryStream();
            //stream.ReadTimeout = 100000;
            image.Save(stream, ImageFormat.Jpeg);
            stream.Position = 0;
            //stream.ReadTimeout = 100000;
            return stream;
        }

    }
    public class StreamBusiness
    {
        private static string[] whiteList = new string[] { ".jpg", ".jpeg", ".png", ".gif", ".ico", ".doc", ".docx", ".pdf", ".zip", ".rar", ".xls", ".xlsx" };
        public String UploadFullFiles(System.IO.Stream File, String ServerMappath, String userName, String fileName)
        {

            string extension = System.IO.Path.GetExtension(fileName);

            try
            {

                if (whiteList.Contains(extension.ToLower()))
                {
                    String path = GetNewFileName(fileName, ServerMappath, userName);
                    String serverPath = HttpContext.Current.Server.MapPath(path);
                    String DirectorysPath = HttpContext.Current.Server.MapPath(ServerMappath + userName + "/" + Ultil.Times.getSortStringByTimeNow());
                    if (!System.IO.Directory.Exists(DirectorysPath))
                    {
                        System.IO.Directory.CreateDirectory(DirectorysPath);
                    }
                    using (var fileStream = new FileStream(serverPath + extension, FileMode.Create, FileAccess.Write))
                    {
                        File.CopyTo(fileStream);
                    }
                    File.Dispose();
                    return path.Replace("~", String.Empty) + extension;


                }
                else
                {
                    return "/noimages.gif";
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        public string UploadFiles(System.IO.Stream File, string ServerMappath, string userName, string fileName, int maxWidthMaxHeight, int MaxLengthFile)
        {

            string extension = System.IO.Path.GetExtension(fileName);
            try
            {
                if (whiteList.Contains(extension.ToLower()))
                {

                    if (File.Length < MaxLengthFile && File.Length > 0)
                    {
                        System.Drawing.Image image;
                        System.IO.Stream streams = null;
                        if (maxWidthMaxHeight != 0)
                        {
                            streams = ImagesResize.ScaleImage(File, maxWidthMaxHeight, maxWidthMaxHeight);
                            image = System.Drawing.Image.FromStream(streams);

                        }
                        else
                        {
                            image = System.Drawing.Image.FromStream(File);
                        }
                        string path = GetNewFileName(fileName, ServerMappath, userName);

                        string serverPath = HttpContext.Current.Server.MapPath(path);
                        string DirectorysPath = HttpContext.Current.Server.MapPath(ServerMappath + userName + "/" + Ultil.Times.getSortStringByTimeNow());
                        if (!System.IO.Directory.Exists(DirectorysPath))
                        {
                            System.IO.Directory.CreateDirectory(DirectorysPath);

                        }
                        image.Save(serverPath + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                        File.Dispose();
                        image.Dispose();
                        if (streams != null)
                        {
                            streams.Dispose();
                        }
                        return path.Replace("~", String.Empty) + ".jpg";

                    }
                    else
                    {
                        return @"/noimages.gif";

                    }
                }
                else
                {
                    return "/noimages.gif";
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
   
        private string GetNewFileName(string fileName, string ServerMappath, string userName)
        {

            //lấy chuỗi theo milisicon+secon+munuites+hor+month+year

            string s = Ultil.Times.GetYYYYMMDDHHmmssmsNow();

            string fileWithNoExtenSion = Ultil.StringHelper.RemoveExtenSion(fileName);
            //tạo một tên file mới đã thêm một chuỗi ngẫu nhiên tránh trùng file
            String newFileName = Ultil.StringHelper.ToURLgach(fileWithNoExtenSion) + s;
            return ServerMappath + userName + "/" + Ultil.Times.getSortStringByTimeNow() + "/" + newFileName;

        }
        private System.IO.Stream ToStream(System.Drawing.Image image)
        {
            var stream = new System.IO.MemoryStream();
            //stream.ReadTimeout = 100000;
            image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            stream.Position = 0;
            //stream.ReadTimeout = 100000;
            return stream;
        }
        private System.Drawing.Image clonImages(System.Drawing.Image img)
        {

            Graphics graphicImage = Graphics.FromImage(img);

            graphicImage.SmoothingMode = SmoothingMode.AntiAlias;
            //Write your text.
            System.Drawing.Image imageFilea = System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath("~/images/logoClon.png"));
            graphicImage.DrawImage(imageFilea, new Point(img.Width - 5, img.Height - 5));
            return img;
        }
    }


}
