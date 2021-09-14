using DAES.Infrastructure.GestionDocumental;
using DAES.Infrastructure.SistemaIntegrado;
using DAES.Web.BackOffice.Helper;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace DAES.Web.BackOffice.Controllers
{
    [Audit]
    [Authorize]

    public class HomeController : Controller
    {
        private SistemaIntegradoContext db = new SistemaIntegradoContext();

        public class DTOResume
        {
            public DTOResume()
            {
            }
            public int WorkflowCount { get; set; }
            public int ProcesoCount { get; set; }
            public int PerfilId { get; set; }
        }
        public ActionResult Index()
        {
            var Hoy = DateTime.Now;

            if (!User.Identity.IsAuthenticated || Helper.Helper.CurrentUser == null)
                return RedirectToAction("LogOff", "Account");

            return View(new DTOResume()
            {
                WorkflowCount = db.Workflow.Count(q => !q.Terminada && q.UserId == Helper.Helper.CurrentUser.Id),
                ProcesoCount = db.Proceso.Count(q => !q.Terminada && Hoy > q.FechaVencimiento),
                PerfilId = Helper.Helper.CurrentUser.PerfilId
            });
        }
    }
}