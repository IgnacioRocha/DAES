using DAES.Infrastructure.SistemaIntegrado;
using DAES.Model.DTO;
using DAES.Web.FrontOffice.Helper;
using System.Linq;
using System.Web.Mvc;

namespace DAES.Web.FrontOffice.Controllers
{
    [Audit]
    public class DirectorioController : Controller
    {
        private SistemaIntegradoContext _db = new SistemaIntegradoContext();

        public ActionResult DirectorioAdd(DTOAsambleaOrdinaria model)
        {
            var directorio = new DTODirectorio()
            {
                OrganizacionId = model.OrganizacionId,
                NombreCompleto = "?",
                GeneroId = (int)DAES.Infrastructure.Enum.Genero.SinGenero,
                CargoId = 135
            };

            model.Directorio.Add(directorio);

            ViewBag.CargoId = new SelectList(_db.Cargo.OrderBy(q => q.Nombre), "CargoId", "Nombre");
            ViewBag.GeneroId = new SelectList(_db.Genero.OrderBy(q => q.Nombre), "GeneroId", "Nombre");

            return PartialView("_DirectorioEdit", model);
        }
    }
}