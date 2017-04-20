using System.Web;
using System.Web.Optimization;

namespace StudentRegistrationSystem
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"

                      
                      
                      ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                "~/Scripts/jquery-1.12.4.js",
                "~/Scripts/jquery-1.12.4.min.js",
          "~/Scripts/jquery-ui-1.12.1.js",
          "~/Scripts/jquery-ui-1.12.11.min.js"
          ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/jquery-ui.css"));

            //FOR POPUP
            bundles.Add(new StyleBundle("~/Content/POPUPCSS").Include(
                      "~/Content/RemodalPopup/dist/remodal.css",
                      "~/Content/RemodalPopup/dist/remodal-default-theme.css"));

            bundles.Add(new ScriptBundle("~/bundles/POPUPJS").Include(
          "~/Content/RemodalPopup/dist/remodal.js",
          "~/Content/RemodalPopup/dist/remodal.min.js"
          ));
            



        }
    }
}
