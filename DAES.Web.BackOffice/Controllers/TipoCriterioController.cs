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
    public class TipoCriterioController : Controller
    {
        private SistemaIntegradoContext db = new SistemaIntegradoContext();

        public ActionResult Index()
        {
            return View(db.TipoCriterio.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoCriterio tipoCriterio = db.TipoCriterio.Find(id);
            if (tipoCriterio == null)
            {
                return HttpNotFound();
            }
            return View(tipoCriterio);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TipoCriterio tipoCriterio)
        {
            if (ModelState.IsValid)
            {
                db.TipoCriterio.Add(tipoCriterio);
                db.SaveChanges();
                TempData["Message"] = Properties.Settings.Default.Success;
                return RedirectToAction("Index");
            }

            return View(tipoCriterio);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoCriterio tipoCriterio = db.TipoCriterio.Find(id);
            if (tipoCriterio == null)
            {
                return HttpNotFound();
            }
            return View(tipoCriterio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TipoCriterio tipoCriterio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoCriterio).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Message"] = Properties.Settings.Default.Success;
                return RedirectToAction("Index");
            }
            return View(tipoCriterio);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoCriterio tipoCriterio = db.TipoCriterio.Find(id);
            if (tipoCriterio == null)
            {
                return HttpNotFound();
            }
            return View(tipoCriterio);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoCriterio tipoCriterio = db.TipoCriterio.Find(id);
            db.TipoCriterio.Remove(tipoCriterio);
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