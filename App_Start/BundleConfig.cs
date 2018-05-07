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


            bundles.Add(new ScriptBundle("~/bundles/all").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/bootstrap.min.js",
                        "~/Scripts/jquery.validate.min.js",
                        "~/Scripts/jquery.validate.unobtrusive.js",
                        "~/Scripts/jquery.unobtrusive-ajax.min.js",
                        "~/admin-lte/js/adminlte.js",
                        "~/Scripts/BE/be-base.js",
                        "~/Scripts/AdminLTE/plugins/select2/select2.full.min.js",
                        "~/Scripts/AdminLTE/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/pace").Include(
                        "~/Scripts/AdminLTE/plugins/pace/pace.min.js"));


            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap.min.js"));


            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/smenu").Include(
                //"~/Scripts/ie10-viewport-bug-workaround.js"
                "~/Scripts/jquery.smenu.all.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/adminlte").Include(
                "~/admin-lte/js/adminlte.js",
                "~/Scripts/BE/be-base.js"));

            bundles.Add(new ScriptBundle("~/bundles/select2").Include(
                "~/Scripts/AdminLTE/plugins/select2/select2.full.min.js"));


            bundles.Add(new ScriptBundle("~/bundles/inputmask").Include(
                        //~/Scripts/Inputmask/dependencyLibs/inputmask.dependencyLib.js",  //if not using jquery
                        "~/Scripts/Inputmask/inputmask.js",
                        "~/Scripts/Inputmask/jquery.inputmask.js",
                        "~/Scripts/Inputmask/inputmask.extensions.js",
                        "~/Scripts/Inputmask/inputmask.date.extensions.js",
                        //and other extensions you want to include
                        "~/Scripts/Inputmask/inputmask.numeric.extensions.js"));

            //Styles

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                //"~/Content/bootstrap-theme.min.css"
                "~/Content/bootstrap.min.css",
                "~/Content/bootstrap-grid.min",
                "~/Content/pace/themes/white/pace-theme-minimal.css",
                "~/Scripts/AdminLTE/plugins/bootstrap-datepicker/css/bootstrap-datepicker.min.css"

                ));

            bundles.Add(new StyleBundle("~/Content/themes/smenu/css").Include(
                "~/Content/jquery.smenu.all.css"
                ));

            bundles.Add(new StyleBundle("~/Content/themes/adminlte/css").Include(
                //"~/Scripts/AdminLTE/plugins/morris/morris.css",
                "~/admin-lte/css/AdminLTE.css",
                "~/admin-lte/css/skins/_all-skins.min.css"
                ));

            bundles.Add(new StyleBundle("~/Content/themes/select2").Include(
                "~/Scripts/AdminLTE/plugins/select2/select2.min.css",
                "~/Content/Custom/site.css"
                ));

            bundles.Add(new StyleBundle("~/Content/themes/table-daily-upate-view").Include(
                "~/Content/Custom/table-daily-upate-view.css"
                ));
        }
    }
}