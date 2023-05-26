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
    public class ActaFiscalizacionController : Controller
    {
        private SistemaIntegradoContext db = new SistemaIntegradoContext();

        // GET: ActaFiscalizacion
        public ActionResult Index()
        {
            var actaFiscalizacion = db.ActaFiscalizacion.Include(a => a.Comuna).Include(a => a.GeneroGerente).Include(a => a.GeneroRepresentanteLegal).Include(a => a.Organizacion).Include(a => a.Region);
            return View(actaFiscalizacion.ToList());
        }

        // GET: ActaFiscalizacion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActaFiscalizacion actaFiscalizacion = db.ActaFiscalizacion.Find(id);
            if (actaFiscalizacion == null)
            {
                return HttpNotFound();
            }
            return View(actaFiscalizacion);
        }

        // GET: ActaFiscalizacion/Create
        public ActionResult Create()
        {
            ViewBag.ComunaId = new SelectList(db.Comuna, "ComunaId", "Nombre");
            ViewBag.GeneroGerenteId = new SelectList(db.Genero, "GeneroId", "Nombre");
            ViewBag.GeneroRepresentanteLegalId = new SelectList(db.Genero, "GeneroId", "Nombre");
            ViewBag.OrganizacionId = new SelectList(db.Organizacion, "OrganizacionId", "NumeroRegistro");
            ViewBag.RegionId = new SelectList(db.Region, "RegionId", "Nombre");
            return View();
        }

        // POST: ActaFiscalizacion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ActaFiscalizacionId,FechaCreacion,OrganizacionId,NOficioAcreditacioRequerimientos,FechaSalidaOficioAcreditacionRequerimientos,NActaReunionFiscalizacionInSitu,FechaFiscalizacionInSitu,RUT,RepresentanteLegal,GeneroRepresentanteLegalId,VigenciaRepresentanteLegal,Gerente,GeneroGerenteId,RegionId,ComunaId,DireccionActual,CambioDireccion,HechosLegales,ObservacionesLegales")] ActaFiscalizacion actaFiscalizacion)
        {
            if (ModelState.IsValid)
            {
                db.ActaFiscalizacion.Add(actaFiscalizacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ComunaId = new SelectList(db.Comuna, "ComunaId", "Nombre", actaFiscalizacion.ComunaId);
            ViewBag.GeneroGerenteId = new SelectList(db.Genero, "GeneroId", "Nombre", actaFiscalizacion.GeneroGerenteId);
            ViewBag.GeneroRepresentanteLegalId = new SelectList(db.Genero, "GeneroId", "Nombre", actaFiscalizacion.GeneroRepresentanteLegalId);
            ViewBag.OrganizacionId = new SelectList(db.Organizacion, "OrganizacionId", "NumeroRegistro", actaFiscalizacion.OrganizacionId);
            ViewBag.RegionId = new SelectList(db.Region, "RegionId", "Nombre", actaFiscalizacion.RegionId);
            return View(actaFiscalizacion);
        }

        // GET: ActaFiscalizacion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActaFiscalizacion actaFiscalizacion = db.ActaFiscalizacion.Find(id);
            if (actaFiscalizacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.ComunaId = new SelectList(db.Comuna, "ComunaId", "Nombre", actaFiscalizacion.ComunaId);
            ViewBag.GeneroGerenteId = new SelectList(db.Genero, "GeneroId", "Nombre", actaFiscalizacion.GeneroGerenteId);
            ViewBag.GeneroRepresentanteLegalId = new SelectList(db.Genero, "GeneroId", "Nombre", actaFiscalizacion.GeneroRepresentanteLegalId);
            ViewBag.OrganizacionId = new SelectList(db.Organizacion, "OrganizacionId", "NumeroRegistro", actaFiscalizacion.OrganizacionId);
            ViewBag.RegionId = new SelectList(db.Region, "RegionId", "Nombre", actaFiscalizacion.RegionId);
            return View(actaFiscalizacion);
        }

        // POST: ActaFiscalizacion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ActaFiscalizacionId,FechaCreacion,OrganizacionId,NOficioAcreditacioRequerimientos,FechaSalidaOficioAcreditacionRequerimientos,NActaReunionFiscalizacionInSitu,FechaFiscalizacionInSitu,RUT,RepresentanteLegal,GeneroRepresentanteLegalId,VigenciaRepresentanteLegal,Gerente,GeneroGerenteId,RegionId,ComunaId,DireccionActual,CambioDireccion,HechosLegales,ObservacionesLegales")] ActaFiscalizacion actaFiscalizacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(actaFiscalizacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ComunaId = new SelectList(db.Comuna, "ComunaId", "Nombre", actaFiscalizacion.ComunaId);
            ViewBag.GeneroGerenteId = new SelectList(db.Genero, "GeneroId", "Nombre", actaFiscalizacion.GeneroGerenteId);
            ViewBag.GeneroRepresentanteLegalId = new SelectList(db.Genero, "GeneroId", "Nombre", actaFiscalizacion.GeneroRepresentanteLegalId);
            ViewBag.OrganizacionId = new SelectList(db.Organizacion, "OrganizacionId", "NumeroRegistro", actaFiscalizacion.OrganizacionId);
            ViewBag.RegionId = new SelectList(db.Region, "RegionId", "Nombre", actaFiscalizacion.RegionId);
            return View(actaFiscalizacion);
        }

        // GET: ActaFiscalizacion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActaFiscalizacion actaFiscalizacion = db.ActaFiscalizacion.Find(id);
            if (actaFiscalizacion == null)
            {
                return HttpNotFound();
            }
            return View(actaFiscalizacion);
        }

        // POST: ActaFiscalizacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ActaFiscalizacion actaFiscalizacion = db.ActaFiscalizacion.Find(id);
            db.ActaFiscalizacion.Remove(actaFiscalizacion);
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