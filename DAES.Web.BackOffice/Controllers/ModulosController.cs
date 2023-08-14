using DAES.Infrastructure.SistemaIntegrado;
using DAES.Model.DTO;
using DAES.Model.SistemaIntegrado;
using DAES.Web.BackOffice.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DAES.Web.BackOffice.Controllers
{
    [Audit]
    [Authorize]

    public class ModulosController : Controller
    {
        private SistemaIntegradoContext db = new SistemaIntegradoContext();
        private BLL.Custom _custom = new BLL.Custom();


        // GET: Modulos
        public ActionResult Index()
        {
            var Hoy = DateTime.Now;

            if (!User.Identity.IsAuthenticated || Helper.Helper.CurrentUser == null)
                return RedirectToAction("LogOff", "Account");

            ViewBag.Users = db.Users.ToList();
            return View();

        }

        [HttpPost]
        public ActionResult Edit(DTOModulo model)
        {

            var modulos = db.ModulosConsulta.Where(q => q.Email == model.Email).ToList();

            //si no existe el objeto de modulo, se crea, sino se edita
            if (!modulos.Any())
            {
                _custom.CrearModuloConsulta(model);
                ViewBag.Users = db.Users.ToList();
                return View("Index");
            }
            else
            {
                _custom.EditarModulosConsulta(model);
                ViewBag.Users = db.Users.ToList();
                return View("Index");
            }


        }


        public ActionResult Edit(string UserName)
        {
            var usuario = db.Users.Where(q => q.UserName == UserName).ToList();
            ViewBag.Users = db.Users.ToList();
            var model = new DTOModulo();
            foreach (var item in usuario)
            {
                var modulos = db.ModulosConsulta.Where(q => q.Email == item.Email).ToList();
                if (!modulos.Any())
                {
                    model.NombreFuncionario = item.Nombre;
                    model.UserName = item.UserName;
                    model.Email = item.Email;
                    model.Id = item.Id;
                }
                else
                {
                    for (int i = 0; i < modulos.Count(); i++)
                    {
                        model.NombreFuncionario = item.Nombre;
                        model.UserName = item.UserName;
                        model.Email = item.Email;
                        model.IdModulos = modulos[i].IdModulos;
                        model.Ayuda = modulos[i].Ayuda;
                        model.Cargos = modulos[i].Cargos;
                        model.ConfigurarCertificados = modulos[i].ConfigurarCertificados;
                        model.EstadosDeOrganizacion = modulos[i].EstadosDeOrganizacion;
                        model.Id = item.Id;
                        model.Firmantes = modulos[i].Firmantes;
                        model.Generos = modulos[i].Generos;
                        model.HechosLegales = modulos[i].HechosLegales;
                        model.HechosContables = modulos[i].HechosContables;
                        model.Organizaciones = modulos[i].Organizaciones;
                        model.Regiones = modulos[i].Regiones;
                        model.Rubros = modulos[i].Rubros;
                        model.Situacion = modulos[i].Situacion;
                        model.SubRubros = modulos[i].SubRubros;
                        model.TiposDeDocumentos = modulos[i].TiposDeDocumentos;
                        model.TiposDeOrganizaciones = modulos[i].TiposDeOrganizaciones;
                        model.TiposDeMateria = modulos[i].TiposDeMateria;
                        model.TiposDeCriterios = modulos[i].TiposDeCriterios;
                        model.TiposDeFiscalizacion = modulos[i].TiposDeFiscalizacion;
                        model.TiposDeHallazgos = modulos[i].TiposDeHallazgos;
                        model.TiposDeOficios = modulos[i].TiposDeOficios;
                        model.DefinirTareas = modulos[i].DefinirTareas;
                        model.DefinirProcesos = modulos[i].DefinirProcesos;
                        model.IniciarProcesoGestionDocumental = modulos[i].IniciarProcesoGestionDocumental;
                        model.IniciarProcesoManualmente = modulos[i].IniciarProcesoManualmente;
                        model.AdministrarProcesos = modulos[i].AdministrarProcesos;
                        model.AdministrarCargaDeTareas = modulos[i].AdministrarCargaDeTareas;
                        model.DashboardProcesos = modulos[i].DashboardProcesos;
                        model.DashboardTareas = modulos[i].DashboardTareas;
                        model.CuentasUsuario = modulos[i].CuentasUsuario;
                        model.PerfilesDeUsuario = modulos[i].PerfilesDeUsuario;
                        model.Configuracion = modulos[i].Configuracion;
                        model.MisTareasYDocumentos = modulos[i].MisTareasYDocumentos;
                        model.ConsultaOrganizaciones = modulos[i].ConsultaOrganizaciones;
                        model.ConsultaProcesos = modulos[i].ConsultaProcesos;
                        model.ConsultaDocumentos = modulos[i].ConsultaDocumentos;
                        model.ConsultaAyuda = modulos[i].ConsultaAyuda;
                        model.ConsultaFiscalizaciones = modulos[i].ConsultaFiscalizaciones;
                        model.ExportarProcesosExcel = modulos[i].ExportarProcesosExcel;
                        model.ExportarTareasExcel = modulos[i].ExportarTareasExcel;
                        model.ReportePMG = modulos[i].ReportePMG;
                        model.CambiarContraseña = modulos[i].CambiarContraseña;
                        model.Comunas = modulos[i].Comunas;
                        model.AdministracionModulos = modulos[i].AdministracionModulos;
                        model.Neuronales = modulos[i].Neuronales;
                        model.DocumentoFiscalizador = modulos[i].DocumentoFiscalizador;
                        model.Periodo = modulos[i].Periodo;
                        model.VisualizadorDocumentos = modulos[i].VisualizadorDocumentos;
                        model.VisualizadorFiscalizacion = modulos[i].VisualizadorFiscalizacion;
                        model.VisualizadorSupervisor = modulos[i].VisualizadorSupervisor;
                        model.VisualizadorCoordinador = modulos[i].VisualizadorCoordinador;
                        model.VisualizadorArchivarDocumento = modulos[i].VisualizadorArchivarDocumento;
                    }
                    
                }
            }

            return View(model);
        }
    }
}
