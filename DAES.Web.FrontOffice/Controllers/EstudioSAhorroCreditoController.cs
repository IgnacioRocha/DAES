using DAES.Infrastructure;
using DAES.Infrastructure.SistemaIntegrado;
using DAES.Model.SistemaIntegrado;
using DAES.Web.FrontOffice.Helper;
using DAES.Web.FrontOffice.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;


namespace DAES.Web.FrontOffice.Controllers
{
    [Audit]
    public class EstudioSAhorroCreditoController : Controller
    {

        private SistemaIntegradoContext _db = new SistemaIntegradoContext();
        private BLL.Custom _custom = new BLL.Custom();

        // GET: EstudioSAhorroCredito

        public ActionResult Index()
        {
            return View();
        }

        // GET: EstudioSAhorroCredito/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EstudioSAhorroCredito/Create

        public ActionResult Create()
        {

            if (!Global.CurrentClaveUnica.IsAutenticated)
            {
                return View("_Error", new Exception("Usuario no autenticado con Clave Única."));
            }

            ViewBag.TipoOrganizacionId = new SelectList(_db.TipoOrganizacion.Where(q => q.TipoOrganizacionId == 1).OrderBy(q => q.Nombre), "TipoOrganizacionId", "Nombre");
            ViewBag.RegionId = new SelectList(_db.Region, "RegionId", "nombre");
            ViewBag.ComunaId = new SelectList(_db.Comuna, "ComunaId", "nombre");
            ViewBag.SubRubroId = new SelectList(_db.SubRubro, "SubRubroId", "nombre");
            ViewBag.RubroId = new SelectList(_db.Rubro, "RubroId", "nombre");
            ViewBag.RegionSolicitante = new SelectList(_db.Region, "RegionId", "nombre");
            ViewBag.max_tamano_file = Properties.Settings.Default.max_tamano_file;




            return View(new Model.DTO.DTOEstudioSocioeconomico()
            {
                RutSolicitante = string.Concat(Global.CurrentClaveUnica.ClaveUnicaUser.RolUnico.numero, "-", Global.CurrentClaveUnica.ClaveUnicaUser.RolUnico.DV),
                Apellidos = string.Join(" ", Global.CurrentClaveUnica.ClaveUnicaUser.name.nombres).ToUpperNull(),
                Nombres = string.Join(" ", Global.CurrentClaveUnica.ClaveUnicaUser.name.apellidos).ToUpperNull()
            });
        }

        public ActionResult Clave()
        {
            return View();
        }


        public ActionResult Start()
        {
            Global.CurrentClaveUnica.ClaveUnicaRequestAutorization.controller = "EstudioSAhorroCredito";
            return Redirect(Global.CurrentClaveUnica.ClaveUnicaRequestAutorization.uri);
        }

        public ActionResult Finish()
        {
            return View();
        }

