using System.Web;
using System.Web.Optimization;

namespace Web.UI
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/custom").Include(
                "~/Scripts/custom/positionNavDemo.js"));

            bundles.Add(new StyleBundle("~/fonts/css").Include(
                "~/fonts/font-awesome.min.css", 
                "~/fonts/fontawesome-all.min.css", 
                "~/fonts/fontawesome5-overrides.min.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                            "~/Content/site.css"));
        }
    }
}
