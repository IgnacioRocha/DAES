using DAES.Infrastructure;
using DAES.Infrastructure.SistemaIntegrado;
using DAES.Model.SistemaIntegrado;
using DAES.Web.FrontOffice.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DAES.Web.FrontOffice.Controllers
{
    public class CooperativaAbiertaController : Controller
    {

        private SistemaIntegradoContext _db = new SistemaIntegradoContext();
        private BLL.Custom _custom = new BLL.Custom();

        // GET: Controller
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Confirm()
        {
            return View();
        }

        public ActionResult Start()
        {
            Global.CurrentClaveUnica.ClaveUnicaRequestAutorization.controller = "CooperativaAbierta";
            Global.CurrentClaveUnica.ClaveUnicaRequestAutorization.method = "Create";

            //a
            return RedirectToAction(Global.CurrentClaveUnica.ClaveUnicaRequestAutorization.method, Global.CurrentClaveUnica.ClaveUnicaRequestAutorization.controller);

            //clave unica
            //return Redirect(Global.CurrentClaveUnica.ClaveUnicaRequestAutorization.uri);
        }


        // GET: Controller/Create
        public ActionResult Create()
        {
            //if (!Global.CurrentClaveUnica.IsAutenticated)
            //{
            //    return View("_Error", new Exception("Usuario no autenticado con Clave Única."));
            //}

            ViewBag.TipoOrganizacionId = new SelectList(_db.TipoOrganizacion.Where(t => t.TipoOrganizacionId == 1).OrderBy(t => t.Nombre), "TipoOrganizacionId", "Nombre");
            ViewBag.RegionId = new SelectList(_db.Region, "RegionId", "Nombre");
            ViewBag.ComunaId = new SelectList(_db.Comuna, "ComunaId", "Nombre");
            ViewBag.RubroId = new SelectList(_db.Rubro, "RubroId", "Nombre");
            ViewBag.SubRubroId = new SelectList(_db.SubRubro, "SubRubroId", "Nombre");
            ViewBag.RegionSolicitante = new SelectList(_db.Region, "RegionId", "Nombre");
            ViewBag.max_tamano_file = Properties.Settings.Default.max_tamano_file;

            return View(new Model.DTO.DTOCooperativaAbierta()
            {
                RutSolicitante = "98.7654.321-0",
                Nombres = "Fulgore",
                Apellidos = "Cinder"
            });

        }

        // POST: Controller/Create
        [HttpPost]
        public ActionResult Create(DAES.Model.DTO.DTOCooperativaAbierta model)
        {
            //if (!Global.CurrentClaveUnica.IsAutenticated)
            //{
            //    ModelState.AddModelError(string.Empty, "Usuario no autenticado con clave única");
            //}

            ViewBag.TipoOrganizacionId = new SelectList(_db.TipoOrganizacion.Where(t => t.TipoOrganizacionId == 1).OrderBy(t => t.Nombre), "TipoOrganizacionId", "Nombre");
            ViewBag.RegionId = new SelectList(_db.Region, "RegionId", "Nombre");
            ViewBag.ComunaId = new SelectList(_db.Comuna, "ComunaId", "Nombre");
            ViewBag.RubroId = new SelectList(_db.Rubro, "RubroId", "Nombre");
            ViewBag.SubRubroId = new SelectList(_db.SubRubro, "SubRubroId", "Nombre");
            ViewBag.RegionSolicitante = new SelectList(_db.Region, "RegionId", "Nombre");
            ViewBag.max_tamano_file = Properties.Settings.Default.max_tamano_file;


            if (ModelState.IsValid)
            {
                //proceso
                var proceso = new Proceso()
                {
                    DefinicionProcesoId = (int)Infrastructure.Enum.DefinicionProceso.CooperativaViviendaAbierta,
                    Observacion = model.Observacion.ToUpperNull()
                };

                //solicitante
                proceso.Solicitante = new Solicitante()
                {
                    Rut = model.RutSolicitante,
                    Nombres = model.Nombres,
                    Apellidos = model.Apellidos,
                    Fono = model.FonoSolicitante,
                    Email = model.MailSolicitante.ToUpperNull(),
                    RegionId = model.RegionSolicitante.GetValueOrDefault(),
                };

                //organizacion
                proceso.Organizacion = new Organizacion()
                {
                    OrganizacionId = model.OrganizacionId,
                    //se debe clasificar la organizacion como inexistente
                    EstadoId = (int)Infrastructure.Enum.Estado.Inexistente,
                    SituacionId = (int)Infrastructure.Enum.Situacion.Inactiva,
                    TipoOrganizacionId = 1,
                    RazonSocial = model.RazonSocial.ToUpperNull(),
                    Direccion = model.Direccion.ToUpperNull(),
                    RUT = model.Rut,
                    Sigla = model.Sigla.ToUpperNull(),
                    Fono = model.Fono,
                    Email = model.Email.ToUpperNull(),
                    RubroId = 6, //6 -> Servicios 
                    SubRubroId = 16, //16 -> Vivienda Abierta
                    ComunaId = model.ComunaId,
                    RegionId = model.RegionId,
                    NumeroRegistro = string.Empty
                };

                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFileBase file = Request.Files[i];
                    if (file.FileName == "")
                    {
                        ViewBag.errorMessage = "*Error al enviar documentos, faltan documentos por adjuntar";
                        return View(new Model.DTO.DTOCooperativaAbierta()
                        {
                            RutSolicitante = "98.7654.321-0",
                            Nombres = "Fulgore",
                            Apellidos = "Cinder"
                        });
                    }
                    var target = new MemoryStream();
                    using (MemoryStream ms = new MemoryStream())
                    {
                        file.InputStream.CopyTo(ms);
                        string filename = Path.GetFileName(file.FileName);
                        string fileEx = System.IO.Path.GetExtension(filename);

                        //Se crea un estudio SocioEconomico Cooperativa Abierta
                        proceso.CooperativaAbiertas.Add(new CooperativaAbierta
                        {
                            FechaCreacion = DateTime.Now,
                            DocumentoAdjunto = ms.ToArray(),
                            Proceso = proceso
                        });

                        if (ms.Length > 52428800)
                        {
                            ViewBag.errorMessage = "*Error al enviar documento, tamaño supera el límite 50 MB.";
                            return View(new Model.DTO.DTOCooperativaAbierta()
                            {
                                RutSolicitante = "98.7654.321-0",
                                Nombres = "Fulgore",
                                Apellidos = "Cinder"
                            });
                        }

                        if (fileEx != ".pdf" && fileEx != ".xls" && fileEx != ".xlsx" && fileEx != ".doc" && fileEx != ".docx")
                        {

                            ViewBag.errorMessage = "*Error al enviar documento, los archivos deben ser archivos de tipo Word, Excel o Pdf";
                            return View(new Model.DTO.DTOCooperativaAbierta()
                            {
                                RutSolicitante = "98.7654.321-0",
                                Nombres = "Fulgore",
                                Apellidos = "Cinder"
                            });
                        }
                        else
                        {
                            proceso.Documentos.Add(new Documento()
                            {
                                FechaCreacion = DateTime.Now,
                                Autor = proceso.Solicitante.Email,
                                Descripcion = model.Observacion,
                                Content = ms.ToArray(),
                                FileName = file.FileName,
                                Organizacion = proceso.Organizacion,
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
            return View();

        }

        public ActionResult Finish()
        {
            return View();
        }

    }
}
