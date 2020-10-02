using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Laiki.Startup))]
namespace Laiki
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
