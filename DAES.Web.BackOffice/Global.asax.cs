using System.Linq;
using System.Security.Claims;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ExpressiveAnnotations.MvcUnobtrusive.Providers;

namespace DAES.Web.BackOffice
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ModelValidatorProviders.Providers.Remove(ModelValidatorProviders.Providers.FirstOrDefault(x => x is DataAnnotationsModelValidatorProvider));
            ModelValidatorProviders.Providers.Add(new ExpressiveAnnotationsModelValidatorProvider());

            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.Name;
            AntiForgeryConfig.SuppressIdentityHeuristicChecks = true;
        }
    }
}
