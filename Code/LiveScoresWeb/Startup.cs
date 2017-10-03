using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LiveScoresWeb.Startup))]
namespace LiveScoresWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
