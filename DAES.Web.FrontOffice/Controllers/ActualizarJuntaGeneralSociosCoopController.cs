﻿using DAES.Infrastructure.SistemaIntegrado;
using DAES.Model.SistemaIntegrado;
using DAES.Web.FrontOffice.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAES.Model.DTO;
using DAES.Web.FrontOffice.Models;
using System.IO;
using DAES.Infrastructure;

namespace DAES.Web.FrontOffice.Controllers
{
    [Audit]
    public class ActualizarJuntaGeneralSociosCoopController : Controller
    {

        public class DTOSearch
        {

            public DTOSearch()
            {
                Organizacions = new List<Organizacion>();
            }

            public bool First { get; set; } = true;

            [Display(Name = "Razón social o número registro")]
            [Required(ErrorMessage = "Es necesario especificar este dato")]
            public string Filter { get; set; }

            public List<Organizacion> Organizacions { get; set; }
        }

        private SistemaIntegradoContext _db = new SistemaIntegradoContext();
        private BLL.Custom _custom = new BLL.Custom();
        private static DTOJuntaGeneralSociosCoop model = new DTOJuntaGeneralSociosCoop();

        public ActionResult Start()
        {
            Global.CurrentClaveUnica.ClaveUnicaRequestAutorization.controller = "ActualizarJuntaGeneralSociosCoop";
            Global.CurrentClaveUnica.ClaveUnicaRequestAutorization.method = "Search";

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


            //////a
            //return RedirectToAction(Global.CurrentClaveUnica.ClaveUnicaRequestAutorization.method, Global.CurrentClaveUnica.ClaveUnicaRequestAutorization.controller);
            return Redirect(Global.CurrentClaveUnica.ClaveUnicaRequestAutorization.uri);
        }

        public ActionResult Search()
        {
            if (!Global.CurrentClaveUnica.IsAutenticated)
            {
                return View("_Error", new Exception("Usuario no autenticado con Clave Única."));
            }

            return View(new DTOSearch());
        }

        [HttpPost]
        public ActionResult Search(string Filter)
        {
            if (!Global.CurrentClaveUnica.IsAutenticated)
            {
                return View("_Error", new Exception("Usuario no autenticado con Clave Única."));
            }

            IQueryable<Organizacion> query = _db.Organizacion;
            query = query.Where(q => q.TipoOrganizacionId == (int)Infrastructure.Enum.TipoOrganizacion.Cooperativa);
            query = query.Where(q => q.EstadoId == (int)Infrastructure.Enum.Estado.Vigente);
            query = query.Where(q => q.RazonSocial.Contains(Filter) || q.NumeroRegistro.Contains(Filter) || q.Sigla.Contains(Filter));

            var model = new DTOSearch();
            model.Organizacions = query.OrderBy(q => q.NumeroRegistro).ToList();
            model.First = false;

            return View(model);
        }

        // GET: ActualizarJuntaGeneralSociosCoop
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Finish()
        {
            return View();
        }

