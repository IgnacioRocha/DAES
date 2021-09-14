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
    public class TipoOficioController : Controller
    {
        private SistemaIntegradoContext db = new SistemaIntegradoContext();

        public ActionResult Index()
        {
            return View(db.TipoOficio.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoOficio TipoOficio = db.TipoOficio.Find(id);
            if (TipoOficio == null)
            {
                return HttpNotFound();
            }
            return View(TipoOficio);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TipoOficio TipoOficio)
        {
            if (ModelState.IsValid)
            {
                db.TipoOficio.Add(TipoOficio);
                db.SaveChanges();
                TempData["Message"] = Properties.Settings.Default.Success;
                return RedirectToAction("Index");
            }

            return View(TipoOficio);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoOficio TipoOficio = db.TipoOficio.Find(id);
            if (TipoOficio == null)
            {
                return HttpNotFound();
            }
            return View(TipoOficio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TipoOficio TipoOficio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(TipoOficio).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Message"] = Properties.Settings.Default.Success;
                return RedirectToAction("Index");
            }
            return View(TipoOficio);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoOficio TipoOficio = db.TipoOficio.Find(id);
            if (TipoOficio == null)
            {
                return HttpNotFound();
            }
            return View(TipoOficio);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoOficio TipoOficio = db.TipoOficio.Find(id);
            db.TipoOficio.Remove(TipoOficio);
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