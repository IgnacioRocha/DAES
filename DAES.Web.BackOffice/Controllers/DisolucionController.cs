using System;
using System.Collections.Generic;
using DAES.Infrastructure.SistemaIntegrado;
using DAES.Model.SistemaIntegrado;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DAES.Web.FrontOffice.Controllers
{
    public class DisolucionController : Controller
    {
        private SistemaIntegradoContext _db = new SistemaIntegradoContext();

        public ActionResult Index()
        {
            return null;
        }
    }
}