        // GET: ActualizarJuntaGeneralSociosCoop/Create
        public ActionResult Create(int id)
        {
            if (!Global.CurrentClaveUnica.IsAutenticated)
            {
                return View("_Error", new Exception("Usuario no autenticado con Clave Única."));
            }

            var organizacion = _db.Organizacion.FirstOrDefault(q => q.OrganizacionId == id);
            if (organizacion == null)
            {
                return View("_Error", new Exception("Organización no encontrada"));
            }

            ViewBag.CiudadId = new SelectList(_db.Ciudad.OrderBy(q => q.Nombre), "CiudadId", "Nombre", organizacion.CiudadId);
            ViewBag.ComunaId = new SelectList(_db.Comuna.OrderBy(q => q.Nombre), "ComunaId", "Nombre", organizacion.ComunaId);
            ViewBag.EstadoId = new SelectList(_db.Estado.OrderBy(q => q.Nombre), "EstadoId", "Nombre", organizacion.EstadoId);
            ViewBag.SituacionId = new SelectList(_db.Situacion.OrderBy(q => q.Nombre), "SituacionId", "Nombre", organizacion.SituacionId);
            ViewBag.RegionId = new SelectList(_db.Region.OrderBy(q => q.Nombre), "RegionId", "Nombre", organizacion.RegionId);
            ViewBag.TipoOrganizacionId = new SelectList(_db.TipoOrganizacion.OrderBy(q => q.Nombre), "TipoOrganizacionId", "Nombre", organizacion.TipoOrganizacionId);
            ViewBag.RegionSolicitanteId = new SelectList(_db.Region.OrderBy(q => q.Nombre), "RegionId", "Nombre");
            ViewBag.CargoId = new SelectList(_db.Cargo.OrderBy(q => q.Nombre), "CargoId", "Nombre");
            ViewBag.GeneroId = new SelectList(_db.Genero.OrderBy(q => q.Nombre), "GeneroId", "Nombre");
            ViewBag.Cargo = _db.Cargo.ToList();
            ViewBag.Genero = _db.Genero.ToList();

            model = new DTOJuntaGeneralSociosCoop
            {

                RUTSolicitante = string.Concat(Global.CurrentClaveUnica.ClaveUnicaUser.RolUnico.numero, Global.CurrentClaveUnica.ClaveUnicaUser.RolUnico.DV),
                NombresSolicitante = string.Join(" ", Global.CurrentClaveUnica.ClaveUnicaUser.name.nombres).ToUpperNull(),
                ApellidosSolicitante = string.Join(" ", Global.CurrentClaveUnica.ClaveUnicaUser.name.apellidos).ToUpperNull(),
                //TODO: revisar si llega bien el dato de correo
                EmailSolicitante = model.EmailSolicitante,
                FonoSolicitante = model.FonoSolicitante,

                OrganizacionId = organizacion.OrganizacionId,
                TipoOrganizacionId = organizacion.TipoOrganizacionId,
                TipoOrganizacion = organizacion.TipoOrganizacion,
                EstadoId = organizacion.EstadoId,
                Estado = organizacion.Estado,
                SituacionId = organizacion.SituacionId,
                Situacion = organizacion.Situacion,
                RubroId = organizacion.RubroId,
                Rubro = organizacion.Rubro,
                SubRubroId = organizacion.SubRubroId,
                SubRubro = organizacion.SubRubro,
                RegionId = organizacion.RegionId,
                Region = organizacion.Region,
                ComunaId = organizacion.ComunaId,
                Comuna = organizacion.Comuna,
                NumeroRegistro = organizacion.NumeroRegistro,
                RazonSocial = organizacion.RazonSocial,
                Direccion = organizacion.Direccion,
                Directorio = organizacion.Directorios.Select(q => new DTODirectorio
                {
                    Rut = q.Rut,
                    DirectorioId = q.DirectorioId,
                    OrganizacionId = q.OrganizacionId,
                    GeneroId = q.GeneroId,
                    CargoId = q.CargoId,
                    FechaInicio = q.FechaInicio ?? DateTime.Now,
                    FechaTermino = q.FechaTermino ?? DateTime.Now,
                    NombreCompleto = q.NombreCompleto
                }).ToList()
            };

            var cont = model.Directorio.Count();
            ViewBag.cont = cont;
            return View(model);
        }

