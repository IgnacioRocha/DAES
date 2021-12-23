using DAES.Infrastructure;
using DAES.Infrastructure.SistemaIntegrado;
using DAES.Model.SistemaIntegrado;
using DAES.Web.FrontOffice.Helper;
using DAES.Web.FrontOffice.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DAES.Web.FrontOffice.Controllers
{
    [Audit]
    public class InformacionAnualController : Controller
    {
        public class DTOSearch
        {

            public DTOSearch()
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
        private List<Documento> documentos = new List<Documento>();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Start()
        {
            Global.CurrentClaveUnica.ClaveUnicaRequestAutorization.controller = "InformacionAnual";
            Global.CurrentClaveUnica.ClaveUnicaRequestAutorization.method = "Search";

            //activar en desarrollo, bypass de clave única
            //Global.CurrentClaveUnica.ClaveUnicaUser = new ClaveUnicaUser();
            //Global.CurrentClaveUnica.ClaveUnicaUser.name = new Name
            //{
            //    nombres = new System.Collections.Generic.List<string> { "DESA", "DESA" },
            //    apellidos = new System.Collections.Generic.List<string> { "DESA", "DESA" }
            //};
            //Global.CurrentClaveUnica.ClaveUnicaUser.RolUnico = new RolUnico
            //{
            //    numero = 13703244,
            //    DV = "9",
            //    tipo = "RUN"
            //};
            //return RedirectToAction(Global.CurrentClaveUnica.ClaveUnicaRequestAutorization.method, Global.CurrentClaveUnica.ClaveUnicaRequestAutorization.controller);

            return Redirect(Global.CurrentClaveUnica.ClaveUnicaRequestAutorization.uri);
        }
        public ActionResult Finish()
        {
            return View();
        }

        public ActionResult Search()
        {
            if (!Global.CurrentClaveUnica.IsAutenticated)
            {
                return View("_Error", new Exception("Usuario no autenticado con Clave Única."));
            }

            return View(new DTOSearch());
        }

        [HttpPost]
        public ActionResult Search(string Filter)
        {
            if (!Global.CurrentClaveUnica.IsAutenticated)
            {
                return View("_Error", new Exception("Usuario no autenticado con Clave Única."));
            }

            IQueryable<Organizacion> query = _db.Organizacion;
            query = query.Where(q => q.TipoOrganizacionId == (int)Infrastructure.Enum.TipoOrganizacion.AsociacionConsumidores);
            query = query.Where(q => q.EstadoId == (int)Infrastructure.Enum.Estado.Vigente);
            query = query.Where(q => q.RazonSocial.Contains(Filter) || q.NumeroRegistro.Contains(Filter) || q.Sigla.Contains(Filter));

            var model = new DTOSearch();
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

            ViewBag.Periodo = new SelectList(_db.Periodo.Where(q => q.Tipo == "InformacionAnual").OrderByDescending(q => q.PeriodoId), "Descripcion", "Descripcion");
            ViewBag.RegionSolicitanteId = new SelectList(_db.Region.OrderBy(q => q.Nombre), "RegionId", "Nombre");
            ViewBag.CiudadId = new SelectList(_db.Ciudad.OrderBy(q => q.Nombre), "CiudadId", "Nombre", model.CiudadId);
            ViewBag.ComunaId = new SelectList(_db.Comuna.OrderBy(q => q.Nombre), "ComunaId", "Nombre", model.ComunaId);
            ViewBag.EstadoId = new SelectList(_db.Estado.OrderBy(q => q.Nombre), "EstadoId", "Nombre", model.EstadoId);
            ViewBag.SituacionId = new SelectList(_db.Situacion.OrderBy(q => q.Nombre), "SituacionId", "Nombre", model.SituacionId);
            ViewBag.RegionId = new SelectList(_db.Region.OrderBy(q => q.Nombre), "RegionId", "Nombre", model.RegionId);
            ViewBag.RubroId = new SelectList(_db.Rubro.OrderBy(q => q.Nombre), "RubroId", "Nombre", model.RubroId);
            ViewBag.SubRubroId = new SelectList(_db.SubRubro.OrderBy(q => q.Nombre), "SubRubroId", "Nombre", model.SubRubroId);
            ViewBag.TipoOrganizacionId = new SelectList(_db.TipoOrganizacion.OrderBy(q => q.Nombre), "TipoOrganizacionId", "Nombre", model.TipoOrganizacionId);

            return View(new Model.DTO.DTOInformacionAnual()
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
                FechaPublicacionDiarioOficial = model.FechaPublicacionDiarioOficial,
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

        protected void SetFile(HttpPostedFileBase file, int TipoDocumentoId)
        {
            if (file == null)
            {
                return;
            }

            var target = new MemoryStream();
            file.InputStream.CopyTo(target);

            documentos.Add(new Documento()
            {
                FechaCreacion = DateTime.Now,
                Content = target.ToArray(),
                FileName = file.FileName,
                TipoDocumentoId = TipoDocumentoId,
                TipoPrivacidadId = (int)DAES.Infrastructure.Enum.TipoPrivacidad.Privado

            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Model.DTO.DTOInformacionAnual model)
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

            if (ModelState.IsValid)
            {
                var proceso = new Proceso()
                {
                    DefinicionProcesoId = (int)Infrastructure.Enum.DefinicionProceso.InformacionACAnual,
                    OrganizacionId = model.OrganizacionId,
                    Observacion = model.Observacion
                };

                proceso.Solicitante = new Solicitante()
                {
                    Rut = model.RUTSolicitante,
                    Nombres = model.NombresSolicitante,
                    Apellidos = model.ApellidosSolicitante,
                    Email = model.EmailSolicitante,
                    Fono = model.FonoSolicitante,
                    RegionId = model.RegionSolicitanteId,
                    Cargo = model.CargoFuncion
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
                    FechaPublicacionDiarioOficial = model.FechaPublicacionDiarioOficial,
                    FechaActualizacion = model.FechaActualizacion,
                    EsImportanciaEconomica = model.EsImportanciaEconomica,
                    FechaVigente = model.FechaVigente,
                    FechaDisolucion = model.FechaDisolucion,
                    FechaConstitucion = model.FechaConstitucion,
                    FechaCancelacion = model.FechaCancelacion,
                    FechaInexistencia = model.FechaInexistencia,
                    FechaAsignacionRol = model.FechaAsignacionRol
                });

                SetFile(model.ActaAsambleaSocios, (int)Infrastructure.Enum.TipoDocumento.ActaAsambleaSocios);
                SetFile(model.EstadosFinancierosDebidamenteAprobadosPorAsamblea, (int)Infrastructure.Enum.TipoDocumento.EstadosFinancierosDebidamenteAprobadosPorAsamblea);
                SetFile(model.FichaDetalladaFuentesFinancieros, (int)Infrastructure.Enum.TipoDocumento.FichaDetalladaFuentesFinancieros);
                SetFile(model.FichaDatos, (int)Infrastructure.Enum.TipoDocumento.FichaDatos);
                SetFile(model.InformeComisionRevisodoraCuentas, (int)Infrastructure.Enum.TipoDocumento.InformeComisionRevisodoraCuentas);

                foreach (var item in documentos)
                {
                    item.Autor = model.EmailSolicitante;
                    item.Organizacion = proceso.Organizacion;
                    item.Periodo = model.Periodo;
                }

                proceso.Documentos = documentos;

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

            ViewBag.Periodo = new SelectList(_db.Periodo.Where(q => q.Tipo == "InformacionAnual").OrderByDescending(q => q.PeriodoId), "Descripcion", "Descripcion");
            ViewBag.RegionSolicitanteId = new SelectList(_db.Region.OrderBy(q => q.Nombre), "RegionId", "Nombre");
            ViewBag.CiudadId = new SelectList(_db.Ciudad.OrderBy(q => q.Nombre), "CiudadId", "Nombre", model.CiudadId);
            ViewBag.ComunaId = new SelectList(_db.Comuna.OrderBy(q => q.Nombre), "ComunaId", "Nombre", model.ComunaId);
            ViewBag.EstadoId = new SelectList(_db.Estado.OrderBy(q => q.Nombre), "EstadoId", "Nombre", model.EstadoId);
            ViewBag.SituacionId = new SelectList(_db.Situacion.OrderBy(q => q.Nombre), "SituacionId", "Nombre", model.SituacionId);
            ViewBag.RegionId = new SelectList(_db.Region.OrderBy(q => q.Nombre), "RegionId", "Nombre", model.RegionId);
            ViewBag.RubroId = new SelectList(_db.Rubro.OrderBy(q => q.Nombre), "RubroId", "Nombre", model.RubroId);
            ViewBag.SubRubroId = new SelectList(_db.SubRubro.OrderBy(q => q.Nombre), "SubRubroId", "Nombre", model.SubRubroId);
            ViewBag.TipoOrganizacionId = new SelectList(_db.TipoOrganizacion.OrderBy(q => q.Nombre), "TipoOrganizacionId", "Nombre", model.TipoOrganizacionId);

            return View(model);
        }
    }
}