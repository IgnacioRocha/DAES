using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DAES.Web.BackOffice.Startup))]
namespace DAES.Web.BackOffice
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
