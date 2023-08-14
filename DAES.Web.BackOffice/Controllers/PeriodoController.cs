using DAES.Infrastructure.SistemaIntegrado;
using DAES.Model.SistemaIntegrado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DAES.Web.BackOffice.Controllers
{
    public class PeriodoController : Controller
    {
        private BLL.Custom _custom = new BLL.Custom();
        private SistemaIntegradoContext db = new SistemaIntegradoContext();

        // GET: Periodo
        public ActionResult Index()
        {
            return View();
        }

        // GET: Periodo/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Periodo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Periodo/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Periodo/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Periodo/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Periodo/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Periodo/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult PeriodoCac()
        {
            ViewBag.PeriodoId = new SelectList(db.PeriodoCAC.Where(q => q.Tipo == "ModeloSupervisionCAC").OrderByDescending(q => q.PeriodoId), "Descripcion", "Descripcion");
            ViewBag.periodo = db.PeriodoCAC.Where(q => q.Tipo == "ModeloSupervisionCAC").ToList();
            return View();
        }

        public ActionResult PeriodoNoCac()
        {
            ViewBag.PeriodoId = new SelectList(db.PeriodoCAC.Where(q => q.Tipo == "ModeloSupervisionNOCAC").OrderByDescending(q => q.PeriodoId), "Descripcion", "Descripcion");
            ViewBag.periodo = db.PeriodoCAC.Where(q => q.Tipo == "ModeloSupervisionNOCAC").ToList();
            return View();
        }

        public ActionResult PeriodoArticulo90()
        {
            ViewBag.PeriodoId = new SelectList(db.PeriodoCAC.Where(q => q.Tipo == "articulo90").OrderByDescending(q => q.PeriodoId), "Descripcion", "Descripcion");
            ViewBag.periodo = db.PeriodoCAC.Where(q => q.Tipo == "articulo90").ToList();
            return View();
        }
        [HttpGet]
        public ActionResult CreatePeriodoCAC()
        {

            return View();
        }

        [HttpGet]
        public ActionResult CreatePeriodoNoCAC()
        {

            return View();
        }

        [HttpGet]
        public ActionResult CreatePeriodoArticulo90()
        {

            return View();
        }

        [HttpPost]
        public ActionResult CreatePeriodoNoCAC(PeriodoCAC model)
        {
            _custom.CrearPeriodoNoCAC(model);


            return RedirectToAction("PeriodoNoCac", "Periodo");
        }

        [HttpPost]
        public ActionResult CreatePeriodoArticulo90(PeriodoCAC model)
        {
            _custom.CreatePeriodoArticulo90(model);


            return RedirectToAction("PeriodoArticulo90", "Periodo");
        }

        [HttpPost]
        public ActionResult CreatePeriodoCAC(PeriodoCAC model)
        {
            _custom.CrearPeriodoCAC(model);


            return RedirectToAction("PeriodoCac", "Periodo");
        }

        [HttpGet]
        public ActionResult EditPeriodoCAC(int periodoId)
        {
            ViewBag.Periodo = db.PeriodoCAC.Where(q => q.PeriodoId == periodoId);
            PeriodoCAC periodo = db.PeriodoCAC.Find(periodoId);

            return View(periodo);
        }

        [HttpGet]
        public ActionResult EditPeriodoArticulo90(int periodoId)
        {
            ViewBag.Periodo = db.PeriodoCAC.Where(q => q.PeriodoId == periodoId);
            PeriodoCAC periodo = db.PeriodoCAC.Find(periodoId);

            return View(periodo);
        }

        [HttpGet]
        public ActionResult EditPeriodoNoCAC(int periodoId)
        {
            ViewBag.Periodo = db.PeriodoCAC.Where(q => q.PeriodoId == periodoId);
            PeriodoCAC periodo = db.PeriodoCAC.Find(periodoId);

            return View(periodo);
        }

        [HttpPost]
        public ActionResult EditPeriodoNoCAC(PeriodoCAC periodo)
        {
            var periodos = db.PeriodoCAC.Where(q => q.PeriodoId == periodo.PeriodoId);
            _custom.EditPeriodoNoCAC(periodo);

            return RedirectToAction("PeriodoCac", "Periodo");
        }

        [HttpPost]
        public ActionResult EditPeriodoArticulo90(PeriodoCAC periodo)
        {
            var periodos = db.PeriodoCAC.Where(q => q.PeriodoId == periodo.PeriodoId);
            _custom.EditPeriodoArticulo90(periodo);

            return RedirectToAction("PeriodoArticulo90", "Periodo");
        }

        [HttpPost]
        public ActionResult EditPeriodoCAC(PeriodoCAC periodo)
        {
            var periodos = db.PeriodoCAC.Where(q => q.PeriodoId == periodo.PeriodoId);
            _custom.EditPeriodoCAC(periodo);

            return RedirectToAction("PeriodoNoCac", "Periodo");
        }

        public ActionResult EliminarPeriodoCac(int PeriodoId)
        {
            _custom.EliminarPeriodoCAC(PeriodoId);


            return RedirectToAction("PeriodoCac", "Periodo");
        }

        public ActionResult EliminarPeriodoNoCac(int PeriodoId)
        {
            _custom.EliminarPeriodoCAC(PeriodoId);


            return RedirectToAction("PeriodoNoCac", "Periodo");
        }

        public ActionResult EliminarPeriodoArticulos(int PeriodoId)
        {
            _custom.EliminarPeriodoCAC(PeriodoId);


            return RedirectToAction("PeriodoArticulo90", "Periodo");
        }
    }
}
