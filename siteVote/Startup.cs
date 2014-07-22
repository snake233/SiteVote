using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(siteVote.Startup))]
namespace siteVote
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
