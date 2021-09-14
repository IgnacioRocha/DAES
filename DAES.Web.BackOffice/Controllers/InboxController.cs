using DAES.Infrastructure.SistemaIntegrado;
using DAES.Web.BackOffice.Helper;
using System.Web.Mvc;

namespace DAES.Web.BackOffice.Controllers
{

    [Audit]
    [Authorize]
    public class InboxController : Controller
    {
        private SistemaIntegradoContext db = new SistemaIntegradoContext();

        public ActionResult Index()
        {
            return View();
        }
    }
}