using System.Web;
using System.Web.Optimization;

namespace LiveScores
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            RegisterScripts(bundles);
            RegisterStyles(bundles);

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/site.css"));
        }

        private static void RegisterScripts(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/JQuery/Bundle").Include("~/Scripts/JQuery/*.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Bootstrap/Bundle").Include("~/Scripts/Bootstrap/*.js"));
            bundles.Add(new ScriptBundle("~/Scripts/JQueryUI/Bundle").Include("~/Scripts/JQueryUI/*.js"));
            bundles.Add(new ScriptBundle("~/Scripts/MvcGrid/Bundle").Include("~/Scripts/MvcGrid/*.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Shared/Bundle").Include("~/Scripts/Shared/*.js"));
            bundles.Add(new ScriptBundle("~/Scripts/DataTables/Bundle").Include("~/Scripts/DataTables/*.js"));
		}
        private static void RegisterStyles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/JQueryUI/Bundle").Include("~/Content/JQueryUI/*.css"));
            bundles.Add(new StyleBundle("~/Content/Bootstrap/Bundle").Include("~/Content/Bootstrap/*.css"));
            bundles.Add(new StyleBundle("~/Content/FontAwesome/Bundle").Include("~/Content/FontAwesome/*.css"));
            bundles.Add(new StyleBundle("~/Content/MvcGrid/Bundle").Include("~/Content/MvcGrid/*.css"));
            bundles.Add(new StyleBundle("~/Content/Shared/Bundle").Include("~/Content/Shared/*.css"));
            bundles.Add(new StyleBundle("~/Content/Datatables/Bundle").Include("~/Content/DataTables/css/*.css"));
		}
    }
}
