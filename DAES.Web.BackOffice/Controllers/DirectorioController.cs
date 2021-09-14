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
    public class DirectorioController : Controller
    {

        private SistemaIntegradoContext db = new SistemaIntegradoContext();

        public ActionResult Index()
        {

            var directorio = db.Directorio.Include(d => d.Cargo).Include(d => d.Genero).Include(d => d.Organizacion);
            return View(directorio.ToList());
        }

        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var directorio = db.Directorio.Find(id);
            if (directorio == null)
            {
                return HttpNotFound();
            }

            return View(directorio);
        }

        public ActionResult Create(int OrganizacionId)
        {

            ViewBag.CargoId = new SelectList(db.Cargo.OrderBy(q => q.Nombre), "CargoId", "Nombre");
            ViewBag.GeneroId = new SelectList(db.Genero.OrderBy(q => q.Nombre), "GeneroId", "Nombre");
            ViewBag.OrganizacionId = OrganizacionId;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Directorio directorio)
        {

            if (ModelState.IsValid)
            {
                db.Directorio.Add(directorio);
                db.SaveChanges();
                TempData["Message"] = Properties.Settings.Default.Success;
                return RedirectToAction("Edit", "Organizacion", new { id = directorio.OrganizacionId });
            }

            ViewBag.CargoId = new SelectList(db.Cargo.OrderBy(q => q.Nombre), "CargoId", "Nombre", directorio.CargoId);
            ViewBag.GeneroId = new SelectList(db.Genero.OrderBy(q => q.Nombre), "GeneroId", "Nombre", directorio.GeneroId);
            ViewBag.OrganizacionId = directorio.OrganizacionId;
            return View(directorio);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var directorio = db.Directorio.Find(id);
            if (directorio == null)
            {
                return HttpNotFound();
            }
            ViewBag.CargoId = new SelectList(db.Cargo.OrderBy(q => q.Nombre), "CargoId", "Nombre", directorio.CargoId);
            ViewBag.GeneroId = new SelectList(db.Genero.OrderBy(q => q.Nombre), "GeneroId", "Nombre", directorio.GeneroId);
            ViewBag.OrganizacionId = directorio.OrganizacionId;

            return View(directorio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Directorio directorio)
        {

            if (ModelState.IsValid)
            {
                db.Entry(directorio).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Message"] = Properties.Settings.Default.Success;
                return RedirectToAction("Edit", "Organizacion", new { id = directorio.OrganizacionId });
            }

            ViewBag.CargoId = new SelectList(db.Cargo.OrderBy(q => q.Nombre), "CargoId", "Nombre", directorio.CargoId);
            ViewBag.GeneroId = new SelectList(db.Genero.OrderBy(q => q.Nombre), "GeneroId", "Nombre", directorio.GeneroId);
            ViewBag.OrganizacionId = directorio.OrganizacionId;

            return View(directorio);
        }

        public ActionResult Delete(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var directorio = db.Directorio.Find(id);
            if (directorio == null)
            {
                return HttpNotFound();
            }
            return View(directorio);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            var directorio = db.Directorio.Find(id);

            var OrganizacionId = directorio.OrganizacionId;
            db.Directorio.Remove(directorio);
            db.SaveChanges();
            TempData["Message"] = Properties.Settings.Default.Success;

            return RedirectToAction("Edit", "Organizacion", new { id = OrganizacionId });
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