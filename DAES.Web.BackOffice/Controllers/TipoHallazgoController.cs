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
    public class TipoHallazgoController : Controller
    {
        private SistemaIntegradoContext db = new SistemaIntegradoContext();

        public ActionResult Index()
        {
            return View(db.TipoHallazgo.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoHallazgo TipoHallazgo = db.TipoHallazgo.Find(id);
            if (TipoHallazgo == null)
            {
                return HttpNotFound();
            }
            return View(TipoHallazgo);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TipoHallazgo TipoHallazgo)
        {
            if (ModelState.IsValid)
            {
                db.TipoHallazgo.Add(TipoHallazgo);
                db.SaveChanges();
                TempData["Message"] = Properties.Settings.Default.Success;
                return RedirectToAction("Index");
            }

            return View(TipoHallazgo);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoHallazgo TipoHallazgo = db.TipoHallazgo.Find(id);
            if (TipoHallazgo == null)
            {
                return HttpNotFound();
            }
            return View(TipoHallazgo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TipoHallazgo TipoHallazgo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(TipoHallazgo).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Message"] = Properties.Settings.Default.Success;
                return RedirectToAction("Index");
            }
            return View(TipoHallazgo);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoHallazgo TipoHallazgo = db.TipoHallazgo.Find(id);
            if (TipoHallazgo == null)
            {
                return HttpNotFound();
            }
            return View(TipoHallazgo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoHallazgo TipoHallazgo = db.TipoHallazgo.Find(id);
            db.TipoHallazgo.Remove(TipoHallazgo);
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