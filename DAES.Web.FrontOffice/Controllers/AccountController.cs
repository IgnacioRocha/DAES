using DAES.Web.FrontOffice.Models;
using System.Web.Mvc;

namespace DAES.Web.FrontOffice.Controllers
{
    public class AccountController : Controller
    {
        public virtual ActionResult Logoff()
        {
            Session.Abandon();
            ViewBag.User = null;
            Global.CurrentClaveUnica = new ClaveUnica();
            Global.CurrentClaveUnica.ClaveUnicaRequestAutorization = new ClaveUnicaRequestAutorization();

            return RedirectToAction("Index", "Home");
        }
    }
}