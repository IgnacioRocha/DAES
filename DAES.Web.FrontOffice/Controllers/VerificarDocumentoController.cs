using DAES.Infrastructure.SistemaIntegrado;
using DAES.Web.FrontOffice.Helper;
using System;
using System.Linq;
using System.Web.Mvc;

namespace DAES.Web.FrontOffice.Controllers
{
    [Audit]
    public class VerificarDocumentoController : Controller
    {
        private SistemaIntegradoContext db = new SistemaIntegradoContext();
        private BLL.Custom _custom = new BLL.Custom();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search(int? id)
        {
            if (!id.HasValue)
            {
                ModelState.AddModelError(string.Empty, "Debe especificar el id del documento");
            }

            if (id.HasValue && !db.Documento.Any(q => q.DocumentoId == id.Value))
            {
                ModelState.AddModelError(string.Empty, "Documento no encontrado");
            }

            if (id.HasValue && db.Documento.Any(q => q.DocumentoId == id.Value && q.TipoPrivacidadId == (int)Infrastructure.Enum.TipoPrivacidad.Privado))
            {
                ModelState.AddModelError(string.Empty, "El documento no se puede verificar ya que es privado");
            }

            if (ModelState.IsValid)
            {
                return RedirectToAction("Finish", new { id = id.Value });
            }

            return View();
        }

        public ActionResult Finish(int id)
        {
            var model = db.Documento.FirstOrDefault(q => q.DocumentoId == id);
            if (model == null)
            {
                ModelState.AddModelError(string.Empty, "Documento no encontrado");
            }

            if (model != null && model.TipoPrivacidadId == (int)Infrastructure.Enum.TipoPrivacidad.Privado)
            {
                ModelState.AddModelError(string.Empty, "El documento no se puede verificar ya que es privado");
            }

            if (ModelState.IsValid)
            {
                return View(model);
            }

            return View();
        }

        public ActionResult Download(int id)
        {
            var model = db.Documento.Find(id);
            if (model == null)
            {
                return View("_Error", new Exception("Documento no encontrado."));
            }

            return File(model.Content, "application/pdf");
        }
    }
}