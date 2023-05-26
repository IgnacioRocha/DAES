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
    public class ComunaController : Controller
    {

        private SistemaIntegradoContext db = new SistemaIntegradoContext();

        public JsonResult GetComuna(int RegionId)
        {
            ViewBag.ComunaId = new SelectList(db.Comuna.Where(q => q.RegionId == RegionId).OrderBy(q => q.Nombre), "ComunaId", "Nombre");
            return Json(ViewBag.ComunaId, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCiudad(int RegionId)
        {
            ViewBag.CiudadId = new SelectList(db.Ciudad.Where(q => q.RegionId == RegionId).OrderBy(q => q.Nombre), "CiudadId", "Nombre");
            return Json(ViewBag.CiudadId, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            var comuna = db.Comuna.Include(c => c.Ciudad);
            return View(comuna.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comuna comuna = db.Comuna.Find(id);
            if (comuna == null)
            {
                return HttpNotFound();
            }
            return View(comuna);
        }

        public ActionResult Create()
        {
            ViewBag.RegionId = new SelectList(db.Region.OrderBy(q => q.Nombre), "RegionId", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ComunaId,Nombre,RegionId,Codigo")] Comuna comuna)
        {
            if (ModelState.IsValid)
            {
                db.Comuna.Add(comuna);
                db.SaveChanges();
                TempData["Message"] = Properties.Settings.Default.Success;
                return RedirectToAction("Index");
            }

            ViewBag.RegionId = new SelectList(db.Region.OrderBy(q => q.Nombre), "RegionId", "Nombre", comuna.RegionId);

            return View(comuna);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comuna comuna = db.Comuna.Find(id);
            if (comuna == null)
            {
                return HttpNotFound();
            }

            ViewBag.RegionId = new SelectList(db.Region.OrderBy(q => q.Nombre), "RegionId", "Nombre", comuna.RegionId);
            return View(comuna);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ComunaId,Nombre,RegionId,Codigo")] Comuna comuna)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comuna).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Message"] = Properties.Settings.Default.Success;
                return RedirectToAction("Index");
            }
            ViewBag.CiudadId = new SelectList(db.Ciudad.OrderBy(q => q.Nombre), "CiudadId", "Nombre", comuna.CiudadId);
            ViewBag.RegionId = new SelectList(db.Region.OrderBy(q => q.Nombre), "RegionId", "Nombre", comuna.RegionId);
            return View(comuna);
        }

        // GET: Comuna/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comuna comuna = db.Comuna.Find(id);
            if (comuna == null)
            {
                return HttpNotFound();
            }
            return View(comuna);
        }

        // POST: Comuna/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comuna comuna = db.Comuna.Find(id);
            db.Comuna.Remove(comuna);
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