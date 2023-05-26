using DAES.Web.FrontOffice.Models;
using System.Web.Mvc;

namespace DAES.Web.FrontOffice.Controllers
{
    public class SharedController : Controller
    {
        public ActionResult Error(string message)
        {
            ViewBag.Error = message;
            return View(message);
        }

        [ChildActionOnly]
        public ActionResult _LoginPartial()
        {
            ViewBag.User = Global.CurrentClaveUnica.User;
            return PartialView();
        }
    }
}