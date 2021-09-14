using DAES.Infrastructure.SistemaIntegrado;
using System;
using System.Web.Mvc;

namespace DAES.Web.FrontOffice.Controllers
{
    public class DocumentoController : Controller
    {
        private SistemaIntegradoContext db = new SistemaIntegradoContext();

        public ActionResult Emitir()
        {
            return RedirectToAction("Index", "Certificado");
        }
        public ActionResult Verificar()
        {
            return RedirectToAction("Index", "VerificarDocumento");
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