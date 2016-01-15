using System.Web;
using System.Web.Optimization;

namespace HiQScoreboard
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
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/hiqscoreboard").Include(
                      "~/Scripts/HiQScoreboard/HiQScoreboard.js"));

            bundles.Add(new StyleBundle("~/bundles/push-api").Include(
                    "~/Scripts/Push-API/main.js",
                    "~/Scripts/Push-API/config.js",
                    "~/Scripts/Push-API/service-worker.js",
                    "~/Scripts/Push-API/demo.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/HiQScoreboard.css"));

        }
    }
}
