using DAES.Infrastructure;
using DAES.Infrastructure.SistemaIntegrado;
using DAES.Model.SistemaIntegrado;
using DAES.Web.FrontOffice.Helper;
using DAES.Web.FrontOffice.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace DAES.Web.FrontOffice.Controllers
{

    [Audit]
    public class ActualizacionController : Controller
    {

        public class SearchModel
        {

            public SearchModel()
            {
                Organizacions = new List<Organizacion>();
            }

            public bool First { get; set; } = true;

            [Display(Name = "Razón social o número registro")]
            [Required(ErrorMessage = "Es necesario especificar este dato")]
            public string Filter { get; set; }

            public List<Organizacion> Organizacions { get; set; }
        }

        private SistemaIntegradoContext _db = new SistemaIntegradoContext();
        private BLL.Custom _custom = new BLL.Custom();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Start()
        {

            Global.CurrentClaveUnica.ClaveUnicaRequestAutorization.controller = "Actualizacion";
            Global.CurrentClaveUnica.ClaveUnicaRequestAutorization.method = "Search";
            return Redirect(Global.CurrentClaveUnica.ClaveUnicaRequestAutorization.uri);
        }

        public ActionResult Search()
        {
            if (!Global.CurrentClaveUnica.IsAutenticated)
            {
                return View("_Error", new Exception("Usuario no autenticado con Clave Única."));
            }

            return View(new SearchModel());
        }

        [HttpPost]
        public ActionResult Search(string Filter)
        {
            if (!Global.CurrentClaveUnica.IsAutenticated)
            {
                return View("_Error", new Exception("Usuario no autenticado con Clave Única."));
            }

            IQueryable<Organizacion> query = _db.Organizacion;
            query = query.Where(q => q.RazonSocial.Contains(Filter) || q.NumeroRegistro.Contains(Filter) || q.Sigla.Contains(Filter));
            query = query.Where(q => q.EstadoId == (int)Infrastructure.Enum.Estado.Vigente);

            var model = new SearchModel();
            model.Organizacions = query.OrderBy(q => q.NumeroRegistro).ToList();
            model.First = false;

            return View(model);
        }

        public ActionResult Create(int id)
        {
            if (!Global.CurrentClaveUnica.IsAutenticated)
            {
                return View("_Error", new Exception("Usuario no autenticado con Clave Única."));
            }

            var model = _db.Organizacion.FirstOrDefault(q => q.OrganizacionId == id);
            if (model == null)
            {
                return View("_Error", new Exception("Organización no encontrada"));
            }

            ViewBag.CiudadId = new SelectList(_db.Ciudad.OrderBy(q => q.Nombre), "CiudadId", "Nombre", model.CiudadId);
            ViewBag.ComunaId = new SelectList(_db.Comuna.OrderBy(q => q.Nombre), "ComunaId", "Nombre", model.ComunaId);
            ViewBag.EstadoId = new SelectList(_db.Estado.OrderBy(q => q.Nombre), "EstadoId", "Nombre", model.EstadoId);
            ViewBag.SituacionId = new SelectList(_db.Situacion.OrderBy(q => q.Nombre), "SituacionId", "Nombre", model.SituacionId);
            ViewBag.RegionId = new SelectList(_db.Region.OrderBy(q => q.Nombre), "RegionId", "Nombre", model.RegionId);
            ViewBag.RubroId = new SelectList(_db.Rubro.OrderBy(q => q.Nombre), "RubroId", "Nombre", model.RubroId);
            ViewBag.SubRubroId = new SelectList(_db.SubRubro.OrderBy(q => q.Nombre), "SubRubroId", "Nombre", model.SubRubroId);
            ViewBag.TipoOrganizacionId = new SelectList(_db.TipoOrganizacion.OrderBy(q => q.Nombre), "TipoOrganizacionId", "Nombre", model.TipoOrganizacionId);
            ViewBag.RegionSolicitanteId = new SelectList(_db.Region.OrderBy(q => q.Nombre), "RegionId", "Nombre");

            return View(new Model.DTO.DTOActualizacion()
            {
                RUTSolicitante = string.Concat(Global.CurrentClaveUnica.ClaveUnicaUser.RolUnico.numero, Global.CurrentClaveUnica.ClaveUnicaUser.RolUnico.DV),
                NombresSolicitante = string.Join(" ", Global.CurrentClaveUnica.ClaveUnicaUser.name.nombres).ToUpperNull(),
                ApellidosSolicitante = string.Join(" ", Global.CurrentClaveUnica.ClaveUnicaUser.name.apellidos).ToUpperNull(),
                OrganizacionId = model.OrganizacionId,
                TipoOrganizacionId = model.TipoOrganizacionId,
                TipoOrganizacion = model.TipoOrganizacion,
                EstadoId = model.EstadoId,
                Estado = model.Estado,
                SituacionId = model.SituacionId,
                RubroId = model.RubroId,
                SubRubroId = model.SubRubroId,
                RegionId = model.RegionId,
                ComunaId = model.ComunaId,
                CiudadId = model.CiudadId,
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
                FechaPubliccionDiarioOficial = model.FechaPubliccionDiarioOficial,
                FechaActualizacion = model.FechaActualizacion,
                EsImportanciaEconomica = model.EsImportanciaEconomica,
                FechaVigente = model.FechaVigente,
                FechaDisolucion = model.FechaDisolucion,
                FechaConstitucion = model.FechaConstitucion,
                FechaCancelacion = model.FechaCancelacion,
                FechaInexistencia = model.FechaInexistencia,
                FechaAsignacionRol = model.FechaAsignacionRol
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Model.DTO.DTOActualizacion model)
        {
            if (!Global.CurrentClaveUnica.IsAutenticated)
            {
                ModelState.AddModelError(string.Empty, "Usuario no autenticado con clave única");
            }

            if (!_db.Organizacion.Any(q => q.OrganizacionId == model.OrganizacionId))
            {
                ModelState.AddModelError(string.Empty, "La organización no fue encontrada.");
            }

            if (!model.RUTSolicitante.IsRut())
            {
                ModelState.AddModelError(string.Empty, "El rut del solicitante ingresado no es válido");
            }

            if (!string.IsNullOrWhiteSpace(model.RUT) && !model.RUT.IsRut())
            {
                ModelState.AddModelError(string.Empty, "El rut de la organización ingresado no es válido");
            }

            if (ModelState.IsValid)
            {
                var proceso = new Proceso()
                {
                    DefinicionProcesoId = (int)Infrastructure.Enum.DefinicionProceso.Actualizacion,
                    OrganizacionId = model.OrganizacionId
                };

                proceso.Solicitante = new Solicitante()
                {
                    Rut = model.RUTSolicitante,
                    Nombres = model.NombresSolicitante,
                    Apellidos = model.ApellidosSolicitante,
                    Email = model.EmailSolicitante,
                    Fono = model.FonoSolicitante,
                    RegionId = model.RegionSolicitanteId
                };

                proceso.ActualizacionOrganizacions.Add(new ActualizacionOrganizacion()
                {
                    OrganizacionId = model.OrganizacionId,
                    TipoOrganizacionId = model.TipoOrganizacionId,
                    EstadoId = model.EstadoId,
                    SituacionId = model.SituacionId,
                    RubroId = model.RubroId,
                    SubRubroId = model.SubRubroId,
                    RegionId = model.RegionId,
                    ComunaId = model.ComunaId,
                    CiudadId = model.CiudadId,
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
                    FechaPubliccionDiarioOficial = model.FechaPubliccionDiarioOficial,
                    FechaActualizacion = model.FechaActualizacion,
                    EsImportanciaEconomica = model.EsImportanciaEconomica,
                    FechaVigente = model.FechaVigente,
                    FechaDisolucion = model.FechaDisolucion,
                    FechaConstitucion = model.FechaConstitucion,
                    FechaCancelacion = model.FechaCancelacion,
                    FechaInexistencia = model.FechaInexistencia,
                    FechaAsignacionRol = model.FechaAsignacionRol
                });

                try
                {
                    var p = _custom.ProcesoStart(proceso);
                    TempData["Success"] = string.Format("Trámite número {0} terminado correctamente. Se ha enviado una notificación al correo {1} con los detalles.", p.ProcesoId, proceso.Solicitante.Email);

                    return RedirectToAction("Finish");
                }
                catch (Exception ex)
                {
                    return View("_Error", ex);
                }
            }

            ViewBag.CiudadId = new SelectList(_db.Ciudad.OrderBy(q => q.Nombre), "CiudadId", "Nombre", model.CiudadId);
            ViewBag.ComunaId = new SelectList(_db.Comuna.OrderBy(q => q.Nombre), "ComunaId", "Nombre", model.ComunaId);
            ViewBag.EstadoId = new SelectList(_db.Estado.OrderBy(q => q.Nombre), "EstadoId", "Nombre", model.EstadoId);
            ViewBag.SituacionId = new SelectList(_db.Situacion.OrderBy(q => q.Nombre), "SituacionId", "Nombre", model.SituacionId);
            ViewBag.RegionId = new SelectList(_db.Region.OrderBy(q => q.Nombre), "RegionId", "Nombre", model.RegionId);
            ViewBag.RubroId = new SelectList(_db.Rubro.OrderBy(q => q.Nombre), "RubroId", "Nombre", model.RubroId);
            ViewBag.SubRubroId = new SelectList(_db.SubRubro.OrderBy(q => q.Nombre), "SubRubroId", "Nombre", model.SubRubroId);
            ViewBag.TipoOrganizacionId = new SelectList(_db.TipoOrganizacion.OrderBy(q => q.Nombre), "TipoOrganizacionId", "Nombre", model.TipoOrganizacionId);
            ViewBag.RegionSolicitanteId = new SelectList(_db.Region.OrderBy(q => q.Nombre), "RegionId", "Nombre", model.RegionSolicitanteId);

            return View(model);
        }

        public ActionResult Finish()
        {
            return View();
        }
    }
}