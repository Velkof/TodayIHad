using System.Web.Optimization;

namespace TodayIHad.WebApp
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/framework").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/mustache.js",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/respond.js",
                        "~/Scripts/mustache.js",
                        "~/Scripts/moment-with-locales.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-ui").Include(
                        "~/Scripts/jquery-ui-1.12.0.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/chartist").Include(
                        "~/Scripts/chartist.js"));

            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                        "~/Scripts/Site/dashboard-controller.js",
                        "~/Scripts/Site/foods-controller.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/themes/base/jquery-ui.min.css",
                      "~/Content/chartist.css",
                      "~/Content/Site/Dashboard.css",
                      "~/Content/Site/Foods.css",
                      "~/Content/templatemo-style.css"
                      ));
        }
    }
}
