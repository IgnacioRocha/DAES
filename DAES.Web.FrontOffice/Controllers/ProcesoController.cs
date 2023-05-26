using DAES.Infrastructure.SistemaIntegrado;
using DAES.Web.FrontOffice.Helper;
using DAES.Web.FrontOffice.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace DAES.Web.FrontOffice.Controllers
{
    [Audit]
    public class ProcesoController : Controller
    {
        private SistemaIntegradoContext db = new SistemaIntegradoContext();

        public class DtoProceso
        {
            public DtoProceso()
            {
            }

            [Required(ErrorMessage = "Es necesario especificar este dato")]
            [Display(Name = "Id de trámite")]
            public int? id { get; set; }

            public DAES.Model.SistemaIntegrado.Proceso proceso { get; set; }
        }

        public ProcesoController()
        {
            ViewBag.User = Global.CurrentClaveUnica.User;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(int? id)
        {
            if (!id.HasValue)
            {
                ModelState.AddModelError(string.Empty, "Debe especificar el id del proceso");
            }

            if (id.HasValue && !db.Proceso.Any(q => q.ProcesoId == id.Value))
            {
                ModelState.AddModelError(string.Empty, "Proceso no encontrado");
            }

            if (ModelState.IsValid)
            {
                return RedirectToAction("Details", new { id = id.Value });
            }

            return View();
        }

        public ActionResult Details(int id)
        {
            var model = db.Proceso.Find(id);
            if (model == null)
            {
                return View("_Error", new Exception("Proceso no encontrado."));
            }

            return View(model);
        }
    }
}