using System.Web.Mvc;
using Models.Modul.Product;


namespace Web.Mvc.Controllers
{
    
    [IsAuthenlication(RoleName = "CanUpdateTheme")]
    public class ThemeController : BaseController
    {
        
        
        public ActionResult Ads()
        {
            SetUpAll();
            return View();
        }
        [ValidateAntiForgeryToken]        
        [ValidateInput(false)]
        public int AddSlide(Ads model)
        {
            int rs = -1;
            if (!string.IsNullOrEmpty(model.ReferenceId) && model.ImagesUrl != "" && model.ReferenceId != "undefined")
            {
               AModul.Product.Img Img = new AModul.Product.Img();
               rs = Img.AddNewsImg(model.ReferenceId, model.ImagesUrl, AppSession.CurentProfile.UserId.ToString(), model.ClickUrl, 0, model.Description);
            }

            return rs;
        }
        //
        // GET: /theme/
        
        public ActionResult Index()
        {
            SetUpAll();
            return View();
        }
      
      
    }
}
