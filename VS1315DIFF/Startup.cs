using Microsoft.Owin;
using Owin;
using VS1315DIFF;

[assembly: OwinStartup(typeof(Startup))]

namespace VS1315DIFF
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}