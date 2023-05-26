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
    public class FirmanteController : Controller
    {
        private SistemaIntegradoContext db = new SistemaIntegradoContext();

        public ActionResult Index()
        {
            return View(db.Firmante.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Firmante firmante = db.Firmante.FirstOrDefault(q => q.FirmanteId == id);
            if (firmante == null)
            {
                return HttpNotFound();
            }
            return View(firmante);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Firmante firmante)
        {
            if (ModelState.IsValid)
            {
                db.Firmante.Add(firmante);
                db.SaveChanges();
                TempData["Message"] = Properties.Settings.Default.Success;
                return RedirectToAction("Index");
            }

            return View(firmante);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Firmante firmante = db.Firmante.FirstOrDefault(q => q.FirmanteId == id);
            if (firmante == null)
            {
                return HttpNotFound();
            }
            return View(firmante);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Firmante firmante)
        {
            if (ModelState.IsValid)
            {
                db.Entry(firmante).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Message"] = Properties.Settings.Default.Success;
                return RedirectToAction("Index");
            }
            return View(firmante);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Firmante firmante = db.Firmante.FirstOrDefault(q => q.FirmanteId == id);
            if (firmante == null)
            {
                return HttpNotFound();
            }
            return View(firmante);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Firmante firmante = db.Firmante.Find(id);
            db.Firmante.Remove(firmante);
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