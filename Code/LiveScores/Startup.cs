using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LiveScoresSite.Startup))]
namespace LiveScoresSite
{
	public partial class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			ConfigureAuth(app);

#if debug
			return;
#endif

			//GlobalConfiguration.Configuration.UseSqlServerStorage(ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString);

			/*
			app.UseHangfireServer();

			var filter = new BasicAuthAuthorizationFilter(
				new BasicAuthAuthorizationFilterOptions
				{
					// Require secure connection for dashboard
					RequireSsl = true,
					Users = new[]
					{
						new BasicAuthAuthorizationUser
						{
							Login = "admin",
							PasswordClear = "admin"
						}
					}
				});

			var options = new DashboardOptions
			{
				AuthorizationFilters = new[]
				{
					new AuthorizationFilter { Users = "dvanorny, imagetrend\\dvanorny" },
				}
			};

			app.UseHangfireDashboard("/hangfire", options);
			ConfigureHangfireJobs();
			*/
		}

		private static void ConfigureHangfireJobs()
		{
			//BackgroundJob.Enqueue(() => Hangfire.SyncOverseerIds());

			//RecurringJob.AddOrUpdate(() => Hangfire.SyncEliteSites(), Cron.Hourly);
			//RecurringJob.AddOrUpdate(() => Hangfire.SyncOverseerIds(), Cron.Hourly);
		}
	}
}