using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SocialNetwork.WebHost.Startup))]
namespace SocialNetwork.WebHost
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
