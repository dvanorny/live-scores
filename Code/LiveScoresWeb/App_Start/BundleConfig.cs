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

		}

		private static void RegisterScripts(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/Scripts/AllJsBundle").Include("~/Scripts/*.js"));
			bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include("~/Scripts/jquery.validate*"));
			bundles.Add(new ScriptBundle("~/Scripts/DataTablesBundle")
				.Include("~/Scripts/DataTables/media/js/*.js")
				.Include("~/Scripts/DataTables/extensions/Responsive/js/*.js")
				.Include("~/Scripts/DataTables/extensions/Buttons/js/*.js")
				.Include("~/Scripts/DataTables/extensions/Select/js/*.js"));
			//bundles.Add(new ScriptBundle("~/Scripts/Bootstrap/BootstrapBundle").Include("~/Scripts/Bootstrap/*.js"));
			//bundles.Add(new ScriptBundle("~/Scripts/JQueryUI/JQueryBundle").Include("~/Scripts/JQueryUI/*.js"));
			//bundles.Add(new ScriptBundle("~/Scripts/MvcGrid/MvcGridBundle").Include("~/Scripts/MvcGrid/*.js"));
			//bundles.Add(new ScriptBundle("~/Scripts/Shared/SharedBundle").Include("~/Scripts/Shared/*.js"));
		}
		private static void RegisterStyles(BundleCollection bundles)
		{
			bundles.Add(new StyleBundle("~/Content/FontAwesomeBundle").Include("~/Content/FontAwesome/*.css"));
			bundles.Add(new StyleBundle("~/Content/AllCssBundle").Include("~/Content/*.css"));
			bundles.Add(new StyleBundle("~/Content/DataTablesBundle")
				.Include("~/Content/DataTables/media/css/*.css")
				.Include("~/Content/DataTables/extensions/Responsive/css/*.css")
				.Include("~/Content/DataTables/extensions/Buttons/css/*.css")
				.Include("~/Content/DataTables/extensions/Select/css/*.css"));
			//bundles.Add(new StyleBundle("~/Content/Bootstrap/BootstrapStyleBundle").Include("~/Content/Bootstrap/*.css"));
			//bundles.Add(new StyleBundle("~/Content/MvcGrid/MvcGridStyleBundle").Include("~/Content/MvcGrid/*.css"));
			//bundles.Add(new StyleBundle("~/Content/Shared/SharedStyleBundle").Include("~/Content/Shared/*.css"));
		}
	}
}
