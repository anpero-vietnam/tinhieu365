using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Ultil.Cache;
using Models.Modul.Product;
namespace AModul
{
    public class AdsControl
    {
        public List<Ads> GetSlide(string type)
        {
            AModul.Product.Img img = new AModul.Product.Img();
            return img.GetImgOfReferal(type);
        }
        public List<Ads> GetRandomSlide( string type, int numberOfAds)
        {
            AModul.Product.Img img = new AModul.Product.Img();
            return img.GetRandomImgOfReferal(type, numberOfAds);
        }
    }
}
