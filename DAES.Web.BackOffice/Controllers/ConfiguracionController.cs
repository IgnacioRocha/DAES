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
    public class ConfiguracionController : Controller
    {
        private SistemaIntegradoContext db = new SistemaIntegradoContext();

        // GET: Configuracion
        public ActionResult Index()
        {
            return View(db.Configuracion.ToList());
        }

        // GET: Configuracion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Configuracion configuracion = db.Configuracion.Find(id);
            if (configuracion == null)
            {
                return HttpNotFound();
            }
            return View(configuracion);
        }

        // GET: Configuracion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Configuracion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ConfiguracionId,Nombre,Valor")] Configuracion configuracion)
        {
            if (ModelState.IsValid)
            {
                db.Configuracion.Add(configuracion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(configuracion);
        }

        // GET: Configuracion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Configuracion configuracion = db.Configuracion.Find(id);
            if (configuracion == null)
            {
                return HttpNotFound();
            }
            return View(configuracion);
        }

        // POST: Configuracion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ConfiguracionId,Nombre,Valor")] Configuracion configuracion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(configuracion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(configuracion);
        }

        // GET: Configuracion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Configuracion configuracion = db.Configuracion.Find(id);
            if (configuracion == null)
            {
                return HttpNotFound();
            }
            return View(configuracion);
        }

        // POST: Configuracion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Configuracion configuracion = db.Configuracion.Find(id);
            db.Configuracion.Remove(configuracion);
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