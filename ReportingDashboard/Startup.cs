using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ReportingDashboard.Startup))]
namespace ReportingDashboard
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
