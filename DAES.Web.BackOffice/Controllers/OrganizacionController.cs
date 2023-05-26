﻿using DAES.Infrastructure.SistemaIntegrado;
using DAES.Model.SistemaIntegrado;
using DAES.Web.BackOffice.Helper;
using OfficeOpenXml;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using DAES.Model.DTO;
using System.Dynamic;

namespace DAES.Web.BackOffice.Controllers
{

    [Audit]
    [Authorize]
    public class OrganizacionController : Controller
    {
        public class SearchViewModel
        {

            public SearchViewModel()
            {
                Organizacions = new HashSet<Organizacion>();
                //TipoNormas = new HashSet<TipoNorma>();
            }

            public string Query { get; set; }
            public int? TipoOrganizacionId { get; set; }
            public int? EstadoId { get; set; }
            public int? SituacionId { get; set; }
            public int? RubroId { get; set; }
            public int? RegionId { get; set; }
            public int? ComunaId { get; set; }

            public int? TipoNormaId { get; set; }


            public ICollection<Organizacion> Organizacions { get; set; }
            //public ICollection<TipoNorma> TipoNormas { get; set; }
        }

        private SistemaIntegradoContext db = new SistemaIntegradoContext();
        private BLL.Custom _custom = new BLL.Custom();


        [AllowAnonymous]
        public JsonResult Get(string term)
        {
            IQueryable<Organizacion> query = db.Organizacion;

            //excluir en proceso de constitucion o inexistentes
            query = query.Where(q => q.EstadoId != (int)Infrastructure.Enum.Estado.EnConstitucion);
            query = query.Where(q => q.EstadoId != (int)Infrastructure.Enum.Estado.Inexistente);

            //filtrar por texto de busqueda
            if (!string.IsNullOrEmpty(term))
            {
                query = query.Where(q => q.NumeroRegistro.Contains(term) || q.RazonSocial.Contains(term));
            }

            return Json(query.Select(q => new { q.OrganizacionId, q.RazonSocial }).ToList(), JsonRequestBehavior.AllowGet);
        }


