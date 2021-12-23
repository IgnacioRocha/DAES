using DAES.Infrastructure;
using DAES.Infrastructure.SistemaIntegrado;
using DAES.Model.SistemaIntegrado;
using DAES.Web.FrontOffice.Helper;
using DAES.Web.FrontOffice.Models;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DAES.Web.FrontOffice.Controllers
{

    [Audit]
    public class ConstitucionController : Controller
    {

        private SistemaIntegradoContext _db = new SistemaIntegradoContext();
        private BLL.Custom _custom = new BLL.Custom();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Confirm()
        {
            return View();
        }

        public ActionResult Start()
        {

            Global.CurrentClaveUnica.ClaveUnicaRequestAutorization.controller = "Constitucion";
            Global.CurrentClaveUnica.ClaveUnicaRequestAutorization.method = "Create";

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

        public ActionResult Create()
        {

            if (!Global.CurrentClaveUnica.IsAutenticated)
            {
                return View("_Error", new Exception("Usuario no autenticado con Clave Única."));
            }

            ViewBag.CiudadId = new SelectList(_db.Ciudad.OrderBy(q => q.Nombre), "CiudadId", "Nombre");
            ViewBag.ComunaId = new SelectList(_db.Comuna.OrderBy(q => q.Nombre), "ComunaId", "Nombre");
            ViewBag.RegionId = new SelectList(_db.Region.OrderBy(q => q.Nombre), "RegionId", "Nombre");
            ViewBag.RubroId = new SelectList(_db.Rubro.OrderBy(q => q.Nombre), "RubroId", "Nombre");
            ViewBag.SubRubroId = new SelectList(_db.SubRubro.OrderBy(q => q.Nombre), "SubRubroId", "Nombre");
            ViewBag.TipoOrganizacionId = new SelectList(_db.TipoOrganizacion.Where(q => q.TipoOrganizacionId < 4).OrderBy(q => q.Nombre), "TipoOrganizacionId", "Nombre");
            ViewBag.RegionSolicitanteId = new SelectList(_db.Region.OrderBy(q => q.Nombre), "RegionId", "Nombre");

            return View(new Model.DTO.DTOConstitucion()
            {
                RUTSolicitante = string.Concat(Global.CurrentClaveUnica.ClaveUnicaUser.RolUnico.numero, Global.CurrentClaveUnica.ClaveUnicaUser.RolUnico.DV),
                NombresSolicitante = string.Join(" ", Global.CurrentClaveUnica.ClaveUnicaUser.name.nombres).ToUpperNull(),
                ApellidosSolicitante = string.Join(" ", Global.CurrentClaveUnica.ClaveUnicaUser.name.apellidos).ToUpperNull()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Model.DTO.DTOConstitucion model)
        {

            if (!Global.CurrentClaveUnica.IsAutenticated)
            {
                ModelState.AddModelError(string.Empty, "Usuario no autenticado con clave única");
            }

            if (!string.IsNullOrWhiteSpace(model.RUT) && !model.RUT.IsRut())
            {
                ModelState.AddModelError(string.Empty, "El rut de la organización ingresado no es válido");
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
                    DefinicionProcesoId = (int)Infrastructure.Enum.DefinicionProceso.ConstitucionWeb,
                };

                proceso.Solicitante = new Solicitante()
                {
                    Rut = model.RUTSolicitante,
                    Nombres = model.NombresSolicitante.ToUpperNull(),
                    Apellidos = model.ApellidosSolicitante.ToUpperNull(),
                    Email = model.EmailSolicitante.ToUpperNull(),
                    Fono = model.FonoSolicitante,
                    RegionId = model.RegionSolicitanteId
                };

                proceso.Organizacion = new Organizacion()
                {
                    OrganizacionId = model.OrganizacionId,
                    TipoOrganizacionId = model.TipoOrganizacionId,
                    EstadoId = (int)Infrastructure.Enum.Estado.EnConstitucion,
                    SituacionId = (int)Infrastructure.Enum.Situacion.Inactiva,
                    RubroId = model.RubroId,
                    SubRubroId = model.SubRubroId,
                    RegionId = model.RegionId,
                    ComunaId = model.ComunaId,
                    CiudadId = model.CiudadId,
                    NumeroRegistro = string.Empty,
                    RUT = !string.IsNullOrWhiteSpace(model.RUT) ? model.RUT.Replace(".", string.Empty).Replace("-", string.Empty) : string.Empty,
                    RazonSocial = model.RazonSocial.ToUpperNull(),
                    Sigla = model.Sigla.ToUpperNull(),
                    Direccion = model.Direccion.ToUpperNull(),
                    Fono = model.Fono.ToUpperNull(),
                    Fax = model.Fax.ToUpperNull(),
                    Email = model.Email.ToUpperNull(),
                    URL = model.URL.ToUpperNull(),
                    NumeroSociosConstituyentes = model.NumeroSociosConstituyentes,
                    NumeroSocios = model.NumeroSocios,
                    NumeroSociosHombres = model.NumeroSociosHombres,
                    NumeroSociosMujeres = model.NumeroSociosMujeres,
                    MinistroDeFe = model.MinistroDeFe.ToUpperNull(),
                    EsGeneroFemenino = false,
                    CiudadAsamblea = model.CiudadAsamblea.ToUpperNull(),
                    NombreContacto = model.NombreContacto.ToUpperNull(),
                    DireccionContacto = model.DireccionContacto.ToUpperNull(),
                    TelefonoContacto = model.TelefonoContacto.ToUpperNull(),
                    EmailContacto = model.EmailContacto.ToUpperNull(),
                    FechaCelebracion = model.FechaCelebracion,
                    FechaPublicacionDiarioOficial = model.FechaPublicacionDiarioOficial,
                    FechaActualizacion = model.FechaActualizacion,
                    EsImportanciaEconomica = false,
                    FechaVigente = model.FechaVigente,
                    FechaDisolucion = model.FechaDisolucion,
                    FechaConstitucion = model.FechaConstitucion,
                    FechaCancelacion = model.FechaCancelacion,
                    FechaInexistencia = model.FechaInexistencia,
                    FechaAsignacionRol = model.FechaAsignacionRol
                };

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
                        TipoPrivacidadId = (int)DAES.Infrastructure.Enum.TipoPrivacidad.Privado
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