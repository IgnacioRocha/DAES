using DAES.Infrastructure.SistemaIntegrado;
using DAES.Model.SistemaIntegrado;
using DAES.Web.BackOffice.Helper;
using OfficeOpenXml;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DAES.Web.BackOffice.Controllers
{
    [Audit]
    [Authorize]
    public class FiscalizacionController : Controller
    {
        private BLL.Custom _custom = new BLL.Custom();

        public class DTOOrganizacion
        {
            public DTOOrganizacion()
            {
            }

            [Required(ErrorMessage = "Es necesario especificar este dato")]
            [Display(Name = "Razón social")]
            public string RazonSocial { get; set; }

            [Display(Name = "Id")]
            public int OrganizacionId { get; set; }
        }

        public class DTOProcesoEdit
        {
            public DTOProcesoEdit()
            {
            }

            [Required(ErrorMessage = "Es necesario especificar este dato")]
            [Display(Name = "Razón social")]
            public string RazonSocial { get; set; }

            public int ProcesoId { get; set; }
            public int OrganizacionId { get; set; }
        }

        private SistemaIntegradoContext db = new SistemaIntegradoContext();


        public ActionResult Index()
        {
            var model =
                db
                .Proceso
                .AsNoTracking()
                .Where(q => q.DefinicionProcesoId == (int)DAES.Infrastructure.Enum.DefinicionProceso.Fiscalizacion)
                .OrderByDescending(q => q.ProcesoId)
                .ToList();

            foreach (var item in model)
                foreach (var fiscalizacion in item.Fiscalizacions.Where(q => q.Activo))
                    fiscalizacion.ProcesoRelacionado = db.Proceso.Find(fiscalizacion.ProcesoRelacionadoId);

            return View(model);
        }

        public ActionResult Inbox()
        {
            var user = Helper.Helper.CurrentUser.UserName;
            var model = db.Fiscalizacion
            .AsNoTracking()
            .Where(q => q.Activo)
            .Where(q => !q.Proceso.Terminada && q.Proceso.DefinicionProcesoId == (int)DAES.Infrastructure.Enum.DefinicionProceso.Fiscalizacion)
            .Where(q => q.Responsable1 == user || q.Responsable2 == user || q.Responsable3 == user)
            .OrderByDescending(q => q.ProcesoId)
            .Select(q => q.Proceso).Distinct().ToList();

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(DTOOrganizacion model)
        {
            if (ModelState.IsValid)
            {
                var proceso = new Proceso()
                {
                    DefinicionProcesoId = (int)Infrastructure.Enum.DefinicionProceso.Fiscalizacion,
                    OrganizacionId = model.OrganizacionId,
                    Solicitante = new Solicitante()
                    {
                        Nombres = Helper.Helper.CurrentUser.UserName,
                        Email = Helper.Helper.CurrentUser.Email
                    }
                };

                try
                {
                    var p = _custom.ProcesoStart(proceso);
                    TempData["Message"] = Properties.Settings.Default.Success;
                    return RedirectToAction("Edit", null, new { id = p.ProcesoId });
                }
                catch (Exception ex)
                {
                    TempData["Error"] = ex.Message;
                }
            }

            return View(model);
        }

        public ActionResult Change(int id)
        {
            var proceso = db.Proceso.Find(id);
            var model = new DTOProcesoEdit() { ProcesoId = proceso.ProcesoId };
            return View(model);
        }

        [HttpPost]
        public ActionResult Change(DTOProcesoEdit proceso)
        {
            if (ModelState.IsValid)
            {
                var p = db.Proceso.Find(proceso.ProcesoId);
                if (p != null)
                {
                    p.OrganizacionId = proceso.OrganizacionId;
                    db.SaveChanges();
                    TempData["Message"] = Properties.Settings.Default.Success;
                }
            }

            return RedirectToAction("Edit", new { id = proceso.ProcesoId });
        }

        [HttpPost]
        public ActionResult ProcesoFinish(int ProcesoId)
        {
            var proceso = db.Proceso.Find(ProcesoId);
            if (proceso != null)
            {
                proceso.Terminada = true;
                proceso.FechaTermino = DateTime.Now;
                db.SaveChanges();
                TempData["Message"] = Properties.Settings.Default.Success;
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var model = db.Proceso.Find(id);
            ViewBag.TipoFiscalizacionId = new SelectList(db.TipoFiscalizacion.OrderBy(q => q.Descripcion).Select(q => new SelectListItem { Value = q.TipoFiscalizacionId.ToString(), Text = q.Descripcion }), "Value", "Text");
            ViewBag.TipoHallazgoId = new SelectList(db.TipoHallazgo.OrderBy(q => q.Descripcion).Select(q => new SelectListItem { Value = q.TipoHallazgoId.ToString(), Text = q.Descripcion }), "Value", "Text");
            ViewBag.NumeroReiteracion = new SelectList(Enumerable.Range(1, 15).Select(q => new SelectListItem { Value = q.ToString(), Text = q.ToString() }), "Value", "Text");
            ViewBag.NumeroHallazgoPendientes = new SelectList(Enumerable.Range(1, 30).Select(q => new SelectListItem { Value = q.ToString(), Text = q.ToString() }), "Value", "Text");
            ViewBag.TipoOficioId = new SelectList(db.TipoOficio.OrderBy(q => q.Descripcion).Select(q => new SelectListItem { Value = q.TipoOficioId.ToString(), Text = q.Descripcion }), "Value", "Text");
            ViewBag.TipoMateriaId = new SelectList(db.TipoMateria.OrderBy(q => q.Descripcion).Select(q => new SelectListItem { Value = q.TipoMateriaId.ToString(), Text = q.Descripcion }), "Value", "Text");
            ViewBag.TipoCriterioId = new SelectList(db.TipoCriterio.OrderBy(q => q.Descripcion).Select(q => new SelectListItem { Value = q.TipoCriterioId.ToString(), Text = q.Descripcion }), "Value", "Text");
            ViewBag.ProcesoRelacionadoId = new SelectList(db.Proceso.Where(q => q.OrganizacionId == model.OrganizacionId).OrderBy(q => q.ProcesoId).ToList().Select(q => new SelectListItem { Value = q.ProcesoId.ToString(), Text = string.Format("{0} - {1} - {2}", q.ProcesoId, q.DefinicionProceso.Nombre, q.FechaCreacion.ToString("dd/MM/yyy")) }), "Value", "Text");
            ViewBag.Responsable1 = new SelectList(db.Users.Where(q => q.Habilitado && (q.Perfil.Nombre.ToUpper().Contains("CONTABLE") || q.Perfil.Nombre.ToUpper().Contains("LEGAL"))).OrderBy(q => q.UserName).ToList(), "UserName", "UserName");
            ViewBag.Responsable2 = new SelectList(db.Users.Where(q => q.Habilitado && (q.Perfil.Nombre.ToUpper().Contains("CONTABLE") || q.Perfil.Nombre.ToUpper().Contains("LEGAL"))).OrderBy(q => q.UserName).ToList(), "UserName", "UserName");
            ViewBag.Responsable3 = new SelectList(db.Users.Where(q => q.Habilitado && (q.Perfil.Nombre.ToUpper().Contains("CONTABLE") || q.Perfil.Nombre.ToUpper().Contains("LEGAL"))).OrderBy(q => q.UserName).ToList(), "UserName", "UserName");

            foreach (var item in model.Fiscalizacions.Where(q => q.Activo))
                item.ProcesoRelacionado = db.Proceso.Find(item.ProcesoRelacionadoId);

            return View(model);
        }

        public ActionResult View(int id)
        {
            var model = db.Proceso.Find(id);
            return View(model);
        }

        //public ActionResult FiscalizacionCreate(int id)
        //{
        //    var model = db.Proceso.Find(id);
        //    return View(new Fiscalizacion() { ProcesoId = id });
        //}

        [HttpPost]
        public ActionResult FiscalizacionCreate(Fiscalizacion FiscalizacionNuevo)
        {
            if (ModelState.IsValid)
            {
                var proceso = db.Proceso.Find(FiscalizacionNuevo.ProcesoId);
                if (proceso != null)
                {
                    proceso.Fiscalizacions.Add(FiscalizacionNuevo);
                    db.SaveChanges();
                    TempData["Message"] = Properties.Settings.Default.Success;
                }
            }

            return Redirect(Request.UrlReferrer.ToString());
        }

        [HttpPost]
        public ActionResult FiscalizacionEdit(Fiscalizacion fiscalizacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fiscalizacion).State = System.Data.Entity.EntityState.Modified;
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
                    foreach (var documento in db.Documento.Where(q => q.ProcesoId == fiscalizacion.ProcesoId).ToList())
                        documento.Activo = false;
                    foreach (var hallazgo in db.Hallazgo.Where(q => q.FiscalizacionId == id).ToList())
                    {
                        hallazgo.Activo = false;
                        hallazgo.EliminadoPor = User.Identity.Name;
                    }
                    fiscalizacion.Activo = false;
                    fiscalizacion.EliminadoPor = User.Identity.Name;
                }

                db.SaveChanges();

                TempData["Message"] = Properties.Settings.Default.Success;
            }
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult DocumentoAdd(HttpPostedFileBase Archivo, int id)
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
                    ProcesoId = id,
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
                    documento.Activo = false;

                db.SaveChanges();

                TempData["Message"] = Properties.Settings.Default.Success;
            }
            return Redirect(Request.UrlReferrer.ToString());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HallazgoCreate(Hallazgo HallazgoNuevo, int FiscalizacionId)
        {
            if (ModelState.IsValid)
            {
                var fiscalizacion = db.Fiscalizacion.Find(FiscalizacionId);
                if (fiscalizacion != null)
                {
                    fiscalizacion.Hallazgos.Add(HallazgoNuevo);
                    db.SaveChanges();
                    TempData["Message"] = Properties.Settings.Default.Success;
                }
            }
            return Redirect(Request.UrlReferrer.ToString());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HallazgoEdit(Hallazgo hallazgo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hallazgo).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Message"] = Properties.Settings.Default.Success;
            }

            return Redirect(Request.UrlReferrer.ToString());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HallazgoDelete(int id)
        {
            var hallazgo = db.Hallazgo.Find(id);
            if (hallazgo != null)
            {
                hallazgo.Activo = false;
                hallazgo.EliminadoPor = User.Identity.Name;

                db.SaveChanges();
                TempData["Message"] = Properties.Settings.Default.Success;
            }
            return Redirect(Request.UrlReferrer.ToString());
        }

        public FileResult Download()
        {
            var db = new SistemaIntegradoContext();
            var excel = new ExcelPackage(new FileInfo(string.Concat(Request.PhysicalApplicationPath, @"App_Data\FISCALIZACION.xlsx")));

            var proceso = db.Proceso.AsNoTracking().Where(q => q.DefinicionProcesoId == (int)Infrastructure.Enum.DefinicionProceso.Fiscalizacion).Select(q => new
            {
                q.ProcesoId,
                DefinicionProceso = q.DefinicionProceso != null ? q.DefinicionProceso.Nombre : string.Empty,
                q.FechaCreacion,
                q.FechaTermino,
                Solicitante = q.Solicitante != null ? q.Solicitante.Email : q.Creador,
                Terminada = q.Terminada ? "SI" : "NO",
                q.Observacion,
                Organizacion = q.Organizacion != null ? q.Organizacion.NumeroRegistro : string.Empty,
                RazonSocial = q.Organizacion != null ? q.Organizacion.RazonSocial : string.Empty,
                Tipo = q.Organizacion != null && q.Organizacion.TipoOrganizacion != null ? q.Organizacion.TipoOrganizacion.Nombre : string.Empty,
                q.Correlativo
            }).ToList();

            var fiscalizacion = db.Fiscalizacion.AsNoTracking().Where(q => q.Activo && q.Proceso.DefinicionProcesoId == (int)Infrastructure.Enum.DefinicionProceso.Fiscalizacion).Select(q => new
            {
                q.ProcesoId,
                q.FiscalizacionId,
                q.Fecha,
                TipoFiscalizacion = q.TipoFiscalizacion != null ? q.TipoFiscalizacion.Descripcion : string.Empty,
                q.NumeroIngreso,
                q.Observacion,
                TipoHallazgo = q.TipoHallazgo != null ? q.TipoHallazgo.Descripcion : string.Empty,
                TipoMateria = q.TipoMateria != null ? q.TipoMateria.Descripcion : string.Empty,
                q.OficioAnterior,
                q.FechaOficioAnterior,
                q.OficioRemitido,
                q.FechaOficioRemitido,
                TipoOficio = q.TipoOficio != null ? q.TipoOficio.Descripcion : string.Empty,
                q.NumeroReiteracion,
                q.NumeroHallazgoPendientes,
                q.Plazo,
                Multa = q.Multa ? "SI" : "NO",
                q.Responsable1,
                q.Responsable2,
                q.Responsable3
            }).ToList();

            var hallazgo = db.Hallazgo.AsNoTracking().Where(q => q.Activo && q.Fiscalizacion.Proceso.DefinicionProcesoId == (int)Infrastructure.Enum.DefinicionProceso.Fiscalizacion).Select(q => new
            {
                q.FiscalizacionId,
                q.HallazgoId,
                TipoMateria = q.TipoMateria != null ? q.TipoMateria.Descripcion : string.Empty,
                TipoCriterio = q.TipoCriterio != null ? q.TipoCriterio.Descripcion : string.Empty,
                TipoHallazgo = q.TipoHallazgo != null ? q.TipoHallazgo.Descripcion : string.Empty,
                q.Plazo,
                Respondido = q.Respondido ? "SI" : "NO",
                Resuelto = q.Resuelto ? "SI" : "NO",
                q.Descripcion
            }).ToList();

            excel.Workbook.Worksheets[1].Cells[2, 1].LoadFromCollection(proceso);
            excel.Workbook.Worksheets[1].Cells[excel.Workbook.Worksheets[1].Dimension.Address].AutoFitColumns();

            excel.Workbook.Worksheets[2].Cells[2, 1].LoadFromCollection(fiscalizacion);
            excel.Workbook.Worksheets[2].Cells[excel.Workbook.Worksheets[2].Dimension.Address].AutoFitColumns();

            excel.Workbook.Worksheets[3].Cells[2, 1].LoadFromCollection(hallazgo);
            excel.Workbook.Worksheets[3].Cells[excel.Workbook.Worksheets[3].Dimension.Address].AutoFitColumns();

            return File(excel.GetAsByteArray(), System.Net.Mime.MediaTypeNames.Application.Octet, DateTime.Now.ToString("yyyyMMddhhmmss") + ".xlsx");
        }

        public FileResult DownloadDeteled()
        {
            var db = new SistemaIntegradoContext();
            var excel = new ExcelPackage(new FileInfo(string.Concat(Request.PhysicalApplicationPath, @"App_Data\FISCALIZACIONEliminados.xlsx")));

            var fiscalizacion = db.Fiscalizacion.AsNoTracking().Where(q => !q.Activo && q.Proceso.DefinicionProcesoId == (int)DAES.Infrastructure.Enum.DefinicionProceso.Fiscalizacion).Select(q => new
            {
                q.ProcesoId,
                q.FiscalizacionId,
                q.Fecha,
                TipoFiscalizacion = q.TipoFiscalizacion != null ? q.TipoFiscalizacion.Descripcion : string.Empty,
                q.NumeroIngreso,
                q.Observacion,
                TipoHallazgo = q.TipoHallazgo != null ? q.TipoHallazgo.Descripcion : string.Empty,
                TipoMateria = q.TipoMateria != null ? q.TipoMateria.Descripcion : string.Empty,
                q.OficioAnterior,
                q.FechaOficioAnterior,
                q.OficioRemitido,
                q.FechaOficioRemitido,
                TipoOficio = q.TipoOficio != null ? q.TipoOficio.Descripcion : string.Empty,
                q.NumeroReiteracion,
                q.NumeroHallazgoPendientes,
                q.Plazo,
                Multa = q.Multa ? "SI" : "NO",
                q.Responsable1,
                q.Responsable2,
                q.Responsable3,
                q.EliminadoPor
            }).ToList();

            var hallazgo = db.Hallazgo.AsNoTracking().Where(q => !q.Activo && q.Fiscalizacion.Proceso.DefinicionProcesoId == (int)DAES.Infrastructure.Enum.DefinicionProceso.Fiscalizacion).Select(q => new
            {
                q.FiscalizacionId,
                q.HallazgoId,
                TipoMateria = q.TipoMateria != null ? q.TipoMateria.Descripcion : string.Empty,
                TipoCriterio = q.TipoCriterio != null ? q.TipoCriterio.Descripcion : string.Empty,
                TipoHallazgo = q.TipoHallazgo != null ? q.TipoHallazgo.Descripcion : string.Empty,
                q.Plazo,
                Respondido = q.Respondido ? "SI" : "NO",
                Resuelto = q.Resuelto ? "SI" : "NO",
                q.Descripcion,
                q.EliminadoPor
            }).ToList();


            excel.Workbook.Worksheets[1].Cells[2, 1].LoadFromCollection(fiscalizacion);
            excel.Workbook.Worksheets[1].Cells[excel.Workbook.Worksheets[1].Dimension.Address].AutoFitColumns();

            excel.Workbook.Worksheets[2].Cells[2, 1].LoadFromCollection(hallazgo);
            excel.Workbook.Worksheets[2].Cells[excel.Workbook.Worksheets[2].Dimension.Address].AutoFitColumns();

            return File(excel.GetAsByteArray(), System.Net.Mime.MediaTypeNames.Application.Octet, DateTime.Now.ToString("yyyyMMddhhmmss") + ".xlsx");
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