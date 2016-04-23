using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Skunkworks.Ics.Web.Startup))]
namespace Skunkworks.Ics.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
