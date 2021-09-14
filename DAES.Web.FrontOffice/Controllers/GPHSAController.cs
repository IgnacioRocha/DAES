using DAES.Web.FrontOffice.Helper;
using DAES.Web.FrontOffice.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace DAES.Web.FrontOffice.Controllers
{
    public class DTOInformeHSAResponse
    {
        public DTOInformeHSAResponse()
        {
        }
        public bool ok { get; set; }
        public string error { get; set; }
        public int id { get; set; }
    }

    public class DTOProcesoResponse
    {
        public DTOProcesoResponse()
        {
        }

        public bool ok { get; set; }
        public string error { get; set; }

        public IEnumerable<DTOInformeHSA> DTOInformeHSA { get; set; }

    }

    public class DTOInformeHSA
    {
        public DTOInformeHSA()
        {
            DTOArchivos = new List<DTOArchivo>();
        }

        public int InformHSAId { get; set; }

        [Display(Name = "Fecha solicitud")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime FechaSolicitud { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Es necesario especificar el dato Actividades desde")]
        [Display(Name = "Actividades desde")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime FechaDesde { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el dato Actividades hasta")]
        [Display(Name = "Actividades hasta")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime FechaHasta { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el dato RUT")]
        [Display(Name = "RUT")]
        public int RUT { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el dato Nombre")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el dato Unidad de Desempeño")]
        [Display(Name = "Unidad de Desempeño")]
        public string Unidad { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el dato Jefatura responsable")]
        [Display(Name = "Jefatura responsable")]
        public string NombreJefatura { get; set; }

        [Display(Name = "Con jornada?")]
        public bool ConJornada { get; set; } = false;

        [Required(ErrorMessage = "Es necesario especificar el dato Funciones establecidas en el contrato, numeradas")]
        [Display(Name = "Funciones establecidas en el contrato, numeradas.")]
        [DataType(DataType.MultilineText)]
        public string Funciones { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el dato Actividades desarrolladas en el periodo")]
        [Display(Name = "Actividades desarrolladas en el periodo")]
        [DataType(DataType.MultilineText)]
        public string Actividades { get; set; }

        [Display(Name = "Observaciones")]
        [DataType(DataType.MultilineText)]
        public string Observaciones { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el dato Número de boleta")]
        [Display(Name = "Número de boleta")]
        public string NumeroBoleta { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el dato Fecha de emisión de boleta")]
        [Display(Name = "Fecha de emisión de boleta")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime FechaBoleta { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el dato Archivo Boleta")]
        [Display(Name = "Archivo de boleta")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase Boleta { get; set; }

        public string Email { get; set; }
        public string Estado { get; set; }

        public List<DTOArchivo> DTOArchivos { get; set; }
    }


    public class DTOArchivo
    {
        public DTOArchivo()
        {
        }

        public string FileString { get; set; }
        public string Filename { get; set; }
        public string Filetype { get; set; }
    }

    public class DTOUserInfoResponse
    {
        public DTOUserInfoResponse()
        {

        }

        public bool ok { get; set; }
        public string error { get; set; }
        public string Nombre { get; set; }
        public string Unidad { get; set; }
        public string NombreJefatura { get; set; }
        public DateTime FechaDesde { get; set; }
        public string Email { get; set; }
    }

    [Audit]
    public class GPHSAController : Controller
    {
        public ActionResult Index()
        {
            if (!Global.CurrentClaveUnica.IsAutenticated)
                return View("_Error", new Exception("Usuario no autenticado con Clave Única."));

            var url = Properties.Settings.Default.url_gestion_procesos + "/InformeHSA/GetInformes/" + Global.CurrentClaveUnica.ClaveUnicaUser.RolUnico.numero;
            var client = new RestClient(url);
            var response = client.Execute(new RestRequest());
            DTOProcesoResponse procesos = JsonConvert.DeserializeObject<DTOProcesoResponse>(response.Content);

            if (!procesos.ok)
                return View("_Error", new Exception(procesos.error));

            return View(procesos.DTOInformeHSA);
        }

        public ActionResult Create()
        {
            if (!Global.CurrentClaveUnica.IsAutenticated)
                return View("_Error", new Exception("Usuario no autenticado con Clave Única."));

            var url = Properties.Settings.Default.url_gestion_procesos + "/InformeHSA/GetHonorarioByRUT/" + Global.CurrentClaveUnica.ClaveUnicaUser.RolUnico.numero;
            var client = new RestClient(url);
            var response = client.Execute(new RestRequest());
            DTOUserInfoResponse honorario = JsonConvert.DeserializeObject<DTOUserInfoResponse>(response.Content);

            if (!honorario.ok)
                return View("_Error", new Exception(honorario.error));

            var model = new DTOInformeHSA();
            model.RUT = Global.CurrentClaveUnica.ClaveUnicaUser.RolUnico.numero;
            model.Nombre = honorario.Nombre.Trim();
            model.Unidad = honorario.Unidad.Trim();
            model.NombreJefatura = honorario.NombreJefatura.Trim();
            model.FechaDesde = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            model.FechaHasta = model.FechaDesde.AddMonths(1).AddDays(-1);
            model.FechaBoleta = model.FechaHasta;
            model.Email = honorario.Email.Trim();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DTOInformeHSA model)
        {
            if (Request.Files.Count == 0)
                ModelState.AddModelError(string.Empty, "Es necesario especificar el archivo con la boleta");

            if (model == null)
                ModelState.AddModelError(string.Empty, "Informe vacio");
            if (string.IsNullOrEmpty(model.Actividades))
                ModelState.AddModelError(string.Empty, "Debe indicar las actividades");
            if (string.IsNullOrEmpty(model.Funciones))
                ModelState.AddModelError(string.Empty, "Debe indicar las funciones");
            if (string.IsNullOrEmpty(model.NumeroBoleta))
                ModelState.AddModelError(string.Empty, "Debe indicar el número de boleta");
            if (string.IsNullOrEmpty(model.Email))
                ModelState.AddModelError(string.Empty, "Debe indicar el email de funcionario");
            if (string.IsNullOrEmpty(model.Nombre))
                ModelState.AddModelError(string.Empty, "Nombre de funcionario no encontrado");
            if (string.IsNullOrEmpty(model.NombreJefatura))
                ModelState.AddModelError(string.Empty, "Nombre de la jefatura no encontrado");
            if (string.IsNullOrEmpty(model.Unidad))
                ModelState.AddModelError(string.Empty, "Unidad del funcionario no encontrada");

            if (ModelState.IsValid)
            {
                var obj = new DTOInformeHSA();
                obj.Actividades = model.Actividades;
                obj.ConJornada = model.ConJornada;
                obj.Email = model.Email;
                obj.FechaBoleta = model.FechaBoleta;
                obj.FechaDesde = model.FechaDesde;
                obj.FechaHasta = model.FechaHasta;
                obj.FechaSolicitud = model.FechaSolicitud;
                obj.Funciones = model.Funciones;
                obj.Nombre = model.Nombre;
                obj.NombreJefatura = model.NombreJefatura;
                obj.NumeroBoleta = model.NumeroBoleta;
                obj.Observaciones = model.Observaciones;
                obj.RUT = model.RUT;
                obj.Unidad = model.Unidad;


                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFileBase file = Request.Files[i];
                    using (MemoryStream ms = new MemoryStream())
                    {
                        file.InputStream.CopyTo(ms);

                        obj.DTOArchivos.Add(new DTOArchivo
                        {
                            FileString = Convert.ToBase64String(ms.ToArray(), 0, ms.ToArray().Length),
                            Filename = file.FileName,
                            Filetype = file.ContentType
                        });
                    }
                }

                try
                {
                    var client = new RestClient(Properties.Settings.Default.url_gestion_procesos + "/InformeHSA/StartProcess/");
                    var request = new RestRequest(Method.POST);
                    request.AddJsonBody(obj);
                    var response = client.Execute(request);
                    var returnValue = JsonConvert.DeserializeObject<DTOInformeHSAResponse>(response.Content);
                    if (returnValue.ok)
                        return RedirectToAction("Finish", new { returnValue.id });

                    ModelState.AddModelError(string.Empty, returnValue.error.Replace("\"", ""));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            return View(model);
        }

        public ActionResult Finish(int id)
        {
            return View(id);
        }
    }
}