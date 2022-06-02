using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DistributorManagement.Startup))]
namespace DistributorManagement
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
