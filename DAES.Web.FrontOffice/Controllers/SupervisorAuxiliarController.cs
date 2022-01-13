using DAES.Web.FrontOffice.Helper;
using DAES.Model.SistemaIntegrado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAES.Infrastructure.SistemaIntegrado;
using DAES.Web.FrontOffice.Models;
using System.ComponentModel.DataAnnotations;
using DAES.Infrastructure;
using System.IO;

namespace DAES.Web.FrontOffice.Controllers
{
    [Audit]
    public class SupervisorAuxiliarController : Controller
    {
        private SistemaIntegradoContext db = new SistemaIntegradoContext();
        private BLL.Custom _custom = new BLL.Custom();
        public class Search
        {
            public Search()
            {
                SupervisoresAuxiliares = new List<SupervisorAuxiliar>();
            }
            [Display(Name="Razon Social")]
            public string Query { get; set; }
            public ICollection<SupervisorAuxiliar> SupervisoresAuxiliares { get; set; }
        }

        public ActionResult Start()
        {
            //TODO Aplicar Clave unica en modo produccion
            Global.CurrentClaveUnica.ClaveUnicaRequestAutorization.method = "Create";
            Global.CurrentClaveUnica.ClaveUnicaRequestAutorization.controller = "SupervisorAuxiliar";

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
            return RedirectToAction(Global.CurrentClaveUnica.ClaveUnicaRequestAutorization.method, Global.CurrentClaveUnica.ClaveUnicaRequestAutorization.controller);
            //return Redirect(Global.CurrentClaveUnica.ClaveUnicaRequestAutorization.uri);
        }

