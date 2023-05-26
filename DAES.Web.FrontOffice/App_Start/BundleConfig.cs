using System.Web.Optimization;

namespace DAES.Web.FrontOffice
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include("~/Scripts/jquery.validate*"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include("~/Scripts/jquery-ui-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryunob").Include("~/Scripts/jquery.unobtrusive*"));
            bundles.Add(new ScriptBundle("~/bundles/annotations").Include("~/Scripts/expressive.annotations.validate*"));
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js", "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            bundles.Add(new StyleBundle("~/Content/themes/jqueryui").Include("~/Content/themes/base/jquery-ui*"));
            bundles.Add(new StyleBundle("~/Content/gridmvc").Include(
                        "~/Content/gridmvc.css",
                        "~/Content/gridmvc.datepicker.css"));
            bundles.Add(new ScriptBundle("~/bundles/gridmvcjs").Include(
                      "~/Scripts/gridmvc.js",
                      "~/Scripts/gridmvc.lang.es.js",
                      "~/Scripts/bootstrap-datepicker.js"));
            bundles.Add(new StyleBundle("~/Content/DataTables").Include(
                      "~/Content/DataTables/css/dataTables.bootstrap.css"));
            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
                    "~/Scripts/DataTables/jquery.dataTables.js",
                    "~/Scripts/DataTables/dataTables.tableTools.js",
                    "~/Scripts/DataTables/dataTables.scroller.min.js",
                    "~/Scripts/DataTables/dataTables.bootstrap.js"));
        }
    }
}
