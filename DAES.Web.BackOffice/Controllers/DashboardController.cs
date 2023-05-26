using DAES.Infrastructure.SistemaIntegrado;
using DAES.Model.SistemaIntegrado;
using DAES.Web.BackOffice.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace DAES.Web.BackOffice.Controllers
{

    [Audit]
    [Authorize]
    public class DashboardController : Controller
    {
        private SistemaIntegradoContext db = new SistemaIntegradoContext();

        public ActionResult Index()
        {
            var proceso = db.Proceso;
            Response.AddHeader("Refresh", "60");
            return View(proceso.ToList());
        }

        public class Chart
        {
            public string[] labels { get; set; }
            public List<Datasets> datasets { get; set; }
        }

        public class Datasets
        {
            public string label { get; set; }
            public string[] backgroundColor { get; set; }
            public string[] borderColor { get; set; }
            public string borderWidth { get; set; }
            public int[] data { get; set; }
            public bool fill { get; set; }
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proceso proceso = db.Proceso.Find(id);
            if (proceso == null)
            {
                return HttpNotFound();
            }
            return View(proceso);
        }

        public JsonResult NumeroProcesosNoTerminados()
        {
            var result = db.Proceso.Where(q => !q.Terminada).GroupBy(q => q.DefinicionProceso.Nombre).Select(y => new { Text = y.Key, Value = y.Count() });

            Chart _chart = new Chart()
            {
                labels = result.ToList().Select(q => q.Text).ToArray(),
                datasets = new List<Datasets>()
            };
            List<Datasets> _dataSet = new List<Datasets>
            {
                new Datasets()
                {
                    data = result.ToList().Select(q => q.Value).ToArray(),
                backgroundColor = new string[] { "#2ecc71", "#3498db", "#f1c40f", "#e74c3c", "#9b59b6", "#9b59b6", "#34495e" },
                }
            };
            _chart.datasets = _dataSet;
            return Json(_chart, JsonRequestBehavior.AllowGet);
        }

        public JsonResult NumeroProcesos()
        {
            var result = db.Proceso.GroupBy(q => q.DefinicionProceso.Nombre).Select(y => new { Text = y.Key, Value = y.Count() });

            Chart _chart = new Chart()
            {
                labels = result.ToList().Select(q => q.Text).ToArray(),
                datasets = new List<Datasets>()
            };
            List<Datasets> _dataSet = new List<Datasets>
            {
                new Datasets()
                {
                    data = result.ToList().Select(q => q.Value).ToArray(),
                backgroundColor = new string[] { "#2ecc71", "#3498db", "#f1c40f", "#e74c3c", "#9b59b6", "#9b59b6", "#34495e" },
                }
            };
            _chart.datasets = _dataSet;
            return Json(_chart, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FuncionarioTareas()
        {
            var result = db.Workflow.Where(q => !q.Proceso.Terminada).GroupBy(q => q.User.UserName).Select(y => new { Text = y.Key, Value = y.Count() });

            Chart _chart = new Chart();
            _chart.labels = result.ToList().Select(q => q.Text).ToArray();
            _chart.datasets = new List<Datasets>();
            List<Datasets> _dataSet = new List<Datasets>();
            _dataSet.Add(new Datasets()
            {
                data = result.ToList().Select(q => q.Value).ToArray(),
                backgroundColor = new string[] { "#2ecc71", "#3498db", "#f1c40f", "#e74c3c", "#9b59b6", "#9b59b6", "#34495e" },
            });
            _chart.datasets = _dataSet;
            return Json(_chart, JsonRequestBehavior.AllowGet);
        }

        public JsonResult OrganizacionProceso()
        {
            var result = db.Proceso.Where(q => !q.Terminada).GroupBy(q => q.Organizacion.NumeroRegistro).Select(y => new { Text = y.Key, Value = y.Count() });

            Chart _chart = new Chart();
            _chart.labels = result.ToList().Select(q => q.Text).ToArray();
            _chart.datasets = new List<Datasets>();
            List<Datasets> _dataSet = new List<Datasets>();
            _dataSet.Add(new Datasets()
            {
                data = result.ToList().Select(q => q.Value).ToArray(),
                backgroundColor = new string[] { "#2ecc71", "#3498db", "#f1c40f", "#e74c3c", "#9b59b6", "#9b59b6", "#34495e" },
            });
            _chart.datasets = _dataSet;
            return Json(_chart, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ProcesoEstado()
        {
            var result = db.Proceso.Where(q => !q.Terminada)
                .GroupBy(q => q.FechaVencimiento > DateTime.Now)
                .Select(y => new { Text = y.Key ? "Terminado" : "En proceso", Value = y.Count() });

            Chart _chart = new Chart();
            _chart.labels = result.ToList().Select(q => q.Text).ToArray();
            _chart.datasets = new List<Datasets>();
            List<Datasets> _dataSet = new List<Datasets>();
            _dataSet.Add(new Datasets()
            {
                data = result.ToList().Select(q => q.Value).ToArray(),
                backgroundColor = new string[] { "#2ecc71", "#3498db", "#f1c40f", "#e74c3c", "#9b59b6", "#9b59b6", "#34495e" },
                //borderColor = new string[] { "#FF0000", "#800000", "#808000", "#008080", "#800080", "#0000FF", "#000080", "#999999", "#E9967A", "#CD5C5C", "#1A5276", "#27AE60" },
            });
            _chart.datasets = _dataSet;
            return Json(_chart, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ProcesoDia()
        {
            var result = db.Proceso.Where(q => !q.Terminada)
                .GroupBy(q => q.FechaCreacion)
                .Select(y => new { Text = y.Key.ToShortDateString(), Value = y.Count() });

            Chart _chart = new Chart();
            _chart.labels = result.ToList().Select(q => q.Text).ToArray();
            _chart.datasets = new List<Datasets>();
            List<Datasets> _dataSet = new List<Datasets>();
            _dataSet.Add(new Datasets()
            {
                data = result.ToList().Select(q => q.Value).ToArray(),
                backgroundColor = new string[] { "#e74c3c", "#2ecc71" },
                //borderColor = new string[] { "#FF0000", "#800000", "#808000", "#008080", "#800080", "#0000FF", "#000080", "#999999", "#E9967A", "#CD5C5C", "#1A5276", "#27AE60" },
            });
            _chart.datasets = _dataSet;
            return Json(_chart, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LineChart()
        {
            var result = db.Proceso.GroupBy(q => q.DefinicionProceso.Nombre).Select(y => new { Text = y.Key, Value = y.Count() });

            Chart _chart = new Chart();
            _chart.labels = result.ToList().Select(q => q.Text).ToArray();
            _chart.datasets = new List<Datasets>();
            List<Datasets> _dataSet = new List<Datasets>();

            _dataSet.Add(new Datasets()
            {
                label = "Current Year",
                data = result.ToList().Select(q => q.Value).ToArray(),
                borderColor = new string[] { "#800080" },
                borderWidth = "1"
            });

            _chart.datasets = _dataSet;
            return Json(_chart, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Eficiencia()
        {
            var groups = from p in db.Proceso
                         group p by new
                         {
                             p.DefinicionProceso.Nombre,
                             p.FechaCreacion.Day
                         }
                        into gcs
                         select new
                         {
                             Proceso = gcs.Key.Nombre,
                             Fecha = gcs.Key.Day.ToString(),
                             Total = gcs.Count()
                         };

            Chart _chart = new Chart();
            //_chart.labels = new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "Novemeber", "December" };
            _chart.labels = groups.ToList().Select(q => q.Fecha).Distinct().ToArray();
            _chart.datasets = new List<Datasets>();
            List<Datasets> _dataSet = new List<Datasets>();

            foreach (var item in groups.Select(q => q.Proceso).Distinct())
            {
                _dataSet.Add(new Datasets()
                {
                    label = item,
                    //data = new int[] { 28, 48, 40, 19, 86, 27, 90, 20, 45, 65, 34, 22 },
                    data = groups.Where(q => q.Proceso == item).ToList().Select(q => q.Total).ToArray(),
                    borderColor = new string[] { "rgba(75,192,192,1)" },
                    backgroundColor = new string[] { "rgba(75,192,192,0.4)" },
                    borderWidth = "1",
                });
            }
            _chart.datasets = _dataSet;
            return Json(_chart, JsonRequestBehavior.AllowGet);
        }

        public JsonResult MultiLineChart()
        {
            Chart _chart = new Chart();
            _chart.labels = new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "Novemeber", "December" };
            _chart.datasets = new List<Datasets>();
            List<Datasets> _dataSet = new List<Datasets>();

            _dataSet.Add(new Datasets()
            {
                label = "Current Year",
                data = new int[] { 28, 48, 40, 19, 86, 27, 90, 20, 45, 65, 34, 22 },
                borderColor = new string[] { "rgba(75,192,192,1)" },
                backgroundColor = new string[] { "rgba(75,192,192,0.4)" },
                borderWidth = "1",
            });
            _dataSet.Add(new Datasets()
            {
                label = "Last Year",
                data = new int[] { 65, 59, 80, 81, 56, 55, 40, 55, 66, 77, 88, 34 },
                borderColor = new string[] { "rgba(75,192,192,1)" },
                backgroundColor = new string[] { "rgba(75,192,192,0.4)" },
                borderWidth = "1"
            });
            _chart.datasets = _dataSet;
            return Json(_chart, JsonRequestBehavior.AllowGet);
        }

        public class Stats
        {

            public Stats()
            {
                items = new List<item>();
            }

            public int total { get; set; }
            public List<item> items { get; set; }
        }

        public class item
        {

            public item()
            {
            }

            public string username { get; set; }
            public int pendientes { get; set; }
            public int terminados { get; set; }
            public int totales { get; set; }

            public decimal ppendientes { get; set; }
            public decimal pterminados { get; set; }
            public decimal ptotales { get; set; }
        }

        public ActionResult Estadistica()
        {
            Response.AddHeader("Refresh", "60");

            var query = from w in db.Workflow
                        group w by w.User.UserName into g
                        select new item
                        {
                            username = g.Key,
                            totales = g.Count(),
                            pendientes = g.Count(q => !q.Terminada),
                            terminados = g.Count(q => q.Terminada),
                        };

            var st = new Stats();
            st.total = query.Select(q => q.totales).DefaultIfEmpty(0).Sum();
            st.items = query.ToList();

            if (st.total > 0)
            {
                foreach (var item in st.items)
                {
                    item.ppendientes = decimal.Divide(item.pendientes, st.total);
                    item.pterminados = decimal.Divide(item.terminados, st.total);
                    item.ptotales = decimal.Divide(item.totales, st.total);
                }
            }

            //foreach (var user in users)
            //{
            //    var obj = new item();
            //    obj.username = user.UserName;
            //    obj.pendientes = db.Workflow.Count(q => q.UserId == user.Id && !q.Terminada);
            //    obj.terminados = db.Workflow.Count(q => q.UserId == user.Id && q.Terminada);
            //    obj.totales = db.Workflow.Count(q => q.UserId == user.Id);

            //    if (st.total > 0)
            //    {
            //        obj.ppendientes = decimal.Divide(obj.pendientes,st.total);
            //        obj.pterminados = decimal.Divide(obj.terminados, st.total);
            //        obj.ptotales = decimal.Divide(obj.totales, st.total);
            //    }

            //    st.items.Add(obj);
            //}
            return View(st);
        }
    }
}