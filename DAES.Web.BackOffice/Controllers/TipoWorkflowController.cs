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
    public class TipoWorkflowController : Controller
    {
        private SistemaIntegradoContext db = new SistemaIntegradoContext();

        public ActionResult Index()
        {
            return View(db.TipoWorkflow.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoWorkflow tipoWorkflow = db.TipoWorkflow.Find(id);
            if (tipoWorkflow == null)
            {
                return HttpNotFound();
            }
            return View(tipoWorkflow);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TipoWorkflowId,Nombre,Formulario,Descripcion")] TipoWorkflow tipoWorkflow)
        {
            if (ModelState.IsValid)
            {
                db.TipoWorkflow.Add(tipoWorkflow);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoWorkflow);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoWorkflow tipoWorkflow = db.TipoWorkflow.Find(id);
            if (tipoWorkflow == null)
            {
                return HttpNotFound();
            }
            return View(tipoWorkflow);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TipoWorkflowId,Nombre,Formulario,Descripcion")] TipoWorkflow tipoWorkflow)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoWorkflow).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoWorkflow);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoWorkflow tipoWorkflow = db.TipoWorkflow.Find(id);
            if (tipoWorkflow == null)
            {
                return HttpNotFound();
            }
            return View(tipoWorkflow);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoWorkflow tipoWorkflow = db.TipoWorkflow.Find(id);
            db.TipoWorkflow.Remove(tipoWorkflow);
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