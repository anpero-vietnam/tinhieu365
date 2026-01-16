using System.Web;
using System.Web.Optimization;

namespace FrontEnd
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/script").Include(
                  "~/Assets/js/jquery-1.10.2.min.js",
                  "~/Assets/js/jquery-ui.js",
                  "~/Assets/js/popper.min.js",
                  "~/Assets/js/bootstrap.min.js",
                  "~/Assets/js/template.min.js",
                  "~/Assets/js/javascript.js",                  
                  "~/Assets/gritter/js/jquery.gritter.min.js",
                  "~/Assets/js/Modul/Common.js",
                  "~/Assets/js/Modul/main-search.js"                    
                ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js"));
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Assets/scss/vendor/css/bootstrap.min.css",
                      "~/Assets/scss/vendor/css/all.min.css",
                      "~/Assets/scss/vendor/css/light.css",                      
                      "~/Assets/gritter/css/jquery.gritter.css",
                      "~/Assets/scss/style.css"));
        }
    }
}

