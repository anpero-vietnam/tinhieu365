using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Web.Mvc.Controllers
{
    public class Bussiness
    {
    }
    public static class ImagesResize 
    {


        public static System.Drawing.Image ScaleImage(System.Drawing.Image image, int maxWidth, int maxHeight)
        {
            var width = image.Width;
            var height = image.Height;
            var newWidth = 0;
            var newHeight = 0;
            var divisor = 0;
            if (width > height)
            {
                newWidth = maxWidth;
                divisor = width / maxWidth;
                if (divisor == 0)
                {
                    divisor = 1;
                }
                newHeight = Convert.ToInt32(height / divisor);
            }
            else
            {
                newHeight = maxHeight;
                divisor = height / maxHeight;
                if (divisor == 0)
                {
                    divisor = 1;
                }
                newWidth = Convert.ToInt32(width / divisor);
            }
            var newImage = new Bitmap(newWidth, newHeight);
            Graphics.FromImage(newImage).DrawImage(image, 0, 0, newWidth, newHeight);
            return newImage;

        }
        public static System.Drawing.Image ScaleImage(System.IO.Stream stream, int maxWidth, int maxHeight)
        {
            Image image = System.Drawing.Image.FromStream(stream);
            var width = image.Width;
            var height = image.Height;

            var newWidth = 0;
            var newHeight = 0;
            var divisor = 0;
            if (width > height)
            {
                newWidth = maxWidth;
                divisor = width / maxWidth;
                if (divisor == 0)
                {
                    divisor = 1;
                }
                newHeight = Convert.ToInt32(height / divisor);
            }
            else
            {
                newHeight = maxHeight;
                divisor = height / maxHeight;
                if (divisor == 0)
                {
                    divisor = 1;
                }
                newWidth = Convert.ToInt32(width / divisor);
            }


            var newImage = new Bitmap(newWidth, newHeight);

            Graphics.FromImage(newImage).DrawImage(image, 0, 0, newWidth, newHeight);
            return newImage;
        }
        public static System.IO.Stream ScaleImageStream(System.IO.Stream stream, int maxWidth, int maxHeight)
        {
            Image image = System.Drawing.Image.FromStream(stream);
            var width = image.Width;
            var height = image.Height;

            var newWidth = 0;
            var newHeight = 0;
            var divisor = 0;
            if (width > height)
            {
                newWidth = maxWidth;
                divisor = width / maxWidth;
                if (divisor == 0)
                {
                    divisor = 1;
                }
                newHeight = Convert.ToInt32(height / divisor);
            }
            else
            {
                newHeight = maxHeight;
                divisor = height / maxHeight;
                if (divisor == 0)
                {
                    divisor = 1;
                }
                newWidth = Convert.ToInt32(width / divisor);
            }


            var newImage = new Bitmap(newWidth, newHeight);

            Graphics.FromImage(newImage).DrawImage(image, 0, 0, newWidth, newHeight);
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
    public static class ftpSend{
        
        public static void  sendFptNow(String fileName,Stream stream)
        {
            String fptCongif = ConfigurationManager.AppSettings.Get("fptconfig").ToString().ToLower();
            String[] allConfig = fptCongif.Split(';');
            String ftpfullpaths = null;
            Boolean KeepAlives = false;
            Boolean EnableSsls = false;
            Boolean UsePassives = false;
            Boolean UseBinarys = false;
            String Usename = "";
            String passw = "";
            String s = "";
            foreach (string word in allConfig)
            {

                if (word.Contains("usebinary"))
                {
                    s = word.Replace(@"usebinary=", String.Empty);
                    UseBinarys = Convert.ToBoolean(s);
                }
                if (word.Contains("pass"))
                {
                    s = word.Replace(@"pass=", String.Empty);
                    passw = s.Trim();
                }
                if (word.Contains("usename"))
                {
                    s = word.Replace(@"usename=", String.Empty);
                    Usename = s.Trim();
                }
                if (word.Contains("useuassives"))
                {
                    s = word.Replace(@"usepassive=", String.Empty);
                    UsePassives = Convert.ToBoolean(word);
                }
                if (word.Contains("enablessl"))
                {
                    s = word.Replace(@"enablessl=", String.Empty);
                    EnableSsls = Convert.ToBoolean(s);
                }
                if (word.Contains("keepalive"))
                {
                  s= word.Replace(@"keepalive=", String.Empty);
                   KeepAlives = Convert.ToBoolean(s.Trim());
                }
                
                if (word.Contains("ftpfullpath"))
                {
                    s = word.Replace(@"ftpfullpath=", String.Empty);
                    ftpfullpaths = s.Trim();
                }
            }
            try
            {
                System.Net.FtpWebRequest ftp = (System.Net.FtpWebRequest)System.Net.FtpWebRequest.Create(ftpfullpaths + fileName);
                ftp.Credentials = new System.Net.NetworkCredential(Usename.Trim(), passw.Trim());
                //ftp.ConnectionGroupName = "webmaster";
                ftp.KeepAlive = KeepAlives;
                ftp.UseBinary = UseBinarys;
                ftp.EnableSsl = EnableSsls;
                ftp.UsePassive = UsePassives;
                ftp.Proxy = null;
                ftp.Method = System.Net.WebRequestMethods.Ftp.UploadFile;
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);                
                System.IO.Stream requestStream = ftp.GetRequestStream();
                requestStream.Write(buffer, 0, buffer.Length);
                requestStream.Flush();
                requestStream.Close();
                //stream.Close();
                //FtpWebResponse response = (FtpWebResponse)ftp.GetResponse();
                //String s = response.StatusDescription;
                //return ftpfullpath.Replace("ftp", "http");

            }
            catch (Exception)
            {
                throw;
       //return "/images/noimages.jpg";
            }
      //string ftpfullpath = @"ftp://media.aztv.vn/avatar/" + fileName;                                                    
    }
    }
}