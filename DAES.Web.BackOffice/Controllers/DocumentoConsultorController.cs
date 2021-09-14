using DAES.Infrastructure.SistemaIntegrado;
using DAES.Model.SistemaIntegrado;
using DAES.Web.BackOffice.Helper;
using System;
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
    public class DocumentoConsultorController : Controller
    {
        public class DocumentoSinContenidoLocal
        {
            [Display(Name = "Id")]
            public int DocumentoId { get; set; }

            [Display(Name = "Razón social")]
            public string RazonSocial { get; set; }

            [Display(Name = "N° registro")]
            public string Registro { get; set; }

            [Display(Name = "Proceso")]
            public string Proceso { get; set; }

            [Display(Name = "Tipo")]
            public string Tipo { get; set; }

            [Display(Name = "Fecha creación")]
            public DateTime? FechaCreacion { get; set; }

            [Display(Name = "Periodo")]
            public string Periodo { get; set; }
            [Display(Name = "Nombre archivo")]
            public string FileName { get; set; }
            public bool HasContent { get; set; }
            [Display(Name = "URL")]
            public string Url { get; set; }
        }
        public class ModelView
        {

            public ModelView()
            {
                Documentos = new List<DocumentoSinContenidoLocal>();
            }

            [Display(Name = "Texto de búsqueda")]
            public string Filter { get; set; }

            [Display(Name = "Tipo de documento")]
            public int? TipoDocumentoId { get; set; }

            public List<DocumentoSinContenidoLocal> Documentos { get; set; }
            public bool IsFirst { get; set; } = true;
        }

        private SistemaIntegradoContext db = new SistemaIntegradoContext();

        public ActionResult Index(ModelView model)
        {

            IQueryable<DAES.Model.SistemaIntegrado.DocumentoSinContenido> query = db.DocumentoSinContenido.Where(q => q.Activo);

            if (!string.IsNullOrEmpty(model.Filter) || model.TipoDocumentoId != null)
            {
                if (!string.IsNullOrEmpty(model.Filter))
                    query = query.Where(q => q.Autor.Contains(model.Filter)
                        || q.Autor.Contains(model.Filter)
                        || q.Descripcion.Contains(model.Filter)
                        || q.FileName.Contains(model.Filter)
                        || q.Organizacion.NumeroRegistro.Contains(model.Filter)
                        || q.Organizacion.RazonSocial.Contains(model.Filter));

                if (model.TipoDocumentoId != null)
                    query = query.Where(q => q.TipoDocumentoId == model.TipoDocumentoId);

                model.Documentos = query
                    .Select(q => new DocumentoSinContenidoLocal
                    {
                        DocumentoId = q.DocumentoId,
                        Registro = q.Organizacion.NumeroRegistro,
                        RazonSocial = q.Organizacion.RazonSocial,
                        Proceso = q.Proceso.DefinicionProceso.Nombre,
                        Tipo = q.TipoDocumento.Nombre,
                        FechaCreacion = q.FechaCreacion,
                        Periodo = q.Periodo,
                        FileName = q.FileName,
                        HasContent = q.HasContent,
                        Url = q.Url
                    }).ToList();
            }

            ViewBag.TipoDocumentoId = new SelectList(db.TipoDocumento.OrderBy(q => q.Nombre), "TipoDocumentoid", "Nombre", model.TipoDocumentoId);

            return View(model);
        }

        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var documento = db.Documento.Find(id);
            if (documento == null)
            {
                return HttpNotFound();
            }

            return View(documento);
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