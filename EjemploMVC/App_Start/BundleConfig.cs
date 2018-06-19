using System.Web.Optimization;

namespace EjemploMVC.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js"));
            bundles.Add(new ScriptBundle("~/bundles/BlockUI").Include(
                "~/Scripts/jquery.blockUI.js"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrapsubmenu").Include(
                "~/Scripts/bootstrap-submenu.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/themes/base/all.css"));
            bundles.Add(new StyleBundle("~/Content/bootstrap").Include("~/Content/bootstrap.min.css", new CssRewriteUrlTransform()));
            bundles.Add(new StyleBundle("~/Content/bootstraptheme").Include("~/Content/bootstrap-theme.css"));
            bundles.Add(new StyleBundle("~/Content/bootstrapsubmenu").Include("~/Content/bootstrap-submenu.css"));
        }
    }
}