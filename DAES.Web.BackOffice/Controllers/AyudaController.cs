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
    public class AyudaController : Controller
    {
        private SistemaIntegradoContext db = new SistemaIntegradoContext();

        // GET: Ayuda
        public ActionResult Index()
        {
            return View(db.Ayuda.ToList());
        }

        public ActionResult Show()
        {
            return View(db.Ayuda.ToList());
        }

        // GET: Ayuda/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ayuda ayuda = db.Ayuda.Find(id);
            if (ayuda == null)
            {
                return HttpNotFound();
            }
            return View(ayuda);
        }

        // GET: Ayuda/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ayuda/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AyudaId,Pregunta,Respuesta,Valoracion")] Ayuda ayuda)
        {
            if (ModelState.IsValid)
            {
                db.Ayuda.Add(ayuda);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ayuda);
        }

        // GET: Ayuda/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ayuda ayuda = db.Ayuda.Find(id);
            if (ayuda == null)
            {
                return HttpNotFound();
            }
            return View(ayuda);
        }

        // POST: Ayuda/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AyudaId,Pregunta,Respuesta,Valoracion")] Ayuda ayuda)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ayuda).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ayuda);
        }

        // GET: Ayuda/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ayuda ayuda = db.Ayuda.Find(id);
            if (ayuda == null)
            {
                return HttpNotFound();
            }
            return View(ayuda);
        }

        // POST: Ayuda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ayuda ayuda = db.Ayuda.Find(id);
            db.Ayuda.Remove(ayuda);
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