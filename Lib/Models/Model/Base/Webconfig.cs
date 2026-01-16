using Models.Modul.Common;
using Models.Modul.Product;
using System.Collections.Generic;

namespace Models
{
    public class Webconfig
    {
        public Webconfig()
        {
            MenuList = new List<Menu>();            
            
            FaceBookLink = "";
            PageScript = "";
            Skype = "";
            Favicon = "";
            HotLine = "";
            Footer = "";
            Logo = "";
            ShortDesc = "";
            Email = "";
            Address = "";
            AnperoPlugin = "";
            Title = "";
            Token = "";
        }
     
        public string Token { get; set; }
        public List<Menu> MenuList { get; set; }
        public List<Menu> FooterMenuList { get; set; }

        public string FaceBookLink { get; set; }

        public string Skype { get; set; }

        public string OtherPhone { get; set; }
        public string HotLine { get; set; }

        public string PageScript { get; set; }

        public string Footer { get; set; }
        public string Logo { get; set; }
        public string ShortDesc { get; set; }
        public List<ProductCategory> ProductCategoryList { get; set; }        
        public string Email { get; set; }
        public string Address { get; set; }
        public string AnperoPlugin { get; set; }
        public string Title { get; set; }        
        
        public string Favicon { get; set; }
    }
}
