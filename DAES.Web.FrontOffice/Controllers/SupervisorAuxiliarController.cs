using DAES.Web.FrontOffice.Helper;
using DAES.Model.SistemaIntegrado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAES.Infrastructure.SistemaIntegrado;
using DAES.Web.FrontOffice.Models;

namespace DAES.Web.FrontOffice.Controllers
{
    [Audit]
    public class SupervisorAuxiliarController : Controller
    {
        private SistemaIntegradoContext db = new SistemaIntegradoContext();
        public class Search
        {
            public Search()
            {
                SupervisoresAuxiliares = new List<SupervisorAuxiliar>();
            }
            public string Query { get; set; }
            public ICollection<SupervisorAuxiliar> SupervisoresAuxiliares { get; set; }
        }

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

        public ActionResult UpdateSearch()
        {
            //TODO Aplicar Clave unica en modo produccion
            Global.CurrentClaveUnica.ClaveUnicaRequestAutorization.method = "Update";
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

        /*Funcion para gestionar la vista del Actualizar Supervisor*/
        public ActionResult UpdateSupervisor(int id)
        {
            SupervisorAuxiliar supervisorAuxiliar = db.SupervisorAuxiliars.Find(id);
            
            if(supervisorAuxiliar == null)
            {
                return HttpNotFound();
            }

            /*var repre = db.RepresentantesLegals.FirstOrDefault(q => q.SupervisorAuxiliarId == id);
            supervisorAuxiliar.RepresentanteLegals.Add(repre);*/

            ViewBag.TipoPersonaJuridicaId = new SelectList(db.TipoPersonaJuridicas.OrderBy(q => q.TipoPersonaJuridicaId), "TipoPersonaJuridicaId", "NombrePersonaJuridica");
            ViewBag.max_tamano_file = Properties.Settings.Default.max_tamano_file;
            return View(supervisorAuxiliar);
        }

        /*Funcion para gestioanr la busqueda de un supervisor para su posterior actualizacion*/
        public ActionResult Update(Search model)
        {
            IQueryable<SupervisorAuxiliar> query = db.SupervisorAuxiliars;

            if(!string.IsNullOrEmpty(model.Query))
            {
                query = query.Where(q => q.RazonSocial.Contains(model.Query));
            }

            model.SupervisoresAuxiliares = query.ToList();

            return View(model);
        }
        public ActionResult Index()
        {
            var super = new SupervisorAuxiliar();
            return View(super);
        }

        public ActionResult Create()
        {
            var super = new SupervisorAuxiliarTemporal() { };
            var representante = new RepresentanteLegal() { SupervisorAuxiliarId = super.SupervisorAuxiliarTempId };
            var extracto = new ExtractoAuxiliar() { SupervisorAuxiliarId = super.SupervisorAuxiliarTempId };
            var escritura = new EscrituraConstitucion() { SupervisorAuxiliarId = super.SupervisorAuxiliarTempId };
            var facultadas = new PersonaFacultada() { SupervisorAuxiliarId = super.SupervisorAuxiliarTempId };

            ViewBag.TipoPersonaJuridicaId = new SelectList(db.TipoPersonaJuridicas.OrderBy(q => q.TipoPersonaJuridicaId), "TipoPersonaJuridicaId", "NombrePersonaJuridica");
            ViewBag.max_tamano_file = Properties.Settings.Default.max_tamano_file;
            db.SupervisorAuxiliarTemporals.Add(super);
            /*db.RepresentantesLegals.Add(representante);
            db.ExtractoAuxiliars.Add(extracto);
            db.EscrituraConstitucions.Add(escritura);
            db.PersonaFacultadas.Add(facultadas);
            db.SaveChanges();*/

            super.RepresentanteLegals.Add(representante);
            super.ExtractoAuxiliars.Add(extracto);
            super.EscrituraConstitucionModificaciones.Add(escritura);
            super.PersonaFacultadas.Add(facultadas);

            return View(super);
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

            if (repre != null)
            {
                db.RepresentantesLegals.Remove(repre);
                db.SaveChanges();
            }


            return PartialView("_Representante", super);
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
    }
}