        [AllowAnonymous]
        public JsonResult AutoComplete(string term)
        {
            IQueryable<Organizacion> query = db.Organizacion;

            //excluir en proceso de constitucion o inexistentes
            query = query.Where(q => q.EstadoId != (int)Infrastructure.Enum.Estado.EnConstitucion);
            query = query.Where(q => q.EstadoId != (int)Infrastructure.Enum.Estado.Inexistente);

            //filtrar por texto de busqueda
            if (!string.IsNullOrEmpty(term))
            {
                query = query.Where(q => q.NumeroRegistro.Contains(term) || q.RazonSocial.Contains(term));
            }

            var result = query
                .Select(c => new { id = c.OrganizacionId, value = c.TipoOrganizacion.Nombre + " " + c.NumeroRegistro + " - " + c.RazonSocial })
                .Take(25)
                .ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetComuna(int RegionId)
        {
            ViewBag.ComunaId = new SelectList(db.Comuna.Where(q => q.RegionId == RegionId).OrderBy(q => q.Nombre), "ComunaId", "Nombre");
            return Json(ViewBag.ComunaId, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCiudad(int RegionId)
        {
            ViewBag.CiudadId = new SelectList(db.Ciudad.Where(q => q.RegionId == RegionId).OrderBy(q => q.Nombre), "CiudadId", "Nombre");
            return Json(ViewBag.CiudadId, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index(SearchViewModel model)
        {

            ViewBag.TipoOrganizacionId = new SelectList(db.TipoOrganizacion.OrderBy(q => q.Nombre), "TipoOrganizacionId", "Nombre", model.TipoOrganizacionId);
            ViewBag.EstadoId = new SelectList(db.Estado.OrderBy(q => q.Nombre), "EstadoId", "Nombre", model.EstadoId);
            ViewBag.SituacionId = new SelectList(db.Situacion.OrderBy(q => q.Nombre), "SituacionId", "Nombre", model.SituacionId);
            ViewBag.RubroId = new SelectList(db.Rubro.OrderBy(q => q.Nombre), "RubroId", "Nombre", model.RubroId);
            ViewBag.RegionId = new SelectList(db.Region.OrderBy(q => q.Nombre), "RegionId", "Nombre", model.RegionId);
            ViewBag.ComunaId = new SelectList(db.Comuna.OrderBy(q => q.Nombre), "ComunaId", "Nombre", model.ComunaId);
            ViewBag.TipoNormaId = new SelectList(db.TipoNorma.OrderBy(q => q.Nombre), "TipoNormaId", "Nombre", model.TipoNormaId);
            //ViewBag.AprobacionId = new SelectList(db.Aprobacion.OrderBy(q => q.Nombre).ToList(), "AprobacionId", "Nombre", model.AprobacionId);
            //ViewBag.TipoNormaId = new SelectList(db.TipoNorma.OrderBy(q => q.Nombre), "TipoNormaId", "Nombre");

            IQueryable<Organizacion> query = db.Organizacion;
            //IQueryable<ExistenciaLegal> querys = db.ExistenciaLegal;

            if (model.TipoOrganizacionId.HasValue)
            {
                query = query.Where(q => q.TipoOrganizacionId == model.TipoOrganizacionId.Value);
            }

            if (model.EstadoId.HasValue)
            {
                query = query.Where(q => q.EstadoId == model.EstadoId.Value);
            }

            if (model.RubroId.HasValue)
            {
                query = query.Where(q => q.RubroId == model.RubroId.Value);
            }

            if (model.RegionId.HasValue)
            {
                query = query.Where(q => q.RegionId == model.RegionId.Value);
            }

            if (model.ComunaId.HasValue)
            {
                query = query.Where(q => q.ComunaId == model.ComunaId.Value);
            }

            if (model.SituacionId.HasValue)
            {
                query = query.Where(q => q.SituacionId == model.SituacionId.Value);
            }

            if (model.TipoNormaId.HasValue)
            {
                query = query.Where(q => q.TipoNormaId == model.TipoNormaId.Value);
            }



            if (!string.IsNullOrEmpty(model.Query))
            {
                query = query.Where(q => q.RazonSocial.Contains(model.Query) || q.NumeroRegistro.Contains(model.Query) || q.Sigla.Contains(model.Query) || q.Direccion.Contains(model.Query));
            }

            model.Organizacions = query.ToList();

            return View(model);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organizacion organizacion = db.Organizacion.Find(id);
            if (organizacion == null)
            {
                return HttpNotFound();
            }

            return View(organizacion);
        }

        public ActionResult GenerarRol()
        {
            ViewBag.RegionId = new SelectList(db.Region.OrderBy(q => q.Nombre), "RegionId", "Nombre");
            ViewBag.TipoOrganizacionId = new SelectList(db.TipoOrganizacion.OrderBy(q => q.Nombre), "TipoOrganizacionId", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GenerarRol(Organizacion organizacion)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    organizacion = _custom.GenerarRegistro(organizacion);
                    TempData["Message"] = Properties.Settings.Default.Success;
                    return RedirectToAction("GenerarRolOk", new { id = organizacion.OrganizacionId });
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Se detectó un problema: " + ex.Message;
                }
            }

            ViewBag.TipoOrganizacionId = new SelectList(db.TipoOrganizacion.Where(q => q.TipoOrganizacionId != (int)Infrastructure.Enum.TipoOrganizacion.AunNoDefinida).OrderBy(q => q.Nombre), "TipoOrganizacionId", "Nombre", organizacion.TipoOrganizacionId);
            ViewBag.RegionId = new SelectList(db.Region.OrderBy(q => q.Nombre), "RegionId", "Nombre", organizacion.TipoOrganizacionId);
            ViewBag.SituacionId = new SelectList(db.Situacion.OrderBy(q => q.Nombre), "SituacionId", "Nombre", organizacion.SituacionId);

            return View(organizacion);
        }

        public ActionResult GenerarRolOk(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Organizacion organizacion = db.Organizacion.Find(id);
            if (organizacion == null)
            {
                return HttpNotFound();
            }

            return View(organizacion);
        }

        public ActionResult Create()
        {
            ViewBag.CiudadId = new SelectList(db.Ciudad.OrderBy(q => q.Nombre), "CiudadId", "Nombre");
            ViewBag.ComunaId = new SelectList(db.Comuna.OrderBy(q => q.Nombre), "ComunaId", "Nombre");
            ViewBag.EstadoId = new SelectList(db.Estado.OrderBy(q => q.Nombre), "EstadoId", "Nombre");
            ViewBag.RegionId = new SelectList(db.Region.OrderBy(q => q.Nombre), "RegionId", "Nombre");
            ViewBag.RubroId = new SelectList(db.Rubro.OrderBy(q => q.Nombre), "RubroId", "Nombre");
            ViewBag.SubRubroId = new SelectList(db.SubRubro.OrderBy(q => q.Nombre), "SubRubroId", "Nombre");
            ViewBag.TipoOrganizacionId = new SelectList(db.TipoOrganizacion.OrderBy(q => q.Nombre), "TipoOrganizacionId", "Nombre");
            ViewBag.SituacionId = new SelectList(db.Situacion.OrderBy(q => q.Nombre), "SituacionId", "Nombre");
            ViewBag.TipoNormaId = new SelectList(db.TipoNorma.OrderBy(q => q.Nombre), "TipoNormaId", "Nombre");
            //ViewBag.AprobacionId = new SelectList(db.Aprobacion.OrderBy(q => q.Nombre), "AprobacionId", "Nombre");
            ViewBag.AsambleaDepId = new SelectList(db.AsambleaDeposito.OrderBy(q => q.Descripcion), "AsambleaDepId", "Descripcion");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Organizacion organizacion)
        {
            organizacion.FechaActualizacion = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.Organizacion.Add(organizacion);
                db.SaveChanges();
                TempData["Message"] = Properties.Settings.Default.Success;
                return RedirectToAction("Index");
            }

            ViewBag.CiudadId = new SelectList(db.Ciudad.OrderBy(q => q.Nombre), "CiudadId", "Nombre", organizacion.CiudadId);
            ViewBag.ComunaId = new SelectList(db.Comuna.OrderBy(q => q.Nombre), "ComunaId", "Nombre", organizacion.ComunaId);
            ViewBag.EstadoId = new SelectList(db.Estado.OrderBy(q => q.Nombre), "EstadoId", "Nombre", organizacion.EstadoId);
            ViewBag.RegionId = new SelectList(db.Region.OrderBy(q => q.Nombre), "RegionId", "Nombre", organizacion.RegionId);
            ViewBag.RubroId = new SelectList(db.Rubro.OrderBy(q => q.Nombre), "RubroId", "Nombre", organizacion.RubroId);
            ViewBag.SubRubroId = new SelectList(db.SubRubro.OrderBy(q => q.Nombre), "SubRubroId", "Nombre", organizacion.SubRubroId);
            ViewBag.TipoOrganizacionId = new SelectList(db.TipoOrganizacion.OrderBy(q => q.Nombre), "TipoOrganizacionId", "Nombre", organizacion.TipoOrganizacionId);
            ViewBag.SituacionId = new SelectList(db.Situacion.OrderBy(q => q.Nombre), "SituacionId", "Nombre", organizacion.SituacionId);
            ViewBag.TipoNormaId = new SelectList(db.TipoNorma.OrderBy(q => q.Nombre), "TipoNormaId", "Nombre");
            //ViewBag.AprobacionId = new SelectList(db.Aprobacion.OrderBy(q => q.Nombre), "AprobacionId", "Nombre");
            ViewBag.AsambleaDepId = new SelectList(db.AsambleaDeposito.OrderBy(q => q.Descripcion), "AsambleaDepId", "Descripcion");
            ViewBag.TipoNormaaId = new SelectList(db.TipoNorma.OrderBy(q => q.Nombre).ToList(), "TipoNormaId", "Nombre");

            return View(organizacion);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organizacion organizacion = db.Organizacion.Find(id);
            if (organizacion == null)
            {
                return HttpNotFound();
            }

            ViewBag.CiudadId = new SelectList(db.Ciudad.OrderBy(q => q.Nombre), "CiudadId", "Nombre", organizacion.CiudadId);
            ViewBag.ComunaId = new SelectList(db.Comuna.OrderBy(q => q.Nombre), "ComunaId", "Nombre", organizacion.ComunaId);
            ViewBag.EstadoId = new SelectList(db.Estado.OrderBy(q => q.Nombre), "EstadoId", "Nombre", organizacion.EstadoId);
            ViewBag.RegionId = new SelectList(db.Region.OrderBy(q => q.Nombre), "RegionId", "Nombre", organizacion.RegionId);
            ViewBag.RubroId = new SelectList(db.Rubro.OrderBy(q => q.Nombre), "RubroId", "Nombre", organizacion.RubroId);
            ViewBag.SubRubroId = new SelectList(db.SubRubro.OrderBy(q => q.Nombre), "SubRubroId", "Nombre", organizacion.SubRubroId);
            ViewBag.TipoOrganizacionId = new SelectList(db.TipoOrganizacion.OrderBy(q => q.Nombre), "TipoOrganizacionId", "Nombre", organizacion.TipoOrganizacionId);
            ViewBag.SituacionId = new SelectList(db.Situacion.OrderBy(q => q.Nombre), "SituacionId", "Nombre", organizacion.SituacionId);
            ViewBag.CargoId = new SelectList(db.Cargo.OrderBy(q => q.Nombre), "CargoId", "Nombre");
            ViewBag.GeneroId = new SelectList(db.Genero.OrderBy(q => q.Nombre), "GeneroId", "Nombre");
            ViewBag.TipoNormaId = new SelectList(db.TipoNorma.OrderBy(q => q.Nombre).ToList(), "TipoNormaId", "Nombre");
            ViewBag.TipoNormaaId = new SelectList(db.TipoNorma.OrderBy(q => q.Nombre).ToList(), "TipoNormaId", "Nombre");            
            ViewBag.AprobacionId = new SelectList(db.Aprobacion.OrderBy(q => q.Nombre), "AprobacionId", "Nombre");
            ViewBag.AsambleaDepId = new SelectList(db.AsambleaDeposito.OrderBy(q => q.Descripcion).ToList(), "AsambleaDepId", "Descripcion");

            return View(organizacion);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Organizacion model, ExistenciaLegal existenciaLegals, Saneamiento san, ReformaAnterior refAnt, ReformaPosterior refPost, Disolucion disolucion, ExistenciaAnterior ExiAnt, ExistenciaPosterior ExiPost, ReformaAGAC refAG)
        {
            model.FechaActualizacion = DateTime.Now;

            if (ModelState.IsValid)
            {
                var mod = db.ExistenciaLegal.Any(q => q.OrganizacionId == model.OrganizacionId);
                var sane = db.Saneamiento.Any(q => q.OrganizacionId == model.OrganizacionId);
                var exiAnterior = db.ExistenciaLegalAnterior.Any(q => q.OrganizacionId == model.OrganizacionId);
                var exisPosterior = db.ExistenciaPosterior.Any(q => q.OrganizacionId == model.OrganizacionId);
                var refAnterior = db.ReformaAnterior.Any(q => q.OrganizacionId == model.OrganizacionId);
                var refoPost = db.ReformaPosterior.Any(q => q.OrganizacionId == model.OrganizacionId);
                var refoAG = db.ReformaAGAC.Any(q => q.OrganizacionId == model.OrganizacionId);
                var exoAG = db.ExistenciaLegal.Any(q => q.OrganizacionId == model.OrganizacionId);

                if (exiAnterior == false && ExiAnt.FNorma != null)
                {
                    _custom.ExistenciaAnteriorCreate(model, ExiAnt);
                }
                else
                {
                    _custom.ExistenciaAnteriorUpdate(model.ExistenciaAnteriors);
                }

                if (exisPosterior == false && ExiPost.FechaConstitutivaSocios != null)
                {
                    _custom.ExistenciaPostCreate(model, ExiPost);
                }
                else
                {
                    _custom.ExistenciaPostUpdate(model.ExistenciaPosteriors);
                }

                if (refAnterior == false && refAnt.FechaNorma != null)
                {
                    _custom.ReformaAnteriorCreate(refAnt, model);
                }
                else
                {
                    _custom.ReformaUpdateAnt(model.ReformaAnteriors);
                }

                if (refoPost == false && refPost.FechaJuntGeneralSocios != null)
                {
                    _custom.ReformaPosteriorCreate(refPost, model);
                }
                else
                {
                    _custom.ReformaUpdatePost(model.ReformaPosteriors);
                }

                if (refoAG != false && refAG.FechaAsambleaDep != null)
                {
                    _custom.ReformaAGCreate(refAG, model);
                }
                else
                {
                    _custom.ReformaUpdateAG(model.ReformaAGACs);
                }

                if (exoAG == false && existenciaLegals.FechaConstitutivaSocios != null)
                {
                    _custom.ExistenciaCreate(model, existenciaLegals);
                }
                else
                {
                    _custom.ExistenciaUpdate(model.ExistenciaLegals, model);
                }

                if (sane == false && san.FechaaInscripcion != null)
                {
                    _custom.SaneamientoCreate(san, model);
                }
                else
                {
                    _custom.SaneamientoUpdate(model.Saneamientos);
                }

               


                _custom.DirectorioUpdate(model.Directorios);
                _custom.ModificacionUpdate(model.ModificacionEstatutos);
                _custom.DisolucionUpdate(model.Disolucions, disolucion, model.ComisionLiquidadoras);

                //model.Reformas = null;
                //db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();


                TempData["Message"] = Properties.Settings.Default.Success;
                return RedirectToAction("Edit", new { id = model.OrganizacionId });

            }

            ViewBag.CiudadId = new SelectList(db.Ciudad.OrderBy(q => q.Nombre), "CiudadId", "Nombre", model.CiudadId);
            ViewBag.ComunaId = new SelectList(db.Comuna.OrderBy(q => q.Nombre), "ComunaId", "Nombre", model.ComunaId);
            ViewBag.EstadoId = new SelectList(db.Estado.OrderBy(q => q.Nombre), "EstadoId", "Nombre", model.EstadoId);
            ViewBag.RegionId = new SelectList(db.Region.OrderBy(q => q.Nombre), "RegionId", "Nombre", model.RegionId);
            ViewBag.RubroId = new SelectList(db.Rubro.OrderBy(q => q.Nombre), "RubroId", "Nombre", model.RubroId);
            ViewBag.SubRubroId = new SelectList(db.SubRubro.OrderBy(q => q.Nombre), "SubRubroId", "Nombre", model.SubRubroId);
            ViewBag.TipoOrganizacionId = new SelectList(db.TipoOrganizacion.OrderBy(q => q.Nombre), "TipoOrganizacionId", "Nombre", model.TipoOrganizacionId);
            ViewBag.SituacionId = new SelectList(db.Situacion.OrderBy(q => q.Nombre), "SituacionId", "Nombre", model.SituacionId);
            ViewBag.CargoId = new SelectList(db.Cargo.OrderBy(q => q.Nombre), "CargoId", "Nombre");
            ViewBag.GeneroId = new SelectList(db.Genero.OrderBy(q => q.Nombre), "GeneroId", "Nombre");
            ViewBag.AprobacionId = new SelectList(db.Aprobacion.OrderBy(q => q.Nombre), "AprobacionId", "Nombre");
            ViewBag.AsambleaDepId = new SelectList(db.AsambleaDeposito.OrderBy(q => q.Descripcion).ToList(), "AsambleaDepId", "Descripcion");

            ViewBag.TipoNormaId = new SelectList(db.TipoNorma.OrderBy(q => q.Nombre).ToList(), "TipoNormaId", "Nombre");
            ViewBag.TipoNormaaId = new SelectList(db.TipoNorma.OrderBy(q => q.Nombre).ToList(), "TipoNormaaId", "Nombre");

            return View("Edit", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditOrganizacion(Organizacion model)
        {
            model.FechaActualizacion = DateTime.Now;

            if (ModelState.IsValid)
            {


                //model.Reformas = null;
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();


                TempData["Message"] = Properties.Settings.Default.Success;
                return RedirectToAction("Edit", new { id = model.OrganizacionId });

            }

            ViewBag.CiudadId = new SelectList(db.Ciudad.OrderBy(q => q.Nombre), "CiudadId", "Nombre", model.CiudadId);
            ViewBag.ComunaId = new SelectList(db.Comuna.OrderBy(q => q.Nombre), "ComunaId", "Nombre", model.ComunaId);
            ViewBag.EstadoId = new SelectList(db.Estado.OrderBy(q => q.Nombre), "EstadoId", "Nombre", model.EstadoId);
            ViewBag.RegionId = new SelectList(db.Region.OrderBy(q => q.Nombre), "RegionId", "Nombre", model.RegionId);
            ViewBag.RubroId = new SelectList(db.Rubro.OrderBy(q => q.Nombre), "RubroId", "Nombre", model.RubroId);
            ViewBag.SubRubroId = new SelectList(db.SubRubro.OrderBy(q => q.Nombre), "SubRubroId", "Nombre", model.SubRubroId);
            ViewBag.TipoOrganizacionId = new SelectList(db.TipoOrganizacion.OrderBy(q => q.Nombre), "TipoOrganizacionId", "Nombre", model.TipoOrganizacionId);
            ViewBag.SituacionId = new SelectList(db.Situacion.OrderBy(q => q.Nombre), "SituacionId", "Nombre", model.SituacionId);
            ViewBag.CargoId = new SelectList(db.Cargo.OrderBy(q => q.Nombre), "CargoId", "Nombre");
            ViewBag.GeneroId = new SelectList(db.Genero.OrderBy(q => q.Nombre), "GeneroId", "Nombre");
            ViewBag.AprobacionId = new SelectList(db.Aprobacion.OrderBy(q => q.Nombre), "AprobacionId", "Nombre");
            ViewBag.AsambleaDepId = new SelectList(db.AsambleaDeposito.OrderBy(q => q.Descripcion).ToList(), "AsambleaDepId", "Descripcion");


            ViewBag.TipoNormaId = new SelectList(db.TipoNorma.OrderBy(q => q.Nombre).ToList(), "TipoNormaId", "Nombre");
            ViewBag.TipoNormaaId = new SelectList(db.TipoNorma.OrderBy(q => q.Nombre).ToList(), "TipoNormaaId", "Nombre");

            return View("Edit", model);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organizacion organizacion = db.Organizacion.Find(id);
            if (organizacion == null)
            {
                return HttpNotFound();
            }
            return View(organizacion);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Organizacion organizacion = db.Organizacion.Find(id);
            db.Organizacion.Remove(organizacion);
            db.SaveChanges();
            TempData["Message"] = Properties.Settings.Default.Success;
            return RedirectToAction("Index");
        }

        public FileResult Download()
        {
            var db = new SistemaIntegradoContext();
            var file = string.Concat(Request.PhysicalApplicationPath, @"App_Data\ORGANIZACIONES.xlsx");
            var fileInfo = new FileInfo(file);
            var excelPackage = new ExcelPackage(fileInfo);

            var fila = 1;
            var worksheet = excelPackage.Workbook.Worksheets[1];
            var organizaciones = db.Organizacion.ToList();

            foreach (var organizacion in organizaciones)
            {
                fila++;
                worksheet.Cells[fila, 1].Value = organizacion.OrganizacionId;
                worksheet.Cells[fila, 2].Value = organizacion.TipoOrganizacion != null ? organizacion.TipoOrganizacion.Nombre : string.Empty;
                worksheet.Cells[fila, 3].Value = organizacion.Estado != null ? organizacion.Estado.Nombre : string.Empty;
                worksheet.Cells[fila, 4].Value = organizacion.Situacion != null ? organizacion.Situacion.Nombre : string.Empty;
                worksheet.Cells[fila, 5].Value = organizacion.Rubro != null ? organizacion.Rubro.Nombre : string.Empty;
                worksheet.Cells[fila, 6].Value = organizacion.SubRubro != null ? organizacion.SubRubro.Nombre : string.Empty;
                worksheet.Cells[fila, 7].Value = organizacion.Region != null ? organizacion.Region.Nombre : string.Empty;
                worksheet.Cells[fila, 8].Value = organizacion.Comuna != null ? organizacion.Comuna.Nombre : string.Empty;
                worksheet.Cells[fila, 9].Value = organizacion.Ciudad != null ? organizacion.Ciudad.Nombre : string.Empty;
                worksheet.Cells[fila, 10].Value = organizacion.NumeroRegistro;
                worksheet.Cells[fila, 11].Value = organizacion.RUT;
                worksheet.Cells[fila, 12].Value = organizacion.RazonSocial;
                worksheet.Cells[fila, 13].Value = organizacion.Sigla;
                worksheet.Cells[fila, 14].Value = organizacion.Direccion;
                worksheet.Cells[fila, 15].Value = organizacion.Fono;
                worksheet.Cells[fila, 16].Value = organizacion.Fax;
                worksheet.Cells[fila, 17].Value = organizacion.Email;
                worksheet.Cells[fila, 18].Value = organizacion.URL;
                worksheet.Cells[fila, 19].Value = organizacion.NumeroSocios;
                worksheet.Cells[fila, 20].Value = organizacion.NumeroSociosConstituyentes;
                worksheet.Cells[fila, 21].Value = organizacion.NumeroSociosHombres;
                worksheet.Cells[fila, 22].Value = organizacion.NumeroSociosMujeres;
                worksheet.Cells[fila, 23].Value = organizacion.MinistroDeFe;
                worksheet.Cells[fila, 24].Value = organizacion.EsGeneroFemenino;
                worksheet.Cells[fila, 25].Value = organizacion.CiudadAsamblea;
                worksheet.Cells[fila, 26].Value = organizacion.NombreContacto;
                worksheet.Cells[fila, 27].Value = organizacion.DireccionContacto;
                worksheet.Cells[fila, 28].Value = organizacion.TelefonoContacto;
                worksheet.Cells[fila, 29].Value = organizacion.EmailContacto;
                worksheet.Cells[fila, 30].Value = organizacion.FechaCreacion;
                worksheet.Cells[fila, 31].Value = organizacion.FechaCelebracion;
                worksheet.Cells[fila, 32].Value = organizacion.FechaPubliccionDiarioOficial;
                worksheet.Cells[fila, 33].Value = organizacion.EsImportanciaEconomica;
                worksheet.Cells[fila, 34].Value = organizacion.FechaActualizacion;
            }

            fila = 1;
            worksheet = excelPackage.Workbook.Worksheets[2];
            var directorios = db.Directorio.ToList();
            foreach (var directorio in directorios)
            {
                fila++;
                worksheet.Cells[fila, 1].Value = directorio.DirectorioId;
                worksheet.Cells[fila, 2].Value = directorio.Organizacion.NumeroRegistro;
                worksheet.Cells[fila, 3].Value = directorio.Organizacion.RazonSocial;
                worksheet.Cells[fila, 4].Value = directorio.NombreCompleto;
                worksheet.Cells[fila, 5].Value = directorio.Rut;
                worksheet.Cells[fila, 6].Value = directorio.FechaInicio;
                worksheet.Cells[fila, 7].Value = directorio.FechaTermino;
                worksheet.Cells[fila, 8].Value = directorio.Cargo != null ? directorio.Cargo.Nombre : string.Empty;
                worksheet.Cells[fila, 9].Value = directorio.Genero != null ? directorio.Genero.Nombre : string.Empty;
            }

            return File(excelPackage.GetAsByteArray(), System.Net.Mime.MediaTypeNames.Application.Octet, DateTime.Now.ToString("yyyyMMddhhmmss") + ".xlsx");
        }




        #region Directorio
        public ActionResult DirectorioAdd(int OrganizacionId)
        {
            db.Directorio.Add(new Directorio() { OrganizacionId = OrganizacionId, NombreCompleto = "?", GeneroId = (int)DAES.Infrastructure.Enum.Genero.SinGenero, CargoId = 135 });

            db.SaveChanges();

            //ViewBag.TipoNorma = new SelectList(db.Cargo.OrderBy(q => q.Nombre), "IdTipo", "Nombre");
            ViewBag.CargoId = new SelectList(db.Cargo.OrderBy(q => q.Nombre), "CargoId", "Nombre");
            ViewBag.GeneroId = new SelectList(db.Genero.OrderBy(q => q.Nombre), "GeneroId", "Nombre");
            ViewBag.TipoNormaId = new SelectList(db.TipoNorma.OrderBy(q => q.Nombre), "TipoNormaId", "Nombre");
            ViewBag.AprobacionId = new SelectList(db.Aprobacion.OrderBy(q => q.Nombre), "AprobacionId", "Nombre");
            ViewBag.AsambleaDepId = new SelectList(db.AsambleaDeposito.OrderBy(q => q.Descripcion), "AsambleaDepId", "Descripcion");
            ViewBag.TipoNormaaId = new SelectList(db.TipoNorma.OrderBy(q => q.Nombre).ToList(), "TipoNormaId", "Nombre");

            var model = db.Organizacion.Find(OrganizacionId);
            return PartialView("_DirectorioEdit", model);
        }


        public ActionResult ExistenciaAdd(int OrganizacionId, Organizacion models)
        {

            db.ExistenciaLegal.Add(new ExistenciaLegal()
            {
                OrganizacionId = OrganizacionId,
                TipoNormaId = models.TipoNormaId,

            });
            db.SaveChanges();


            ViewBag.CargoId = new SelectList(db.Cargo.OrderBy(q => q.Nombre), "CargoId", "Nombre");
            ViewBag.GeneroId = new SelectList(db.Genero.OrderBy(q => q.Nombre), "GeneroId", "Nombre");
            ViewBag.TipoNormaId = new SelectList(db.TipoNorma.OrderBy(q => q.Nombre), "TipoNormaId", "Nombre");
            ViewBag.AprobacionId = new SelectList(db.Aprobacion.OrderBy(q => q.Nombre), "AprobacionId", "Nombre");
            ViewBag.AsambleaDepId = new SelectList(db.AsambleaDeposito.OrderBy(q => q.Descripcion), "AsambleaDepId", "Descripcion");
            ViewBag.TipoNormaaId = new SelectList(db.TipoNorma.OrderBy(q => q.Nombre).ToList(), "TipoNormaId", "Nombre");

            var model = db.Organizacion.Find(OrganizacionId);
            return PartialView("_modificacionEdit", model);
        }

        public ActionResult DirectorioDelete(int DirectorioId, int OrganizacionId)
        {
            var directorio = db.Directorio.FirstOrDefault(q => q.DirectorioId == DirectorioId);
            if (directorio != null)
            {
                db.Directorio.Remove(directorio);
                db.SaveChanges();
            }

            var model = db.Organizacion.Find(OrganizacionId);

            ViewBag.CargoId = new SelectList(db.Cargo.OrderBy(q => q.Nombre), "CargoId", "Nombre");
            ViewBag.GeneroId = new SelectList(db.Genero.OrderBy(q => q.Nombre), "GeneroId", "Nombre");
            ViewBag.TipoNormaaId = new SelectList(db.TipoNorma.OrderBy(q => q.Nombre).ToList(), "TipoNormaId", "Nombre");

            return PartialView("_DirectorioEdit", model);
        }
        #endregion

        #region Modificacion
        public ActionResult ModificacionAdd(int OrganizacionId)
        {
            db.ModificacionEstatutos.Add(new ModificacionEstatuto() { OrganizacionId = OrganizacionId });
            db.SaveChanges();

            ViewBag.TipoNormaId = new SelectList(db.TipoNorma.OrderBy(q => q.Nombre), "TipoNormaId", "Nombre");
            ViewBag.AprobacionId = new SelectList(db.Aprobacion.OrderBy(q => q.Nombre), "AprobacionId", "Nombre");
            ViewBag.AsambleaDepId = new SelectList(db.AsambleaDeposito.OrderBy(q => q.Descripcion), "AsambleaDepId", "Descripcion");
            ViewBag.TipoNormaaId = new SelectList(db.TipoNorma.OrderBy(q => q.Nombre).ToList(), "TipoNormaId", "Nombre");

            var model = db.Organizacion.Find(OrganizacionId);

            return PartialView("_ModificacionEdit", model);
        }

        public ActionResult ModificacionDelete(int ModificacionEstatutoId, int OrganizacionId)
        {
            var modificacionEstatuto = db.ModificacionEstatutos.FirstOrDefault(q => q.ModificacionEstatutoId == ModificacionEstatutoId);
            if (modificacionEstatuto != null)
            {
                db.ModificacionEstatutos.Remove(modificacionEstatuto);
                db.SaveChanges();
            }
            ViewBag.TipoNormaId = new SelectList(db.TipoNorma.OrderBy(q => q.Nombre), "TipoNormaId", "Nombre");
            ViewBag.AprobacionId = new SelectList(db.Aprobacion.OrderBy(q => q.Nombre), "AprobacionId", "Nombre");
            ViewBag.AsambleaDepId = new SelectList(db.AsambleaDeposito.OrderBy(q => q.Descripcion), "AsambleaDepId", "Descripcion");
            ViewBag.TipoNormaaId = new SelectList(db.TipoNorma.OrderBy(q => q.Nombre).ToList(), "TipoNormaId", "Nombre");

            var model = db.Organizacion.Find(OrganizacionId);

            return PartialView("_ModificacionEdit", model);
        }
        #endregion

        public ActionResult DisolucionAdd(int OrganizacionId)
        {
            var model = db.Organizacion.Find(OrganizacionId);

            if (model.TipoOrganizacionId == (int)Infrastructure.Enum.TipoOrganizacion.Cooperativa)
            {
                var disolucion = new Disolucion() { OrganizacionId = OrganizacionId, TipoOrganizacionId = model.TipoOrganizacionId };
                ViewBag.CargoId = new SelectList(db.Cargo.OrderBy(q => q.Nombre).ToList(), "CargoId", "Nombre");
                ViewBag.GeneroId = new SelectList(db.Genero.OrderBy(q => q.Nombre).ToList(), "GeneroId", "Nombre");
                ViewBag.TipoNormaId = new SelectList(db.TipoNorma.OrderBy(q => q.Nombre).ToList(), "TipoNormaId", "Nombre");
                db.Disolucions.Add(disolucion);
                foreach (var item in model.Directorios)
                {
                    var aux = db.ComisionLiquidadora.Add(new ComisionLiquidadora()
                    {
                        OrganizacionId = item.OrganizacionId,
                        CargoId = item.CargoId,
                        GeneroId = item.GeneroId,
                        FechaInicio = item.FechaInicio,
                        FechaTermino = item.FechaTermino,
                        /*EsMiembro = model.ComisionLiquidadoras.FirstOrDefault().EsMiembro,*/
                        Rut = item.Rut,
                        DirectorioId = item.DirectorioId,
                        NombreCompleto = item.NombreCompleto
                    });
                    /*model.Disolucions.FirstOrDefault().ComisionLiquidadoras.Add(aux);*/
                }
                /*model.Disolucions.Add(disolucion);*/

                db.SaveChanges();
                return PartialView("_DisolucionEdit", model);
            }
            if (model.TipoOrganizacionId == (int)Infrastructure.Enum.TipoOrganizacion.AsociacionGremial ||
                model.TipoOrganizacionId == (int)Infrastructure.Enum.TipoOrganizacion.AsociacionConsumidores)
            {
                db.Disolucions.Add(new Disolucion() { OrganizacionId = OrganizacionId, TipoOrganizacionId = model.TipoOrganizacionId });
                db.SaveChanges();
                return PartialView("_DisolucionEdit", model);
            }
            return PartialView("_ErrorMessage", model);
        }


        public ActionResult ReformaAdd(int OrganizacionId)
        {
            db.ReformaAnterior.Add(new ReformaAnterior() { OrganizacionId = OrganizacionId}) ;
            db.SaveChanges();

            ViewBag.TipoNormaId = new SelectList(db.TipoNorma.OrderBy(q => q.Nombre), "TipoNormaId", "Nombre");
            ViewBag.AprobacionId = new SelectList(db.Aprobacion.OrderBy(q => q.Nombre), "AprobacionId", "Nombre");
            //ViewBag.AsambleaDepId = new SelectList(db.AsambleaDeposito.OrderBy(q => q.Descripcion), "AsambleaDepId", "Descripcion");
            ViewBag.TipoNormaaId = new SelectList(db.TipoNorma.OrderBy(q => q.Nombre).ToList(), "TipoNormaId", "Nombre");
            ViewBag.AsambleaDepId = new SelectList(db.AsambleaDeposito.OrderBy(q => q.Descripcion).ToList(), "AsambleaDepId", "Descripcion");


            var model = db.Organizacion.Find(OrganizacionId);
            return PartialView("_Reforma", model);
        }
        
        public ActionResult ReformaAddAGAC(int OrganizacionId)
        {
            db.ReformaAGAC.Add(new ReformaAGAC() { OrganizacionId = OrganizacionId}) ;
            db.SaveChanges();
            
            ViewBag.TipoNormaId = new SelectList(db.TipoNorma.OrderBy(q => q.Nombre), "TipoNormaId", "Nombre");
            ViewBag.AprobacionId = new SelectList(db.Aprobacion.OrderBy(q => q.Nombre), "AprobacionId", "Nombre");
            //ViewBag.AsambleaDepId = new SelectList(db.AsambleaDeposito.OrderBy(q => q.Descripcion), "AsambleaDepId", "Descripcion");
            ViewBag.AsambleaDepId = new SelectList(db.AsambleaDeposito.OrderBy(q => q.Descripcion).ToList(), "AsambleaDepId", "Descripcion");
            ViewBag.TipoNormaaId = new SelectList(db.TipoNorma.OrderBy(q => q.Nombre).ToList(), "TipoNormaId", "Nombre");
            

            var model = db.Organizacion.Find(OrganizacionId);
            return PartialView("_ReformaAGAC", model);
        }
              

        public ActionResult ReformaAddPost(int OrganizacionId)
        {
            db.ReformaPosterior.Add(new ReformaPosterior() { OrganizacionId = OrganizacionId });
            db.SaveChanges();

            ViewBag.TipoNormaId = new SelectList(db.TipoNorma.OrderBy(q => q.Nombre), "TipoNormaId", "Nombre");
            ViewBag.AprobacionId = new SelectList(db.Aprobacion.OrderBy(q => q.Nombre), "AprobacionId", "Nombre");
            //ViewBag.AsambleaDepId = new SelectList(db.AsambleaDeposito.OrderBy(q => q.Descripcion), "AsambleaDepId", "Descripcion");
            ViewBag.AsambleaDepId = new SelectList(db.AsambleaDeposito.OrderBy(q => q.Descripcion).ToList(), "AsambleaDepId", "Descripcion");
            ViewBag.TipoNormaaId = new SelectList(db.TipoNorma.OrderBy(q => q.Nombre).ToList(), "TipoNormaId", "Nombre");

            var model = db.Organizacion.Find(OrganizacionId);
            return PartialView("_ReformaPost", model);
        }

        public ActionResult ReformaDeletePost(int IdReformaPost, int OrganizacionId)
        {
            var reforma = db.ReformaPosterior.FirstOrDefault(q => q.IdReformaPost == IdReformaPost);
            if (reforma != null)
            {
                db.ReformaPosterior.Remove(reforma);
                db.SaveChanges();
            }
            ViewBag.TipoNormaId = new SelectList(db.TipoNorma.OrderBy(q => q.Nombre), "TipoNormaId", "Nombre");
            ViewBag.AprobacionId = new SelectList(db.Aprobacion.OrderBy(q => q.Nombre), "AprobacionId", "Nombre");
            //ViewBag.AsambleaDepId = new SelectList(db.AsambleaDeposito.OrderBy(q => q.Descripcion), "AsambleaDepId", "Descripcion");
            ViewBag.AsambleaDepId = new SelectList(db.AsambleaDeposito.OrderBy(q => q.Descripcion).ToList(), "AsambleaDepId", "Descripcion");
            ViewBag.TipoNormaaId = new SelectList(db.TipoNorma.OrderBy(q => q.Nombre).ToList(), "TipoNormaId", "Nombre");

            var model = db.Organizacion.Find(OrganizacionId);
            return PartialView("_ReformaPost", model);
        }
        //public ActionResult ReformaDelete(int IdReforma, int OrganizacionId)
        //{
        //    var reforma = db.Reforma.FirstOrDefault(q => q.IdReforma == IdReforma);
        //    if (reforma != null)
        //    {
        //        db.Reforma.Remove(reforma);
        //        db.SaveChanges();
        //    }
        //    ViewBag.TipoNormaId = new SelectList(db.TipoNorma.OrderBy(q => q.Nombre), "TipoNormaId", "Nombre");
        //    ViewBag.AprobacionId = new SelectList(db.Aprobacion.OrderBy(q => q.Nombre), "AprobacionId", "Nombre");
        //    ViewBag.AsambleaDepId = new SelectList(db.AsambleaDeposito.OrderBy(q => q.Descripcion), "AsambleaDepId", "Descripcion");
        //    ViewBag.TipoNormaaId = new SelectList(db.TipoNorma.OrderBy(q => q.Nombre).ToList(), "TipoNormaId", "Nombre");

        //    var model = db.Organizacion.Find(OrganizacionId);
        //    return PartialView("_Reforma", model);
        //}
        
        public ActionResult ReformaDeleteAGAC(int IdReformaAGAC, int OrganizacionId)
        {
            var reforma = db.ReformaAGAC.FirstOrDefault(q => q.IdReformaAGAC == IdReformaAGAC);
            if (reforma != null)
            {
                db.ReformaAGAC.Remove(reforma);
                db.SaveChanges();
            }
            ViewBag.TipoNormaId = new SelectList(db.TipoNorma.OrderBy(q => q.Nombre), "TipoNormaId", "Nombre");
            ViewBag.AprobacionId = new SelectList(db.Aprobacion.OrderBy(q => q.Nombre), "AprobacionId", "Nombre");
            //ViewBag.AsambleaDepId = new SelectList(db.AsambleaDeposito.OrderBy(q => q.Descripcion), "AsambleaDepId", "Descripcion");
            ViewBag.TipoNormaaId = new SelectList(db.TipoNorma.OrderBy(q => q.Nombre).ToList(), "TipoNormaId", "Nombre");
            ViewBag.AsambleaDepId = new SelectList(db.AsambleaDeposito.OrderBy(q => q.Descripcion), "AsambleaDepId", "Descripcion");

            var model = db.Organizacion.Find(OrganizacionId);
            return PartialView("_ReformaAGAC", model);
        }
        public ActionResult ReformaDeleteAnterior(int IdReformaAnterior, int OrganizacionId)
        {
            var reforma = db.ReformaAnterior.FirstOrDefault(q => q.IdReformaAnterior == IdReformaAnterior);
            if (reforma != null)
            {
                db.ReformaAnterior.Remove(reforma);
                db.SaveChanges();
            }
            ViewBag.TipoNormaId = new SelectList(db.TipoNorma.OrderBy(q => q.Nombre), "TipoNormaId", "Nombre");
            ViewBag.AprobacionId = new SelectList(db.Aprobacion.OrderBy(q => q.Nombre), "AprobacionId", "Nombre");
            //ViewBag.AsambleaDepId = new SelectList(db.AsambleaDeposito.OrderBy(q => q.Descripcion), "AsambleaDepId", "Descripcion");
            ViewBag.AsambleaDepId = new SelectList(db.AsambleaDeposito.OrderBy(q => q.Descripcion).ToList(), "AsambleaDepId", "Descripcion");
            ViewBag.TipoNormaaId = new SelectList(db.TipoNorma.OrderBy(q => q.Nombre).ToList(), "TipoNormaId", "Nombre");

            var model = db.Organizacion.Find(OrganizacionId);
            return PartialView("_Reforma", model);
        }

        public ActionResult EliminarSanemiento(int IdSaneamiento, int IdOrganizacion)
        {
            var saneamiento = db.Saneamiento.FirstOrDefault(q => q.IdSaneamiento == IdSaneamiento);
            if (saneamiento != null)
            {
                db.Saneamiento.Remove(saneamiento);
                db.SaveChanges();
            }
            var model = db.Organizacion.Find(IdOrganizacion);
            ViewBag.CiudadId = new SelectList(db.Ciudad.OrderBy(q => q.Nombre), "CiudadId", "Nombre", model.CiudadId);
            ViewBag.ComunaId = new SelectList(db.Comuna.OrderBy(q => q.Nombre), "ComunaId", "Nombre", model.ComunaId);
            ViewBag.EstadoId = new SelectList(db.Estado.OrderBy(q => q.Nombre), "EstadoId", "Nombre", model.EstadoId);
            ViewBag.RegionId = new SelectList(db.Region.OrderBy(q => q.Nombre), "RegionId", "Nombre", model.RegionId);
            ViewBag.RubroId = new SelectList(db.Rubro.OrderBy(q => q.Nombre), "RubroId", "Nombre", model.RubroId);
            ViewBag.SubRubroId = new SelectList(db.SubRubro.OrderBy(q => q.Nombre), "SubRubroId", "Nombre", model.SubRubroId);
            ViewBag.TipoOrganizacionId = new SelectList(db.TipoOrganizacion.OrderBy(q => q.Nombre), "TipoOrganizacionId", "Nombre", model.TipoOrganizacionId);
            ViewBag.SituacionId = new SelectList(db.Situacion.OrderBy(q => q.Nombre), "SituacionId", "Nombre", model.SituacionId);
            ViewBag.CargoId = new SelectList(db.Cargo.OrderBy(q => q.Nombre), "CargoId", "Nombre");
            ViewBag.GeneroId = new SelectList(db.Genero.OrderBy(q => q.Nombre), "GeneroId", "Nombre");
            ViewBag.AprobacionId = new SelectList(db.Aprobacion.OrderBy(q => q.Nombre), "AprobacionId", "Nombre");
            ViewBag.AsambleaDepId = new SelectList(db.AsambleaDeposito.OrderBy(q => q.Descripcion).ToList(), "AsambleaDepId", "Descripcion");


            ViewBag.TipoNormaId = new SelectList(db.TipoNorma.OrderBy(q => q.Nombre).ToList(), "TipoNormaId", "Nombre");
            ViewBag.TipoNormaaId = new SelectList(db.TipoNorma.OrderBy(q => q.Nombre).ToList(), "TipoNormaId", "Nombre");

            return View("Edit", model);
        }

        public ActionResult DisolucionDelete(int DisolucionId, int OrganizacionId)
        {
            var disolucion = db.Disolucions.FirstOrDefault(q => q.DisolucionId == DisolucionId);
            var comision = db.ComisionLiquidadora.Where(q => q.OrganizacionId == OrganizacionId).ToList();

            if (disolucion != null)
            {
                db.Disolucions.Remove(disolucion);
                db.SaveChanges();
            }
            foreach (var item in comision)
            {
                db.ComisionLiquidadora.Remove(item);
                db.SaveChanges();
            }

            var model = db.Organizacion.Find(OrganizacionId);
            ViewBag.CargoId = new SelectList(db.Cargo.OrderBy(q => q.Nombre).ToList(), "CargoId", "Nombre");
            ViewBag.GeneroId = new SelectList(db.Genero.OrderBy(q => q.Nombre).ToList(), "GeneroId", "Nombre");
            ViewBag.TipoNormaId = new SelectList(db.TipoNorma.OrderBy(q => q.Nombre).ToList(), "TipoNormaId", "Nombre");

            return PartialView("_DisolucionEdit", model);
        }

        public ActionResult ExistenciaDelete(int IdExistencia, int OrganizacionId)
        {
            var existencia = db.ExistenciaLegal.FirstOrDefault(q => q.ExistenciaId == IdExistencia);
            if (existencia != null)
            {
                db.ExistenciaLegal.Remove(existencia);
                db.SaveChanges();
            }
            var model = db.Organizacion.Find(OrganizacionId);
            return PartialView("_ExistenciaEdit", model);
        }
        public ActionResult ComisionAdd(int OrganizacionId)
        {
            ViewBag.CargoId = new SelectList(db.Cargo.OrderBy(q => q.Nombre), "CargoId", "Nombre");
            ViewBag.GeneroId = new SelectList(db.Genero.OrderBy(q => q.Nombre), "GeneroId", "Nombre");
            ViewBag.TipoNormaId = new SelectList(db.TipoNorma.OrderBy(q => q.Nombre).ToList(), "TipoNormaId", "Nombre");

            var model = db.Organizacion.Find(OrganizacionId);
            var comi = db.ComisionLiquidadora.Add(new ComisionLiquidadora() { OrganizacionId = OrganizacionId });

            db.SaveChanges();

            return PartialView("_ComisionEdit", model);
        }

        public ActionResult ComisionDelete(int ComisionLiquidadoraId, int OrganizacionId)
        {
            ViewBag.CargoId = new SelectList(db.Cargo.OrderBy(q => q.Nombre), "CargoId", "Nombre");
            ViewBag.GeneroId = new SelectList(db.Genero.OrderBy(q => q.Nombre), "GeneroId", "Nombre");
            ViewBag.TipoNormaId = new SelectList(db.TipoNorma.OrderBy(q => q.Nombre).ToList(), "TipoNormaId", "Nombre");
            var comision = db.ComisionLiquidadora.FirstOrDefault(q => q.ComisionLiquidadoraId == ComisionLiquidadoraId);

            if (comision != null)
            {
                db.ComisionLiquidadora.Remove(comision);
                db.SaveChanges();
            }

            var model = db.Organizacion.Find(OrganizacionId);
            return PartialView("_ComisionEdit", model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}