using System.Configuration;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Hangfire;
using Hangfire.Dashboard;
using LiveScores;
using Owin;

namespace LiveScores
{
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
		}
		
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
#if !DEBUG
				filters.Add(new RequireHttpsAttribute());
#endif
		}
	}
}
