using DAES.Infrastructure.SistemaIntegrado;
using DAES.Model.SistemaIntegrado;
using DAES.Web.BackOffice.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace DAES.Web.BackOffice.Controllers
{

    [Audit]
    [Authorize]
    public class LogController : Controller
    {

        public class ModelView
        {

            public ModelView()
            {
                Logs = new List<Log>();
            }

            [Required(ErrorMessage = "Es necesario especificar este dato")]
            [Display(Name = "Texto de búsqueda")]
            public string Filter { get; set; }

            public List<Log> Logs { get; set; }
        }

        private SistemaIntegradoContext db = new SistemaIntegradoContext();

        public ActionResult Index(ModelView model)
        {
            if (!string.IsNullOrWhiteSpace(model.Filter))
            {
                IQueryable<Log> query = db.Log.Where(q =>
                          q.LogAgent.Contains(model.Filter)
                       || q.LogAreaAccessed.Contains(model.Filter)
                       || q.LogContent.Contains(model.Filter)
                       || q.LogDetails.Contains(model.Filter)
                       || q.LogHeader.Contains(model.Filter)
                       || q.LogHttpMethod.Contains(model.Filter)
                       || q.LogIpAddress.Contains(model.Filter)
                       || q.LogUserName.Contains(model.Filter)
                    );

                model.Logs = query.ToList();
            }

            return View(model);
        }

        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Log log = db.Log.Find(id);
            if (log == null)
            {
                return HttpNotFound();
            }
            return View(log);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LogId,LogUserName,LogIpAddress,LogAreaAccessed,LogTimeLocal,LogTimeUtc,LogDetails,LogAgent,LogHttpMethod,LogHeader,LogContent")] Log log)
        {
            if (ModelState.IsValid)
            {
                log.LogId = Guid.NewGuid();
                db.Log.Add(log);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(log);
        }

        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Log log = db.Log.Find(id);
            if (log == null)
            {
                return HttpNotFound();
            }
            return View(log);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LogId,LogUserName,LogIpAddress,LogAreaAccessed,LogTimeLocal,LogTimeUtc,LogDetails,LogAgent,LogHttpMethod,LogHeader,LogContent")] Log log)
        {
            if (ModelState.IsValid)
            {
                db.Entry(log).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(log);
        }

        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Log log = db.Log.Find(id);
            if (log == null)
            {
                return HttpNotFound();
            }
            return View(log);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Log log = db.Log.Find(id);
            db.Log.Remove(log);
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