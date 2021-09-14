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
    public class CiudadController : Controller
    {
        private SistemaIntegradoContext db = new SistemaIntegradoContext();

        // GET: Ciudad
        public ActionResult Index()
        {
            var ciudad = db.Ciudad.Include(c => c.Region);
            return View(ciudad.ToList());
        }

        // GET: Ciudad/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ciudad ciudad = db.Ciudad.Find(id);
            if (ciudad == null)
            {
                return HttpNotFound();
            }
            return View(ciudad);
        }

        // GET: Ciudad/Create
        public ActionResult Create()
        {
            ViewBag.RegionId = new SelectList(db.Region.OrderBy(q => q.Nombre), "RegionId", "Nombre");
            return View();
        }

        // POST: Ciudad/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CiudadId,Nombre,RegionId")] Ciudad ciudad)
        {
            if (ModelState.IsValid)
            {
                db.Ciudad.Add(ciudad);
                db.SaveChanges();
                TempData["Message"] = Properties.Settings.Default.Success;
                return RedirectToAction("Index");
            }

            ViewBag.RegionId = new SelectList(db.Region, "RegionId", "Nombre", ciudad.RegionId);
            return View(ciudad);
        }

        // GET: Ciudad/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ciudad ciudad = db.Ciudad.Find(id);
            if (ciudad == null)
            {
                return HttpNotFound();
            }
            ViewBag.RegionId = new SelectList(db.Region.OrderBy(q => q.Nombre), "RegionId", "Nombre", ciudad.RegionId);
            return View(ciudad);
        }

        // POST: Ciudad/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CiudadId,Nombre,RegionId")] Ciudad ciudad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ciudad).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Message"] = Properties.Settings.Default.Success;
                return RedirectToAction("Index");
            }
            ViewBag.RegionId = new SelectList(db.Region.OrderBy(q => q.Nombre), "RegionId", "Nombre", ciudad.RegionId);
            return View(ciudad);
        }

        // GET: Ciudad/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ciudad ciudad = db.Ciudad.Find(id);
            if (ciudad == null)
            {
                return HttpNotFound();
            }
            return View(ciudad);
        }

        // POST: Ciudad/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ciudad ciudad = db.Ciudad.Find(id);
            db.Ciudad.Remove(ciudad);
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