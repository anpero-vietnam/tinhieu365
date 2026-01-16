using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FrontEnd
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
             name: "siteMapRouter",
             url: "sitemap.xml",
             defaults: new { controller = "Seo", action = "SitemapXml" }
         );
            routes.MapRoute(
                   name: "checkout",
                   url: "checkout",
                   defaults: new { controller = "product", action = "checkout" }
            );
            routes.MapRoute(
                name: "blogList3",
                url: "blog",
                defaults: new { controller = "Article", action = "Category", id = 0 }
            );
            routes.MapRoute(
                name: "blogList2",
                url: "blog/{title}-b{id}",
                defaults: new { controller = "Article", action = "Category", id = UrlParameter.Optional }
            );
            routes.MapRoute(
              name: "blogList",
              url: "{title}-b{id}",
              defaults: new { controller = "Article", action = "Category", id = UrlParameter.Optional },
              constraints: new { id = @"\d+", title = @"[^/]+" }
           );


            routes.MapRoute(
              name: "productDetail",
              url: "Symbols/{Ticker}",
              defaults: new { controller = "Symbols", action = "Index", Ticker = UrlParameter.Optional },
              constraints: new { Ticker = @"[^/]+" }
           );
            routes.MapRoute(
              name: "SymbolsRouters",
              url: "project/{title}-pr{id}",
              defaults: new { controller = "project", action = "detail", id = UrlParameter.Optional },
              constraints: new { id = @"\d+", title = @"[^/]+" }
           );
            routes.MapRoute(
              name: "search",
              url: "search",
              defaults: new { controller = "project", action = "index" }
           );
            routes.MapRoute(
             name: "districRouter",
             url: "{shortLink}-d{DistrictId}",
             defaults: new { controller = "project", action = "index", DistrictId = UrlParameter.Optional },
             constraints: new { DistrictId = @"\d+", shortLink = @"[^/]+" }
           );
            routes.MapRoute(
           name: "ProviderRouter",
           url: "{shortLink}-p{ProvinceId}",
           defaults: new { controller = "project", action = "index", ProvinceId = UrlParameter.Optional },
           constraints: new { ProvinceId = @"\d+", shortLink = @"[^/]+" }
         );
            routes.MapRoute(
             name: "category",
             url: "{shortLink}-c{CategoryId}",
             defaults: new { controller = "project", action = "index", CategoryId = UrlParameter.Optional },
             constraints: new { CategoryId = @"\d+", shortLink = @"[^/]+" }
           );

            routes.MapRoute(
              name: "Article",
              url: "{title}-a{id}",
              defaults: new { controller = "article", action = "index", id = UrlParameter.Optional },
              constraints: new { id = @"\d+", title = @"[^/]+" }
           );
            routes.MapRoute(
                 name: "PaymentInfo",
                 url: "PaymentInfo",
                 defaults: new { controller = "home", action = "policy", type = 6 }
            );
            routes.MapRoute(
                 name: "PrivacyPolicy",
                 url: "PrivacyPolicy",
                 defaults: new { controller = "home", action = "policy", type = 5 }
           );

            routes.MapRoute(
                 name: "ProductReturnPolicy",
                 url: "ProductReturnPolicy",
                 defaults: new { controller = "home", action = "policy", type = 4 }
               );
            routes.MapRoute(
              name: "WarrantyRule",
              url: "WarrantyPolicy",
              defaults: new { controller = "home", action = "policy", type = 3 }
            );
            routes.MapRoute(
              name: "aboutRule",
              url: "about",
              defaults: new { controller = "home", action = "policy", type = 1 }
            );
            routes.MapRoute(
                name: "ShipPolicyRule",
                url: "shippolicy",
                defaults: new { controller = "home", action = "policy", type = 2 }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
