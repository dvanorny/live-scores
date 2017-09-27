using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LiveScores.Startup))]
namespace LiveScores
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
