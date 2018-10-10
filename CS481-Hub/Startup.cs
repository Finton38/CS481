using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CS481_Hub.Startup))]
namespace CS481_Hub
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
