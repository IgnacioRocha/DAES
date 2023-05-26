using DAES.Infrastructure.SistemaIntegrado;
using DAES.Model.SistemaIntegrado;
using DAES.Web.FrontOffice.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace DAES.Web.FrontOffice.Controllers
{
    [Audit]
    public class OrganizacionController : Controller
    {
        public class Search
        {

            public Search()
            {
                Organizacions = new List<Organizacion>();
            }

            public bool First { get; set; } = true;

            [Display(Name = "Tipo organización")]
            public int? TipoOrganizacionId { get; set; }

            [Display(Name = "Estado")]
            public int? EstadoId { get; set; }

            [Display(Name = "Rubro")]
            public int? RubroId { get; set; }

            [Display(Name = "Región")]
            public int? RegionId { get; set; }

            [Display(Name = "Comuna")]
            public int? ComunaId { get; set; }

            [Display(Name = "Situación")]
            public int? SituacionId { get; set; }

            [Display(Name = "Texto")]
            public string Filter { get; set; }

            public List<Organizacion> Organizacions { get; set; }

            public string Verb { get; set; }
        }

        public class DTOOrganizacion
        {

            public DTOOrganizacion()
            {
            }

            public string Direccion { get; set; }
            public string RazonSocial { get; set; }
        }

        private SistemaIntegradoContext _db = new SistemaIntegradoContext();
        private BLL.Custom _custom = new BLL.Custom();

        public JsonResult AutoComplete(string term)
        {
            IQueryable<Organizacion> query = _db.Organizacion;
            query = query.Where(q => q.EstadoId != (int)Infrastructure.Enum.Estado.RolAsignado);

            if (!string.IsNullOrEmpty(term))
            {
                query = query.Where(q => q.NumeroRegistro.Contains(term) || q.RazonSocial.Contains(term));
            }

            var result = query.Select(c => new { id = c.OrganizacionId, value = c.TipoOrganizacion.Nombre + " " + c.NumeroRegistro + " - " + c.RazonSocial }).Take(25).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetComuna(int RegionId)
        {
            return Json(new SelectList(_db.Comuna.Where(q => q.RegionId == RegionId).OrderBy(q => q.Nombre), "ComunaId", "Nombre"), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSubRubro(int RubroId)
        {
            return Json(new SelectList(_db.SubRubro.Where(q => q.RubroId == RubroId).OrderBy(q => q.Nombre), "SubrubroId", "Nombre"), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAddress()
        {
            var model = _db.Organizacion
                .Where(q => q.Direccion != null && q.TipoOrganizacionId == (int)Infrastructure.Enum.TipoOrganizacion.AsociacionGremial)
                .Take(20)
                .ToList()
                .Select(q => new DTOOrganizacion()
                {
                    Direccion = string.Concat(q.Direccion, " ", q.Comuna != null ? q.Comuna.Nombre : string.Empty),
                    RazonSocial = q.RazonSocial
                }).ToList();

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Map()
        {
            return View();
        }

        public ActionResult Index(int? TipoOrganizacionId, int? EstadoId, int? RubroId, int? RegionId, int? ComunaId, int? SituacionId, string Filter)
        {
            ViewBag.TipoOrganizacionId = new SelectList(_db.TipoOrganizacion.OrderBy(q => q.Nombre), "TipoOrganizacionId", "Nombre", TipoOrganizacionId);
            ViewBag.EstadoId = new SelectList(_db.Estado.OrderBy(q => q.Nombre), "EstadoId", "Nombre", EstadoId);
            ViewBag.RubroId = new SelectList(_db.Rubro.OrderBy(q => q.Nombre), "RubroId", "Nombre", RubroId);
            ViewBag.RegionId = new SelectList(_db.Region.OrderBy(q => q.Nombre), "RegionId", "Nombre", RegionId);
            ViewBag.ComunaId = new SelectList(_db.Comuna.OrderBy(q => q.Nombre), "ComunaId", "Nombre", ComunaId);
            ViewBag.SituacionId = new SelectList(_db.Situacion.OrderBy(q => q.Nombre), "SituacionId", "Nombre", SituacionId);

            if (!TipoOrganizacionId.HasValue && !EstadoId.HasValue && !RubroId.HasValue && !RegionId.HasValue && !ComunaId.HasValue && string.IsNullOrEmpty(Filter))
            {
                return View(new Search());
            }

            IQueryable<Organizacion> query = _db.Organizacion;
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

            if (!string.IsNullOrEmpty(Filter))
            {
                query = query.Where(q => q.RazonSocial.Contains(Filter) || q.NumeroRegistro.Contains(Filter) || q.Sigla.Contains(Filter) || q.Direccion.Contains(Filter));
            }

            var model = new Search();
            model.Organizacions = query.OrderBy(q => q.NumeroRegistro).ToList();
            model.First = false;

            return View(model);
        }

        public ActionResult Details(int? id)
        {
            var model = _db.Organizacion.Find(id);
            if (model == null)
            {
                return View("_Error", new Exception("Organización no encontrada."));
            }

            return View(model);
        }
    }
}