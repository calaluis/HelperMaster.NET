using System.Web.Optimization;

namespace EjemploMVC.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/bootstrap").Include("~/Content/bootstrap.min.css", new CssRewriteUrlTransform()));
            bundles.Add(new StyleBundle("~/Content/bootstraptheme").Include("~/Content/bootstrap-theme.css"));
        }
    }
}