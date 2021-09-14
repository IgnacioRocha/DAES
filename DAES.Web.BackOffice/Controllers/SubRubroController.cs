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
    public class SubRubroController : Controller
    {
        private SistemaIntegradoContext db = new SistemaIntegradoContext();

        public ActionResult Index()
        {
            var subRubro = db.SubRubro.Include(s => s.Rubro);
            return View(subRubro.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubRubro subRubro = db.SubRubro.Find(id);
            if (subRubro == null)
            {
                return HttpNotFound();
            }
            return View(subRubro);
        }

        public ActionResult Create()
        {
            ViewBag.RubroId = new SelectList(db.Rubro.OrderBy(q => q.Nombre), "RubroId", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SubRubroId,Nombre,RubroId")] SubRubro subRubro)
        {
            if (ModelState.IsValid)
            {
                db.SubRubro.Add(subRubro);
                db.SaveChanges();
                TempData["Message"] = Properties.Settings.Default.Success;
                return RedirectToAction("Index");
            }

            ViewBag.RubroId = new SelectList(db.Rubro.OrderBy(q => q.Nombre), "RubroId", "Nombre", subRubro.RubroId);
            return View(subRubro);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubRubro subRubro = db.SubRubro.Find(id);
            if (subRubro == null)
            {
                return HttpNotFound();
            }
            ViewBag.RubroId = new SelectList(db.Rubro.OrderBy(q => q.Nombre), "RubroId", "Nombre", subRubro.RubroId);
            return View(subRubro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SubRubroId,Nombre,RubroId")] SubRubro subRubro)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subRubro).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Message"] = Properties.Settings.Default.Success;
                return RedirectToAction("Index");
            }
            ViewBag.RubroId = new SelectList(db.Rubro.OrderBy(q => q.Nombre), "RubroId", "Nombre", subRubro.RubroId);
            return View(subRubro);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubRubro subRubro = db.SubRubro.Find(id);
            if (subRubro == null)
            {
                return HttpNotFound();
            }
            return View(subRubro);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SubRubro subRubro = db.SubRubro.Find(id);
            db.SubRubro.Remove(subRubro);
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