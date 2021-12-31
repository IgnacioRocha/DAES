using DAES.Infrastructure.SistemaIntegrado;
using DAES.Model.SistemaIntegrado;
using DAES.Web.FrontOffice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DAES.Web.FrontOffice.Controllers
{
    public class RegistroSupervisorController : Controller
    {
        private SistemaIntegradoContext db = new SistemaIntegradoContext();
        public ActionResult Start()
        {
            //TODO Aplicar Clave unica en modo produccion
            Global.CurrentClaveUnica.ClaveUnicaRequestAutorization.method = "Create";
            /*Global.CurrentClaveUnica.ClaveUnicaRequestAutorization.controller = "SupervisionAuxiliar";*/

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

            ViewBag.TipoPersonaJuridicaId = new SelectList(db.TipoPersonaJuridicas.OrderBy(q => q.TipoPersonaJuridicaId), "TipoPersonaJuridicaId", "NombrePersonaJuridica");
            db.SupervisorAuxiliars.Add(super);
            /*db.RepresentantesLegals.Add(representante);
            db.ExtractoAuxiliars.Add(extracto);
            db.EscrituraConstitucions.Add(escritura);
            db.PersonaFacultadas.Add(facultadas);*/
            /*db.SaveChanges();*/

            super.RepresentanteLegals.Add(representante);
            super.ExtractoAuxiliars.Add(extracto);
            super.EscrituraConstitucionModificaciones.Add(escritura);
            super.PersonaFacultadas.Add(facultadas);
            
            return View(super);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SupervisorAuxiliar SuperAux)
        {
            if(ModelState.IsValid)
            {
                db.SupervisorAuxiliars.Add(SuperAux);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TipoPersonaJuridicaId = new SelectList(db.TipoPersonaJuridicas.OrderBy(q => q.NombrePersonaJuridica).ToList(), "TipoPersonaJuridicaId", "NombrePersonaJuridica");

            return View(SuperAux);
        }        

        public ActionResult RepresentanteAdd(int SuperId)
        {
            var model = db.SupervisorAuxiliars.Find(SuperId);
            var repre = db.RepresentantesLegals.Add(new RepresentanteLegal() { SupervisorAuxiliarId = SuperId });
            
            db.SaveChanges();
            return PartialView("_Representantes", model);
        }

        public ActionResult DeleteRepresentante(int RepreId, int SuperId)
        {
            var repre = db.RepresentantesLegals.FirstOrDefault(q => q.RepresentanteLegalId == RepreId);
            var super = db.SupervisorAuxiliars.Find(SuperId);

            if(repre!=null)
            {
                db.RepresentantesLegals.Remove(repre);
                db.SaveChanges();
            }


            return PartialView("_RepresentanteLegal", super);
        }
        public ActionResult ConstitucionAdd(int SuperId)
        {
            var model = db.SupervisorAuxiliars.Find(SuperId);
            var modificacion = new EscrituraConstitucion() { SupervisorAuxiliarId = model.SupervisorAuxiliarId };
            db.EscrituraConstitucions.Add(modificacion);
            db.SaveChanges();

            return PartialView("_Constitucion", model);
        }

        public ActionResult ConstitucionDelete(int ConstiId, int SuperId)
        {
            var consti = db.EscrituraConstitucions.FirstOrDefault(q => q.EscrituraConstitucionId == ConstiId);
            var super = db.SupervisorAuxiliars.Find(SuperId);

            if (consti != null)
            {
                db.EscrituraConstitucions.Remove(consti);
                db.SaveChanges();
            }

            return PartialView("_Constitucion", super);
        }

        public ActionResult PersonaFacultadaAdd(int SuperId)
        {
            var model = db.SupervisorAuxiliars.Find(SuperId);
            var facultada = new PersonaFacultada() { SupervisorAuxiliarId = model.SupervisorAuxiliarId };
            db.PersonaFacultadas.Add(facultada);
            db.SaveChanges();

            return PartialView("_PersonasFacultadas", model);
        }

        public ActionResult DeleteFacultada(int FacultadaId, int SuperId)
        {
            var facultada = db.PersonaFacultadas.FirstOrDefault(q => q.PersonaFacultadaId == FacultadaId);
            var super = db.SupervisorAuxiliars.Find(SuperId);

            if (facultada != null)
            {
                db.PersonaFacultadas.Remove(facultada);
                db.SaveChanges();
            }


            return PartialView("_PersonasFacultadas", super);
        }

        // GET: RegistroSupervisor
        public ActionResult Index()
        {
            return View();
        }
    }
}