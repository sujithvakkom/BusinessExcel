using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace BusinessExcel
{
    public class BundleConfig
    {
        internal static void RegisterBundles(BundleCollection bundles)
        {
            //Scripts


            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/smenu").Include(
                //"~/Scripts/ie10-viewport-bug-workaround.js"
                "~/Scripts/jquery.smenu.all.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/adminlte").Include(
                        "~/Scripts/AdminLTE/app.min.js"));

            //Styles

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                "~/Content/bootstrap-theme.min.css",
                "~/Content/bootstrap.min.css"));

            bundles.Add(new StyleBundle("~/Content/themes/smenu/css").Include(
                "~/Content/jquery.smenu.all.css"
                ));

            bundles.Add(new StyleBundle("~/Content/themes/adminlte/css").Include(
                "~/Scripts/AdminLTE/plugins/morris/morris.css",
                "~/Scripts/AdminLTE/plugins/jvectormap/jquery-jvectormap-1.2.2.css",
                "~/Content/AdminLTE/AdminLTE.min.css",
                "~/Content/AdminLTE/skins/_all-skins.min.css"
                ));
        }
    }
}