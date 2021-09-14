using DAES.Infrastructure.SistemaIntegrado;
using DAES.Model.SistemaIntegrado;
using DAES.Web.BackOffice.Helper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DAES.Web.BackOffice.Controllers
{

    [Audit]
    [Authorize]
    public class DocumentoController : Controller
    {
        private SistemaIntegradoContext db = new SistemaIntegradoContext();

        public class DTODocumento
        {

            public DTODocumento()
            {
            }

            public int DocumentoId { get; set; }
            public int EstadoId { get; set; }
            public int? WorkflowId { get; set; }
            public string Workflow { get; set; }
            public int? ProcesoId { get; set; }
            public string Proceso { get; set; }
            public string NumeroRegistro { get; set; }
            public string Correlativo { get; set; }
            public string Descripcion { get; set; }
            public string Recordatorio { get; set; }
            public bool Resuelto { get; set; }
            public bool HasContent { get; set; }
            public DateTime? FechaRecordatorio { get; set; }
            public string FileName { get; set; }
        }

        public ActionResult Index()
        {
            var model = new List<DTODocumento>();

            if (Helper.Helper.CurrentUser.PerfilId == (int)Infrastructure.Enum.Perfil.Admininstrador)
            {
                model.AddRange(db.DocumentoSinContenido.Where(q => q.Activo && !q.Resuelto && q.FechaRecordatorio.HasValue).Select(q => new DTODocumento
                {
                    DocumentoId = q.DocumentoId,
                    EstadoId = 1,
                    WorkflowId = q.WorkflowId,
                    Workflow = q.Workflow.DefinicionWorkflow.TipoWorkflow.Descripcion,
                    ProcesoId = q.ProcesoId,
                    Proceso = q.Proceso.DefinicionProceso.Descripcion,
                    NumeroRegistro = q.Organizacion.NumeroRegistro,
                    Correlativo = q.Workflow.Proceso.Correlativo,
                    Descripcion = q.Descripcion,
                    Recordatorio = q.Recordatorio,
                    Resuelto = q.Resuelto,
                    HasContent = q.HasContent,
                    FechaRecordatorio = q.FechaRecordatorio,
                    FileName = q.FileName
                }).ToList());
            }
            else
            {
                model.AddRange(db.DocumentoSinContenido.Where(q => q.Activo && !q.Resuelto && q.FechaRecordatorio.HasValue && q.Workflow.UserId == Helper.Helper.CurrentUser.Id).Select(q => new DTODocumento
                {
                    DocumentoId = q.DocumentoId,
                    EstadoId = 1,
                    WorkflowId = q.WorkflowId,
                    Workflow = q.Workflow.DefinicionWorkflow.TipoWorkflow.Descripcion,
                    ProcesoId = q.ProcesoId,
                    Proceso = q.Proceso.DefinicionProceso.Descripcion,
                    NumeroRegistro = q.Organizacion.NumeroRegistro,
                    Correlativo = q.Workflow.Proceso.Correlativo,
                    Descripcion = q.Descripcion,
                    Recordatorio = q.Recordatorio,
                    Resuelto = q.Resuelto,
                    HasContent = q.HasContent,
                    FechaRecordatorio = q.FechaRecordatorio,
                    FileName = q.FileName
                }).ToList());
            }

            return View(model);
        }

        public FileResult Download(int id)
        {
            var documento = db.Documento.Find(id);
            return File(documento.Content, System.Net.Mime.MediaTypeNames.Application.Octet, documento.FileName);
        }

        public ActionResult Edit(int? id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Documento documento)
        {

            if (ModelState.IsValid)
            {

                documento.Resuelto = true;
                documento.FechaResolucion = DateTime.Now;

                db.Entry(documento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Inbox");
            }
            return View(documento);
        }

        public async Task<ActionResult> ShowPDF(int id)
        {
            var model = await db.Documento.FindAsync(id);
            return File(model.Content, "application/pdf");
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