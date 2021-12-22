using DAES.Web.FrontOffice.Helper;
using DAES.Web.FrontOffice.Models;
using System.Web.Mvc;

namespace DAES.Web.FrontOffice.Controllers
{

    [Audit]
    public class Articulo90Controller : Controller
    {

        public Articulo90Controller()
        {
            ViewBag.User = Global.CurrentClaveUnica.User;
        }

        private ActionResult Redirect()
        {
            //activar en desarrollo, bypass de clave única
            //Global.CurrentClaveUnica.ClaveUnicaUser = new ClaveUnicaUser();
            //Global.CurrentClaveUnica.ClaveUnicaUser.name = new Name
            //{
            //    nombres = new System.Collections.Generic.List<string> { "DESA", "DESA" },
            //    apellidos = new System.Collections.Generic.List<string> { "DESA", "DESA" }
            //};
            //Global.CurrentClaveUnica.ClaveUnicaUser.RolUnico = new RolUnico
            //{
            //    numero = 44444444,
            //    DV = "4",
            //    tipo = "RUN"
            //};
            //return RedirectToAction(Global.CurrentClaveUnica.ClaveUnicaRequestAutorization.method, Global.CurrentClaveUnica.ClaveUnicaRequestAutorization.controller);

            //activar en testing y produccion
            return Redirect(Global.CurrentClaveUnica.ClaveUnicaRequestAutorization.uri);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Articulo90IncisoPrimero()
        {
            Global.CurrentClaveUnica.ClaveUnicaRequestAutorization.controller = "Articulo90IncisoPrimero";
            Global.CurrentClaveUnica.ClaveUnicaRequestAutorization.method = "Index";
            return Redirect();
        }

        public ActionResult Articulo90IncisoSegundo()
        {
            Global.CurrentClaveUnica.ClaveUnicaRequestAutorization.controller = "Articulo90IncisoSegundo";
            Global.CurrentClaveUnica.ClaveUnicaRequestAutorization.method = "Index";
            return Redirect();
        }
    }
}