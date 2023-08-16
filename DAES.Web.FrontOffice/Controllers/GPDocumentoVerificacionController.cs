using DAES.Infrastructure;
using DAES.Infrastructure.SistemaIntegrado;
using DAES.Web.FrontOffice.Helper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace DAES.Web.FrontOffice.Controllers
{
    [Audit]
    public class GPDocumentoVerificacionController : Controller
    {
        private SistemaIntegradoContext db = new SistemaIntegradoContext();
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
            bool esDaes = false;
            bool esGp = false;
            int a = int.Parse(id);

            //var doc = db.Documento.Where(q => q.Activo && q.DocumentoId.Equals(id)).First();
            var doc = db.Documento.Where(q => q.DocumentoId == a).FirstOrDefault();


            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            var url = Properties.Settings.Default.url_gestion_procesos + "/Documento/GetById/" + id;
            var client = new RestClient(url);
            var response = client.Execute(new RestRequest());


            var documento = JsonConvert.DeserializeObject<DTODocumento>(response.Content);

            if (!documento.OK)
                return View("_Error", new Exception(documento.Error));

            return View(documento);
        }


        [HttpPost]
        public ActionResult Details(DTODocumento model/*DTODocumento model, bool captchaValid*/)
        {
            if (IsReCaptchValid())
            {
                string messagetodb = "correcto";

                var url = Properties.Settings.Default.url_gestion_procesos + "/Documento/GetById/" + model.Id;
                var client = new RestClient(url);
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                var response = client.Execute(new RestRequest());
                var documento = JsonConvert.DeserializeObject<DTODocumento>(response.Content);

                return File(documento.Pdf, System.Net.Mime.MediaTypeNames.Application.Octet, documento.Nombre);
            }
            else
            {
                return View("_Error", new Exception("Debe completar el captcha"));
            }

            //if (!ModelState.IsValid || !captchaValid)
            //    return Redirect(Request.UrlReferrer.ToString());

            //System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            //var url = Properties.Settings.Default.url_gestion_procesos + "/Documento/GetById/" + model.Id;
            //var client = new RestClient(url);
            //var response = client.Execute(new RestRequest());
            //var documento = JsonConvert.DeserializeObject<DTODocumento>(response.Content);

        }

        //Método para validar Captcha
        public bool IsReCaptchValid()
        {
            var result = false;
            var captchaResponse = Request.Form["g-recaptcha-response"];
            var secretKey = ConfigurationManager.AppSettings["SecretKey"];
            var apiUrl = "https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}";
            var requestUri = string.Format(apiUrl, secretKey, captchaResponse);
            var request = (HttpWebRequest)WebRequest.Create(requestUri);

            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                {
                    JObject jResponse = JObject.Parse(stream.ReadToEnd());
                    var isSuccess = jResponse.Value<bool>("success");
                    result = (isSuccess) ? true : false;
                }
            }
            return result;
        }
    }
}