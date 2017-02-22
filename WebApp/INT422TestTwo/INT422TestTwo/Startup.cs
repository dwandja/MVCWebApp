using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(INT422TestTwo.Startup))]
namespace INT422TestTwo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
