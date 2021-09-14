using DAES.Infrastructure.GestionDocumental;
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
    public class ProcesoController : Controller
    {
        private GestionDocumentalContext dbgd = new GestionDocumentalContext();
        private SistemaIntegradoContext db = new SistemaIntegradoContext();
        private BLL.Custom _custom = new BLL.Custom();

        public JsonResult GetOrganizacion(string term)
        {
            IQueryable<Organizacion> query = db.Organizacion;
            if (!string.IsNullOrEmpty(term))
            {
                query = query.Where(q => q.NumeroRegistro.Contains(term) || q.RazonSocial.Contains(term));
            }

            var result = query.Select(c => new { id = c.OrganizacionId, value = c.TipoOrganizacion.Nombre + " " + c.NumeroRegistro + " - " + c.RazonSocial }).Take(25).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            var model = new Model.DTO.DTOConsultaProceso();
            model.Filter = string.Empty;
            model.DefinicionProcesos = db.DefinicionProceso.Where(q => q.Habilitado).OrderBy(q => q.Nombre).Select(q => new Model.DTO.DTOConsultaProceso.DTODefinicionProceso { selected = false, text = q.Nombre, value = q.DefinicionProcesoId }).ToList();

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

            ViewBag.PerfilId = Helper.Helper.CurrentUser.PerfilId;

            return View(proceso);
        }

        public ActionResult Admin(int? id)
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

        public ActionResult Documento()
        {
            Response.AddHeader("Refresh", "60");

            ViewBag.Ids = db.Proceso.Where(q => !q.Terminada).Select(q => q.DocumentoId).ToList();

            var model =
                dbgd.Documento.Where(q => q.Activo && q.ProcesoDocumento.Any(i => i.Activo && i.Pdo_IdUnidadEntidadDestino == 55)).Distinct().OrderByDescending(q => q.Id).Take(1000).Select(
                q => new DAES.Model.DTO.DTODocumentoGD()
                {
                    Id = q.Id,
                    Doc_Correlativo = q.Doc_Correlativo,
                    Doc_FechaCreacion = q.Doc_FechaCreacion,
                    Doc_Descripcion = q.Doc_Descripcion,
                    Tdo_Nombre = q.TipoDocumento.Tdo_Nombre,
                    Doc_Asunto = q.Doc_Asunto,
                    Doc_Referencia = q.Doc_Referencia
                }).ToList();

            return View(model);
        }

        public ActionResult DocumentoCreate(int? id)
        {

            ViewBag.DefinicionProcesoId = new SelectList(db.DefinicionProceso.Where(q => q.Habilitado).OrderBy(q => q.Nombre), "DefinicionProcesoId", "Nombre");
            ViewBag.UserId = new SelectList(db.Users.Where(q => q.Habilitado).OrderBy(q => q.UserName).ToList(), "Id", "UserName");

            var model = new DAES.Model.DTO.DTOProceso();
            model.Documento = dbgd.Documento.Find(id);
            model.DocumentoId = model.Documento.Id;
            model.Observacion = model.Documento.Doc_Asunto;
            model.Adjuntos = dbgd.Adjunto.Where(q => q.IdRegistro == model.Documento.Id).ToList();
            model.ProcesoDocumentos = dbgd.ProcesoDocumento.Where(q => q.Documento_Id == model.Documento.Id).ToList();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DocumentoCreate(Model.DTO.DTOProceso model)
        {

            if (model.DefinicionProcesoId != (int)Infrastructure.Enum.DefinicionProceso.ConstitucionOP)
            {
                if (!db.Organizacion.Any(q => q.OrganizacionId == model.OrganizacionId))
                {
                    ModelState.AddModelError(string.Empty, "La organización no fue encontrada.");
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var proceso = new Proceso()
                    {
                        DefinicionProcesoId = model.DefinicionProcesoId,
                        Observacion = model.Observacion,
                        UserId = model.UserId,
                        DocumentoId = model.DocumentoId,
                    };

                    if (model.DefinicionProcesoId != (int)Infrastructure.Enum.DefinicionProceso.ConstitucionOP)
                    {

                        var organizacion = db.Organizacion.FirstOrDefault(q => q.OrganizacionId == model.OrganizacionId);
                        proceso.Organizacion = new Organizacion()
                        {
                            OrganizacionId = organizacion.OrganizacionId,
                            TipoOrganizacionId = organizacion.TipoOrganizacionId
                        };

                        proceso.OrganizacionId = model.OrganizacionId.Value;
                    }

                    proceso.Solicitante = new Solicitante()
                    {
                        Nombres = Helper.Helper.CurrentUser.UserName,
                        Email = Helper.Helper.CurrentUser.Email
                    };

                    var p = _custom.ProcesoStart(proceso);

                    TempData["Message"] = Properties.Settings.Default.Success;
                    return RedirectToAction("Details", "ProcesoConsultor", new { id = p.ProcesoId });
                }
                catch (System.Exception ex)
                {
                    TempData["Error"] = ex.Message;
                }
            }

            ViewBag.DefinicionProcesoId = new SelectList(db.DefinicionProceso.Where(q => q.Habilitado).OrderBy(q => q.Nombre), "DefinicionProcesoId", "Nombre", model.DefinicionProcesoId);
            ViewBag.UserId = new SelectList(db.Users.Where(q => q.Habilitado).OrderBy(q => q.UserName).ToList(), "Id", "UserName", model.UserId);

            model.Documento = dbgd.Documento.Find(model.DocumentoId);
            model.Observacion = model.Documento.Doc_Asunto;
            model.Adjuntos = dbgd.Adjunto.Where(q => q.IdRegistro == model.Documento.Id).ToList();
            model.ProcesoDocumentos = dbgd.ProcesoDocumento.Where(q => q.Documento_Id == model.Documento.Id).ToList();

            return View(model);
        }

        public ActionResult Create()
        {
            ViewBag.DefinicionProcesoId = new SelectList(db.DefinicionProceso.Where(q => q.Habilitado).OrderBy(q => q.Nombre), "DefinicionProcesoId", "Nombre");
            ViewBag.UserId = new SelectList(db.Users.Where(q => q.Habilitado).OrderBy(q => q.UserName).ToList(), "Id", "UserName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Model.DTO.DTOProceso model)
        {

            if (model.DefinicionProcesoId != (int)Infrastructure.Enum.DefinicionProceso.ConstitucionOP)
            {
                if (!db.Organizacion.Any(q => q.OrganizacionId == model.OrganizacionId))
                {
                    ModelState.AddModelError(string.Empty, "La organización no fue encontrada.");
                }
            }

            try
            {

                var proceso = new Proceso()
                {
                    DefinicionProcesoId = model.DefinicionProcesoId,
                    Observacion = model.Observacion,
                    UserId = model.UserId,
                    DocumentoId = model.DocumentoId
                };

                if (model.DefinicionProcesoId != (int)Infrastructure.Enum.DefinicionProceso.ConstitucionWeb &&
                    model.DefinicionProcesoId != (int)Infrastructure.Enum.DefinicionProceso.ConstitucionOP)
                {

                    var organizacion = db.Organizacion.FirstOrDefault(q => q.OrganizacionId == model.OrganizacionId);

                    proceso.Organizacion = new Organizacion()
                    {
                        OrganizacionId = organizacion.OrganizacionId,
                        TipoOrganizacionId = organizacion.TipoOrganizacionId,
                    };

                    proceso.OrganizacionId = model.OrganizacionId.Value;
                }

                proceso.Solicitante = new Solicitante()
                {
                    Nombres = Helper.Helper.CurrentUser.UserName,
                    Email = Helper.Helper.CurrentUser.Email
                };

                var p = _custom.ProcesoStart(proceso);

                TempData["Message"] = Properties.Settings.Default.Success;
                return RedirectToAction("Details", "ProcesoConsultor", new { id = p.ProcesoId });
            }
            catch (System.Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            ViewBag.DefinicionProcesoId = new SelectList(db.DefinicionProceso.Where(q => q.Habilitado).OrderBy(q => q.Nombre), "DefinicionProcesoId", "Nombre", model.DefinicionProcesoId);
            ViewBag.UserId = new SelectList(db.Users.Where(q => q.Habilitado).OrderBy(q => q.UserName).ToList(), "Id", "UserName", model.UserId);

            return View(model);
        }

        public ActionResult Edit(int? id)
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
            ViewBag.OrganizacionId = new SelectList(db.Organizacion.OrderBy(q => q.NumeroRegistro), "OrganizacionId", "NumeroRegistro", proceso.OrganizacionId);
            ViewBag.DefinicionProcesoId = new SelectList(db.DefinicionProceso.Where(q => q.Habilitado).OrderBy(q => q.Nombre), "DefinicionProcesoId", "Nombre", proceso.DefinicionProcesoId);
            return View(proceso);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Proceso proceso)
        {
            if (ModelState.IsValid)
            {
                db.Entry(proceso).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Message"] = Properties.Settings.Default.Success;
                return RedirectToAction("Index");
            }
            ViewBag.OrganizacionId = new SelectList(db.Organizacion.OrderBy(q => q.NumeroRegistro), "OrganizacionId", "NumeroRegistro", proceso.OrganizacionId);
            ViewBag.DefinicionProcesoId = new SelectList(db.DefinicionProceso.Where(q => q.Habilitado).OrderBy(q => q.Nombre), "DefinicionProcesoId", "Nombre", proceso.DefinicionProcesoId);
            return View(proceso);
        }

        public ActionResult Delete(int? id)
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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _custom.ProcesoDelete(id);
            TempData["Message"] = Properties.Settings.Default.Success;
            return RedirectToAction("Index");
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