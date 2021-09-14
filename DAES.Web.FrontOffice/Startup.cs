using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DAES.Web.FrontOffice.Startup))]
namespace DAES.Web.FrontOffice
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
