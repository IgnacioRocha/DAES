using DAES.Infrastructure.SistemaIntegrado;
using DAES.Web.BackOffice.Helper;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DAES.Web.BackOffice.Controllers
{

    [Audit]
    [Authorize]
    public class FirmaController : Controller
    {

        private SistemaIntegradoContext db = new SistemaIntegradoContext();
        private BLL.Custom _custom = new BLL.Custom();

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
            [Display(Name = "Id de tipo de documento")]
            public int TipoDocumentoId { get; set; }

            [Required(ErrorMessage = "Es necesario especificar este dato")]
            [Display(Name = "Id tipo de organización")]
            public int TipoOrganizacionId { get; set; }
        }

        public ActionResult Index()
        {
            return View(new FileModel());
        }

        [HttpPost]
        public FileResult Index(FileModel model)
        {

            if (ModelState.IsValid)
            {

                var file = Request.Files[0];
                var target = new MemoryStream();

                file.InputStream.CopyTo(target);

                var firmante = db.Firmante.FirstOrDefault(q => q.EsActivo);

                try
                {
                    //var pdf = _custom.SignPDF(0, model.Folio, target.ToArray(), file.FileName, firmante, true, model.TipoDocumentoId, model.TipoOrganizacionId);
                    var pdf = _custom.SignPDF(0, null, target.ToArray(), file.FileName, firmante, true, model.TipoDocumentoId, model.TipoOrganizacionId);
                    return File(pdf, System.Net.Mime.MediaTypeNames.Application.Octet, file.FileName);
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Se detectó un problema: " + ex.Message;
                }
            }

            return null;
        }
    }
}