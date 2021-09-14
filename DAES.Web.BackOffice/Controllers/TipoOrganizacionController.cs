using DAES.Infrastructure.SistemaIntegrado;
using DAES.Model.SistemaIntegrado;
using DAES.Web.BackOffice.Helper;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace DAES.Web.BackOffice.Controllers
{

    [Audit]
    [Authorize]
    public class TipoOrganizacionController : Controller
    {
        private SistemaIntegradoContext db = new SistemaIntegradoContext();

        public ActionResult Index()
        {
            return View(db.TipoOrganizacion.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoOrganizacion tipoOrganizacion = db.TipoOrganizacion.Find(id);
            if (tipoOrganizacion == null)
            {
                return HttpNotFound();
            }
            return View(tipoOrganizacion);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TipoOrganizacionId,Nombre")] TipoOrganizacion tipoOrganizacion)
        {
            if (ModelState.IsValid)
            {
                db.TipoOrganizacion.Add(tipoOrganizacion);
                db.SaveChanges();
                TempData["Message"] = Properties.Settings.Default.Success;
                return RedirectToAction("Index");
            }

            return View(tipoOrganizacion);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoOrganizacion tipoOrganizacion = db.TipoOrganizacion.Find(id);
            if (tipoOrganizacion == null)
            {
                return HttpNotFound();
            }
            return View(tipoOrganizacion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TipoOrganizacionId,Nombre")] TipoOrganizacion tipoOrganizacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoOrganizacion).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Message"] = Properties.Settings.Default.Success;
                return RedirectToAction("Index");
            }
            return View(tipoOrganizacion);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoOrganizacion tipoOrganizacion = db.TipoOrganizacion.Find(id);
            if (tipoOrganizacion == null)
            {
                return HttpNotFound();
            }
            return View(tipoOrganizacion);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoOrganizacion tipoOrganizacion = db.TipoOrganizacion.Find(id);
            db.TipoOrganizacion.Remove(tipoOrganizacion);
            db.SaveChanges();
            TempData["Message"] = Properties.Settings.Default.Success;
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