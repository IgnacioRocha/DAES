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
    public class TipoFiscalizacionController : Controller
    {
        private SistemaIntegradoContext db = new SistemaIntegradoContext();

        public ActionResult Index()
        {
            return View(db.TipoFiscalizacion.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoFiscalizacion TipoFiscalizacion = db.TipoFiscalizacion.Find(id);
            if (TipoFiscalizacion == null)
            {
                return HttpNotFound();
            }
            return View(TipoFiscalizacion);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TipoFiscalizacion TipoFiscalizacion)
        {
            if (ModelState.IsValid)
            {
                db.TipoFiscalizacion.Add(TipoFiscalizacion);
                db.SaveChanges();
                TempData["Message"] = Properties.Settings.Default.Success;
                return RedirectToAction("Index");
            }

            return View(TipoFiscalizacion);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoFiscalizacion TipoFiscalizacion = db.TipoFiscalizacion.Find(id);
            if (TipoFiscalizacion == null)
            {
                return HttpNotFound();
            }
            return View(TipoFiscalizacion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TipoFiscalizacion TipoFiscalizacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(TipoFiscalizacion).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Message"] = Properties.Settings.Default.Success;
                return RedirectToAction("Index");
            }
            return View(TipoFiscalizacion);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoFiscalizacion TipoFiscalizacion = db.TipoFiscalizacion.Find(id);
            if (TipoFiscalizacion == null)
            {
                return HttpNotFound();
            }
            return View(TipoFiscalizacion);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoFiscalizacion TipoFiscalizacion = db.TipoFiscalizacion.Find(id);
            db.TipoFiscalizacion.Remove(TipoFiscalizacion);
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