using DAES.Infrastructure.SistemaIntegrado;
using DAES.Web.FrontOffice.Helper;
using Newtonsoft.Json.Linq;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace DAES.Web.FrontOffice.Controllers
{
    [Audit]
    public class VerificarDocumentoController : Controller
    {
        private SistemaIntegradoContext db = new SistemaIntegradoContext();
        private BLL.Custom _custom = new BLL.Custom();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search(int? id)
        {
            if (IsReCaptchValid())
            {
                if (!id.HasValue)
                {
                    ModelState.AddModelError(string.Empty, "Debe especificar el id del documento");
                }

                if (id.HasValue && !db.Documento.Any(q => q.DocumentoId == id.Value))
                {
                    ModelState.AddModelError(string.Empty, "Documento no encontrado");
                }

                if (id.HasValue && db.Documento.Any(q => q.DocumentoId == id.Value && q.TipoPrivacidadId == (int)Infrastructure.Enum.TipoPrivacidad.Privado))
                {
                    ModelState.AddModelError(string.Empty, "El documento no se puede verificar ya que es privado");
                }

                if (ModelState.IsValid)
                {
                    return RedirectToAction("Finish", new { id = id.Value });
                }

                return View();
            }
            else
            {
                return View("_Error", new Exception("Debe completar el captcha"));
            }



        }

        public ActionResult Finish(int id)
        {
            var model = db.Documento.FirstOrDefault(q => q.DocumentoId == id);
            if (model == null)
            {
                ModelState.AddModelError(string.Empty, "Documento no encontrado");
            }

            if (model != null && model.TipoPrivacidadId == (int)Infrastructure.Enum.TipoPrivacidad.Privado)
            {
                ModelState.AddModelError(string.Empty, "El documento no se puede verificar ya que es privado");
            }

            if (ModelState.IsValid)
            {
                return View(model);
            }

            return View();
        }

        public ActionResult Download(int id)
        {
            var model = db.Documento.Find(id);
            if (model == null)
            {
                return View("_Error", new Exception("Documento no encontrado."));
            }

            return File(model.Content, "application/pdf");
        }

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