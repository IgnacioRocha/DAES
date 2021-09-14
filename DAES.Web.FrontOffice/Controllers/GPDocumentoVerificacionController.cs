using DAES.Infrastructure;
using DAES.Web.FrontOffice.Helper;
using Newtonsoft.Json;
using reCAPTCHA.MVC;
using RestSharp;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DAES.Web.FrontOffice.Controllers
{
    [Audit]
    public class GPDocumentoVerificacionController : Controller
    {
        public class DTODocumento
        {
            public DTODocumento()
            {

            }

            [Required(ErrorMessage = "Es necesario especificar el código del documento")]
            [Display(Name = "Código de documento")]
            public string Id { get; set; }

            [Display(Name = "Contenido")]
            public string Archivo { get; set; }

            [Display(Name = "Nombre de archivo")]
            public string Nombre { get; set; }
            [Display(Name = "Fecha emisión")]
            public DateTime FechaEmision { get; set; }
            public bool OK { get; set; }
            public string Error { get; set; }
            public string Autor { get; set; }
            public string Folio { get; set; }

            [Display(Name = "Archivo pdf")]
            public byte[] Pdf => !this.Archivo.IsNullOrWhiteSpace() ? System.Text.Encoding.Default.GetBytes(this.Archivo) : null;
        }

        public ActionResult Index()
        {
            return View(new DTODocumento());
        }

        [HttpPost]
        public ActionResult Index(DTODocumento model)
        {
            if (ModelState.IsValid)
                return RedirectToAction("Details", new { model.Id });

            return View(model);
        }

        public ActionResult Details(string id)
        {
            var url = Properties.Settings.Default.url_gestion_procesos + "/Documento/GetById/" + id;
            var client = new RestClient(url);
            var response = client.Execute(new RestRequest());
            var documento = JsonConvert.DeserializeObject<DTODocumento>(response.Content);

            if (!documento.OK)
                return View("_Error", new Exception(documento.Error));

            return View(documento);
        }

        [HttpPost]
        [CaptchaValidator(ErrorMessage = "Captcha inválido.", RequiredMessage = "El código captcha es requerido.")]
        public ActionResult Details(DTODocumento model, bool captchaValid)
        {
            if (!ModelState.IsValid || !captchaValid)
                return Redirect(Request.UrlReferrer.ToString());

            var url = Properties.Settings.Default.url_gestion_procesos + "/Documento/GetById/" + model.Id;
            var client = new RestClient(url);
            var response = client.Execute(new RestRequest());
            var documento = JsonConvert.DeserializeObject<DTODocumento>(response.Content);

            return File(documento.Pdf, System.Net.Mime.MediaTypeNames.Application.Octet, documento.Nombre);
        }
    }
}