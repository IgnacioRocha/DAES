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
    public class PublicacionFinancieraController : Controller
    {
        public class Search
        {

            public Search()
            {
                Organizacions = new List<Organizacion>();
            }

            [Display(Name = "Razón social o número registro")]
            [Required(ErrorMessage = "Es necesario especificar este dato")]
            public string Filter { get; set; }

            public bool First { get; set; } = true;
            public List<Organizacion> Organizacions { get; set; }
        }

        private SistemaIntegradoContext _db = new SistemaIntegradoContext();

        public ActionResult Index()
        {
            IQueryable<Organizacion> query = _db.Organizacion;
            query = query.Where(q => q.TipoOrganizacionId == (int)Infrastructure.Enum.TipoOrganizacion.Cooperativa);
            query = query.Where(q => q.EstadoId == (int)Infrastructure.Enum.Estado.Vigente);
            query = query.Where(q => q.EsImportanciaEconomica);

            var model = new Search()
            {
                Organizacions = query.OrderBy(q => q.NumeroRegistro).ToList(),
                First = false
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(string Filter)
        {
            IQueryable<Organizacion> query = _db.Organizacion;
            query = query.Where(q => q.TipoOrganizacionId == (int)Infrastructure.Enum.TipoOrganizacion.Cooperativa);
            query = query.Where(q => q.EsImportanciaEconomica);
            query = query.Where(q => q.EstadoId == (int)Infrastructure.Enum.Estado.Vigente);
            query = query.Where(q => q.RazonSocial.Contains(Filter) || q.NumeroRegistro.Contains(Filter) || q.Sigla.Contains(Filter));

            var model = new Search()
            {
                Organizacions = query.OrderByDescending(q => q.NumeroRegistro).ToList(),
                First = false
            };

            return View(model);
        }
        public ActionResult Details(int id)
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