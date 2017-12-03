using System.Web.Optimization;

namespace MvcPL
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery/jquery-2.2.3.min.js",
                "~/Scripts/jquery/jquery.unobtrusive-ajax.min.js",
                "~/Scripts/jquery/jquery.validate.min.js",
                "~/Scripts/jquery/jquery.validate.unobtrusive.min.js",
                "~/Scripts/jquery/jquery.Jcrop.min.js",
                "~/Scripts/jquery/jquery.form.min.js",
                "~/Scripts/modernizr-2.6.2.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap/bootstrap.min.js",
                "~/Scripts/respond/respond.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/blueimp").Include(
                "~/Content/blueimp-gallery2/js/blueimp-gallery.min.js",
                "~/Content/blueimp-gallery2/js/jquery.blueimp-gallery.min.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                "~/Scripts/LazyLoad.js"
            ));


            bundles.Add(new StyleBundle("~/bundles/css").Include(
                "~/Content/blueimp-gallery2/css/blueimp-gallery.min.css",
                "~/Content/jquery.Jcrop.min.css",
                "~/Content/Site.css"
                ));

            bundles.Add(new StyleBundle("~/bundles/bootstrap_css").Include(
                "~/Content/bootstrap/bootstrap-theme.min.css",
                "~/Content/bootstrap/bootstrap.min.css"));
        }
    }
}