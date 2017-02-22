using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MvcWebApp.Startup))]
namespace MvcWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
