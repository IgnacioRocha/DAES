using DAES.Infrastructure.SistemaIntegrado;
using DAES.Model.SistemaIntegrado;
using DAES.Web.BackOffice.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace DAES.Web.BackOffice.Controllers
{

    [Audit]
    [Authorize]
    public class DefinicionProcesoController : Controller
    {

        private SistemaIntegradoContext db = new SistemaIntegradoContext();
        private BLL.Custom _custom = new BLL.Custom();

        public ActionResult Index()
        {
            var definicionProceso = db.DefinicionProceso;
            return View(definicionProceso.ToList());
        }

        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var definicionProceso = db.DefinicionProceso.Find(id);
            if (definicionProceso == null)
            {
                return HttpNotFound();
            }

            definicionProceso.DefinicionWorkflows = definicionProceso.DefinicionWorkflows.OrderBy(q => q.Secuencia).ToList();

            return View(definicionProceso);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(int DefinicionProcesoId, List<DefinicionWorkflow> DefinicionWorkflowList, string ids)
        {

            _custom.ProcessDefinitionUpdate(DefinicionProcesoId, ids);

            TempData["Message"] = Properties.Settings.Default.Success;
            return RedirectToAction("Details", new { id = DefinicionProcesoId });
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DefinicionProceso definicionProceso)
        {

            definicionProceso.Fecha = DateTime.Now;
            definicionProceso.Habilitado = true;
            definicionProceso.Autor = User.Identity.Name;

            if (ModelState.IsValid)
            {
                db.DefinicionProceso.Add(definicionProceso);
                db.SaveChanges();
                TempData["Message"] = Properties.Settings.Default.Success;
                return RedirectToAction("details", new { id = definicionProceso.DefinicionProcesoId });
            }

            return View(definicionProceso);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var definicionProceso = db.DefinicionProceso.Find(id);
            if (definicionProceso == null)
            {
                return HttpNotFound();
            }
            return View(definicionProceso);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DefinicionProceso definicionProceso)
        {

            if (ModelState.IsValid)
            {
                db.Entry(definicionProceso).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Message"] = Properties.Settings.Default.Success;
                return RedirectToAction("Index");
            }
            return View(definicionProceso);
        }

        public ActionResult Delete(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var definicionProceso = db.DefinicionProceso.Find(id);
            if (definicionProceso == null)
            {
                return HttpNotFound();
            }

            return View(definicionProceso);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            _custom.DefinicionProcesoDelete(id);
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