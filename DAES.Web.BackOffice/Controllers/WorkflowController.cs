using DAES.Infrastructure.SistemaIntegrado;
using DAES.Model.SistemaIntegrado;
using DAES.Web.BackOffice.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace DAES.Web.BackOffice.Controllers
{

    [Audit]
    [Authorize]
    public class WorkflowController : Controller
    {
        private BLL.Custom _custom = new BLL.Custom();
        private SistemaIntegradoContext db = new SistemaIntegradoContext();

        public class DTOWorkflow
        {
            public DTOWorkflow()
            {
            }

            public int EstadoId { get; set; }
            public int WorkflowId { get; set; }
            public string Workflow { get; set; }
            public int ProcesoId { get; set; }
            public string Proceso { get; set; }
            public string NumeroRegistro { get; set; }
            public string Correlativo { get; set; }
            public string Observacion { get; set; }
            public bool Terminada { get; set; }
            public DateTime FechaVencimiento { get; set; }
            public string Formulario { get; set; }
        }

        public JsonResult Update(int elementId, string targetId)
        {
            _custom.WorkflowMove(elementId, targetId.ToString());
            return Json("OK", JsonRequestBehavior.AllowGet);
        }


        public ActionResult Index()
        {
            Response.AddHeader("Refresh", "120");

            var model = new List<DTOWorkflow>();

            if (Helper.Helper.CurrentUser.PerfilId == (int)Infrastructure.Enum.Perfil.Admininstrador)
            {
                model.AddRange(db.Workflow.Where(q => !q.Terminada).Select(q => new DTOWorkflow
                {
                    Formulario = q.DefinicionWorkflow.TipoWorkflow.Formulario,
                    Terminada = q.Terminada,
                    FechaVencimiento = q.Proceso.FechaVencimiento,
                    EstadoId = 1,
                    WorkflowId = q.WorkflowId,
                    Workflow = q.DefinicionWorkflow.TipoWorkflow.Nombre,
                    ProcesoId = q.Proceso.ProcesoId,
                    Proceso = q.Proceso.DefinicionProceso.Nombre,
                    NumeroRegistro = q.Proceso.Organizacion.NumeroRegistro,
                    Correlativo = q.Proceso.Correlativo,
                    //Observacion = q.Proceso.Workflows.Any(i => i.Observacion != null) ? q.Proceso.Workflows.OrderByDescending(i => i.WorkflowId).FirstOrDefault(i => i.Observacion != null).Observacion : string.Empty
                    Observacion = q.Observacion
                }).ToList());
            }
            else
            {
                model.AddRange(db.Workflow.Where(q => !q.Terminada && q.UserId == Helper.Helper.CurrentUser.Id).Select(q => new DTOWorkflow
                {
                    Formulario = q.DefinicionWorkflow.TipoWorkflow.Formulario,
                    Terminada = q.Terminada,
                    FechaVencimiento = q.Proceso.FechaVencimiento,
                    EstadoId = 1,
                    WorkflowId = q.WorkflowId,
                    Workflow = q.DefinicionWorkflow.TipoWorkflow.Nombre,
                    ProcesoId = q.Proceso.ProcesoId,
                    Proceso = q.Proceso.DefinicionProceso.Nombre,
                    NumeroRegistro = q.Proceso.Organizacion.NumeroRegistro,
                    Correlativo = q.Proceso.Correlativo,
                    //Observacion = q.Proceso.Workflows.Any(i => i.Observacion != null) ? q.Proceso.Workflows.OrderByDescending(i => i.WorkflowId).FirstOrDefault(i => i.Observacion != null).Observacion : string.Empty
                    Observacion = q.Observacion
                }).ToList());
            }

            return View(model);
        }

        public ActionResult Manage()
        {
            return View(db.Users);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Workflow workflow = db.Workflow.Find(id);
            if (workflow == null)
            {
                return HttpNotFound();
            }
            return View(workflow);
        }

        public ActionResult Create(int ProcesoId)
        {
            var proceso = db.Proceso.FirstOrDefault(q => q.ProcesoId == ProcesoId);

            ViewBag.ProcesoId = new SelectList(db.Proceso, "ProcesoId", "ProcesoId", ProcesoId);
            ViewBag.DefinicionWorkflowId = new SelectList(db.DefinicionWorkflow.Where(q => q.DefinicionProcesoId == proceso.DefinicionProcesoId).OrderBy(q => q.Secuencia).AsEnumerable().Select(q => new { Text = string.Format("{0} - {1} - {2}", q.Secuencia, q.TipoWorkflow.Nombre, q.Perfil.Nombre), Value = q.DefinicionWorkflowId.ToString() }), "Value", "Text");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Workflow workflow)
        {
            workflow.FechaCreacion = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.Workflow.Add(workflow);
                db.SaveChanges();
                TempData["Message"] = Properties.Settings.Default.Success;
                return RedirectToAction("Details", "Proceso", new { id = workflow.ProcesoId });
            }

            var proceso = db.Proceso.FirstOrDefault(q => q.ProcesoId == workflow.ProcesoId);

            ViewBag.DefinicionWorkflowId = new SelectList(db.DefinicionWorkflow.Where(q => q.DefinicionProcesoId == proceso.DefinicionProcesoId).OrderBy(q => q.Secuencia).AsEnumerable().Select(q => new { Text = string.Format("{0} - {1} - {2}", q.Secuencia, q.TipoWorkflow.Nombre, q.Perfil.Nombre), Value = q.DefinicionWorkflowId.ToString() }), "Value", "Text", workflow.DefinicionWorkflowId);
            ViewBag.ProcesoId = new SelectList(db.Proceso, "ProcesoId", "ProcesoId", workflow.ProcesoId);

            return View(workflow);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Workflow workflow = db.Workflow.Find(id);
            if (workflow == null)
            {
                return HttpNotFound();
            }
            var proceso = db.Proceso.FirstOrDefault(q => q.ProcesoId == workflow.ProcesoId);

            ViewBag.TipoAprobacionId = new SelectList(db.TipoAprobacion, "TipoAprobacionId", "Nombre", workflow.TipoAprobacionId);
            ViewBag.DefinicionWorkflowId = new SelectList(db.DefinicionWorkflow.Where(q => q.DefinicionProcesoId == proceso.DefinicionProcesoId).OrderBy(q => q.Secuencia).Select(q => new { Value = q.DefinicionWorkflowId.ToString(), Text = q.TipoWorkflow.Nombre }), "Value", "Text", workflow.DefinicionWorkflowId);
            ViewBag.ProcesoId = new SelectList(db.Proceso, "ProcesoId", "ProcesoId", workflow.ProcesoId);
            ViewBag.UserId = new SelectList(db.Users.Where(q => q.Habilitado).OrderBy(q => q.Nombre).AsEnumerable().Select(q => new { Text = string.Format("{0} - {1}", q.Nombre, q.Perfil.Nombre), Value = q.Id }), "Value", "Text", workflow.UserId);

            return View(workflow);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Workflow workflow)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workflow).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Message"] = Properties.Settings.Default.Success;
                return RedirectToAction("Details", "Proceso", new { id = workflow.ProcesoId });
            }

            var proceso = db.Proceso.FirstOrDefault(q => q.ProcesoId == workflow.ProcesoId);

            ViewBag.TipoAprobacionId = new SelectList(db.TipoAprobacion, "TipoAprobacionId", "Nombre", workflow.TipoAprobacionId);
            ViewBag.DefinicionWorkflowId = new SelectList(db.DefinicionWorkflow.Where(q => q.DefinicionProcesoId == proceso.DefinicionProcesoId).OrderBy(q => q.Secuencia).Select(q => new { Value = q.DefinicionWorkflowId.ToString(), Text = q.TipoWorkflow.Nombre }), "Value", "Text", workflow.DefinicionWorkflowId);
            ViewBag.ProcesoId = new SelectList(db.Proceso, "ProcesoId", "ProcesoId", workflow.ProcesoId);
            ViewBag.UserId = new SelectList(db.Users.Where(q => q.Habilitado).OrderBy(q => q.Nombre).AsEnumerable().Select(q => new { Text = string.Format("{0} - {1}", q.Nombre, q.Perfil.Nombre), Value = q.Id }), "Value", "Text", workflow.UserId);

            return View(workflow);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Workflow workflow = db.Workflow.Find(id);
            if (workflow == null)
            {
                return HttpNotFound();
            }
            return View(workflow);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Workflow workflow = db.Workflow.Find(id);
            var ProcesoId = workflow.ProcesoId;
            _custom.WorkflowDelete(workflow.WorkflowId);
            TempData["Message"] = Properties.Settings.Default.Success;
            return RedirectToAction("Details", "Proceso", new { id = ProcesoId });
        }

        public ActionResult Stop(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Workflow workflow = db.Workflow.Find(id);
            if (workflow == null)
            {
                return HttpNotFound();
            }
            return View(workflow);
        }

        [HttpPost, ActionName("Stop")]
        [ValidateAntiForgeryToken]
        public ActionResult StopConfirmed(int id)
        {
            Workflow workflow = db.Workflow.Find(id);
            workflow.FechaTermino = DateTime.Now;
            workflow.Terminada = true;
            db.SaveChanges();
            TempData["Message"] = Properties.Settings.Default.Success;
            return RedirectToAction("Details", "Proceso", new { id = workflow.ProcesoId });
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