        // POST: ActualizarJuntaGeneralSociosCoop/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DTOJuntaGeneralSociosCoop model, List<string> NombreCompleto, List<string> Rut, List<int?> SelectCargo, List<int?> SelectGenero, List<string> FechaInicio, List<string> FechaTermino, List<int?> IdSolicitante, List<int?> Eliminado,List<int?> DirectorioId)
        {
            if (!Global.CurrentClaveUnica.IsAutenticated)
            {
                ModelState.AddModelError(string.Empty, "Usuario no autenticado con clave única");
            }

            if (!_db.Organizacion.Any(q => q.OrganizacionId == model.OrganizacionId))
            {
                ModelState.AddModelError(string.Empty, "La organización no fue encontrada.");
            }

            //if (!model.RUTSolicitante.IsRut())
            //{
            //    ModelState.AddModelError(string.Empty, "El rut del solicitante ingresado no es válido");
            //}

            //if (!string.IsNullOrWhiteSpace(model.RUT) && !model.RUT.IsRut())
            //{
            //    ModelState.AddModelError(string.Empty, "El rut de la organización ingresado no es válido");
            //}

            ViewBag.ComunaId = new SelectList(_db.Comuna.OrderBy(q => q.Nombre), "ComunaId", "Nombre", model.ComunaId);
            ViewBag.EstadoId = new SelectList(_db.Estado.OrderBy(q => q.Nombre), "EstadoId", "Nombre", model.EstadoId);
            ViewBag.SituacionId = new SelectList(_db.Situacion.OrderBy(q => q.Nombre), "SituacionId", "Nombre", model.SituacionId);
            ViewBag.RegionId = new SelectList(_db.Region.OrderBy(q => q.Nombre), "RegionId", "Nombre", model.RegionId);
            ViewBag.TipoOrganizacionId = new SelectList(_db.TipoOrganizacion.OrderBy(q => q.Nombre), "TipoOrganizacionId", "Nombre", model.TipoOrganizacionId);
            ViewBag.CargoId = new SelectList(_db.Cargo.OrderBy(q => q.Nombre), "CargoId", "Nombre");
            ViewBag.GeneroId = new SelectList(_db.Genero.OrderBy(q => q.Nombre), "GeneroId", "Nombre");
            ViewBag.Cargo = _db.Cargo.ToList();
            ViewBag.Genero = _db.Genero.ToList();
            //COOPERATIVA
            if (ModelState.IsValid)
            {
                var proceso = new Proceso()
                {
                    DefinicionProcesoId = (int)Infrastructure.Enum.DefinicionProceso.ActualizacionConsejoAdmninistracion,
                    OrganizacionId = model.OrganizacionId
                };
                proceso.Solicitante = new Solicitante()
                {
                    Rut = model.RUTSolicitante,
                    Nombres = model.NombresSolicitante,
                    Apellidos = model.ApellidosSolicitante,
                    Email = model.EmailSolicitante,
                    Fono = model.FonoSolicitante
                };
               
                //Se quitan los integrantes seleccionados
                if (model.Directorio != null)
                {
                        var directDelete = model.Directorio.Where(q => q.eliminado == true).ToList();
                    for (int i = 0; i < directDelete.Count(); i++)
                    {
                        model.Directorio.Remove(model.Directorio.FirstOrDefault(q => q.DirectorioId == directDelete[i].DirectorioId));
                    }
                }
                // se cargan los nuevos integrantes del directorio al objeto
                if (FechaInicio != null)
                {
                    for (int i = 0; i < Rut.Count(); i++)
                    {

                        var directorio = new DTODirectorio()
                        {
                            OrganizacionId = (int)IdSolicitante[i],
                            NombreCompleto = NombreCompleto[i],
                            GeneroId = (int)SelectGenero[i],
                            CargoId = (int)SelectCargo[i],
                            Rut = Rut[i],
                            FechaInicio = DateTime.Parse(FechaInicio[i]),
                            FechaTermino = DateTime.Parse(FechaTermino[i]),
                            eliminado = false
                        };
                        model.Directorio.Add(directorio);

                    }

                }

                
                
                proceso.ActualizacionOrganizacions.Add(new ActualizacionOrganizacion()
                {
                    OrganizacionId = model.OrganizacionId,
                    TipoOrganizacionId = model.TipoOrganizacionId,
                    TipoOrganizacion = model.TipoOrganizacion,
                    EstadoId = model.EstadoId,
                    Estado = model.Estado,
                    SituacionId = model.SituacionId,
                    Situacion = model.Situacion,
                    RubroId = model.RubroId,
                    Rubro = model.Rubro,
                    SubRubroId = model.SubRubroId,
                    SubRubro = model.SubRubro,
                    RegionId = model.RegionId,
                    Region = model.Region,
                    ComunaId = model.ComunaId,
                    Comuna = model.Comuna,
                    NumeroRegistro = model.NumeroRegistro,
                    RazonSocial = model.RazonSocial,
                    Direccion = model.Direccion,
                    //Directorio = model.Directorio.Select(q => new ActualizacionOrganizacionDirectorio
                    //{
                    //    CargoId = q.CargoId,
                    //    FechaInicio = q.FechaInicio,
                    //    FechaTermino = q.FechaTermino,
                    //    GeneroId = q.GeneroId,
                    //    NombreCompleto = q.NombreCompleto,
                    //    Rut = q.Rut,

                    //}).ToList()
                    Directorio = model.Directorio.Select(q => new ActualizacionDirectorioOrganizacion
                    {
                        CargoId = q.CargoId,
                        FechaInicio = q.FechaInicio,
                        FechaTermino = q.FechaTermino,
                        GeneroId = q.GeneroId,
                        NombreCompleto = q.NombreCompleto,
                        Rut = q.Rut,
                        Eliminado = q.eliminado,
                        ActualizacionDirectorioOrganizacionId = q.ActualizacionOrganizacionDirectorioId
                        
                        
                    }).ToList()
                });

                


                foreach (string fileName in Request.Files)
                {
                    var file = Request.Files[fileName];
                    var target = new MemoryStream();
                    file.InputStream.CopyTo(target);

                    if (file.FileName != "")
                    {
                        proceso.Documentos.Add(new Documento()
                        {
                            FechaCreacion = DateTime.Now,
                            Content = target.ToArray(),
                            FileName = file.FileName,
                            Organizacion = proceso.Organizacion,
                            TipoDocumentoId = (int)Infrastructure.Enum.TipoDocumento.SinClasificar,
                            TipoPrivacidadId = (int)DAES.Infrastructure.Enum.TipoPrivacidad.Privado
                        });
                    }
                }

                try
                {
                    var p = _custom.ProcesoStart(proceso);
                    TempData["Success"] = string.Format("Trámite número {0} terminado correctamente. Se ha enviado una notificación al correo {1} con los detalles.", p.ProcesoId, proceso.Solicitante.Email);

                    //Aqui se hace el trampeo
                    //_custom.ActualizarDirectorioA(proceso);

                    return RedirectToAction("Finish");
                }
                catch (Exception ex)
                {
                    return View("_Error", ex);
                }

            }

            model.Rubro = _db.Rubro.Where(q => q.RubroId == model.RubroId).First();
            model.SubRubro = _db.SubRubro.Where(q => q.SubRubroId == model.SubRubroId).First();
            return View(model);

        }

