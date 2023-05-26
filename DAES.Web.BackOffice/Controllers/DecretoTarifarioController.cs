using DAES.Infrastructure.SistemaIntegrado;
using System.Linq;
using System.Web.Mvc;

namespace DAES.Web.BackOffice.Controllers
{

    public class DecretoTarifarioController : Controller
    {

        private SistemaIntegradoContext db = new SistemaIntegradoContext();

        public JsonResult GetCooperativa()
        {
            var model = db.Organizacion.Where(q =>
                q.TipoOrganizacionId == (int)Infrastructure.Enum.TipoOrganizacion.Cooperativa &&
                q.EstadoId != (int)Infrastructure.Enum.Estado.EnConstitucion &&
                q.EstadoId != (int)Infrastructure.Enum.Estado.RolAsignado).Select(q => new
                {
                    ID = q.OrganizacionId,
                    ROL = q.NumeroRegistro,
                    REGION_ID = q.RegionId,
                    REGION_DESCRIPCION = q.RegionId.HasValue ? q.Region.Nombre : string.Empty,
                    COMUNA_ID = q.ComunaId,
                    COMUNA_DESCRIPCION = q.ComunaId.HasValue ? q.Comuna.Nombre : string.Empty,
                    RUBRO_ID = q.RubroId,
                    RUBRO_DESCRIPCION = q.RubroId.HasValue ? q.Rubro.Nombre : string.Empty,
                    NOMBRE = q.RazonSocial,
                    DIRECCION = q.Direccion,
                    IMPORTANCIA_ECONOMICA = q.EsImportanciaEconomica
                }).ToList();

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRegion()
        {
            var model = db.Region.Select(q => new
            {
                q.RegionId,
                q.Nombre
            }).ToList();

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRubro()
        {
            var model = db.Rubro.Select(q => new
            {
                q.RubroId,
                q.Nombre
            }).ToList();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetComuna(int RegionId)
        {
            var model = db.Comuna.Where(q => q.RegionId == RegionId).Select(q => new
            {
                q.ComunaId,
                q.Nombre,
                q.RegionId
            }).ToList();
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}