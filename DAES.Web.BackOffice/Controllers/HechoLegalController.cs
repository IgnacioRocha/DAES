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
    public class HechoLegalController : Controller
    {
        private SistemaIntegradoContext db = new SistemaIntegradoContext();

        public ActionResult Index()
        {
            return View(db.HechoLegal.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HechoLegal hechoLegal = db.HechoLegal.Find(id);
            if (hechoLegal == null)
            {
                return HttpNotFound();
            }
            return View(hechoLegal);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HechoLegalId,Antecedente")] HechoLegal hechoLegal)
        {
            if (ModelState.IsValid)
            {
                db.HechoLegal.Add(hechoLegal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hechoLegal);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HechoLegal hechoLegal = db.HechoLegal.Find(id);
            if (hechoLegal == null)
            {
                return HttpNotFound();
            }
            return View(hechoLegal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HechoLegalId,Antecedente")] HechoLegal hechoLegal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hechoLegal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hechoLegal);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HechoLegal hechoLegal = db.HechoLegal.Find(id);
            if (hechoLegal == null)
            {
                return HttpNotFound();
            }
            return View(hechoLegal);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HechoLegal hechoLegal = db.HechoLegal.Find(id);
            db.HechoLegal.Remove(hechoLegal);
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