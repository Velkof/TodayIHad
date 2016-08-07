using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TodayIHad.WebApp.Startup))]
namespace TodayIHad.WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