        public ActionResult DirectorioAdd()
        {
            var directorio = new DTODirectorio()
            {
                OrganizacionId = model.OrganizacionId,
                NombreCompleto = "",
                GeneroId = (int)Infrastructure.Enum.Genero.SinGenero,
                CargoId = (int)Infrastructure.Enum.Cargo.Defecto,
            };

            model.Directorio.Add(directorio);

            ViewBag.CargoId = new SelectList(_db.Cargo.OrderBy(q => q.Nombre), "CargoId", "Nombre");
            ViewBag.GeneroId = new SelectList(_db.Genero.OrderBy(q => q.Nombre), "GeneroId", "Nombre");

            return PartialView("_JuntaGeneralSociosCoop", model);
        }

        public ActionResult DirectorioDeletee(int ide, int id) {
            

            var organizacion = _db.Organizacion.FirstOrDefault(q => q.OrganizacionId == id);
            if (organizacion == null)
            {
                return View("_Error", new Exception("Organización no encontrada"));
            }

            ViewBag.CiudadId = new SelectList(_db.Ciudad.OrderBy(q => q.Nombre), "CiudadId", "Nombre", organizacion.CiudadId);
            ViewBag.ComunaId = new SelectList(_db.Comuna.OrderBy(q => q.Nombre), "ComunaId", "Nombre", organizacion.ComunaId);
            ViewBag.EstadoId = new SelectList(_db.Estado.OrderBy(q => q.Nombre), "EstadoId", "Nombre", organizacion.EstadoId);
            ViewBag.SituacionId = new SelectList(_db.Situacion.OrderBy(q => q.Nombre), "SituacionId", "Nombre", organizacion.SituacionId);
            ViewBag.RegionId = new SelectList(_db.Region.OrderBy(q => q.Nombre), "RegionId", "Nombre", organizacion.RegionId);
            ViewBag.TipoOrganizacionId = new SelectList(_db.TipoOrganizacion.OrderBy(q => q.Nombre), "TipoOrganizacionId", "Nombre", organizacion.TipoOrganizacionId);
            ViewBag.RegionSolicitanteId = new SelectList(_db.Region.OrderBy(q => q.Nombre), "RegionId", "Nombre");
            ViewBag.CargoId = new SelectList(_db.Cargo.OrderBy(q => q.Nombre), "CargoId", "Nombre");
            ViewBag.GeneroId = new SelectList(_db.Genero.OrderBy(q => q.Nombre), "GeneroId", "Nombre");
            ViewBag.Cargo = _db.Cargo.ToList();
            ViewBag.Genero = _db.Genero.ToList();

            model = new DTOJuntaGeneralSociosCoop
            {

                RUTSolicitante = string.Concat(Global.CurrentClaveUnica.ClaveUnicaUser.RolUnico.numero, Global.CurrentClaveUnica.ClaveUnicaUser.RolUnico.DV),
                NombresSolicitante = string.Join(" ", Global.CurrentClaveUnica.ClaveUnicaUser.name.nombres).ToUpperNull(),
                ApellidosSolicitante = string.Join(" ", Global.CurrentClaveUnica.ClaveUnicaUser.name.apellidos).ToUpperNull(),
                //TODO: revisar si llega bien el dato de correo
                EmailSolicitante = model.EmailSolicitante,
                FonoSolicitante = model.FonoSolicitante,

                OrganizacionId = organizacion.OrganizacionId,
                TipoOrganizacionId = organizacion.TipoOrganizacionId,
                TipoOrganizacion = organizacion.TipoOrganizacion,
                EstadoId = organizacion.EstadoId,
                Estado = organizacion.Estado,
                SituacionId = organizacion.SituacionId,
                Situacion = organizacion.Situacion,
                RubroId = organizacion.RubroId,
                Rubro = organizacion.Rubro,
                SubRubroId = organizacion.SubRubroId,
                SubRubro = organizacion.SubRubro,
                RegionId = organizacion.RegionId,
                Region = organizacion.Region,
                ComunaId = organizacion.ComunaId,
                Comuna = organizacion.Comuna,
                NumeroRegistro = organizacion.NumeroRegistro,
                RazonSocial = organizacion.RazonSocial,
                Direccion = organizacion.Direccion,
                Directorio = organizacion.Directorios.Select(q => new DTODirectorio
                {
                    Rut = q.Rut,
                    DirectorioId = q.DirectorioId,
                    OrganizacionId = q.OrganizacionId,
                    GeneroId = q.GeneroId,
                    CargoId = q.CargoId,
                    FechaInicio = q.FechaInicio ?? DateTime.Now,
                    FechaTermino = q.FechaTermino ?? DateTime.Now,
                    NombreCompleto = q.NombreCompleto
                }).ToList()
            };

            var direct = _db.Directorio.FirstOrDefault(q => q.DirectorioId == ide);
            var directo = new DTODirectorio()
            {
                Rut = direct.Rut,
                DirectorioId = direct.DirectorioId,
                OrganizacionId = direct.OrganizacionId,
                GeneroId = direct.GeneroId,
                CargoId = direct.CargoId,
                FechaInicio = direct.FechaInicio ?? DateTime.Now,
                FechaTermino = direct.FechaTermino ?? DateTime.Now,
            };
            if (direct.DirectorioId >= 0)
            {
                model.Directorio.Remove(directo);
                _db.Directorio.Remove(direct);
                _db.SaveChanges();
            }

            return View("Create", model);
        }

        public ActionResult DirectorioDelete(Guid Id)
        {
            if (model.Directorio.Any(q => q.Id == Id))
            {
                model.Directorio.Remove(model.Directorio.FirstOrDefault(q => q.Id == Id));
            }

            ViewBag.CargoId = new SelectList(_db.Cargo.OrderBy(q => q.Nombre), "CargoId", "Nombre");
            ViewBag.GeneroId = new SelectList(_db.Genero.OrderBy(q => q.Nombre), "GeneroId", "Nombre");

            return PartialView("_JuntaGeneralSociosCoop", model);
        }

    }
}
