using DAES.Infrastructure;
using DAES.Infrastructure.SistemaIntegrado;
using DAES.Model.SistemaIntegrado;
using DAES.Web.FrontOffice.Helper;
using System;
using System.Linq;
using System.Web.Mvc;

namespace DAES.Web.FrontOffice.Controllers
{

    [Audit]
    public class CertificadoController : Controller
    {

        private SistemaIntegradoContext db = new SistemaIntegradoContext();
        private BLL.Custom _custom = new BLL.Custom();

        public JsonResult AutoComplete(string term, int TipoDocumentoId)
        {

            IQueryable<Organizacion> query = db.Organizacion;

            //excluir estado en proceso de constitucion, inexistentes y rol recien creado
            query = query.Where(q => q.EstadoId != (int)Infrastructure.Enum.Estado.EnConstitucion);
            query = query.Where(q => q.EstadoId != (int)Infrastructure.Enum.Estado.Inexistente);
            query = query.Where(q => q.EstadoId != (int)Infrastructure.Enum.Estado.RolAsignado);

            //en el caso de vigencia o vigencia directorio, solo vigentes
            if (TipoDocumentoId == (int)Infrastructure.Enum.TipoDocumento.Vigencia || TipoDocumentoId == (int)Infrastructure.Enum.TipoDocumento.VigenciaDirectorio)
            {
                query = query.Where(q => q.EstadoId == (int)Infrastructure.Enum.Estado.Vigente);
            }

            //en el caso de 8 transitorio, solo cooperativas vigentes
            if (TipoDocumentoId == (int)Infrastructure.Enum.TipoDocumento.Articulo8voTransitorio)
            {
                query = query.Where(q => q.TipoOrganizacionId == (int)Infrastructure.Enum.TipoOrganizacion.Cooperativa && q.EstadoId == (int)Infrastructure.Enum.Estado.Vigente);
            }

            //Testing PDF Disoluciones, solo para mostrar datos a modo de prueba
            if(TipoDocumentoId == (int)Infrastructure.Enum.TipoDocumento.Disolucion)
            {
                query = query.Where(q => q.EstadoId == (int)Infrastructure.Enum.Estado.Vigente);
            }

            //en el caso de certificado disolucion, organizaciones disueltas
            if (TipoDocumentoId == (int)Infrastructure.Enum.TipoDocumento.Disolucion)
            {
                query = query.Where(q => q.EstadoId == (int)Infrastructure.Enum.Estado.Disuelta);
            }

            //en el caso de certificado estatutos, vigentes
            if (TipoDocumentoId == (int)Infrastructure.Enum.TipoDocumento.Estatutos)
            {
                query = query.Where(q => q.EstadoId == (int)Infrastructure.Enum.Estado.Vigente);
            }

            //filtrar por texto de busqueda
            if (!string.IsNullOrEmpty(term))
            {
                query = query.Where(q => q.NumeroRegistro.Contains(term) || q.RazonSocial.Contains(term));
            }

            var result = query
                .Select(c => new { id = c.OrganizacionId, value = c.TipoOrganizacion.Nombre + " " + c.NumeroRegistro + " - " + c.RazonSocial })
                .Take(25)
                .ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Emitir()
        {
            ViewBag.TipoDocumentoId = new SelectList(db.TipoDocumento.Where(q => q.EsExterno).OrderBy(q => q.Nombre).AsEnumerable().Select(q => new { q.TipoDocumentoId, Nombre = string.Format("{0} ({1})", q.Nombre, q.GeneracionManual ? "Solicitar emisión" : "Emisión inmediata") }), "TipoDocumentoId", "Nombre");
            ViewBag.RegionId = new SelectList(db.Region, "RegionId", "Nombre");

            return View();
        }

        [HttpPost]
        public ActionResult Emitir(Model.DTO.DTOSolicitudCertificado model)
        {
            if (!model.Rut.IsRut())
            {
                ModelState.AddModelError(string.Empty, "El rut ingresado no es válido.");
            }

            var organizacion = db.Organizacion.FirstOrDefault(q => q.OrganizacionId == model.OrganizacionId);
            if (organizacion == null)
            {
                ModelState.AddModelError(string.Empty, "La organización no fue encontrada.");
            }

            var tipoDocumento = db.TipoDocumento.FirstOrDefault(q => q.TipoDocumentoId == model.TipoDocumentoId);
            if (tipoDocumento == null)
            {
                ModelState.AddModelError(string.Empty, "No se encontró el tipo de documento.");
            }

            if (organizacion != null && tipoDocumento != null && !(bool)tipoDocumento.GeneracionManual)
            {
                var configuracionCertificado = db.ConfiguracionCertificado.FirstOrDefault(q => q.TipoDocumentoId == model.TipoDocumentoId && q.TipoOrganizacionId == organizacion.TipoOrganizacionId);
                if (configuracionCertificado == null)
                {
                    ModelState.AddModelError(string.Empty, "El tipo de certificado no está disponible para esta organización.");
                }
            }

            if (ModelState.IsValid)
            {

                try
                {

                    var proceso = new Proceso()
                    {
                        DefinicionProcesoId = tipoDocumento.GeneracionManual ? (int)Infrastructure.Enum.DefinicionProceso.SolicitudCertificadoManual : (int)Infrastructure.Enum.DefinicionProceso.SolicitudCertificadoAutomatico,
                        OrganizacionId = organizacion.OrganizacionId
                    };

                    proceso.Solicitante = new Solicitante()
                    {
                        Nombres = !string.IsNullOrWhiteSpace(model.Nombres) ? model.Nombres.ToUpper() : string.Empty,
                        Apellidos = !string.IsNullOrWhiteSpace(model.Apellidos) ? model.Apellidos.ToUpper() : string.Empty,
                        Email = !string.IsNullOrWhiteSpace(model.Email) ? model.Email.ToUpper() : string.Empty,
                        Rut = model.Rut,
                        Fono = model.Fono,
                        RegionId = model.RegionId
                    };

                    proceso.Documentos.Add(new Documento()
                    {
                        TipoDocumentoId = model.TipoDocumentoId,
                        TipoPrivacidadId = (int)DAES.Infrastructure.Enum.TipoPrivacidad.Publico
                    });

                    var p = _custom.ProcesoStart(proceso);

                    TempData["Success"] = string.Format("Operación terminada correctamente. El certificado fue enviado a {0}.", proceso.Solicitante.Email);

                    return RedirectToAction("Finish");
                }
                catch (Exception ex)
                {
                    if (organizacion.Disolucions.Count() == 0)
                    {
                        return View("_ErrorDisolucion", ex);
                    }
                    else { return View("_Error", ex); }
                }
            }

            ViewBag.RegionId = new SelectList(db.Region, "RegionId", "Nombre", model.RegionId);
            ViewBag.TipoDocumentoId = new SelectList(db.TipoDocumento.OrderBy(q => q.Nombre).AsEnumerable().Select(q => new { q.TipoDocumentoId, Nombre = string.Format("{0} ({1})", q.Nombre, q.GeneracionManual ? "Manual" : "Emisión inmediata") }), "TipoDocumentoId", "Nombre", model.TipoDocumentoId);

            return View(model);
        }

        public ActionResult Finish()
        {
            return View();
        }
    }
}