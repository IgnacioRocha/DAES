using DAES.Infrastructure.SistemaIntegrado;
using DAES.Model.SistemaIntegrado;
using DAES.Web.BackOffice.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace DAES.Web.BackOffice.Controllers
{

    [Audit]
    [Authorize]
    public class ModeloSupervisionController : Controller
    {

        public class ModelView
        {

            public ModelView()
            {
                Documentos = new List<DTODocumento>();
            }

            [Required(ErrorMessage = "Es necesario especificar este dato")]
            [Display(Name = "Texto de búsqueda")]
            public string Filter { get; set; }

            public List<DTODocumento> Documentos { get; set; }
        }

        public class DTODocumento
        {
            public int Id { get; set; }

            [Display(Name = "Proceso")]
            public int? ProcesoId { get; set; }

            [Display(Name = "Registro")]
            public string NumeroRegistro { get; set; }

            public DateTime? Fecha { get; set; }
            public string Descripcion { get; set; }
            public string Autor { get; set; }
            public bool HasFile { get; set; }
            public string FileName { get; set; }
            public string Url { get; set; }
            public string Controller { get; set; }

            [Display(Name = "Tipo documento")]
            public string TipoDocumento { get; set; }
        }

        private SistemaIntegradoContext db = new SistemaIntegradoContext();

        public ActionResult Index(ModelView model)
        {
            if (string.IsNullOrWhiteSpace(model.Filter))
            {
                return View(model);
            }

            IQueryable<Documento> query = db.Documento.AsNoTracking().Where(
                q =>
                q.Activo &&
                q.TipoDocumentoId >= 46 && (
                q.Autor.Contains(model.Filter)
                    || q.Autor.Contains(model.Filter)
                    || q.Descripcion.Contains(model.Filter)
                    || q.FileName.Contains(model.Filter)
                    || q.Organizacion.NumeroRegistro.Contains(model.Filter)
                    || q.Organizacion.RazonSocial.Contains(model.Filter))
                );

            var o = query.Select(q => new DTODocumento()
            {
                Id = q.DocumentoId,
                NumeroRegistro = q.Organizacion.NumeroRegistro,
                Autor = q.Autor,
                Fecha = q.FechaCreacion.HasValue ? q.FechaCreacion : null,
                Descripcion = q.Descripcion,
                HasFile = q.Content != null,
                FileName = q.FileName,
                Url = q.Url,
                Controller = "Documento",
                TipoDocumento = q.TipoDocumento.Nombre,
                ProcesoId = q.ProcesoId
            }).ToList();

            model.Documentos = o.ToList();
            return View(model);
        }

        //public ActionResult Details(int? id) {
        //    if (id == null) {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Documento documento = db.Documento.Find(id);
        //    if (documento == null) {
        //        return HttpNotFound();
        //    }

        //    return View(documento);
        //}

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