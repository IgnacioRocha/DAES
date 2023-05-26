using DAES.Infrastructure.SistemaIntegrado;
using DAES.Model.SistemaIntegrado;
using DAES.Web.BackOffice.Helper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace DAES.Web.BackOffice.Controllers
{

    [Audit]
    [Authorize]
    public class OrganizacionConsultorController : Controller
    {

        public class DTOOrganizacion
        {

            public DTOOrganizacion()
            {
            }

            [Display(Name = "Registro")]
            public string NumeroRegistro { get; set; }

            [Display(Name = "Razón social")]
            public string RazonSocial { get; set; }

            [Display(Name = "Id")]
            public int OrganizacionId { get; set; }
        }

        private SistemaIntegradoContext db = new SistemaIntegradoContext();

        public ActionResult Index(string Query, int? TipoOrganizacionId, int? EstadoId, int? RubroId, int? RegionId, int? ComunaId, int? SituacionId)
        {

            ViewBag.TipoOrganizacionId = new SelectList(db.TipoOrganizacion.OrderBy(q => q.Nombre), "TipoOrganizacionId", "Nombre", TipoOrganizacionId);
            ViewBag.EstadoId = new SelectList(db.Estado.OrderBy(q => q.Nombre), "EstadoId", "Nombre", EstadoId);
            ViewBag.SituacionId = new SelectList(db.Situacion.OrderBy(q => q.Nombre), "SituacionId", "Nombre", SituacionId);
            ViewBag.RubroId = new SelectList(db.Rubro.OrderBy(q => q.Nombre), "RubroId", "Nombre", RubroId);
            ViewBag.RegionId = new SelectList(db.Region.OrderBy(q => q.Nombre), "RegionId", "Nombre", RegionId);
            ViewBag.ComunaId = new SelectList(db.Comuna.OrderBy(q => q.Nombre), "ComunaId", "Nombre", ComunaId);

            if (string.IsNullOrWhiteSpace(Query) && !TipoOrganizacionId.HasValue && !EstadoId.HasValue && !RubroId.HasValue && !RegionId.HasValue && !ComunaId.HasValue && !SituacionId.HasValue)
            {
                return View(new List<DTOOrganizacion>());
            }

            IQueryable<Organizacion> query = db.Organizacion;
            if (TipoOrganizacionId.HasValue)
            {
                query = query.Where(q => q.TipoOrganizacionId == TipoOrganizacionId.Value);
            }

            if (EstadoId.HasValue)
            {
                query = query.Where(q => q.EstadoId == EstadoId.Value);
            }

            if (RubroId.HasValue)
            {
                query = query.Where(q => q.RubroId == RubroId.Value);
            }

            if (RegionId.HasValue)
            {
                query = query.Where(q => q.RegionId == RegionId.Value);
            }

            if (ComunaId.HasValue)
            {
                query = query.Where(q => q.ComunaId == ComunaId.Value);
            }

            if (SituacionId.HasValue)
            {
                query = query.Where(q => q.SituacionId == SituacionId.Value);
            }

            if (!string.IsNullOrEmpty(Query))
            {
                query = query.Where(q => q.OrganizacionId.ToString().Contains(Query) || q.RazonSocial.Contains(Query) || q.NumeroRegistro.Contains(Query) || q.Sigla.Contains(Query) || q.Direccion.Contains(Query));
            }

            var model = query.Select(q => new DTOOrganizacion() { NumeroRegistro = q.NumeroRegistro, RazonSocial = q.RazonSocial, OrganizacionId = q.OrganizacionId });

            return View(model);
        }

        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Organizacion organizacion = db.Organizacion.Find(id);

            if (organizacion == null)
            {
                return HttpNotFound();
            }

            return View(organizacion);
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