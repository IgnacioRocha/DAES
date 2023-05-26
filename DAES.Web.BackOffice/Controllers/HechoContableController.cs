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
    public class HechoContableController : Controller
    {
        private SistemaIntegradoContext db = new SistemaIntegradoContext();

        public ActionResult Index()
        {
            return View(db.HechoContable.ToList());
        }

        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var hechoContable = db.HechoContable.Find(id);
            if (hechoContable == null)
            {
                return HttpNotFound();
            }
            return View(hechoContable);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HechoContableId,Antecedente")] HechoContable hechoContable)
        {

            if (ModelState.IsValid)
            {
                db.HechoContable.Add(hechoContable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hechoContable);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var hechoContable = db.HechoContable.Find(id);
            if (hechoContable == null)
            {
                return HttpNotFound();
            }
            return View(hechoContable);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HechoContableId,Antecedente")] HechoContable hechoContable)
        {

            if (ModelState.IsValid)
            {
                db.Entry(hechoContable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hechoContable);
        }

        public ActionResult Delete(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var hechoContable = db.HechoContable.Find(id);
            if (hechoContable == null)
            {
                return HttpNotFound();
            }
            return View(hechoContable);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            var hechoContable = db.HechoContable.Find(id);
            db.HechoContable.Remove(hechoContable);
            db.SaveChanges();
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