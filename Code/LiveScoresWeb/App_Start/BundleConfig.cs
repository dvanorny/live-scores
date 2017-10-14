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
			//bundles.Add(new ScriptBundle("~/Scripts/AllJsBundle").Include("~/Scripts/*.js"));
			//bundles.Add(new ScriptBundle("~/bundles/jqueryval")
			//	.Include("~/Scripts/jquery.validate.min.js")
			//	.Include("~/Scripts/jquery.validate.unobtrusive.min.js"));
			bundles.Add(new ScriptBundle("~/Scripts/DataTablesBundle")
				.Include("~/Scripts/DataTables/media/js/dataTables.bootstrap4.min.js")
				.Include("~/Scripts/DataTables/media/js/jquery.dataTables.min.js")
				.Include("~/Scripts/DataTables/extensions/Responsive/js/*.js")
				.Include("~/Scripts/DataTables/extensions/Buttons/js/*.js")
				.Include("~/Scripts/DataTables/extensions/Select/js/*.js"));
			bundles.Add(new ScriptBundle("~/Scripts/Bootstrap/BootstrapBundle").Include("~/Scripts/Bootstrap/bootstrap.min.js"));
			//bundles.Add(new ScriptBundle("~/Scripts/JQueryUI/JQueryBundle").Include("~/Scripts/JQueryUI/*.js"));
			//bundles.Add(new ScriptBundle("~/Scripts/MvcGrid/MvcGridBundle").Include("~/Scripts/MvcGrid/*.js"));
			//bundles.Add(new ScriptBundle("~/Scripts/Shared/SharedBundle").Include("~/Scripts/Shared/*.js"));
		}
		private static void RegisterStyles(BundleCollection bundles)
		{
			bundles.Add(new StyleBundle("~/Content/JQueryUI/Bundle").Include("~/Content/JQueryUI/*.css"));
			bundles.Add(new StyleBundle("~/Content/Bootstrap/Bundle").Include("~/Content/Bootstrap/*.css"));
			bundles.Add(new StyleBundle("~/Content/FontAwesome/Bundle").Include("~/Content/FontAwesome/*.css"));
			bundles.Add(new StyleBundle("~/Content/Shared/Bundle").Include("~/Content/Shared/*.css"));
			bundles.Add(new StyleBundle("~/Content/Datatables/Bundle").Include("~/Content/DataTables/media/css/*.css"));
		}
	}
}