        public ActionResult UpdateSearch()
        {
            //TODO Aplicar Clave unica en modo produccion
            Global.CurrentClaveUnica.ClaveUnicaRequestAutorization.method = "Update";
            Global.CurrentClaveUnica.ClaveUnicaRequestAutorization.controller = "SupervisorAuxiliar";

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
            return RedirectToAction(Global.CurrentClaveUnica.ClaveUnicaRequestAutorization.method, Global.CurrentClaveUnica.ClaveUnicaRequestAutorization.controller);
            //return Redirect(Global.CurrentClaveUnica.ClaveUnicaRequestAutorization.uri);
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
        
        public ActionResult Index()
        {            
            return View();
        }

        public ActionResult Create()
        {
            /*var super = new SupervisorAuxiliarTemporal() { };
            var representante = new RepresentanteLegal() { SupervisorAuxiliarId = super.SupervisorAuxiliarId };
            var escritura = new EscrituraConstitucion() { SupervisorAuxiliarId = super.SupervisorAuxiliarId };
            var facultadas = new PersonaFacultada() { SupervisorAuxiliarId = super.SupervisorAuxiliarId };*/

            var super = new SupervisorAuxiliar() 
            {
                /*RutSolicitante = string.Concat(Global.CurrentClaveUnica.ClaveUnicaUser.RolUnico.numero, Global.CurrentClaveUnica.ClaveUnicaUser.RolUnico.DV),
                NombreSolicitante = string.Join(" ", Global.CurrentClaveUnica.ClaveUnicaUser.name.nombres).ToUpperNull(),
                ApellidosSolicitante = string.Join(" ", Global.CurrentClaveUnica.ClaveUnicaUser.name.apellidos).ToUpperNull()*/
            };
            var extracto = new ExtractoAuxiliar() { SupervisorAuxiliarId = super.SupervisorAuxiliarId };

            ViewBag.TipoPersonaJuridicaId = new SelectList(db.TipoPersonaJuridicas.OrderBy(q => q.TipoPersonaJuridicaId), "TipoPersonaJuridicaId", "NombrePersonaJuridica");
            ViewBag.max_tamano_file = Properties.Settings.Default.max_tamano_file;
            /*db.SupervisorAuxiliars.Add(new SupervisorAuxiliarTemporal() { });*/
            db.SupervisorAuxiliars.Add(super);
            db.ExtractoAuxiliars.Add(extracto);
            super.ExtractoAuxiliars.Add(extracto);
            /*super.RepresentanteLegals.Add(representante);
            super.ExtractoAuxiliars.Add(extracto);
            super.EscrituraConstitucionModificaciones.Add(escritura);
            super.PersonaFacultadas.Add(facultadas);*/
            /*db.RepresentantesLegals.Add(representante);
            db.ExtractoAuxiliars.Add(extracto);
            db.EscrituraConstitucions.Add(escritura);
            db.PersonaFacultadas.Add(facultadas);*/

            db.SaveChanges();
            return View(super);
        }

        [HttpPost]
        public ActionResult Create(SupervisorAuxiliar model)
        {
            ViewBag.TipoPersonaJuridicaId = new SelectList(db.TipoPersonaJuridicas.OrderBy(q => q.TipoPersonaJuridicaId), "TipoPersonaJuridicaId", "NombrePersonaJuridica");
            ViewBag.max_tamano_file = Properties.Settings.Default.max_tamano_file;
            if(ModelState.IsValid)
            {
                var proceso = new Proceso()
                {
                    DefinicionProcesoId = (int)DAES.Infrastructure.Enum.DefinicionProceso.IngresoSupervisorAuxiliar
                };

                proceso.Solicitante = new Solicitante()
                {
                    /* TODO 
                     * Modificar cuando este en Testing
                    Rut = model.Rut,
                    Nombres = model.NombreSolicitante,
                    Apellidos=model.ApellidosSolicitante,
                    Email=model.MailSolicitante*/
                    Rut = model.Rut,
                    Nombres = model.RazonSocial,
                    Email = model.CorreoElectronico
                };

                for (int i=0;i<Request.Files.Count;i++)
                {
                    HttpPostedFileBase file = Request.Files[i];
                    var target = new MemoryStream();
                    using(MemoryStream ms = new MemoryStream())
                    {
                        file.InputStream.CopyTo(ms);
                        string fileName = Path.GetFileName(file.FileName);
                        string fileEx = System.IO.Path.GetExtension(fileName);
                        fileEx = fileEx.ToLower();

                        model.ProcesoId = proceso.ProcesoId;

                        proceso.SupervisorAuxiliars.Add(model);

                        if (file.FileName == "")
                        {
                            ViewBag.errorMessage = "*Error al enviar documentos, faltan documentos por adjuntar";
                            return View(model);
                        }

                        else if (fileEx != ".pdf" && fileEx != ".xls" && fileEx != ".doc" && fileEx != ".docx")
                        {

                            ViewBag.errorMessage = "*Error al enviar documento(s), solo se aceptan archivos en formato PDF, Word y Excel (sin macros)";
                            return View(model);
                        }
                        else
                        {
                            proceso.Documentos.Add(new Documento()
                            {
                                FechaCreacion = DateTime.Now,
                                Autor = model.CorreoElectronico,
                                Content = ms.ToArray(),
                                FileName = file.FileName,
                                TipoDocumentoId = (int)Infrastructure.Enum.TipoDocumento.SinClasificar,
                                TipoPrivacidadId = (int)DAES.Infrastructure.Enum.TipoPrivacidad.Privado
                            });
                        }
                    }
                }
                try
                {
                    var pro = _custom.ProcesoStart(proceso);
                    TempData["Success"] = string.Format("Trámite número {0} terminado correctamente. Se ha enviado una notificación al correo {1} con los detalles.", pro.ProcesoId, proceso.Solicitante.Email);
                    return RedirectToAction("Finish");
                }
                catch (Exception ex)
                {
                    return View("_Error", ex);
                }
            }

            return View(model);
        }

        /*Funcion para gestionar la busqueda de un supervisor para su posterior actualizacion*/
        public ActionResult Update(Search model)
        {
            IQueryable<SupervisorAuxiliar> query = db.SupervisorAuxiliars;

            if (!string.IsNullOrEmpty(model.Query))
            {
                query = query.Where(q => q.RazonSocial.Contains(model.Query));
            }

            model.SupervisoresAuxiliares = query.ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateSupervisor(SupervisorAuxiliar model)
        {
            ViewBag.TipoPersonaJuridicaId = new SelectList(db.TipoPersonaJuridicas.OrderBy(q => q.TipoPersonaJuridicaId), "TipoPersonaJuridicaId", "NombrePersonaJuridica");
            ViewBag.max_tamano_file = Properties.Settings.Default.max_tamano_file;
            ViewBag.errorMessage = string.Empty;

            if (ModelState.IsValid)
            {
                var proceso = new Proceso()
                {
                    DefinicionProcesoId = (int)DAES.Infrastructure.Enum.DefinicionProceso.ActualizacionSupervisorAuxiliar
                };

                proceso.Solicitante = new Solicitante()
                {
                    /* TODO 
                     * Modificar cuando este en Testing
                    Rut = model.Rut,
                    Nombres = model.NombreSolicitante,
                    Apellidos=model.ApellidosSolicitante,
                    Email=model.MailSolicitante*/
                    Rut = model.Rut,
                    Nombres = model.RazonSocial,
                    Email = model.CorreoElectronico
                };

                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFileBase file = Request.Files[i];
                    var target = new MemoryStream();
                    using (MemoryStream ms = new MemoryStream())
                    {
                        file.InputStream.CopyTo(ms);
                        string fileName = Path.GetFileName(file.FileName);
                        string fileEx = System.IO.Path.GetExtension(fileName);
                        fileEx = fileEx.ToLower();

                        proceso.SupervisorAuxiliars.Add(model);

                        if (file.FileName == "")
                        {
                            ViewBag.errorMessage = "*Error al enviar documentos, faltan documentos por adjuntar";
                            return View(model);
                        }

                        else if (fileEx != ".pdf" && fileEx != ".xls" && fileEx != ".doc" && fileEx != ".docx")
                        {

                            ViewBag.errorMessage = "*Error al enviar documento(s), solo se aceptan archivos en formato PDF, Word y Excel (sin macros)";
                            return View(model);
                        }
                        else
                        {
                            proceso.Documentos.Add(new Documento()
                            {
                                FechaCreacion = DateTime.Now,
                                Autor = model.CorreoElectronico,
                                Content = ms.ToArray(),
                                FileName = file.FileName,
                                TipoDocumentoId = (int)Infrastructure.Enum.TipoDocumento.SinClasificar,
                                TipoPrivacidadId = (int)DAES.Infrastructure.Enum.TipoPrivacidad.Privado
                            });

                        }
                    }
                }
                try
                {

                    var pro = _custom.ProcesoStart(proceso);
                    TempData["Success"] = string.Format("Trámite número {0} terminado correctamente. Se ha enviado una notificación al correo {1} con los detalles.", pro.ProcesoId, proceso.Solicitante.Email);
                    return RedirectToAction("Finish");
                }
                catch (Exception ex)
                {
                    return View("_Error", ex);
                }
            }

            return View(model);
        }

        #region Funciones Representante
        public ActionResult RepresentanteAdd(int SupervisorAuxiliarId)
        {
            ViewBag.TipoPersonaJuridicaId = new SelectList(db.TipoPersonaJuridicas.OrderBy(q => q.TipoPersonaJuridicaId), "TipoPersonaJuridicaId", "NombrePersonaJuridica");
            ViewBag.max_tamano_file = Properties.Settings.Default.max_tamano_file;
            var model = db.SupervisorAuxiliars.Find(SupervisorAuxiliarId);
            var representante = new RepresentanteLegal() { SupervisorAuxiliarId = SupervisorAuxiliarId };
            db.RepresentantesLegals.Add(representante);

            db.SaveChanges();
            return PartialView("_Representantes", model);
        }
        public ActionResult RepresentanteUpdateAdd(int SupervisorAuxiliarId)
        {
            ViewBag.TipoPersonaJuridicaId = new SelectList(db.TipoPersonaJuridicas.OrderBy(q => q.TipoPersonaJuridicaId), "TipoPersonaJuridicaId", "NombrePersonaJuridica");
            ViewBag.max_tamano_file = Properties.Settings.Default.max_tamano_file;
            var model = db.SupervisorAuxiliars.Find(SupervisorAuxiliarId);
            var representante = new RepresentanteLegal() { SupervisorAuxiliarId = SupervisorAuxiliarId };
            db.RepresentantesLegals.Add(representante);

            db.SaveChanges();
            return PartialView("_RepresentantesUpdate", model);
        }
        
        public ActionResult DeleteRepresentante(int RepresentanteLegalId, int SupervisorAuxiliarId)
        {
            ViewBag.TipoPersonaJuridicaId = new SelectList(db.TipoPersonaJuridicas.OrderBy(q => q.TipoPersonaJuridicaId), "TipoPersonaJuridicaId", "NombrePersonaJuridica");
            ViewBag.max_tamano_file = Properties.Settings.Default.max_tamano_file;
            var repre = db.RepresentantesLegals.FirstOrDefault(q => q.RepresentanteLegalId == RepresentanteLegalId);
            var model = db.SupervisorAuxiliars.Find(SupervisorAuxiliarId);

            if (repre != null)
            {
                db.RepresentantesLegals.Remove(repre);
                db.SaveChanges();
            }

            return PartialView("_Representantes", model);
        }public ActionResult DeleteRepresentanteUpdate(int RepresentanteLegalId, int SupervisorAuxiliarId)
        {
            ViewBag.TipoPersonaJuridicaId = new SelectList(db.TipoPersonaJuridicas.OrderBy(q => q.TipoPersonaJuridicaId), "TipoPersonaJuridicaId", "NombrePersonaJuridica");
            ViewBag.max_tamano_file = Properties.Settings.Default.max_tamano_file;
            var repre = db.RepresentantesLegals.FirstOrDefault(q => q.RepresentanteLegalId == RepresentanteLegalId);
            var model = db.SupervisorAuxiliars.Find(SupervisorAuxiliarId);

            if (repre != null)
            {
                db.RepresentantesLegals.Remove(repre);
                db.SaveChanges();
            }

            return PartialView("_RepresentantesUpdate", model);
        }

        #endregion

        #region Funciones Constitucion
        public ActionResult ConstitucionAdd(int SupervisorAuxiliarId)
        {
            ViewBag.TipoPersonaJuridicaId = new SelectList(db.TipoPersonaJuridicas.OrderBy(q => q.TipoPersonaJuridicaId), "TipoPersonaJuridicaId", "NombrePersonaJuridica");
            ViewBag.max_tamano_file = Properties.Settings.Default.max_tamano_file;
            var model = db.SupervisorAuxiliars.Find(SupervisorAuxiliarId);
            var modificacion = new EscrituraConstitucion() { SupervisorAuxiliarId = model.SupervisorAuxiliarId };
            db.EscrituraConstitucions.Add(modificacion);
            db.SaveChanges();

            return PartialView("_Constitucion", model);
        }

        public ActionResult ConstitucionDelete(int EscrituraConstitucionId, int SupervisorAuxiliarId)
        {
            ViewBag.TipoPersonaJuridicaId = new SelectList(db.TipoPersonaJuridicas.OrderBy(q => q.TipoPersonaJuridicaId), "TipoPersonaJuridicaId", "NombrePersonaJuridica");
            ViewBag.max_tamano_file = Properties.Settings.Default.max_tamano_file;
            var consti = db.EscrituraConstitucions.FirstOrDefault(q => q.EscrituraConstitucionId == EscrituraConstitucionId);
            var model = db.SupervisorAuxiliars.Find(SupervisorAuxiliarId);

            if (consti != null)
            {
                db.EscrituraConstitucions.Remove(consti);
                db.SaveChanges();
            }

            return PartialView("_Constitucion", model);
        }

        public ActionResult ConstitucionUpdateAdd(int SupervisorAuxiliarId)
        {
            ViewBag.TipoPersonaJuridicaId = new SelectList(db.TipoPersonaJuridicas.OrderBy(q => q.TipoPersonaJuridicaId), "TipoPersonaJuridicaId", "NombrePersonaJuridica");
            ViewBag.max_tamano_file = Properties.Settings.Default.max_tamano_file;
            var model = db.SupervisorAuxiliars.Find(SupervisorAuxiliarId);
            var modificacion = new EscrituraConstitucion() { SupervisorAuxiliarId = model.SupervisorAuxiliarId };
            db.EscrituraConstitucions.Add(modificacion);
            db.SaveChanges();

            return PartialView("_ConstitucionUpdate", model);
        }
        public ActionResult ConstitucionUpdateDelete(int EscrituraConstitucionId, int SupervisorAuxiliarId)
        {
            ViewBag.TipoPersonaJuridicaId = new SelectList(db.TipoPersonaJuridicas.OrderBy(q => q.TipoPersonaJuridicaId), "TipoPersonaJuridicaId", "NombrePersonaJuridica");
            ViewBag.max_tamano_file = Properties.Settings.Default.max_tamano_file;
            var consti = db.EscrituraConstitucions.FirstOrDefault(q => q.EscrituraConstitucionId == EscrituraConstitucionId);
            var model = db.SupervisorAuxiliars.Find(SupervisorAuxiliarId);

            if (consti != null)
            {
                db.EscrituraConstitucions.Remove(consti);
                db.SaveChanges();
            }

            return PartialView("_ConstitucionUpdate", model);
        }

        #endregion

        #region Funciones Personas Facultadas
        public ActionResult PersonaFacultadaAdd(int SupervisorAuxiliarId)
        {
            ViewBag.TipoPersonaJuridicaId = new SelectList(db.TipoPersonaJuridicas.OrderBy(q => q.TipoPersonaJuridicaId), "TipoPersonaJuridicaId", "NombrePersonaJuridica");
            ViewBag.max_tamano_file = Properties.Settings.Default.max_tamano_file;
            var model = db.SupervisorAuxiliars.Find(SupervisorAuxiliarId);
            var facultada = new PersonaFacultada() { SupervisorAuxiliarId = model.SupervisorAuxiliarId };
            db.PersonaFacultadas.Add(facultada);

            db.SaveChanges();

            return PartialView("_PersonasFacultadas", model);
        }

        public ActionResult DeleteFacultada(int PersonaFacultadaId, int SupervisorAuxiliarId)
        {
            ViewBag.TipoPersonaJuridicaId = new SelectList(db.TipoPersonaJuridicas.OrderBy(q => q.TipoPersonaJuridicaId), "TipoPersonaJuridicaId", "NombrePersonaJuridica");
            ViewBag.max_tamano_file = Properties.Settings.Default.max_tamano_file;
            var facultada = db.PersonaFacultadas.FirstOrDefault(q => q.PersonaFacultadaId == PersonaFacultadaId);
            var model = db.SupervisorAuxiliars.Find(SupervisorAuxiliarId);

            if (facultada != null)
            {
                db.PersonaFacultadas.Remove(facultada);
                db.SaveChanges();
            }
            return PartialView("_PersonasFacultadas", model);
        }

        public ActionResult PersonaFacultadaUpdateAdd(int SupervisorAuxiliarId)
        {
            ViewBag.TipoPersonaJuridicaId = new SelectList(db.TipoPersonaJuridicas.OrderBy(q => q.TipoPersonaJuridicaId), "TipoPersonaJuridicaId", "NombrePersonaJuridica");
            ViewBag.max_tamano_file = Properties.Settings.Default.max_tamano_file;
            var model = db.SupervisorAuxiliars.Find(SupervisorAuxiliarId);
            var facultada = new PersonaFacultada() { SupervisorAuxiliarId = model.SupervisorAuxiliarId };
            db.PersonaFacultadas.Add(facultada);

            db.SaveChanges();

            return PartialView("_PersonasFacultadasUpdate", model);
        }

        public ActionResult DeleteFacultadaUpdate(int PersonaFacultadaId, int SupervisorAuxiliarId)
        {
            ViewBag.TipoPersonaJuridicaId = new SelectList(db.TipoPersonaJuridicas.OrderBy(q => q.TipoPersonaJuridicaId), "TipoPersonaJuridicaId", "NombrePersonaJuridica");
            ViewBag.max_tamano_file = Properties.Settings.Default.max_tamano_file;
            var facultada = db.PersonaFacultadas.FirstOrDefault(q => q.PersonaFacultadaId == PersonaFacultadaId);
            var model = db.SupervisorAuxiliars.Find(SupervisorAuxiliarId);

            if (facultada != null)
            {
                db.PersonaFacultadas.Remove(facultada);
                db.SaveChanges();
            }
            return PartialView("_PersonasFacultadasUpdate", model);
        }
        #endregion
        
        public ActionResult ExtractoAdd(int SupervisorAuxiliarId)
        {
            ViewBag.TipoPersonaJuridicaId = new SelectList(db.TipoPersonaJuridicas.OrderBy(q => q.TipoPersonaJuridicaId), "TipoPersonaJuridicaId", "NombrePersonaJuridica");
            ViewBag.max_tamano_file = Properties.Settings.Default.max_tamano_file;
            var model = db.SupervisorAuxiliars.Find(SupervisorAuxiliarId);
            var extracto = new ExtractoAuxiliar() { SupervisorAuxiliarId = SupervisorAuxiliarId };
            db.ExtractoAuxiliars.Add(extracto);
            db.SaveChanges();

            return PartialView("_Extracto", model);
        }

        public ActionResult DeleteExtracto(int ExtractoAuxiliarId, int SupervisorAuxiliarId)
        {
            ViewBag.TipoPersonaJuridicaId = new SelectList(db.TipoPersonaJuridicas.OrderBy(q => q.TipoPersonaJuridicaId), "TipoPersonaJuridicaId", "NombrePersonaJuridica");
            ViewBag.max_tamano_file = Properties.Settings.Default.max_tamano_file;
            var extracto = db.ExtractoAuxiliars.FirstOrDefault(q => q.ExtractoAuxiliarId == ExtractoAuxiliarId);
            var model = db.SupervisorAuxiliars.Find(SupervisorAuxiliarId);

            if (extracto != null)
            {
                db.ExtractoAuxiliars.Remove(extracto);
                db.SaveChanges();
            }

            return PartialView("_Extracto", model);
        }

        public ActionResult Finish()
        {
            return View();
        }
    }
}