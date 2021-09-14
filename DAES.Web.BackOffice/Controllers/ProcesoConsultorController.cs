using DAES.Infrastructure.SistemaIntegrado;
using DAES.Model.SistemaIntegrado;
using DAES.Web.BackOffice.Helper;
using OfficeOpenXml;
using ServiceStack.OrmLite;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace DAES.Web.BackOffice.Controllers
{

    [Audit]
    [Authorize]
    public class ProcesoConsultorController : Controller
    {
        private SistemaIntegradoContext db = new SistemaIntegradoContext();

        public ActionResult Index()
        {
            var model = new Model.DTO.DTOConsultaProceso();
            model.Filter = string.Empty;
            model.DefinicionProcesos = db.DefinicionProceso.OrderBy(q => q.Nombre).Select(q => new DAES.Model.DTO.DTOConsultaProceso.DTODefinicionProceso { selected = false, text = q.Nombre, value = q.DefinicionProcesoId }).ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(Model.DTO.DTOConsultaProceso model)
        {
            if (string.IsNullOrWhiteSpace(model.Filter) && !model.DefinicionProcesos.Any(q => q.selected))
            {
                return View(model);
            }

            IQueryable<Proceso> query = db.Proceso;
            if (!string.IsNullOrWhiteSpace(model.Filter))
            {
                query = query.Where(
                    q => q.Correlativo.Contains(model.Filter)
                        || q.ProcesoId.ToString().Contains(model.Filter)
                        || q.DefinicionProceso.Nombre.Contains(model.Filter)
                        || q.Organizacion.RazonSocial.Contains(model.Filter)
                        || q.Organizacion.NumeroRegistro.Contains(model.Filter)
                    );
            }

            if (model.MostrarSoloVigentes)
            {
                query = query.Where(q => q.Terminada == false);
            }

            var ids = model.DefinicionProcesos.Where(q => q.selected).Select(q => q.value).ToList();
            if (ids.Any())
            {
                query = query.Where(q => ids.Contains(q.DefinicionProcesoId));
            }

            model.Procesos = query.OrderByDescending(q => q.ProcesoId).ToList();

            return View(model);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proceso proceso = db.Proceso.Find(id);
            if (proceso == null)
            {
                return HttpNotFound();
            }
            return View(proceso);
        }

        public FileResult DownloadProcesos()
        {
            var db = new SistemaIntegradoContext();
            var file = string.Concat(Request.PhysicalApplicationPath, @"App_Data\PROCESOS.xlsx");
            var fileInfo = new FileInfo(file);
            var excelPackage = new ExcelPackage(fileInfo);

            var p = db.Proceso.AsNoTracking().Select(q => new
            {
                q.ProcesoId,
                DefinicionProceso = q.DefinicionProceso != null ? q.DefinicionProceso.Nombre : string.Empty,
                q.FechaCreacion,
                q.FechaVencimiento,
                q.FechaTermino,
                Solicitante = q.Solicitante != null ? q.Solicitante.Email : q.Creador,
                Terminada = q.Terminada ? "SI" : "NO",
                q.Observacion,
                Organizacion = q.Organizacion != null ? q.Organizacion.NumeroRegistro : string.Empty,
                RazonSocial = q.Organizacion != null ? q.Organizacion.RazonSocial : string.Empty,
                Tipo = q.Organizacion != null && q.Organizacion.TipoOrganizacion != null ? q.Organizacion.TipoOrganizacion.Nombre : string.Empty,
                q.Correlativo
            }).ToList();

            excelPackage.Workbook.Worksheets[1].Cells[2, 1].LoadFromCollection(p);

            return File(excelPackage.GetAsByteArray(), System.Net.Mime.MediaTypeNames.Application.Octet, "Procesos " + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xlsx");
        }
        public FileResult DownloadTareas()
        {
            var db = new SistemaIntegradoContext();
            var file = string.Concat(Request.PhysicalApplicationPath, @"App_Data\TAREAS.xlsx");
            var fileInfo = new FileInfo(file);
            var excelPackage = new ExcelPackage(fileInfo);

            var w = db.Workflow.Select(q => new
            {
                q.ProcesoId,
                tipoproceso = q.Proceso.DefinicionProceso != null ? q.Proceso.DefinicionProceso.Nombre : string.Empty,
                Organizacion = q.Proceso.Organizacion != null ? q.Proceso.Organizacion.NumeroRegistro : string.Empty,
                RazonSocial = q.Proceso.Organizacion != null ? q.Proceso.Organizacion.RazonSocial : string.Empty,
                q.WorkflowId,
                q.DefinicionWorkflow.TipoWorkflow.Descripcion,
                q.FechaCreacion,
                q.FechaTermino,
                q.User.Email,
                q.TipoAprobacion.Nombre,
                q.Observacion,
                terminada = q.Terminada ? "SI" : "NO"
            }).AsParallel().ToList();

            excelPackage.Workbook.Worksheets[1].Cells[2, 1].LoadFromCollection(w);

            return File(excelPackage.GetAsByteArray(), System.Net.Mime.MediaTypeNames.Application.Octet, "Tareas " + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xlsx");
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