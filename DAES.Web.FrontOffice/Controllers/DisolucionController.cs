using System;
using System.Collections.Generic;
using DAES.Infrastructure.SistemaIntegrado;
using DAES.Model.SistemaIntegrado;
using System.ComponentModel.DataAnnotations;
using DAES.Web.FrontOffice.Models;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DAES.Web.FrontOffice.Controllers
{
    public class DisolucionController : Controller
    {
        private SistemaIntegradoContext _db = new SistemaIntegradoContext();
        private BLL.Custom _custom = new BLL.Custom();
        public class SearchModel
        {
            public SearchModel()
            {
                Organizacions = new List<Organizacion>();
            }
            public bool First { get; set; } = true;

            [Display(Name = "Ingrese numero de registro")]
            [Required(ErrorMessage = "Es necesario especificar este dato")]
            public string Filter { get; set; }
            public List<Organizacion> Organizacions { get; set; }
        }

        // TODO: Modificar al integrar Clave Única
        public ActionResult Search()
        {            
            /*if(!Global.CurrentClaveUnica.IsAutenticated)
            {
                return View("_Error", new Exception("Usuario no autenticado con Clave Única."));
            }*/

            return View(new SearchModel());
        }

        [HttpPost]
        public ActionResult Search(string Filter)
        {
                IQueryable<Organizacion> query = _db.Organizacion;
            query = query.Where(q => q.EstadoId == (int)Infrastructure.Enum.Estado.Vigente);
            query = query.Where(q => /*q.RazonSocial.Contains(Filter) || */q.NumeroRegistro.Contains(Filter)/* || q.Sigla.Contains(Filter)*/);

            var model = new SearchModel();
            model.Organizacions = query.OrderBy(q => q.NumeroRegistro).ToList();
            model.First = false;

            return View(model);
        }

        public ActionResult Start()
        {
            Global.CurrentClaveUnica.ClaveUnicaRequestAutorization.method = "Search";

            /*Global.CurrentClaveUnica.ClaveUnicaUser.name = new Name
            {
                nombres = new System.Collections.Generic.List<string> { "IGNACIO", "ALFREDO" },
                apellidos = new System.Collections.Generic.List<string> { "ROCHA", "PAVEZ" }
            };
            Global.CurrentClaveUnica.ClaveUnicaUser = new ClaveUnicaUser();

            Global.CurrentClaveUnica.ClaveUnicaUser.RolUnico = new RolUnico
            {
                numero = 17957898,
                DV = "0",
                tipo = "RUN"
            };*/
            return RedirectToAction(Global.CurrentClaveUnica.ClaveUnicaRequestAutorization.method, Global.CurrentClaveUnica.ClaveUnicaRequestAutorization.controller);
            /*return Redirect(Global.CurrentClaveUnica.ClaveUnicaRequestAutorization.uri);*/
        }

        public ActionResult Create(int id)
        {
            /*if (!Global.CurrentClaveUnica.IsAutenticated)
            {
                return View("_Error", new Exception("Usuario no autenticado con Clave Única."));
            }*/

            var model = _db.Organizacion.FirstOrDefault(q => q.OrganizacionId == id );
            if (model == null)
            {
                return View("_Error", new Exception("Organización no encontrada"));
            }

            ViewBag.CiudadId = new SelectList(_db.Ciudad.OrderBy(q => q.Nombre), "CiudadId", "Nombre", model.CiudadId);
            ViewBag.ComunaId = new SelectList(_db.Comuna.OrderBy(q => q.Nombre), "ComunaId", "Nombre", model.ComunaId);
            ViewBag.EstadoId = new SelectList(_db.Estado.OrderBy(q => q.Nombre), "EstadoId", "Nombre", model.EstadoId);
            ViewBag.SituacionId = new SelectList(_db.Situacion.OrderBy(q => q.Nombre), "SituacionId", "Nombre", model.SituacionId);
            ViewBag.RegionId = new SelectList(_db.Region.OrderBy(q => q.Nombre), "RegionId", "Nombre", model.RegionId);
            ViewBag.TipoOrganizacionId = new SelectList(_db.TipoOrganizacion.OrderBy(q => q.Nombre), "TipoOrganizacionId", "Nombre", model.TipoOrganizacionId);
            ViewBag.RegionSolicitanteId = new SelectList(_db.Region.OrderBy(q => q.Nombre), "RegionId", "Nombre");

            var modelDTO = new Model.DTO.DTODisolucion()
            {
                OrganizacionId = model.OrganizacionId,
                TipoOrganizacionId = model.TipoOrganizacionId,
                TipoOrganizacion = model.TipoOrganizacion,
                EstadoId = model.EstadoId,
                Estado = model.Estado,
                SituacionId = model.SituacionId,
                Situacion = model.Situacion,
                RubroId = model.RubroId,
                Rubro = model.Rubro,
                SubRubroId = model.SubRubroId,
                SubRubro = model.SubRubro,
                RegionId = model.RegionId,
                Region = model.Region,
                ComunaId = model.ComunaId,
                Comuna = model.Comuna,
                CiudadId = model.CiudadId,
                Ciudad = model.Ciudad,
                NumeroRegistro = model.NumeroRegistro,
                RUT = !string.IsNullOrWhiteSpace(model.RUT) ? model.RUT.Replace(".", string.Empty).Replace("-", string.Empty) : string.Empty,
                RazonSocial = model.RazonSocial,
                Sigla = model.Sigla,
                Direccion = model.Direccion,
                Fono = model.Fono,
                Fax = model.Fax,
                Email = model.Email,
                URL = model.URL,
                NumeroSociosConstituyentes = model.NumeroSociosConstituyentes,
                NumeroSocios = model.NumeroSocios,
                NumeroSociosHombres = model.NumeroSociosHombres,
                NumeroSociosMujeres = model.NumeroSociosMujeres,
                MinistroDeFe = model.MinistroDeFe,
                EsGeneroFemenino = model.EsGeneroFemenino,
                CiudadAsamblea = model.CiudadAsamblea,
                NombreContacto = model.NombreContacto,
                DireccionContacto = model.DireccionContacto,
                TelefonoContacto = model.TelefonoContacto,
                EmailContacto = model.EmailContacto,
                FechaCelebracion = model.FechaCelebracion,
                FechaPublicacionDiarioOficial = model.FechaPublicacionDiarioOficial,
                FechaActualizacion = model.FechaActualizacion,
                EsImportanciaEconomica = model.EsImportanciaEconomica,
                FechaVigente = model.FechaVigente,
                FechaDisolucion = model.FechaDisolucion,
                FechaConstitucion = model.FechaConstitucion,
                FechaCancelacion = model.FechaCancelacion,
                FechaInexistencia = model.FechaInexistencia,
                FechaAsignacionRol = model.FechaAsignacionRol
            };


            if (model.TipoOrganizacionId == (int)Infrastructure.Enum.TipoOrganizacion.Cooperativa && model.FechaPublicacionDiarioOficial.Value.Year < 2003)
            {
                return View("CooperativaPrevia", modelDTO);
            }
            if (model.TipoOrganizacionId == (int)Infrastructure.Enum.TipoOrganizacion.Cooperativa && model.FechaPublicacionDiarioOficial.Value.Year > 2003)
            {
                return View("CooperativaPosterior");
            }
            if (model.TipoOrganizacionId == (int)Infrastructure.Enum.TipoOrganizacion.AsociacionGremial)
            {
                return View("AsociacionGremial");
            }
            if (model.TipoOrganizacionId == (int)Infrastructure.Enum.TipoOrganizacion.AsociacionConsumidores)
            {
                return View("AsociacionConsumidores", modelDTO);
            }

            return View();

            /*return View(new Model.DTO.DTODisolucion()
            {
                *//*RUTSolicitante = string.Concat(Global.CurrentClaveUnica.ClaveUnicaUser.RolUnico.numero, Global.CurrentClaveUnica.ClaveUnicaUser.RolUnico.DV),
                NombresSolicitante = string.Concat(" ", Global.CurrentClaveUnica.ClaveUnicaUser.name.nombres).ToUpper(),
                ApellidosSolicitante = string.Concat(" ", Global.CurrentClaveUnica.ClaveUnicaUser.name.apellidos).ToUpper(),*//*
                OrganizacionId = model.OrganizacionId,
                TipoOrganizacionId = model.TipoOrganizacionId,
                TipoOrganizacion = model.TipoOrganizacion,
                EstadoId = model.EstadoId,
                Estado = model.Estado,
                SituacionId = model.SituacionId,
                Situacion = model.Situacion,
                RubroId = model.RubroId,
                Rubro = model.Rubro,
                SubRubroId = model.SubRubroId,
                SubRubro = model.SubRubro,
                RegionId = model.RegionId,
                Region = model.Region,
                ComunaId = model.ComunaId,
                Comuna = model.Comuna,
                CiudadId = model.CiudadId,
                Ciudad = model.Ciudad,
                NumeroRegistro = model.NumeroRegistro,
                RUT = !string.IsNullOrWhiteSpace(model.RUT) ? model.RUT.Replace(".", string.Empty).Replace("-", string.Empty) : string.Empty,
                RazonSocial = model.RazonSocial,
                Sigla = model.Sigla,
                Direccion = model.Direccion,
                Fono = model.Fono,
                Fax = model.Fax,
                Email = model.Email,
                URL = model.URL,
                NumeroSociosConstituyentes = model.NumeroSociosConstituyentes,
                NumeroSocios = model.NumeroSocios,
                NumeroSociosHombres = model.NumeroSociosHombres,
                NumeroSociosMujeres = model.NumeroSociosMujeres,
                MinistroDeFe = model.MinistroDeFe,
                EsGeneroFemenino = model.EsGeneroFemenino,
                CiudadAsamblea = model.CiudadAsamblea,
                NombreContacto = model.NombreContacto,
                DireccionContacto = model.DireccionContacto,
                TelefonoContacto = model.TelefonoContacto,
                EmailContacto = model.EmailContacto,
                FechaCelebracion = model.FechaCelebracion,
                FechaPublicacionDiarioOficial = model.FechaPublicacionDiarioOficial,
                FechaActualizacion = model.FechaActualizacion,
                EsImportanciaEconomica = model.EsImportanciaEconomica,
                FechaVigente = model.FechaVigente,
                FechaDisolucion = model.FechaDisolucion,
                FechaConstitucion = model.FechaConstitucion,
                FechaCancelacion = model.FechaCancelacion,
                FechaInexistencia = model.FechaInexistencia,
                FechaAsignacionRol = model.FechaAsignacionRol
            });*/
        }

        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create()
        {
            return View();
        }*/
        // GET: Disolucion
        public ActionResult Index()
        {
            return View();
        }
    }
}