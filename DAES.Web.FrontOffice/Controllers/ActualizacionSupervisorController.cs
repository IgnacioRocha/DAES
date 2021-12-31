using DAES.Infrastructure.SistemaIntegrado;
using DAES.Web.FrontOffice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DAES.Web.FrontOffice.Controllers
{
    public class ActualizacionSupervisorController : Controller
    {        
        public ActionResult Start()
        {
            Global.CurrentClaveUnica.ClaveUnicaRequestAutorization.method = "Update";

            /*Global.CurrentClaveUnica.ClaveUnicaUser.name = new Name
            {
                nombres = new System.Collections.Generic.List<string> { "IGNACIO", "ALFREDO" },
                apellidos = new System.Collections.Generic.List<string> { "ROCHA", "PAVEZ" }
            };
            Global.CurrentClaveUnica.ClaveUnicaUser = new ClaveUnicaUser();

            Global.CurrentClaveUnica.ClaveUnicaUser.RolUnico = new RolUnico
            {
                numero = 17957898,
                DV = "0",
                tipo = "RUN"
            };*/
            return RedirectToAction(Global.CurrentClaveUnica.ClaveUnicaRequestAutorization.method, Global.CurrentClaveUnica.ClaveUnicaRequestAutorization.controller);
            /*return Redirect(Global.CurrentClaveUnica.ClaveUnicaRequestAutorization.uri);*/
        }

        // GET: ActualizacionSupervisor
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Update()
        {


            return View();
        }

        public void Search()
        {

        }
    }
}