using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ElBaraticoWeb.Startup))]
namespace ElBaraticoWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
