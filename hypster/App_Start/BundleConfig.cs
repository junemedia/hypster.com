using System.Web;
using System.Web.Optimization;

namespace hypster
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {


            bundles.Add(new StyleBundle("~/bundles/css").Include(
               "~/css/Site.css",
               "~/css/SiteN.css"
               ));

            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
               "~/scripts/hypScript.js",
               "~/scripts/hypSlide.js"
               ));





            bundles.Add(new StyleBundle("~/bundlesMPL/css").Include(
               "~/css/Site.css",
               "~/css/SiteN.css",
               "~/css/Player.css",
               "~/css/PlayerN.css"
               ));

            bundles.Add(new ScriptBundle("~/bundlesMPL/scripts").Include(
               "~/scripts/hypScript.js",
               "~/scripts/hypMPLScript.js"
               ));






            bundles.Add(new StyleBundle("~/bundlesSENSES/css").Include(
                "~/Areas/senses/css/Player.css"
                ));


            bundles.Add(new ScriptBundle("~/bundlesSENSES/scripts").Include(
               "~/Areas/senses/scripts/hypScript.js",
               "~/Areas/senses/scripts/hypMPLScript.js",
               "~/Areas/senses/scripts/Silverlight.js"
               ));







            bundles.Add(new ScriptBundle("~/bundlesApps/scripts").Include(
               "~/areas/apps/scripts/appHypScript.js",
               "~/areas/apps/scripts/appHypSlide.js",
               "~/areas/apps/scripts/jquery.cookie.js"
               ));


            bundles.Add(new ScriptBundle("~/bundlesAppsMPL/scripts").Include(
               "~/areas/apps/scripts/appHypScript.js",
               "~/areas/apps/scripts/appHypMPLScript.js",
               "~/areas/apps/scripts/jquery.cookie.js"
               ));





            bundles.Add(new ScriptBundle("~/bundlesNewsMPL/scripts").Include(
               "~/areas/apps/scripts/appHypScript.js",
               "~/areas/apps/scripts/appHypNewsMPLScript.js",
               "~/areas/apps/scripts/jquery.cookie.js"
               ));


            bundles.Add(new ScriptBundle("~/bundlesWTHLZMPL/scripts").Include(
               "~/areas/apps/scripts/appHypScript.js",
               "~/areas/apps/scripts/appHypWTHLZMPLScript.js",
               "~/areas/apps/scripts/jquery.cookie.js"
               ));





            BundleTable.EnableOptimizations = true;

        }
    }
}