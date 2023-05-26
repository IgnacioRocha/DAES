//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
////using App.Model.Core;
////using App.Core.Interfaces;
////using App.Core.UseCases;
//using System.ComponentModel.DataAnnotations;
//using DAES.Web.FrontOffice.Helper;
//using DAES.Model.FirmaDocumento;
//using DAES.BLL.Interfaces;
//using DAES.Model.SistemaIntegrado;
//using DAES.BLL;
//using DAES.Model.Core;
//using DAES.Infrastructure.Interfaces;
////using DAES.BLL.Interfaces;

//namespace App.Web.Controllers
//{
//    [Audit]
//    [Authorize]
//    //[NoDirectAccess]
//    public class FirmaDocumentoController : Controller
//    {
//        public class DTOFileUploadCreate
//        {
//            public int FirmaDocumentoId { get; set; }
//            public int ProcesoId { get; set; }
//            public int WorkflowId { get; set; }
//            [Display(Name = "Comentario")]
//            public string Comentario { get; set; }
//            public string Autor { get; set; }


//            [Required(ErrorMessage = "Es necesario especificar este dato")]
//            [Display(Name = "Tipo documento")]
//            public string TipoDocumentoCodigo { get; set; }

//            [Display(Name = "Tipo documento")]
//            public string TipoDocumentoDescripcion { get; set; }

//            [Required(ErrorMessage = "Es necesario especificar este dato")]
//            [Display(Name = "Archivo")]
//            [DataType(DataType.Upload)]
//            public HttpPostedFileBase FileUpload { get; set; }

//            [Display(Name = "Documento a firmar")]
//            public byte[] File { get; set; }

//            public string Firmante { get; set; }

//            public bool TieneFirma { get; set; }

//            [Display(Name = "Fecha creación")]
//            public System.DateTime? FechaCreacion { get; set; }

//            [Display(Name = "Folio")]
//            public string Folio { get; set; }

//            [Display(Name = "URL gestión documental")]
//            [DataType(DataType.Url)]
//            public string URL { get; set; }
//        }

//        public class DTOFileUploadEdit
//        {
//            public int FirmaDocumentoId { get; set; }
//            public int ProcesoId { get; set; }
//            public int WorkflowId { get; set; }
//            [Display(Name = "Comentario")]
//            public string Comentario { get; set; }
//            public string Autor { get; set; }


//            [Required(ErrorMessage = "Es necesario especificar este dato")]
//            [Display(Name = "Tipo documento")]
//            public string TipoDocumentoCodigo { get; set; }

//            [Display(Name = "Tipo documento")]
//            public string TipoDocumentoDescripcion { get; set; }

//            //[Required(ErrorMessage = "Es necesario especificar este dato")]
//            [Display(Name = "Archivo")]
//            [DataType(DataType.Upload)]
//            public HttpPostedFileBase FileUpload { get; set; }

//            [Display(Name = "Documento a firmar")]
//            public byte[] File { get; set; }

//            public string Firmante { get; set; }

//            public bool TieneFirma { get; set; }

//            [Display(Name = "Fecha creación")]
//            public System.DateTime? FechaCreacion { get; set; }

//            [Display(Name = "Folio")]
//            public string Folio { get; set; }

//            [Display(Name = "URL gestión documental")]
//            [DataType(DataType.Url)]
//            public string URL { get; set; }
//        }

//        private readonly IGestionProcesos _repository;
//        private readonly ISIGPER _sigper;
//        private readonly IFile _file;
//        private readonly IFolio _folio;
//        private readonly IHsm _hsm;
//        private readonly IEmail _email;

//        public FirmaDocumentoController(IGestionProcesos repository, ISIGPER sigper, IFile file, IFolio folio, IHsm hsm, IEmail email)
//        {
//            _repository = repository;
//            _sigper = sigper;
//            _file = file;
//            _folio = folio;
//            _hsm = hsm;
//            _email = email;
//        }

//        public ActionResult View(int id)
//        {
//            var model = _repository.GetFirst<FirmaDocumento>(q => q.ProcesoId == id);
//            if (model == null)
//                return RedirectToAction("View", "Proceso", new { id });

//            return View(model);
//        }
//        public ActionResult Details(int id)
//        {
//            var model = _repository.GetFirst<FirmaDocumento>(q => q.ProcesoId == id);
//            if (model == null)
//                return RedirectToAction("Details", "Proceso", new { id });

//            return View(model);
//        }

//        public ActionResult Create(int? WorkFlowId)
//        {
//            ViewBag.TipoDocumentoCodigo = new SelectList(_folio.GetTipoDocumento().Select(q => new { q.Codigo, q.Descripcion }), "Codigo", "Descripcion");

//            var workflow = _repository.GetById<Workflow>(WorkFlowId);
//            var model = new DTOFileUploadCreate
//            {
//                WorkflowId = workflow.WorkflowId,
//                ProcesoId = workflow.ProcesoId,
//            };

//            return View(model);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create(DTOFileUploadCreate model)
//        {
//            if (ModelState.IsValid)
//            {
//                model.Autor = UserExtended.Email(User);

//                var tipodocumento = _folio.GetTipoDocumento().FirstOrDefault(q => q.Codigo == model.TipoDocumentoCodigo);
//                var target = new MemoryStream();
//                model.FileUpload.InputStream.CopyTo(target);

