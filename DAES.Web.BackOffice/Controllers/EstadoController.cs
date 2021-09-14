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
    public class EstadoController : Controller
    {

        private SistemaIntegradoContext db = new SistemaIntegradoContext();

        public ActionResult Index()
        {
            return View(db.Estado.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estado estado = db.Estado.Find(id);
            if (estado == null)
            {
                return HttpNotFound();
            }
            return View(estado);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EstadoId,Nombre")] Estado estado)
        {

            if (ModelState.IsValid)
            {
                db.Estado.Add(estado);
                db.SaveChanges();
                TempData["Message"] = Properties.Settings.Default.Success;
                return RedirectToAction("Index");
            }

            return View(estado);
        }

        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var estado = db.Estado.Find(id);
            if (estado == null)
            {
                return HttpNotFound();
            }
            return View(estado);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EstadoId,Nombre")] Estado estado)
        {

            if (ModelState.IsValid)
            {
                db.Entry(estado).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Message"] = Properties.Settings.Default.Success;
                return RedirectToAction("Index");
            }

            return View(estado);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var estado = db.Estado.Find(id);
            if (estado == null)
            {
                return HttpNotFound();
            }
            return View(estado);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var estado = db.Estado.Find(id);
            db.Estado.Remove(estado);
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