        // POST: EstudioSAhorroCredito/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DAES.Model.DTO.DTOEstudioSocioeconomico model)
        {
            if (!Global.CurrentClaveUnica.IsAutenticated)
            {
                ModelState.AddModelError(string.Empty, "Usuario no autenticado con clave única");
            }

            ViewBag.TipoOrganizacionId = new SelectList(_db.TipoOrganizacion.Where(q => q.TipoOrganizacionId == 1).OrderBy(q => q.Nombre), "TipoOrganizacionId", "Nombre");
            ViewBag.RegionId = new SelectList(_db.Region, "RegionId", "nombre");
            ViewBag.ComunaId = new SelectList(_db.Comuna, "ComunaId", "nombre", model.ComunaId);
            ViewBag.SubRubroId = new SelectList(_db.SubRubro, "SubRubroId", "nombre");
            ViewBag.RubroId = new SelectList(_db.Rubro, "RubroId", "nombre");
            ViewBag.RegionSolicitante = new SelectList(_db.Region, "RegionId", "nombre");
            ViewBag.max_tamano_file = Properties.Settings.Default.max_tamano_file;


            if (ModelState.IsValid)
            {
                //Se crea un proceso
                var proceso = new Proceso()
                {
                    DefinicionProcesoId = (int)Infrastructure.Enum.DefinicionProceso.EstudioSocioEconomico,
                    Observacion = model.Observacion.ToUpperNull()
                };

                //Se crea un solicitante
                proceso.Solicitante = new Solicitante()
                {
                    Rut = Global.CurrentClaveUnica.RUT,
                    Nombres = string.Join(" ", Global.CurrentClaveUnica.ClaveUnicaUser.name.nombres).ToUpperNull(),
                    Email = model.MailSolicitante.ToUpperNull(),
                    Apellidos = string.Join(" ", Global.CurrentClaveUnica.ClaveUnicaUser.name.apellidos).ToUpperNull(),
                    RegionId = model.RegionSolicitante.GetValueOrDefault(),
                    Fono = model.FonoSolicitante
                };



                //Se crea una organización
                proceso.Organizacion = new Organizacion()
                {
                    OrganizacionId = model.OrganizacionId,
                    EstadoId = (int)Infrastructure.Enum.Estado.NoConstituida,
                    SituacionId = (int)Infrastructure.Enum.Situacion.Inactiva,
                    TipoOrganizacionId = 1,
                    RazonSocial = model.RazonSocial.ToUpperNull(),
                    Direccion = model.Direccion.ToUpperNull(),
                    RUT = model.Rut,
                    Sigla = model.Sigla.ToUpperNull(),
                    Fono = model.Fono,
                    Email = model.Email.ToUpperNull(),
                    RubroId = model.RubroId,
                    SubRubroId = model.SubRubroId,
                    ComunaId = model.ComunaId,//== 0?null:model.ComunaId 
                    RegionId = model.RegionId,
                    NumeroRegistro = string.Empty
                };

                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFileBase file = Request.Files[i];
                    var target = new MemoryStream();
                    using (MemoryStream ms = new MemoryStream())
                    {
                        file.InputStream.CopyTo(ms);
                        // Verifica la extensión del archivo
                        string filename = Path.GetFileName(file.FileName);
                        string fileEx = System.IO.Path.GetExtension(filename); // Obtenga el nombre del sufijo requerido

                        //Se crea un estudio SocioEconomico
                        proceso.EstudioSocioEconomicos.Add(new EstudioSocioEconomico
                        {
                            FechaCreacion = DateTime.Now,
                            DocumentoAdjunto = ms.ToArray(),
                            Proceso = proceso
                        });
                        

                        if (file == null || ms.Length > 52428800 || file.ContentLength < 0 || file.FileName == "" || fileEx != ".pdf" && fileEx != ".xls" && fileEx != ".xlsx" && fileEx != ".doc" && fileEx != ".docx")
                        {

                            ViewBag.errorMessage = "Los archivos no pueden estar vacíos y deben ser archivos de tipo Word, Excel o Pdf";


                            return View(new Model.DTO.DTOEstudioSocioeconomico()
                            {
                                RutSolicitante = string.Concat(Global.CurrentClaveUnica.ClaveUnicaUser.RolUnico.numero, "-", Global.CurrentClaveUnica.ClaveUnicaUser.RolUnico.DV),
                                Apellidos = string.Join(" ", Global.CurrentClaveUnica.ClaveUnicaUser.name.nombres).ToUpperNull(),
                                Nombres = string.Join(" ", Global.CurrentClaveUnica.ClaveUnicaUser.name.apellidos).ToUpperNull()
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
                    //Se inicia el proceso
                    var p = _custom.ProcesoStart(proceso);
                    TempData["Success"] = string.Format("Trámite número {0} terminado correctamente. Se ha enviado una notificación al correo {1} con los detalles.", p.ProcesoId, proceso.Solicitante.Email);
                    return RedirectToAction("Finish");
                }
                catch (Exception ex)
                {
                    return View("_Error", ex);
                }

            }
            return View(model);
        }


    }
}
