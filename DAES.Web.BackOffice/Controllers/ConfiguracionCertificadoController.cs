using DAES.Infrastructure.SistemaIntegrado;
using DAES.Model.SistemaIntegrado;
using DAES.Web.BackOffice.Helper;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace DAES.Web.BackOffice.Controllers
{

    [Audit]
    [Authorize]
    public class ConfiguracionCertificadoController : Controller
    {
        private SistemaIntegradoContext db = new SistemaIntegradoContext();

        public ActionResult Index()
        {
            var configuracionCertificado = db.ConfiguracionCertificado.Include(c => c.TipoDocumento).Include(c => c.TipoOrganizacion)
                .OrderBy(q => q.TipoOrganizacion.Nombre)
                .ThenBy(q => q.TipoDocumento.Nombre);
            return View(configuracionCertificado.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConfiguracionCertificado configuracionCertificado = db.ConfiguracionCertificado.Find(id);
            if (configuracionCertificado == null)
            {
                return HttpNotFound();
            }
            return View(configuracionCertificado);
        }

        public ActionResult Create()
        {
            ViewBag.TipoDocumentoId = new SelectList(db.TipoDocumento.OrderBy(q => q.Nombre), "TipoDocumentoId", "Nombre");
            ViewBag.TipoOrganizacionId = new SelectList(db.TipoOrganizacion.OrderBy(q => q.Nombre), "TipoOrganizacionId", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ConfiguracionCertificadoId,Nombre,TipoDocumentoId,TipoOrganizacionId,Parrafo1,Parrafo2,Parrafo3,Titulo,Ciudad,Departamento,XML,IsActivo,TieneDirectorio,TieneEstatuto")] ConfiguracionCertificado configuracionCertificado)
        {
            if (ModelState.IsValid)
            {
                db.ConfiguracionCertificado.Add(configuracionCertificado);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TipoDocumentoId = new SelectList(db.TipoDocumento.OrderBy(q => q.Nombre), "TipoDocumentoId", "Nombre", configuracionCertificado.TipoDocumentoId);
            ViewBag.TipoOrganizacionId = new SelectList(db.TipoOrganizacion.OrderBy(q => q.Nombre), "TipoOrganizacionId", "Nombre", configuracionCertificado.TipoOrganizacionId);
            return View(configuracionCertificado);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConfiguracionCertificado configuracionCertificado = db.ConfiguracionCertificado.Find(id);
            if (configuracionCertificado == null)
            {
                return HttpNotFound();
            }
            ViewBag.TipoDocumentoId = new SelectList(db.TipoDocumento.OrderBy(q => q.Nombre), "TipoDocumentoId", "Nombre", configuracionCertificado.TipoDocumentoId);
            ViewBag.TipoOrganizacionId = new SelectList(db.TipoOrganizacion.OrderBy(q => q.Nombre), "TipoOrganizacionId", "Nombre", configuracionCertificado.TipoOrganizacionId);
            return View(configuracionCertificado);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ConfiguracionCertificadoId,Nombre,TipoDocumentoId,TipoOrganizacionId,Parrafo1,Parrafo2,Parrafo3,Titulo,Ciudad,Departamento,XML,IsActivo,TieneDirectorio,TieneEstatuto")] ConfiguracionCertificado configuracionCertificado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(configuracionCertificado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TipoDocumentoId = new SelectList(db.TipoDocumento.OrderBy(q => q.Nombre), "TipoDocumentoId", "Nombre", configuracionCertificado.TipoDocumentoId);
            ViewBag.TipoOrganizacionId = new SelectList(db.TipoOrganizacion.OrderBy(q => q.Nombre), "TipoOrganizacionId", "Nombre", configuracionCertificado.TipoOrganizacionId);
            return View(configuracionCertificado);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConfiguracionCertificado configuracionCertificado = db.ConfiguracionCertificado.Find(id);
            if (configuracionCertificado == null)
            {
                return HttpNotFound();
            }
            return View(configuracionCertificado);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ConfiguracionCertificado configuracionCertificado = db.ConfiguracionCertificado.Find(id);
            db.ConfiguracionCertificado.Remove(configuracionCertificado);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}