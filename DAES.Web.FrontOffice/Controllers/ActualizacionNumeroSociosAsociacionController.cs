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
    public class ActualizacionNumeroSociosAsociacionController : Controller
    {
        private SistemaIntegradoContext _db = new SistemaIntegradoContext();
        private BLL.Custom _custom = new BLL.Custom();

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

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Start()
        {
            Global.CurrentClaveUnica.ClaveUnicaRequestAutorization.controller = "ActualizacionNumeroSociosAsociacion";
            Global.CurrentClaveUnica.ClaveUnicaRequestAutorization.method = "Search";

            //Global.CurrentClaveUnica.ClaveUnicaUser = new ClaveUnicaUser();
            //Global.CurrentClaveUnica.ClaveUnicaUser.name = new Name
            //{
            //    nombres = new System.Collections.Generic.List<string> { "DESA", "DESA" },
            //    apellidos = new System.Collections.Generic.List<string> { "DESA", "DESA" }
            //};
            //Global.CurrentClaveUnica.ClaveUnicaUser.RolUnico = new RolUnico
            //{
            //    numero = 44444444,
            //    DV = "4",
            //    tipo = "RUN"
            //};
            //return RedirectToAction(Global.CurrentClaveUnica.ClaveUnicaRequestAutorization.method, Global.CurrentClaveUnica.ClaveUnicaRequestAutorization.controller);

            return Redirect(Global.CurrentClaveUnica.ClaveUnicaRequestAutorization.uri);
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

            if (string.IsNullOrEmpty(Filter))
            {
                return View(new DTOSearch());
            }

            IQueryable<Organizacion> query = _db.Organizacion;
            query = query.Where(q => q.TipoOrganizacionId == (int)Infrastructure.Enum.TipoOrganizacion.AsociacionGremial || q.TipoOrganizacionId == (int)Infrastructure.Enum.TipoOrganizacion.AsociacionConsumidores);
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

            ViewBag.CiudadId = new SelectList(_db.Ciudad.OrderBy(q => q.Nombre), "CiudadId", "Nombre", model.CiudadId);
            ViewBag.ComunaId = new SelectList(_db.Comuna.OrderBy(q => q.Nombre), "ComunaId", "Nombre", model.ComunaId);
            ViewBag.EstadoId = new SelectList(_db.Estado.OrderBy(q => q.Nombre), "EstadoId", "Nombre", model.EstadoId);
            ViewBag.SituacionId = new SelectList(_db.Situacion.OrderBy(q => q.Nombre), "SituacionId", "Nombre", model.SituacionId);
            ViewBag.RegionId = new SelectList(_db.Region.OrderBy(q => q.Nombre), "RegionId", "Nombre", model.RegionId);
            ViewBag.TipoOrganizacionId = new SelectList(_db.TipoOrganizacion.OrderBy(q => q.Nombre), "TipoOrganizacionId", "Nombre", model.TipoOrganizacionId);
            ViewBag.RegionSolicitanteId = new SelectList(_db.Region.OrderBy(q => q.Nombre), "RegionId", "Nombre");

            return View(new Model.DTO.DTOActualizacionNumeroSociosAsociacion()
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
        public ActionResult Create(Model.DTO.DTOActualizacionNumeroSociosAsociacion model)
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
                    DefinicionProcesoId = (int)Infrastructure.Enum.DefinicionProceso.ActualizacionNumeroSociosAsociacion,
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

                foreach (string fileName in Request.Files)
                {

                    HttpPostedFileBase file = Request.Files[fileName];
                    var target = new MemoryStream();
                    file.InputStream.CopyTo(target);

                    proceso.Documentos.Add(new Documento()
                    {
                        FechaCreacion = DateTime.Now,
                        Autor = model.EmailSolicitante,
                        Content = target.ToArray(),
                        FileName = file.FileName,
                        Organizacion = proceso.Organizacion,
                        TipoDocumentoId = (int)Infrastructure.Enum.TipoDocumento.SinClasificar,
                        TipoPrivacidadId = (int)Infrastructure.Enum.TipoPrivacidad.Privado
                    });
                }

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