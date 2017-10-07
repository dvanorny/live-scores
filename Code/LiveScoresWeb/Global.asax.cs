using System;
using System.Data.SqlClient;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace LiveScoresWeb
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

	    protected void Application_Error(object sender, EventArgs e)
	    {
		    Exception ex = HttpContext.Current.Server.GetLastError();

		    var sql = "insert into ErrorLog (Details) values (@msg)";
		    string dbConn = WebConfigurationManager.ConnectionStrings["MainDb"].ConnectionString;
			using (var conn = new SqlConnection(dbConn))
			using (var cmd = new SqlCommand(sql, conn))
			{
				conn.Open();
				cmd.Parameters.AddWithValue("@msg", ex.ToString());
				cmd.ExecuteNonQuery();
			}
	}
	}
}
