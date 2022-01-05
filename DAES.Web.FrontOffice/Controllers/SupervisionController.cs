using DAES.Infrastructure.SistemaIntegrado;
using DAES.Model.SistemaIntegrado;
using DAES.Web.FrontOffice.Models;
using System.Linq;
using System.Web.Mvc;

namespace DAES.Web.FrontOffice.Controllers
{
    public class SupervisionController : Controller
    {
        private SistemaIntegradoContext db = new SistemaIntegradoContext();
        public ActionResult Start()
        {
            //TODO Aplicar Clave unica en modo produccion
            Global.CurrentClaveUnica.ClaveUnicaRequestAutorization.method = "Create";

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

        public ActionResult Create()
        {
            var super = new SupervisorAuxiliar() { };
            var representante = new RepresentanteLegal() { };
            var extracto = new ExtractoAuxiliar() { };
            var escritura = new EscrituraConstitucion() { };
            var facultadas = new PersonaFacultada() { };

            ViewBag.TipoPersonaJuridicaId = new SelectList(db.TipoPersonaJuridicas.OrderBy(q => q.NombrePersonaJuridica), "TipoPersonaJuridicaId", "NombrePersonaJuridica");
            ViewBag.max_tamano_file = Properties.Settings.Default.max_tamano_file;
            
            super.RepresentanteLegals.Add(representante);
            super.ExtractoAuxiliars.Add(extracto);
            super.EscrituraConstitucionModificaciones.Add(escritura);
            super.PersonaFacultadas.Add(facultadas);
            return View();
        }

        public ActionResult RepresentanteAdd(int SuperId)
        {
            var model = db.SupervisorAuxiliars.Find(SuperId);
            var repre = new RepresentanteLegal() { SupervisorAuxiliarId = model.SupervisorAuxiliarId };

            db.RepresentantesLegals.Add(repre);
            db.SaveChanges();
            return PartialView("Create", model);
        }

        public ActionResult ConstitucionAdd(int SuperId)
        {
            var model = db.SupervisorAuxiliars.Find(SuperId);
            var modificacion = new EscrituraConstitucion() { SupervisorAuxiliarId = model.SupervisorAuxiliarId };
            db.EscrituraConstitucions.Add(modificacion);
            db.SaveChanges();

            return PartialView("Create", model);
        }
        public ActionResult PersonaFacultadaAdd(int SuperId)
        {
            var model = db.PersonaFacultadas.Find(SuperId);
            var facultada = new PersonaFacultada() { SupervisorAuxiliarId = model.SupervisorAuxiliarId };
            db.PersonaFacultadas.Add(facultada);
            db.SaveChanges();

            return PartialView("Create", model);
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}