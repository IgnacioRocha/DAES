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
    public class Articulo90IncisoPrimeroController : Controller
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
            Global.CurrentClaveUnica.ClaveUnicaRequestAutorization.controller = "Articulo90IncisoPrimero";
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
            //    numero = 44444444,
            //    DV = "4",
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
            query = query.Where(q => q.TipoOrganizacionId == (int)Infrastructure.Enum.TipoOrganizacion.Cooperativa);
            query = query.Where(q => q.EsImportanciaEconomica);
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

            ViewBag.Periodo = new SelectList(_db.Periodo.Where(q => q.Tipo == "Articulo91").OrderByDescending(q => q.PeriodoId), "Descripcion", "Descripcion");
            ViewBag.RegionSolicitanteId = new SelectList(_db.Region.OrderBy(q => q.Nombre), "RegionId", "Nombre");

            return View(new Model.DTO.DTOArticulo90IncisoPrimero()
            {
                RUTSolicitante = string.Concat(Global.CurrentClaveUnica.ClaveUnicaUser.RolUnico.numero, Global.CurrentClaveUnica.ClaveUnicaUser.RolUnico.DV),
                NombresSolicitante = string.Join(" ", Global.CurrentClaveUnica.ClaveUnicaUser.name.nombres).ToUpperNull(),
                ApellidosSolicitante = string.Join(" ", Global.CurrentClaveUnica.ClaveUnicaUser.name.apellidos).ToUpperNull(),
                OrganizacionId = model.OrganizacionId
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
        public ActionResult Create(Model.DTO.DTOArticulo90IncisoPrimero model)
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
                    DefinicionProcesoId = (int)Infrastructure.Enum.DefinicionProceso.Articulo90IncisoPrimero,
                    Observacion = model.Observacion,
                    OrganizacionId = model.OrganizacionId
                };

                proceso.Solicitante = new Solicitante()
                {
                    Rut = model.RUTSolicitante,
                    Nombres = model.NombresSolicitante,
                    Apellidos = model.ApellidosSolicitante,
                    Email = model.EmailSolicitante,
                    Fono = model.FonoSolicitante,
                    RegionId = model.RegionSolicitanteId,
                };

                SetFile(model.BalanceGeneralClasificado, (int)Infrastructure.Enum.TipoDocumento.C901_BalanceGeneralClasificado);
                SetFile(model.EstadoDeResultados, (int)Infrastructure.Enum.TipoDocumento.C901_EstadoDeResultados);
                SetFile(model.BalanceOchoColumnas, (int)Infrastructure.Enum.TipoDocumento.C901_BalanceOchoColumnas);
                SetFile(model.InformdeAuditoria, (int)Infrastructure.Enum.TipoDocumento.C901_InformdeAuditoria);
                SetFile(model.EstadoDeFlujoDeEfectivo, (int)Infrastructure.Enum.TipoDocumento.C901_EstadoDeFlujoDeEfectivo);
                SetFile(model.NotasExplicativasDeLosEstadosFinancieros, (int)Infrastructure.Enum.TipoDocumento.C901_NotasExplicativasDeLosEstadosFinancieros);
                SetFile(model.CertificadoInscripcionAuditoria, (int)Infrastructure.Enum.TipoDocumento.C901_CertificadoInscripcionAuditoria);

                foreach (var item in documentos)
                {
                    item.Autor = model.EmailSolicitante;
                    item.Organizacion = proceso.Organizacion;
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

            ViewBag.RegionSolicitanteId = new SelectList(_db.Region.OrderBy(q => q.Nombre), "RegionId", "Nombre", model.RegionSolicitanteId);

            return View(model);
        }
    }
}