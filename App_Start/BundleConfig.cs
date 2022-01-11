using System.Web;
using System.Web.Optimization;

namespace JupiterEcoTech
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

            bundles.Add(new StyleBundle("~/content/maincss").Include(

                      "~/Content/plugins/font-awesome-4.7.0/css/font-awesome.min.css",
                      "~/Content/plugins/OwlCarousel2-2.2.1/owl.carousel.css",
                      "~/Content/plugins/OwlCarousel2-2.2.1/owl.theme.default.css",
                      "~/Content/plugins/OwlCarousel2-2.2.1/animate.css",
                      "~/Content/css/style.min.css",
                      "~/Content/css/Custom.min.css"));

            bundles.Add(new StyleBundle("~/content/vendorcss").Include(
                      "~/Content/vendor/bootstrap/css/bootstrap.min.css",
                      "~/Content/vendor/icofont/icofont.min.css",
                      "~/Content/vendor/boxicons/css/boxicons.min.css",
                      "~/Content/vendor/animate.css/animate.min.css",
                      "~/Content/vendor/venobox/venobox.css",
                      "~/Content/vendor/aos/aos.css",
                      "~/Content/vendor/remixicon/remixicon.css"));

            bundles.Add(new ScriptBundle("~/content/vendorjs").Include(
                        "~/Content/vendor/jquery/jquery.min.js",
                        "~/Content/vendor/bootstrap/js/bootstrap.min.js",
                        "~/Content/vendor/jquery.easing/jquery.easing.min.js",
                        "~/Content/vendor/jquery-sticky/jquery.sticky.js",
                        "~/Content/vendor/isotope-layout/isotope.pkgd.min.js",
                        "~/Content/vendor/venobox/venobox.min.js",
                        "~/Content/vendor/waypoints/jquery.waypoints.min.js",
                        "~/Content/vendor/aos/aos.js"));

            bundles.Add(new ScriptBundle("~/content/mainjs").Include(
                "~/Content/plugins/OwlCarousel2-2.2.1/owl.carousel.js",
                "~/Content/js/Custom.js",
                "~/Content/js/main.js"));

            bundles.IgnoreList.Clear();
        }
    }
}
