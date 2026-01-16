using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
/// <summary>
/// Summary description for makeRandom
/// </summary>
namespace web.imagesResizw
{
    public class ImagesResize : Controller
    {
        public ImagesResize()
        {           
        }
       
        public System.Drawing.Image ScaleImage(System.Drawing.Image image, int maxWidth, int maxHeight)
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
        public System.Drawing.Image ScaleImage(System.IO.Stream stream, int maxWidth, int maxHeight)
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
        public System.IO.Stream ScaleImage(System.IO.Stream stream, int maxWidth, int maxHeight)
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
        public Stream ToStream(this System.Drawing.Image image)
        {
            var stream = new System.IO.MemoryStream();
            //stream.ReadTimeout = 100000;
            image.Save(stream,ImageFormat.Jpeg);            
            stream.Position = 0;
            //stream.ReadTimeout = 100000;
            return stream;
        }
    }
}