//                var _useCaseInteractor = new Custom(_repository);
//                var _UseCaseResponseMessage = _useCaseInteractor.Insert(new FirmaDocumento()
//                {
//                    FirmaDocumentoId = model.FirmaDocumentoId,
//                    ProcesoId = model.ProcesoId,
//                    WorkflowId = model.WorkflowId,
//                    TipoDocumentoCodigo = model.TipoDocumentoCodigo,
//                    TipoDocumentoDescripcion = tipodocumento != null ? tipodocumento.Descripcion : "No encontrado",
//                    DocumentoSinFirma = target.ToArray(),
//                    DocumentoSinFirmaFilename = model.FileUpload.FileName,
//                    Observaciones = model.Comentario,
//                    Autor = model.Autor,
//                    URL = model.URL
//                });

//                if (_UseCaseResponseMessage.IsValid)
//                {
//                    TempData["Success"] = "Operación terminada correctamente.";
//                    return RedirectToAction("Execute", "Workflow", new { id = model.WorkflowId });
//                }

//                TempData["Error"] = _UseCaseResponseMessage.Errors;
//            }

//            ViewBag.TipoDocumentoCodigo = new SelectList(_folio.GetTipoDocumento().Select(q => new { q.Codigo, q.Descripcion }), "Codigo", "Descripcion");

//            return View(model);
//        }

//        public ActionResult Edit(int id)
//        {
//            var firma = _repository.GetById<FirmaDocumento>(id);
//            ViewBag.TipoDocumentoCodigo = new SelectList(_folio.GetTipoDocumento().Select(q => new { q.Codigo, q.Descripcion }), "Codigo", "Descripcion", firma.TipoDocumentoCodigo);

//            var model = new DTOFileUploadEdit()
//            {
//                FirmaDocumentoId = firma.FirmaDocumentoId,
//                ProcesoId = firma.ProcesoId,
//                WorkflowId = firma.WorkflowId,
//                File = firma.DocumentoSinFirma,
//                Comentario = firma.Observaciones,
//                URL = firma.URL
//            };

//            return View(model);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit(DTOFileUploadEdit model)
//        {
//            if (ModelState.IsValid)
//            {
//                model.Autor = UserExtended.Email(User);

//                var tipodocumento = _folio.GetTipoDocumento().FirstOrDefault(q => q.Codigo == model.TipoDocumentoCodigo);

//                var target = new MemoryStream();
//                if (model.FileUpload != null)
//                    model.FileUpload.InputStream.CopyTo(target);

//                var _useCaseInteractor = new Custom(_repository);
//                var _UseCaseResponseMessage = _useCaseInteractor.Edit(new FirmaDocumento()
//                {
//                    FirmaDocumentoId = model.FirmaDocumentoId,
//                    ProcesoId = model.ProcesoId,
//                    WorkflowId = model.WorkflowId,
//                    TipoDocumentoCodigo = model.TipoDocumentoCodigo,
//                    TipoDocumentoDescripcion = tipodocumento != null ? tipodocumento.Descripcion : "No encontrado",
//                    DocumentoSinFirma = model.FileUpload != null ? target.ToArray() : null,
//                    DocumentoSinFirmaFilename = model.FileUpload != null ? model.FileUpload.FileName : null,
//                    URL = model.URL,
//                    Observaciones = model.Comentario,
//                    Autor = model.Autor,
//                });

//                if (_UseCaseResponseMessage.IsValid)
//                {
//                    TempData["Success"] = "Operación terminada correctamente.";
//                    return RedirectToAction("Edit", "FirmaDocumento", new { id = model.FirmaDocumentoId });
//                }

//                TempData["Error"] = _UseCaseResponseMessage.Errors;
//            }

//            ViewBag.TipoDocumentoCodigo = new SelectList(_folio.GetTipoDocumento().Select(q => new { q.Codigo, q.Descripcion }), "Codigo", "Descripcion", model.TipoDocumentoCodigo);

//            return View(model);
//        }

//        public ActionResult Sign(int id)
//        {
//            var firma = _repository.GetById<FirmaDocumento>(id);
//            var email = UserExtended.Email(User);

//            var model = new DTOFileUploadEdit()
//            {
//                FirmaDocumentoId = firma.FirmaDocumentoId,
//                ProcesoId = firma.ProcesoId,
//                WorkflowId = firma.WorkflowId,
//                File = firma.DocumentoSinFirma,
//                Comentario = firma.Observaciones,
//                Firmante = email,
//                TieneFirma = _repository.GetExists<Rubrica>(q => q.Email == email),
//                TipoDocumentoDescripcion = firma.TipoDocumentoDescripcion,
//                FechaCreacion = firma.FechaCreacion,
//                Autor = firma.Autor,
//                Folio = firma.Folio,
//                URL = firma.URL
//            };

//            return View(model);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Sign(FirmaDocumento model)
//        {
//            if (ModelState.IsValid)
//            {
//                model.Firmante = UserExtended.Email(User);

//                var _useCaseInteractor = new Custom(_repository, _sigper, _file, _folio, _hsm, _email);
//                var _UseCaseResponseMessage = _useCaseInteractor.Sign(model.FirmaDocumentoId, new List<string> { model.Firmante }, model.Firmante);
//                if (_UseCaseResponseMessage.IsValid)
//                {
//                    TempData["Success"] = "Operación terminada correctamente.";
//                    return RedirectToAction("Sign", "FirmaDocumento", new { id = model.FirmaDocumentoId });
//                }

//                TempData["Error"] = _UseCaseResponseMessage.Errors;
//            }

//            return RedirectToAction("Sign", "FirmaDocumento", new { id = model.FirmaDocumentoId });
//        }

//        public FileResult ShowDocumentoSinFirma(int id)
//        {
//            var model = _repository.GetById<FirmaDocumento>(id);
//            return File(model.DocumentoSinFirma, "application/pdf");
//        }
//    }
//}