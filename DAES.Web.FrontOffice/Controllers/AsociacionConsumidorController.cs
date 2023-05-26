
using DAES.Web.FrontOffice.Helper;
using System.Web.Mvc;

namespace DAES.Web.FrontOffice.Controllers
{

    [Audit]
    public class AsociacionConsumidorController : Controller
    {
   
        public ActionResult Index()
        {
            return View();
        }
    }
}