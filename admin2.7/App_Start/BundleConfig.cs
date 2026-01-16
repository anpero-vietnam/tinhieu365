using System.Web;
using System.Web.Optimization;

namespace adminv2._4
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            //BundleTable.EnableOptimizations =false;// false; // true;
            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));
         

            bundles.Add(new ScriptBundle("~/bundles/global.js").Include(
                   "~/assets/plugins/jquery.cookie.min.js",
                   "~/assets/plugins/js.cookie.min.js",                 
                    "~/assets/global/plugins/bootstrap/js/bootstrap.min.js",                  
                     //"~/assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js",                    
                    //"~/assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js",                    
                     "~/assets/global/scripts/app.min.js",
                    "~/assets/layouts/layout4/scripts/layout.min.js",                  
                    "~/assets/layouts/global/scripts/quick-sidebar.min.js",
                    "~/assets/layouts/global/scripts/quick-nav.min.js",
                    "~/assets/plugins/jquery-inputmask/jquery.inputmask.bundle.min.js",
                     "~/assets/plugins/jquery.pulsate.min.js",
                      "~/assets/plugins/gritter/js/jquery.gritter.min.js",
                      "~/Scripts/webScipt.js",
                      "~/Scripts/main.js",
                      "~/assets/plugins/bootstrap-modal/js/bootstrap-modalmanager.js",
                      "~/assets/plugins/bootstrap-modal/js/bootstrap-modal.js",
                      "~/assets/plugins/bootstrap-datepicker/js/bootstrap-datepicker.js",
                      "~/assets/plugins/bootstrap-datepicker/js/locales/bootstrap-datepicker.vi.js"
                      //"~/Scripts/AjaxHelper.js",
                      //"~/Scripts/Common.js"
                     ));

            bundles.Add(new StyleBundle("~/bundles/global.css").Include(
                       "~/assets/plugins/gritter/css/jquery.gritter.css",
                       "~/assets/global/plugins/bootstrap/css/bootstrap.min.css",
                       "~/assets/plugins/font-awesome/css/font-awesome.min.css",
                       //"~/assets/plugins/simple-line-icons/simple-line-icons.min.css",
                       //"~/assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css",
                        "~/assets/global/css/plugins-md.min.css",
                         "~/assets/global/css/components-md.min.css",                     
                       "~/assets/layouts/layout4/css/layout.min.css",
                       "~/assets/layouts/layout4/css/themes/default.min.css",
                       //"~/assets/plugins/bootstrap-modal/css/bootstrap-modal-bs3patch.css",                       
                         "~/assets/layouts/layout4/css/customs.css",
                         "~/assets/plugins/bootstrap-modal/css/bootstrap-modal.css",
                        "~/assets/plugins/bootstrap-datepicker/css/datepicker.css"                      

         ));
            
        }
    }
}