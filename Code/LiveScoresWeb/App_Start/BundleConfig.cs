using System.Web;
using System.Web.Optimization;

namespace LiveScoresWeb
{
	public class BundleConfig
	{
		// For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles(BundleCollection bundles)
		{
			RegisterScripts(bundles);
			RegisterStyles(bundles);

			bundles.Add(new StyleBundle("~/Content/SiteBundle").Include("~/Content/bootstrap.css", "~/Content/site.css"));
		}

		private static void RegisterScripts(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/Scripts/AllJsBundle").Include("~/Scripts/*.js"));
			//bundles.Add(new ScriptBundle("~/Scripts/Bootstrap/BootstrapBundle").Include("~/Scripts/Bootstrap/*.js"));
			//bundles.Add(new ScriptBundle("~/Scripts/JQueryUI/JQueryBundle").Include("~/Scripts/JQueryUI/*.js"));
			//bundles.Add(new ScriptBundle("~/Scripts/MvcGrid/MvcGridBundle").Include("~/Scripts/MvcGrid/*.js"));
			//bundles.Add(new ScriptBundle("~/Scripts/Shared/SharedBundle").Include("~/Scripts/Shared/*.js"));
			//bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include("~/Scripts/jquery.validate*"));
		}
		private static void RegisterStyles(BundleCollection bundles)
		{
			bundles.Add(new StyleBundle("~/Content/AllCssBundle").Include("~/Content/*.css"));
			//bundles.Add(new StyleBundle("~/Content/Bootstrap/BootstrapStyleBundle").Include("~/Content/Bootstrap/*.css"));
			bundles.Add(new StyleBundle("~/Content/FontAwesome/FontAwesomeBundle").Include("~/Content/FontAwesome/*.css"));
			//bundles.Add(new StyleBundle("~/Content/MvcGrid/MvcGridStyleBundle").Include("~/Content/MvcGrid/*.css"));
			//bundles.Add(new StyleBundle("~/Content/Shared/SharedStyleBundle").Include("~/Content/Shared/*.css"));
		}
	}
}
