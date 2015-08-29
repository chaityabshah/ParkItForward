using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ParkItForward.Startup))]
namespace ParkItForward
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
