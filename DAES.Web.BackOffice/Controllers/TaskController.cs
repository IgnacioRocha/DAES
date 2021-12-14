using DAES.Infrastructure.SistemaIntegrado;
using DAES.Model.SistemaIntegrado;
using DAES.Web.BackOffice.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DAES.Web.BackOffice.Controllers
{

    public class TaskModel
    {

        public TaskModel()
        {
            Archivos = new List<HttpPostedFileBase>();
        }

        public FileModel Documento { get; set; }
        public Workflow Workflow { get; set; }
        public Organizacion Organizacion { get; set; }
        public ActualizacionOrganizacion ActualizacionOrganizacion { get; set; }
        public Articulo91 Articulo91 { get; set; }
        public Fiscalizacion Fiscalizacion { get; set; }
        public Hallazgo Hallazgo { get; set; }

        public int? OrganizacionId { get; set; }
        public int? TipoOrganizacionId { get; set; }
        public int? ProcesoId { get; set; }
        public int? WorkflowId { get; set; }

        [Display(Name = "Tipo documento")]
        public int TipoDocumentoId { get; set; }

        [Display(Name = "Tipo privacidad")]
        public int TipoPrivacidadId { get; set; }

        public List<Directorio> Directorios { get; set; }
        public List<ModificacionEstatuto> ModificacionEstatutos { get; set; }
        public List<Disolucion> Disolucions { get; set; }
        public List<DisolucionCooperativa> DisolucionCooperativas { get; set; }
        public List<DisolucionAsociacion> DisolucionAsociacions { get; set; }
        public List<Model.DTO.DTOUser> Users { get; set; }
        public List<Documento> Documentos { get; set; }
        public List<Fiscalizacion> Fiscalizacions { get; set; }
        public List<HttpPostedFileBase> Archivos { get; set; }
        public List<Proceso> Procesos { get; set; }
    }

    public class FileModel
    {

        public FileModel()
        {
        }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Archivo")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase File { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Descripción")]
        [DataType(DataType.MultilineText)]
        public string Descripcion { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha respuesta")]
        [DataType(DataType.Date)]
        public DateTime? FechaRespuesta { get; set; }

        [Display(Name = "Texto recordatorio")]
        [DataType(DataType.MultilineText)]
        public string Recordatorio { get; set; }

        [Display(Name = "Resuelto?")]
        public bool Resuelto { get; set; } = false;

        [Display(Name = "Tipo documento")]
        public int TipoDocumentoId { get; set; }

        [Display(Name = "Tipo privacidad")]
        public int TipoPrivacidadId { get; set; }
    }

    [Audit]
    [Authorize]
    public class TaskController : Controller
    {
        private BLL.Custom _custom = new BLL.Custom();
        private SistemaIntegradoContext db = new SistemaIntegradoContext();

        public JsonResult GetInstitucion(string term)
        {
            IQueryable<Organizacion> query = db.Organizacion;

            if (!string.IsNullOrEmpty(term))
            {
                query = query.Where(q => q.NumeroRegistro.Contains(term) || q.RazonSocial.Contains(term));
            }

            var result = query.Select(c => new { id = c.OrganizacionId, value = c.TipoOrganizacion.Nombre + " " + c.NumeroRegistro + " - " + c.RazonSocial }).Take(25).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AsignarResponsable(int WorkflowId)
        {
            var model = new TaskModel();
            model.Workflow = db.Workflow.FirstOrDefault(q => q.WorkflowId == WorkflowId);
            model.Users = db.Users.Where(q => q.Habilitado).OrderBy(q => q.UserName).Select(item => new DAES.Model.DTO.DTOUser() { Id = item.Id, Nombre = item.Nombre, UserName = item.UserName, Selected = false }).ToList();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AsignarResponsable(TaskModel model)
        {
            if (model.Users.All(q => !q.Selected))
            {
                ModelState.AddModelError(string.Empty, "Debe especificar al menos un funcionario para asignar la tarea.");
            }

            if (ModelState.IsValid)
            {
                _custom.WorkflowAssign(model.Workflow, model.Users);
                TempData["Message"] = Properties.Settings.Default.Success;
                return RedirectToAction("AsignarResponsable", new { model.Workflow.WorkflowId });
            }

            model.Workflow = db.Workflow.FirstOrDefault(q => q.WorkflowId == model.Workflow.WorkflowId);
            model.Users = db.Users.Where(q => q.Habilitado).OrderBy(q => q.UserName).Select(item => new DAES.Model.DTO.DTOUser() { Id = item.Id, Nombre = item.Nombre, UserName = item.UserName, Selected = false }).ToList();

            return View(model);
        }

        public ActionResult CrearDocumento(int WorkflowId)
        {

            ViewBag.TipoDocumentoId = new SelectList(db.TipoDocumento.OrderBy(q => q.Nombre), "TipoDocumentoId", "Nombre");
            ViewBag.TipoPrivacidadId = new SelectList(db.TipoPrivacidad.OrderBy(q => q.Nombre), "TipoPrivacidadId", "Nombre");

            var workflow = db.Workflow.FirstOrDefault(q => q.WorkflowId == WorkflowId);
            var model = new TaskModel();
            model.Workflow = workflow;
            model.Documentos = db.Documento.Where(q => q.Workflow.ProcesoId == model.Workflow.ProcesoId).ToList();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearDocumento(TaskModel model)
        {
            if (ModelState.IsValid)
            {

                var file = Request.Files[0];
                var target = new MemoryStream();
                file.InputStream.CopyTo(target);

                db.Documento.Add(new Documento()
                {
                    FechaCreacion = DateTime.Now,
                    Autor = User.Identity.Name,
                    Descripcion = model.Documento.Descripcion,
                    FileName = file.FileName,
                    Content = target.ToArray(),
                    FechaRecordatorio = model.Documento.FechaRespuesta,
                    Recordatorio = model.Documento.Recordatorio,
                    Resuelto = false,
                    TipoDocumentoId = model.TipoDocumentoId,
                    Firmado = false,
                    WorkflowId = model.Workflow.WorkflowId,
                    ProcesoId = model.Workflow.ProcesoId,
                    OrganizacionId = model.Workflow.Proceso.OrganizacionId,
                    TipoPrivacidadId = model.TipoPrivacidadId
                });

                db.SaveChanges();
                TempData["Message"] = Properties.Settings.Default.Success;
                return RedirectToAction("CrearDocumento", new { model.Workflow.WorkflowId });
            }

            ViewBag.TipoDocumentoId = new SelectList(db.TipoDocumento.OrderBy(q => q.Nombre), "TipoDocumentoId", "Nombre", model.Documento.TipoDocumentoId);
            ViewBag.TipoPrivacidadId = new SelectList(db.TipoPrivacidad.OrderBy(q => q.Nombre), "TipoPrivacidadId", "Nombre", model.Documento.TipoPrivacidadId);

            return View(model);
        }

        public ActionResult CrearDocumentoDelete(int DocumentoId, int WorkflowId)
        {
            var doc = db.Documento.FirstOrDefault(q => q.DocumentoId == DocumentoId);
            if (doc != null)
            {
                db.Documento.Remove(doc);
                db.SaveChanges();

                TempData["Message"] = Properties.Settings.Default.Success;
            }
            return RedirectToAction("CrearDocumento", new { WorkflowId });
        }

        public ActionResult ActualizarOrganizacion(int WorkflowId)
        {
            var model = new TaskModel();
            model.Workflow = db.Workflow.FirstOrDefault(q => q.WorkflowId == WorkflowId);
            model.Organizacion = db.Organizacion.Find(model.Workflow.Proceso.OrganizacionId);
            model.Directorios = db.Directorio.Where(q => q.OrganizacionId == model.Organizacion.OrganizacionId).ToList();
            model.ActualizacionOrganizacion = db.ActualizacionOrganizacion.FirstOrDefault(q => q.ProcesoId == model.Workflow.ProcesoId);

            ViewBag.CiudadId = new SelectList(db.Ciudad.OrderBy(q => q.Nombre), "CiudadId", "Nombre");
            ViewBag.ComunaId = new SelectList(db.Comuna.OrderBy(q => q.Nombre), "ComunaId", "Nombre");
            ViewBag.EstadoId = new SelectList(db.Estado.OrderBy(q => q.Nombre), "EstadoId", "Nombre");
            ViewBag.SituacionId = new SelectList(db.Situacion.OrderBy(q => q.Nombre), "SituacionId", "Nombre");
            ViewBag.RegionId = new SelectList(db.Region.OrderBy(q => q.Nombre), "RegionId", "Nombre");
            ViewBag.RubroId = new SelectList(db.Rubro.OrderBy(q => q.Nombre), "RubroId", "Nombre");
            ViewBag.SubRubroId = new SelectList(db.SubRubro.OrderBy(q => q.Nombre), "SubRubroId", "Nombre");
            ViewBag.TipoOrganizacionId = new SelectList(db.TipoOrganizacion.OrderBy(q => q.Nombre), "TipoOrganizacionId", "Nombre");
            ViewBag.CargoId = new SelectList(db.Cargo.OrderBy(q => q.Nombre), "CargoId", "Nombre");
            ViewBag.GeneroId = new SelectList(db.Genero.OrderBy(q => q.Nombre), "GeneroId", "Nombre");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ActualizarOrganizacion(TaskModel model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(model.Organizacion).State = EntityState.Modified;
                db.SaveChanges();
                _custom.DirectorioUpdate(model.Directorios);

                TempData["Message"] = Properties.Settings.Default.Success;
                return RedirectToAction("ActualizarOrganizacion", new { model.Workflow.WorkflowId });
            }

            ViewBag.CiudadId = new SelectList(db.Ciudad.OrderBy(q => q.Nombre), "CiudadId", "Nombre", model.Organizacion.CiudadId);
            ViewBag.ComunaId = new SelectList(db.Comuna.OrderBy(q => q.Nombre), "ComunaId", "Nombre", model.Organizacion.ComunaId);
            ViewBag.EstadoId = new SelectList(db.Estado.OrderBy(q => q.Nombre), "EstadoId", "Nombre", model.Organizacion.EstadoId);
            ViewBag.SituacionId = new SelectList(db.Situacion.OrderBy(q => q.Nombre), "SituacionId", "Nombre", model.Organizacion.SituacionId);
            ViewBag.RegionId = new SelectList(db.Region.OrderBy(q => q.Nombre), "RegionId", "Nombre", model.Organizacion.RegionId);
            ViewBag.RubroId = new SelectList(db.Rubro.OrderBy(q => q.Nombre), "RubroId", "Nombre", model.Organizacion.RubroId);
            ViewBag.SubRubroId = new SelectList(db.SubRubro.OrderBy(q => q.Nombre), "SubRubroId", "Nombre", model.Organizacion.SubRubroId);
            ViewBag.TipoOrganizacionId = new SelectList(db.TipoOrganizacion.OrderBy(q => q.Nombre), "TipoOrganizacionId", "Nombre", model.Organizacion.TipoOrganizacionId);
            ViewBag.CargoId = new SelectList(db.Cargo.OrderBy(q => q.Nombre), "CargoId", "Nombre");
            ViewBag.GeneroId = new SelectList(db.Genero.OrderBy(q => q.Nombre), "GeneroId", "Nombre");

            return View(model);
        }

        public ActionResult EditarOrganizacion(int WorkflowId)
        {

            var model = new TaskModel();
            model.Workflow = db.Workflow.FirstOrDefault(q => q.WorkflowId == WorkflowId);
            model.Organizacion = db.Organizacion.Find(model.Workflow.Proceso.OrganizacionId);
            model.Directorios = db.Directorio.Where(q => q.OrganizacionId == model.Organizacion.OrganizacionId).ToList();
            model.ActualizacionOrganizacion = model.Workflow.Proceso.ActualizacionOrganizacions.FirstOrDefault();
            model.ModificacionEstatutos = db.ModificacionEstatutos.Where(q => q.OrganizacionId == model.Organizacion.OrganizacionId).ToList();
            model.Disolucions = db.Disolucions.Where(q => q.OrganizacionId == model.Organizacion.OrganizacionId).ToList();

            ViewBag.CiudadId = new SelectList(db.Ciudad.OrderBy(q => q.Nombre), "CiudadId", "Nombre");
            ViewBag.ComunaId = new SelectList(db.Comuna.OrderBy(q => q.Nombre), "ComunaId", "Nombre");
            ViewBag.EstadoId = new SelectList(db.Estado.OrderBy(q => q.Nombre), "EstadoId", "Nombre");
            ViewBag.SituacionId = new SelectList(db.Situacion.OrderBy(q => q.Nombre), "SituacionId", "Nombre");
            ViewBag.RegionId = new SelectList(db.Region.OrderBy(q => q.Nombre), "RegionId", "Nombre");
            ViewBag.RubroId = new SelectList(db.Rubro.OrderBy(q => q.Nombre), "RubroId", "Nombre");
            ViewBag.SubRubroId = new SelectList(db.SubRubro.OrderBy(q => q.Nombre), "SubRubroId", "Nombre");
            ViewBag.TipoOrganizacionId = new SelectList(db.TipoOrganizacion.OrderBy(q => q.Nombre), "TipoOrganizacionId", "Nombre");
            ViewBag.CargoId = new SelectList(db.Cargo.OrderBy(q => q.Nombre), "CargoId", "Nombre");
            ViewBag.GeneroId = new SelectList(db.Genero.OrderBy(q => q.Nombre), "GeneroId", "Nombre");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarOrganizacion(TaskModel model, Disolucion disolucion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(model.Organizacion).State = EntityState.Modified;
                db.SaveChanges();
                _custom.DirectorioUpdate(model.Directorios);
                _custom.ModificacionUpdate(model.ModificacionEstatutos);
                /*_custom.DisolucionUpdate(model.Disolucions);*/
                /*_custom.DisolucionUpdate(model.Disolucions, disolucion);*/
                /*_custom.DisolucionAsociacionUpdate(model.Disolucions);*/

                TempData["Message"] = Properties.Settings.Default.Success;
                return RedirectToAction("EditarOrganizacion", new { model.Workflow.WorkflowId });
            }

            ViewBag.CiudadId = new SelectList(db.Ciudad.OrderBy(q => q.Nombre), "CiudadId", "Nombre", model.Organizacion.CiudadId);
            ViewBag.ComunaId = new SelectList(db.Comuna.OrderBy(q => q.Nombre), "ComunaId", "Nombre", model.Organizacion.ComunaId);
            ViewBag.EstadoId = new SelectList(db.Estado.OrderBy(q => q.Nombre), "EstadoId", "Nombre", model.Organizacion.EstadoId);
            ViewBag.SituacionId = new SelectList(db.Situacion.OrderBy(q => q.Nombre), "SituacionId", "Nombre", model.Organizacion.SituacionId);
            ViewBag.RegionId = new SelectList(db.Region.OrderBy(q => q.Nombre), "RegionId", "Nombre", model.Organizacion.RegionId);
            ViewBag.RubroId = new SelectList(db.Rubro.OrderBy(q => q.Nombre), "RubroId", "Nombre", model.Organizacion.RubroId);
            ViewBag.SubRubroId = new SelectList(db.SubRubro.OrderBy(q => q.Nombre), "SubRubroId", "Nombre", model.Organizacion.SubRubroId);
            ViewBag.TipoOrganizacionId = new SelectList(db.TipoOrganizacion.OrderBy(q => q.Nombre), "TipoOrganizacionId", "Nombre", model.Organizacion.TipoOrganizacionId);
            ViewBag.CargoId = new SelectList(db.Cargo.OrderBy(q => q.Nombre), "CargoId", "Nombre");
            ViewBag.GeneroId = new SelectList(db.Genero.OrderBy(q => q.Nombre), "GeneroId", "Nombre");

            return View(model);
        }



        public ActionResult DirectorioAdd(int WorkflowId, int OrganizacionId)
        {

            db.Directorio.Add(new Directorio() { OrganizacionId = OrganizacionId, NombreCompleto = "?", GeneroId = (int)DAES.Infrastructure.Enum.Genero.SinGenero, CargoId = 135 });
            db.SaveChanges();

            var model = new TaskModel();
            model.Workflow = db.Workflow.FirstOrDefault(q => q.WorkflowId == WorkflowId);
            model.Organizacion = db.Organizacion.Find(model.Workflow.Proceso.OrganizacionId);
            model.Directorios = db.Directorio.Where(q => q.OrganizacionId == model.Organizacion.OrganizacionId).ToList();
            model.ModificacionEstatutos = db.ModificacionEstatutos.Where(q => q.OrganizacionId == model.Organizacion.OrganizacionId).ToList();
            model.Disolucions = db.Disolucions.Where(q => q.OrganizacionId == model.Organizacion.OrganizacionId).ToList();

            ViewBag.CargoId = new SelectList(db.Cargo.OrderBy(q => q.Nombre), "CargoId", "Nombre");
            ViewBag.GeneroId = new SelectList(db.Genero.OrderBy(q => q.Nombre), "GeneroId", "Nombre");

            return PartialView("_DirectorioEdit", model);
        }

        public ActionResult DirectorioDelete(int WorkflowId, int DirectorioId)
        {
            var directorio = db.Directorio.FirstOrDefault(q => q.DirectorioId == DirectorioId);
            if (directorio != null)
            {
                db.Directorio.Remove(directorio);
                db.SaveChanges();
            }

            var model = new TaskModel();
            model.Workflow = db.Workflow.FirstOrDefault(q => q.WorkflowId == WorkflowId);
            model.Organizacion = db.Organizacion.Find(model.Workflow.Proceso.OrganizacionId);
            model.Directorios = db.Directorio.Where(q => q.OrganizacionId == model.Organizacion.OrganizacionId).ToList();
            model.ModificacionEstatutos = db.ModificacionEstatutos.Where(q => q.OrganizacionId == model.Organizacion.OrganizacionId).ToList();
            model.Disolucions = db.Disolucions.Where(q => q.OrganizacionId == model.Organizacion.OrganizacionId).ToList();

            ViewBag.CargoId = new SelectList(db.Cargo.OrderBy(q => q.Nombre), "CargoId", "Nombre");
            ViewBag.GeneroId = new SelectList(db.Genero.OrderBy(q => q.Nombre), "GeneroId", "Nombre");

            return PartialView("_DirectorioEdit", model);
        }

        public ActionResult ModificacionAdd(int WorkflowId, int OrganizacionId)
        {

            db.ModificacionEstatutos.Add(new ModificacionEstatuto() { OrganizacionId = OrganizacionId });
            db.SaveChanges();

            var model = new TaskModel();
            model.Workflow = db.Workflow.FirstOrDefault(q => q.WorkflowId == WorkflowId);
            model.Organizacion = db.Organizacion.Find(model.Workflow.Proceso.OrganizacionId);
            model.Directorios = db.Directorio.Where(q => q.OrganizacionId == model.Organizacion.OrganizacionId).ToList();
            model.ModificacionEstatutos = db.ModificacionEstatutos.Where(q => q.OrganizacionId == model.Organizacion.OrganizacionId).ToList();
            model.Directorios = db.Directorio.Where(q => q.OrganizacionId == model.Organizacion.OrganizacionId).ToList();
            model.ModificacionEstatutos = db.ModificacionEstatutos.Where(q => q.OrganizacionId == model.Organizacion.OrganizacionId).ToList();
            model.Disolucions = db.Disolucions.Where(q => q.OrganizacionId == model.Organizacion.OrganizacionId).ToList();

            return PartialView("_ModificacionEdit", model);
        }

        public ActionResult ModificacionDelete(int WorkflowId, int ModificacionEstatutoId)
        {
            var modificacionEstatuto = db.ModificacionEstatutos.FirstOrDefault(q => q.ModificacionEstatutoId == ModificacionEstatutoId);
            if (modificacionEstatuto != null)
            {
                db.ModificacionEstatutos.Remove(modificacionEstatuto);
                db.SaveChanges();
            }

            var model = new TaskModel();
            model.Workflow = db.Workflow.FirstOrDefault(q => q.WorkflowId == WorkflowId);
            model.Organizacion = db.Organizacion.Find(model.Workflow.Proceso.OrganizacionId);
            model.Directorios = db.Directorio.Where(q => q.OrganizacionId == model.Organizacion.OrganizacionId).ToList();
            model.ModificacionEstatutos = db.ModificacionEstatutos.Where(q => q.OrganizacionId == model.Organizacion.OrganizacionId).ToList();
            model.Disolucions = db.Disolucions.Where(q => q.OrganizacionId == model.Organizacion.OrganizacionId).ToList();

            return PartialView("_ModificacionEdit", model);
        }

        public ActionResult DisolucionAdd(int WorkflowId, int OrganizacionId)
        {

            db.Disolucions.Add(new Disolucion() { OrganizacionId = OrganizacionId });
            db.SaveChanges();

            var model = new TaskModel();
            model.Workflow = db.Workflow.FirstOrDefault(q => q.WorkflowId == WorkflowId);
            model.Organizacion = db.Organizacion.Find(model.Workflow.Proceso.OrganizacionId);
            model.Directorios = db.Directorio.Where(q => q.OrganizacionId == model.Organizacion.OrganizacionId).ToList();
            model.ModificacionEstatutos = db.ModificacionEstatutos.Where(q => q.OrganizacionId == model.Organizacion.OrganizacionId).ToList();
            model.Disolucions = db.Disolucions.Where(q => q.OrganizacionId == model.Organizacion.OrganizacionId).ToList();

            return PartialView("_DisolucionEdit", model);
        }

        public ActionResult DisolucionDelete(int WorkflowId, int DisolucionId)
        {
            var disolucion = db.Disolucions.FirstOrDefault(q => q.DisolucionId == DisolucionId);
            if (disolucion != null)
            {
                db.Disolucions.Remove(disolucion);
                db.SaveChanges();
            }

            var model = new TaskModel();
            model.Workflow = db.Workflow.FirstOrDefault(q => q.WorkflowId == WorkflowId);
            model.Organizacion = db.Organizacion.Find(model.Workflow.Proceso.OrganizacionId);
            model.Directorios = db.Directorio.Where(q => q.OrganizacionId == model.Organizacion.OrganizacionId).ToList();
            model.ModificacionEstatutos = db.ModificacionEstatutos.Where(q => q.OrganizacionId == model.Organizacion.OrganizacionId).ToList();
            model.Disolucions = db.Disolucions.Where(q => q.OrganizacionId == model.Organizacion.OrganizacionId).ToList();

            return PartialView("_DisolucionEdit", model);
        }





        public ActionResult DespachoDocumento(int WorkflowId)
        {
            var model = new TaskModel();
            model.Workflow = db.Workflow.FirstOrDefault(q => q.WorkflowId == WorkflowId);
            model.Documentos = db.Documento.Where(q => q.Workflow.ProcesoId == model.Workflow.ProcesoId).ToList();

            return View(model);
        }

        public ActionResult ActaFiscalizacion(int WorkflowId)
        {
            var workflow = db.Workflow.FirstOrDefault(q => q.WorkflowId == WorkflowId);

            var model = db
                .ActaFiscalizacion
                .Include(i => i.Organizacion)
                .Include(i => i.ActaFiscalizacionFiscalizadorContables.Select(c => c.User))
                .Include(i => i.ActaFiscalizacionFiscalizadorLegals.Select(c => c.User))
                .Include(i => i.ActaFiscalizacionHechoContables)
                .Include(i => i.ActaFiscalizacionHechoLegals)
                .FirstOrDefault(q => q.WorkflowId == WorkflowId);

            if (model == null)
            {
                model = new ActaFiscalizacion();
                model.WorkflowId = WorkflowId;
                model.Fecha = DateTime.Now;
                model.FechaFiscalizacionInSitu = DateTime.Now;
                model.FechaSalidaOficioAcreditacionRequerimientos = DateTime.Now;
                model.OrganizacionId = workflow.Proceso.OrganizacionId;

                foreach (var item in db.Users.Where(q => q.Habilitado).OrderBy(q => q.Nombre))
                {
                    model.ActaFiscalizacionFiscalizadorContables.Add(new ActaFiscalizacionFiscalizadorContable() { User = item });
                }

                foreach (var item in db.Users.Where(q => q.Habilitado).OrderBy(q => q.Nombre))
                {
                    model.ActaFiscalizacionFiscalizadorLegals.Add(new ActaFiscalizacionFiscalizadorLegal() { User = item });
                }

                foreach (var item in db.HechoLegal)
                {
                    model.ActaFiscalizacionHechoLegals.Add(new ActaFiscalizacionHechoLegal() { HechoLegal = item });
                }

                foreach (var item in db.HechoContable)
                {
                    model.ActaFiscalizacionHechoContables.Add(new ActaFiscalizacionHechoContable() { HechoContable = item });
                }

                db.ActaFiscalizacion.Add(model);
                db.SaveChanges();
            }

            ViewBag.GeneroRepresentanteLegalId = db.Genero.OrderBy(q => q.Nombre);
            ViewBag.GeneroGerenteId = db.Genero.OrderBy(q => q.Nombre);
            ViewBag.RegionId = db.Region.OrderBy(q => q.Nombre);
            ViewBag.ComunaId = db.Comuna.OrderBy(q => q.Nombre);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ActaFiscalizacion(ActaFiscalizacion model)
        {
            if (ModelState.IsValid)
            {
                _custom.UpdateActaFiscalizacion(model);
                db.SaveChanges();

                TempData["Message"] = Properties.Settings.Default.Success;
                return RedirectToAction("ActaFiscalizacion", new { model.WorkflowId });
            }

            ViewBag.GeneroRepresentanteLegalId = db.Genero.OrderBy(q => q.Nombre);
            ViewBag.GeneroGerenteId = db.Genero.OrderBy(q => q.Nombre);
            ViewBag.RegionId = db.Region.OrderBy(q => q.Nombre);
            ViewBag.ComunaId = db.Comuna.OrderBy(q => q.Nombre);

            return View(model);
        }

        public ActionResult Send(int WorkflowId)
        {
            var model = db.Workflow.FirstOrDefault(q => q.WorkflowId == WorkflowId);
            ViewBag.TipoAprobacionId = new SelectList(db.TipoAprobacion.Where(q => q.TipoAprobacionId > 1).OrderBy(q => q.Nombre), "TipoAprobacionId", "Nombre");
            ViewBag.DefinicionWorkflowId = new SelectList(db.DefinicionWorkflow.Where(q => q.DefinicionWorkflowDependeDeId == model.DefinicionWorkflowId).OrderBy(q => q.Secuencia).AsEnumerable().Select(q => new { Value = q.DefinicionWorkflowId.ToString(), Text = q.TipoWorkflow.Nombre }), "Value", "Text");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Send(Workflow workflow, int? DefinicionWorkflowId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _custom.ProcesoUpdate(workflow, DefinicionWorkflowId);
                    TempData["Message"] = Properties.Settings.Default.Success;
                    return RedirectToAction("Index", "Inbox");
                }
                catch (Exception ex)
                {
                    TempData["Error"] = ex.Message;
                }
            }

            ViewBag.TipoAprobacionId = new SelectList(db.TipoAprobacion.Where(q => q.TipoAprobacionId > 1).OrderBy(q => q.Nombre), "TipoAprobacionId", "Nombre", workflow.TipoAprobacionId);
            ViewBag.DefinicionWorkflowId = new SelectList(db.DefinicionWorkflow.Where(q => q.DefinicionWorkflowDependeDeId == workflow.DefinicionWorkflowId).OrderBy(q => q.Secuencia).AsEnumerable().Select(q => new { Value = q.DefinicionWorkflowId.ToString(), Text = q.TipoWorkflow.Nombre }), "Value", "Text", DefinicionWorkflowId);

            return View(workflow);
        }

        public ActionResult GenerarCorrelativo(int WorkflowId)
        {
            var model = new TaskModel();
            model.Workflow = db.Workflow.FirstOrDefault(q => q.WorkflowId == WorkflowId);
            model.Organizacion = db.Organizacion.Find(model.Workflow.Proceso.OrganizacionId);

            ViewBag.TipoOrganizacionId = new SelectList(db.TipoOrganizacion.Where(q => q.TipoOrganizacionId != (int)Infrastructure.Enum.TipoOrganizacion.AunNoDefinida).OrderBy(q => q.Nombre), "TipoOrganizacionId", "Nombre");
            ViewBag.RegionId = new SelectList(db.Region.OrderBy(q => q.Nombre), "RegionId", "Nombre");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GenerarCorrelativo(TaskModel model)
        {
            var workflow = db.Workflow.FirstOrDefault(q => q.WorkflowId == model.Workflow.WorkflowId);

            if (ModelState.IsValid)
            {
                workflow.Proceso.Organizacion = _custom.GenerarRegistro(model.Organizacion);
                TempData["Message"] = Properties.Settings.Default.Success;
                return RedirectToAction("GenerarCorrelativo", new { model.Workflow.WorkflowId });
            }

            ViewBag.TipoOrganizacionId = new SelectList(db.TipoOrganizacion.Where(q => q.TipoOrganizacionId != (int)Infrastructure.Enum.TipoOrganizacion.AunNoDefinida).OrderBy(q => q.Nombre), "TipoOrganizacionId", "Nombre", model.Organizacion.TipoOrganizacionId);
            ViewBag.RegionId = new SelectList(db.Region.OrderBy(q => q.Nombre), "RegionId", "Nombre", model.Organizacion.TipoOrganizacionId);

            return View(model);
        }

        public ActionResult FirmarDocumentos(int WorkflowId)
        {
            var model = new TaskModel();
            model.Workflow = db.Workflow.FirstOrDefault(q => q.WorkflowId == WorkflowId);
            model.Documentos = db.Documento.Where(q => q.Activo && q.Workflow.ProcesoId == model.Workflow.Proceso.ProcesoId).ToList();

            return View(model);
        }

        public ActionResult FirmarDocumentosPost(int id)
        {
            var documento = db.Documento.Find(id);
            if (documento == null)
                ModelState.AddModelError(string.Empty, "No se encontró el documento.");

            if (documento != null && !documento.FileName.ToUpper().Contains("PDF"))
                ModelState.AddModelError(string.Empty, "Solo se pueden firmar documentos tipo PDF");

            if (!db.Firmante.Any(q => q.EsActivo))
                ModelState.AddModelError(string.Empty, "No se encontró un firmante habilitado, revise la configuración de los firmantes");

            if (ModelState.IsValid)
            {
                var workflow = db.Workflow.FirstOrDefault(q => q.WorkflowId == documento.WorkflowId);

                _custom.SignPDF(documento.DocumentoId, workflow.Proceso.Organizacion.TipoOrganizacionId, null);
                TempData["Message"] = Properties.Settings.Default.Success;
            }

            return RedirectToAction("FirmarDocumentos", new { documento.WorkflowId });
        }


        public ActionResult Articulo91(int WorkflowId)
        {
            var model = new TaskModel();
            model.Workflow = db.Workflow.FirstOrDefault(q => q.WorkflowId == WorkflowId);
            model.Organizacion = db.Organizacion.Find(model.Workflow.Proceso.OrganizacionId);
            model.Documentos = db.Documento.Where(q => q.Workflow.ProcesoId == model.Workflow.ProcesoId).ToList();
            model.Articulo91 = db.Articulo91.FirstOrDefault(q => q.ProcesoId == model.Workflow.ProcesoId);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Articulo91(TaskModel model)
        {
            if (ModelState.IsValid)
            {
                _custom.UpdateArticulo91(model.Articulo91);
                db.SaveChanges();

                TempData["Message"] = Properties.Settings.Default.Success;
                return RedirectToAction("Articulo91", new { model.WorkflowId });
            }
            return View(model);
        }

        public ActionResult Fiscalizacion(int WorkflowId)
        {
            var model = new TaskModel();
            model.Workflow = db.Workflow.FirstOrDefault(q => q.WorkflowId == WorkflowId);
            model.Organizacion = db.Organizacion.Find(model.Workflow.Proceso.OrganizacionId);
            model.Fiscalizacions = db.Fiscalizacion.Where(q => q.ProcesoId == model.Workflow.ProcesoId).ToList();
            model.Fiscalizacion = new Fiscalizacion() { ProcesoId = model.Workflow.ProcesoId };
            model.Hallazgo = new Hallazgo();

            ViewBag.TipoFiscalizacionId = new SelectList(db.TipoFiscalizacion.OrderBy(q => q.Descripcion).Select(q => new SelectListItem { Value = q.TipoFiscalizacionId.ToString(), Text = q.Descripcion }), "Value", "Text");
            ViewBag.TipoHallazgoId = new SelectList(db.TipoHallazgo.OrderBy(q => q.Descripcion).Select(q => new SelectListItem { Value = q.TipoHallazgoId.ToString(), Text = q.Descripcion }), "Value", "Text");
            ViewBag.NumeroReiteracion = new SelectList(Enumerable.Range(1, 15).Select(q => new SelectListItem { Value = q.ToString(), Text = q.ToString() }), "Value", "Text");
            ViewBag.NumeroHallazgoPendientes = new SelectList(Enumerable.Range(1, 30).Select(q => new SelectListItem { Value = q.ToString(), Text = q.ToString() }), "Value", "Text");

            ViewBag.TipoOficioId = new SelectList(db.TipoOficio.OrderBy(q => q.Descripcion).Select(q => new SelectListItem { Value = q.TipoOficioId.ToString(), Text = q.Descripcion }), "Value", "Text");
            ViewBag.TipoMateriaId = new SelectList(db.TipoMateria.OrderBy(q => q.Descripcion).Select(q => new SelectListItem { Value = q.TipoMateriaId.ToString(), Text = q.Descripcion }), "Value", "Text");
            ViewBag.TipoCriterioId = new SelectList(db.TipoCriterio.OrderBy(q => q.Descripcion).Select(q => new SelectListItem { Value = q.TipoCriterioId.ToString(), Text = q.Descripcion }), "Value", "Text");
            ViewBag.ProcesoRelacionadoId = new SelectList(db.Proceso.Where(q => q.OrganizacionId == model.Organizacion.OrganizacionId).OrderBy(q => q.ProcesoId).ToList().Select(q => new SelectListItem { Value = q.ProcesoId.ToString(), Text = string.Format("{0} - {1} - {2}", q.ProcesoId, q.DefinicionProceso.Nombre, q.FechaCreacion.ToString("dd/MM/yyy")) }), "Value", "Text");
            ViewBag.Responsable1 = new SelectList(db.Users.Where(q => (bool)q.Habilitado && q.Perfil.Nombre.ToUpper().Contains("CONTABLE") || q.Perfil.Nombre.ToUpper().Contains("LEGAL")).OrderBy(q => q.UserName).ToList(), "UserName", "UserName");
            ViewBag.Responsable2 = new SelectList(db.Users.Where(q => (bool)q.Habilitado && q.Perfil.Nombre.ToUpper().Contains("CONTABLE") || q.Perfil.Nombre.ToUpper().Contains("LEGAL")).OrderBy(q => q.UserName).ToList(), "UserName", "UserName");
            ViewBag.Responsable3 = new SelectList(db.Users.Where(q => (bool)q.Habilitado && q.Perfil.Nombre.ToUpper().Contains("CONTABLE") || q.Perfil.Nombre.ToUpper().Contains("LEGAL")).OrderBy(q => q.UserName).ToList(), "UserName", "UserName");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FiscalizacionAdd(TaskModel model)
        {
            if (ModelState.IsValid)
            {
                db.Fiscalizacion.Add(model.Fiscalizacion);
                db.SaveChanges();

                TempData["Message"] = Properties.Settings.Default.Success;
            }

            ViewBag.TipoFiscalizacionId = new SelectList(db.TipoFiscalizacion.OrderBy(q => q.Descripcion).Select(q => new SelectListItem { Value = q.TipoFiscalizacionId.ToString(), Text = q.Descripcion }), "Value", "Text");
            ViewBag.TipoHallazgoId = new SelectList(db.TipoHallazgo.OrderBy(q => q.Descripcion).Select(q => new SelectListItem { Value = q.TipoHallazgoId.ToString(), Text = q.Descripcion }), "Value", "Text");
            ViewBag.NumeroReiteracion = new SelectList(Enumerable.Range(0, 15).Select(q => new SelectListItem { Value = q.ToString(), Text = q.ToString() }), "Value", "Text");
            ViewBag.NumeroHallazgoPendientes = new SelectList(Enumerable.Range(0, 30).Select(q => new SelectListItem { Value = q.ToString(), Text = q.ToString() }), "Value", "Text");

            ViewBag.TipoOficioId = new SelectList(db.TipoOficio.OrderBy(q => q.Descripcion).Select(q => new SelectListItem { Value = q.TipoOficioId.ToString(), Text = q.Descripcion }), "Value", "Text");
            ViewBag.TipoMateriaId = new SelectList(db.TipoMateria.OrderBy(q => q.Descripcion).Select(q => new SelectListItem { Value = q.TipoMateriaId.ToString(), Text = q.Descripcion }), "Value", "Text");
            ViewBag.TipoCriterioId = new SelectList(db.TipoCriterio.OrderBy(q => q.Descripcion).Select(q => new SelectListItem { Value = q.TipoCriterioId.ToString(), Text = q.Descripcion }), "Value", "Text");
            ViewBag.ProcesoRelacionadoId = new SelectList(db.Proceso.Where(q => q.OrganizacionId == model.Organizacion.OrganizacionId).OrderBy(q => q.ProcesoId).ToList().Select(q => new SelectListItem { Value = q.ProcesoId.ToString(), Text = string.Format("ID: {0}, Tipo:{1}, Fecha: {2}", q.ProcesoId, q.DefinicionProceso.Nombre, q.FechaCreacion.ToString("dd/MM/yyy")) }), "Value", "Text");
            ViewBag.UserId = new SelectList(db.Users.Where(q => (bool)q.Habilitado && (bool)q.EsFiscalizador).OrderBy(q => q.UserName).ToList(), "Id", "UserName");

            return Redirect(Request.UrlReferrer.ToString());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FiscalizacionEdit(TaskModel model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(model.Fiscalizacion).State = EntityState.Modified;
                db.SaveChanges();

                TempData["Message"] = Properties.Settings.Default.Success;
            }

            ViewBag.TipoFiscalizacionId = new SelectList(db.TipoFiscalizacion.OrderBy(q => q.Descripcion).Select(q => new SelectListItem { Value = q.TipoFiscalizacionId.ToString(), Text = q.Descripcion }), "Value", "Text");
            ViewBag.TipoHallazgoId = new SelectList(db.TipoHallazgo.OrderBy(q => q.Descripcion).Select(q => new SelectListItem { Value = q.TipoHallazgoId.ToString(), Text = q.Descripcion }), "Value", "Text");
            ViewBag.NumeroReiteracion = new SelectList(Enumerable.Range(0, 15).Select(q => new SelectListItem { Value = q.ToString(), Text = q.ToString() }), "Value", "Text");
            ViewBag.NumeroHallazgoPendientes = new SelectList(Enumerable.Range(0, 30).Select(q => new SelectListItem { Value = q.ToString(), Text = q.ToString() }), "Value", "Text");

            ViewBag.TipoOficioId = new SelectList(db.TipoOficio.OrderBy(q => q.Descripcion).Select(q => new SelectListItem { Value = q.TipoOficioId.ToString(), Text = q.Descripcion }), "Value", "Text");
            ViewBag.TipoMateriaId = new SelectList(db.TipoMateria.OrderBy(q => q.Descripcion).Select(q => new SelectListItem { Value = q.TipoMateriaId.ToString(), Text = q.Descripcion }), "Value", "Text");
            ViewBag.TipoCriterioId = new SelectList(db.TipoCriterio.OrderBy(q => q.Descripcion).Select(q => new SelectListItem { Value = q.TipoCriterioId.ToString(), Text = q.Descripcion }), "Value", "Text");
            ViewBag.ProcesoRelacionadoId = new SelectList(db.Proceso.Where(q => q.OrganizacionId == model.Organizacion.OrganizacionId).OrderBy(q => q.ProcesoId).ToList().Select(q => new SelectListItem { Value = q.ProcesoId.ToString(), Text = string.Format("ID: {0}, Tipo:{1}, Fecha: {2}", q.ProcesoId, q.DefinicionProceso.Nombre, q.FechaCreacion.ToString("dd/MM/yyy")) }), "Value", "Text");
            ViewBag.UserId = new SelectList(db.Users.Where(q => (bool)q.Habilitado && (bool)q.EsFiscalizador).OrderBy(q => q.UserName).ToList(), "Id", "UserName");

            return Redirect(Request.UrlReferrer.ToString());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HallazgoAdd(TaskModel model, int FiscalizacionId)
        {
            if (ModelState.IsValid)
            {
                model.Hallazgo.FiscalizacionId = FiscalizacionId;
                db.Hallazgo.Add(model.Hallazgo);
                db.SaveChanges();

                TempData["Message"] = Properties.Settings.Default.Success;
            }

            ViewBag.TipoFiscalizacionId = new SelectList(db.TipoFiscalizacion.OrderBy(q => q.Descripcion).Select(q => new SelectListItem { Value = q.TipoFiscalizacionId.ToString(), Text = q.Descripcion }), "Value", "Text");
            ViewBag.TipoHallazgoId = new SelectList(db.TipoHallazgo.OrderBy(q => q.Descripcion).Select(q => new SelectListItem { Value = q.TipoHallazgoId.ToString(), Text = q.Descripcion }), "Value", "Text");
            ViewBag.NumeroReiteracion = new SelectList(Enumerable.Range(0, 15).Select(q => new SelectListItem { Value = q.ToString(), Text = q.ToString() }), "Value", "Text");
            ViewBag.NumeroHallazgoPendientes = new SelectList(Enumerable.Range(0, 30).Select(q => new SelectListItem { Value = q.ToString(), Text = q.ToString() }), "Value", "Text");

            ViewBag.TipoMateriaId = new SelectList(db.TipoMateria.OrderBy(q => q.Descripcion).Select(q => new SelectListItem { Value = q.TipoMateriaId.ToString(), Text = q.Descripcion }), "Value", "Text");
            ViewBag.TipoCriterioId = new SelectList(db.TipoCriterio.OrderBy(q => q.Descripcion).Select(q => new SelectListItem { Value = q.TipoCriterioId.ToString(), Text = q.Descripcion }), "Value", "Text");
            return Redirect(Request.UrlReferrer.ToString());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HallazgoEdit(TaskModel model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(model.Hallazgo).State = EntityState.Modified;
                db.SaveChanges();

                TempData["Message"] = Properties.Settings.Default.Success;
            }

            ViewBag.TipoFiscalizacionId = new SelectList(db.TipoFiscalizacion.OrderBy(q => q.Descripcion).Select(q => new SelectListItem { Value = q.TipoFiscalizacionId.ToString(), Text = q.Descripcion }), "Value", "Text");
            ViewBag.TipoHallazgoId = new SelectList(db.TipoHallazgo.OrderBy(q => q.Descripcion).Select(q => new SelectListItem { Value = q.TipoHallazgoId.ToString(), Text = q.Descripcion }), "Value", "Text");
            ViewBag.NumeroReiteracion = new SelectList(Enumerable.Range(0, 15).Select(q => new SelectListItem { Value = q.ToString(), Text = q.ToString() }), "Value", "Text");
            ViewBag.NumeroHallazgoPendientes = new SelectList(Enumerable.Range(0, 30).Select(q => new SelectListItem { Value = q.ToString(), Text = q.ToString() }), "Value", "Text");

            ViewBag.TipoMateriaId = new SelectList(db.TipoMateria.OrderBy(q => q.Descripcion).Select(q => new SelectListItem { Value = q.TipoMateriaId.ToString(), Text = q.Descripcion }), "Value", "Text");
            ViewBag.TipoCriterioId = new SelectList(db.TipoCriterio.OrderBy(q => q.Descripcion).Select(q => new SelectListItem { Value = q.TipoCriterioId.ToString(), Text = q.Descripcion }), "Value", "Text");
            return Redirect(Request.UrlReferrer.ToString());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DocumentoAdd(HttpPostedFileBase Archivo, int WorkflowId, int ProcesoId, int OrganizacionId)
        {
            if (ModelState.IsValid && Archivo != null)
            {
                var file = Archivo;
                var target = new MemoryStream();
                file.InputStream.CopyTo(target);

                db.Documento.Add(new Documento()
                {
                    FechaCreacion = DateTime.Now,
                    Autor = User.Identity.Name,
                    FileName = file.FileName,
                    Content = target.ToArray(),
                    Resuelto = false,
                    TipoDocumentoId = (int)DAES.Infrastructure.Enum.TipoDocumento.SinClasificar,
                    Firmado = false,
                    WorkflowId = WorkflowId,
                    ProcesoId = ProcesoId,
                    OrganizacionId = OrganizacionId,
                    TipoPrivacidadId = (int)DAES.Infrastructure.Enum.TipoPrivacidad.Privado,
                });
                db.SaveChanges();
                TempData["Message"] = Properties.Settings.Default.Success;
            }
            return Redirect(Request.UrlReferrer.ToString());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DocumentoDelete(int id)
        {
            if (ModelState.IsValid)
            {
                var documento = db.Documento.Find(id);
                if (documento != null)
                    db.Documento.Remove(documento);

                db.SaveChanges();

                TempData["Message"] = Properties.Settings.Default.Success;
            }
            return Redirect(Request.UrlReferrer.ToString());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HallazgoDelete(int id)
        {
            if (ModelState.IsValid)
            {
                var hallazgo = db.Hallazgo.Find(id);
                if (hallazgo != null)
                    db.Hallazgo.Remove(hallazgo);

                db.SaveChanges();

                TempData["Message"] = Properties.Settings.Default.Success;
            }
            return Redirect(Request.UrlReferrer.ToString());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FiscalizacionDelete(int id)
        {
            if (ModelState.IsValid)
            {
                var fiscalizacion = db.Fiscalizacion.Find(id);
                if (fiscalizacion != null)
                {
                    //remnover documentos
                    foreach (var documento in db.Documento.Where(q => q.ProcesoId == fiscalizacion.ProcesoId).ToList())
                        db.Documento.Remove(documento);

                    //remnover hallazgos
                    foreach (var hallazgo in db.Hallazgo.Where(q => q.FiscalizacionId == id).ToList())
                        db.Hallazgo.Remove(hallazgo);

                    //remnover fiscalizacion
                    db.Fiscalizacion.Remove(fiscalizacion);
                }

                db.SaveChanges();

                TempData["Message"] = Properties.Settings.Default.Success;
            }
            return Redirect(Request.UrlReferrer.ToString());
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