using System.Web;
using System.Web.Optimization;

namespace WebApplication22.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/javascript/jquery").Include(
                        "~/javascript/jquery-3.1.1.min.js"));

            bundles.Add(new ScriptBundle("~/javascript/JavaScript").Include(
                      "~/javascript/JavaScript.js",
                      "~/javascript/Chart.bundle.js"
                     ));

            bundles.Add(new StyleBundle("~/css/css").Include(
                 "~/css/normalize/css",
                      "~/css/style.css"
                      ));
        }
    }
}
