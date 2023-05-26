using DAES.Infrastructure.SistemaIntegrado;
using DAES.Model.SistemaIntegrado;
using DAES.Web.BackOffice.Helper;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace DAES.Web.BackOffice.Controllers
{

    [Audit]
    [Authorize]
    public class DefinicionWorkflowController : Controller
    {

        private SistemaIntegradoContext db = new SistemaIntegradoContext();
        private BLL.Custom _custom = new BLL.Custom();

        public ActionResult Index()
        {
            var DefinicionWorkflow = db.DefinicionWorkflow.Include(d => d.DefinicionProceso).Include(d => d.Perfil).Include(d => d.TipoWorkflow);
            return View(DefinicionWorkflow.ToList());
        }

        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var DefinicionWorkflow = db.DefinicionWorkflow.Find(id);
            if (DefinicionWorkflow == null)
            {
                return HttpNotFound();
            }

            return View(DefinicionWorkflow);
        }

        public ActionResult Create(int DefinicionProcesoId)
        {
            ViewBag.DefinicionProcesoId = new SelectList(db.DefinicionProceso.OrderBy(q => q.Nombre), "DefinicionProcesoId", "Nombre", DefinicionProcesoId);
            ViewBag.TipoWorkflowId = new SelectList(db.TipoWorkflow.OrderBy(q => q.Nombre), "TipoWorkflowId", "Nombre");
            ViewBag.UserId = new SelectList(db.Users.Where(q => q.Habilitado).OrderBy(q => q.UserName).ToList(), "Id", "UserName");
            ViewBag.DefinicionWorkflowRechazoId = new SelectList(db.DefinicionWorkflow.Where(q => q.DefinicionProcesoId == DefinicionProcesoId).OrderBy(q => q.Secuencia).Select(q => new { Value = q.DefinicionWorkflowId.ToString(), Text = q.TipoWorkflow.Nombre }), "Value", "Text");
            ViewBag.DefinicionWorkflowDependeDeId = new SelectList(db.DefinicionWorkflow.Where(q => q.DefinicionProcesoId == DefinicionProcesoId).OrderBy(q => q.Secuencia).Select(q => new { Value = q.DefinicionWorkflowId.ToString(), Text = q.TipoWorkflow.Nombre }), "Value", "Text");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DefinicionWorkflow DefinicionWorkflow)
        {
            DefinicionWorkflow.Habilitado = true;

            if (ModelState.IsValid)
            {
                var lastdefinition = db.DefinicionWorkflow.Where(q => q.DefinicionProcesoId == DefinicionWorkflow.DefinicionProcesoId).OrderByDescending(q => q.Secuencia).FirstOrDefault();
                if (lastdefinition != null)
                {
                    DefinicionWorkflow.Secuencia = lastdefinition.Secuencia + 1;
                }
                else
                {
                    DefinicionWorkflow.Secuencia = 1;
                }

                db.DefinicionWorkflow.Add(DefinicionWorkflow);
                db.SaveChanges();
                TempData["Message"] = Properties.Settings.Default.Success;
                return RedirectToAction("Details", "DefinicionProceso", new { id = DefinicionWorkflow.DefinicionProcesoId });
            }

            ViewBag.DefinicionProcesoId = new SelectList(db.DefinicionProceso.OrderBy(q => q.Nombre), "DefinicionProcesoId", "Nombre", DefinicionWorkflow.DefinicionProcesoId);
            ViewBag.PerfilId = new SelectList(db.Perfil.OrderBy(q => q.Nombre), "PerfilId", "Nombre", DefinicionWorkflow.PerfilId);
            ViewBag.TipoWorkflowId = new SelectList(db.TipoWorkflow.OrderBy(q => q.Nombre), "TipoWorkflowId", "Nombre", DefinicionWorkflow.TipoWorkflow);
            ViewBag.UserId = new SelectList(db.Users.Where(q => q.Habilitado).OrderBy(q => q.UserName).ToList(), "Id", "UserName", DefinicionWorkflow.UserId);
            ViewBag.DefinicionWorkflowRechazoId = new SelectList(db.DefinicionWorkflow.Where(q => q.DefinicionProcesoId == DefinicionWorkflow.DefinicionProcesoId).AsEnumerable().Select(q => new { Text = string.Format("{0} - {1}", q.Secuencia, q.TipoWorkflow.Nombre), Value = q.DefinicionWorkflowId.ToString() }), "Value", "Text", DefinicionWorkflow.DefinicionWorkflowRechazoId);

            return View(DefinicionWorkflow);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DefinicionWorkflow DefinicionWorkflow = db.DefinicionWorkflow.Find(id);
            if (DefinicionWorkflow == null)
            {
                return HttpNotFound();
            }

            ViewBag.DefinicionProcesoId = new SelectList(db.DefinicionProceso.OrderBy(q => q.Nombre), "DefinicionProcesoId", "Nombre", DefinicionWorkflow.DefinicionProcesoId);
            ViewBag.TipoWorkflowId = new SelectList(db.TipoWorkflow.OrderBy(q => q.Nombre), "TipoWorkflowId", "Nombre", DefinicionWorkflow.TipoWorkflowId);
            ViewBag.UserId = new SelectList(db.Users.Where(q => q.Habilitado).OrderBy(q => q.UserName).ToList(), "Id", "UserName", DefinicionWorkflow.UserId);
            ViewBag.DefinicionWorkflowRechazoId = new SelectList(db.DefinicionWorkflow.Where(q => q.DefinicionProcesoId == DefinicionWorkflow.DefinicionProcesoId).OrderBy(q => q.Secuencia).AsEnumerable().Select(q => new { Value = q.DefinicionWorkflowId.ToString(), Text = q.TipoWorkflow.Nombre }), "Value", "Text", DefinicionWorkflow.DefinicionWorkflowRechazoId);
            ViewBag.DefinicionWorkflowDependeDeId = new SelectList(db.DefinicionWorkflow.Where(q => q.DefinicionProcesoId == DefinicionWorkflow.DefinicionProcesoId).OrderBy(q => q.Secuencia).AsEnumerable().Select(q => new { Value = q.DefinicionWorkflowId.ToString(), Text = q.TipoWorkflow.Nombre }), "Value", "Text", DefinicionWorkflow.DefinicionWorkflowDependeDeId);

            return View(DefinicionWorkflow);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DefinicionWorkflow DefinicionWorkflow)
        {
            if (ModelState.IsValid)
            {
                db.Entry(DefinicionWorkflow).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Message"] = Properties.Settings.Default.Success;
                return RedirectToAction("Details", "DefinicionProceso", new { id = DefinicionWorkflow.DefinicionProcesoId });
            }
            ViewBag.DefinicionProcesoId = new SelectList(db.DefinicionProceso.OrderBy(q => q.Nombre), "DefinicionProcesoId", "Nombre", DefinicionWorkflow.DefinicionProcesoId);
            ViewBag.PerfilId = new SelectList(db.Perfil.OrderBy(q => q.Nombre), "PerfilId", "Nombre", DefinicionWorkflow.PerfilId);
            ViewBag.TipoWorkflowId = new SelectList(db.TipoWorkflow.OrderBy(q => q.Nombre), "TipoWorkflowId", "Nombre", DefinicionWorkflow.TipoWorkflowId);
            ViewBag.UserId = new SelectList(db.Users.Where(q => q.Habilitado).OrderBy(q => q.UserName).ToList(), "Id", "UserName", DefinicionWorkflow.UserId);
            ViewBag.DefinicionWorkflowRechazoId = new SelectList(db.DefinicionWorkflow.Where(q => q.DefinicionProcesoId == DefinicionWorkflow.DefinicionProcesoId).AsEnumerable().Select(q => new { Text = string.Format("{0} - {1}", q.Secuencia, q.TipoWorkflow.Nombre), Value = q.DefinicionWorkflowId.ToString() }), "Value", "Text", DefinicionWorkflow.DefinicionWorkflowRechazoId);
            ViewBag.DefinicionWorkflowDependeDeId = new SelectList(db.DefinicionWorkflow.Where(q => q.DefinicionProcesoId == DefinicionWorkflow.DefinicionProcesoId).AsEnumerable().Select(q => new { Text = string.Format("{0} - {1}", q.Secuencia, q.TipoWorkflow.Nombre), Value = q.DefinicionWorkflowId.ToString() }), "Value", "Text", DefinicionWorkflow.DefinicionWorkflowDependeDeId);

            return View(DefinicionWorkflow);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DefinicionWorkflow DefinicionWorkflow = db.DefinicionWorkflow.Find(id);
            if (DefinicionWorkflow == null)
            {
                return HttpNotFound();
            }
            return View(DefinicionWorkflow);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var result = _custom.DefinicionWorkflowDeleteValidate(id);
            foreach (var item in result)
            {
                ModelState.AddModelError(string.Empty, item);
            }

            DefinicionWorkflow DefinicionWorkflow = db.DefinicionWorkflow.Find(id);
            var DefinicionProcesoId = DefinicionWorkflow.DefinicionProcesoId;

            if (ModelState.IsValid)
            {
                _custom.DefinicionWorkflowDelete(id);
                TempData["Message"] = Properties.Settings.Default.Success;
                return RedirectToAction("Details", "DefinicionProceso", new { id = DefinicionProcesoId });
            }

            return View(DefinicionWorkflow);
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