using DAES.Web.FrontOffice.Models;
using System.Web.Mvc;
using System.Web.Routing;

namespace DAES.Web.FrontOffice
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //The culture value determines the results of culture-dependent functions, such as the date, number, and currency (NIS symbol)
            System.Globalization.CultureInfo.DefaultThreadCurrentCulture = new System.Globalization.CultureInfo("es-CL");
            System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = new System.Globalization.CultureInfo("es-CL");
        }

        protected void Session_Start()
        {
            //inicializar variables de clave unica
            Global.CurrentClaveUnica = new ClaveUnica();
            Global.CurrentClaveUnica.ClaveUnicaRequestAutorization = new ClaveUnicaRequestAutorization();
        }

        //protected void Session_End()
        //{
        //    //inicializar variables de clave unica
        //    Global.CurrentClaveUnica = new ClaveUnica();
        //    Global.CurrentClaveUnica.ClaveUnicaRequestAutorization = new ClaveUnicaRequestAutorization();
        //}
    }
}
