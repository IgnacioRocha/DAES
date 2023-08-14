using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAES.Model.SistemaIntegrado;

namespace DAES.Model.DTO
{
    public class DTOModulo
    {
        public DTOModulo()
        {

        }

        public int IdModulos { get; set; }
        public string Id { get; set; }

        [Display(Name="Nombre de funcionario")]
        public string NombreFuncionario { get; set; }

        [Display(Name = "Nombre de Usuario")]
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool Ayuda { get; set; }
        public bool Cargos { get; set; }
        public bool Comunas { get; set; }
        public bool ConfigurarCertificados { get; set; }
        public bool EstadosDeOrganizacion { get; set; }
        public bool Firmantes { get; set; }
        public bool Generos { get; set; }
        public bool HechosLegales { get; set; }
        public bool HechosContables { get; set; }
        public bool Organizaciones { get; set; }
        public bool Regiones { get; set; }
        public bool Rubros { get; set; }
        public bool Situacion { get; set; }
        public bool SubRubros { get; set; }
        public bool TiposDeDocumentos { get; set; }
        public bool TiposDeOrganizaciones { get; set; }
        public bool TiposDeMateria { get; set; }
        public bool TiposDeCriterios { get; set; }
        public bool TiposDeFiscalizacion { get; set; }
        public bool TiposDeHallazgos { get; set; }
        public bool TiposDeOficios { get; set; }
        public bool DefinirTareas { get; set; }
        public bool DefinirProcesos { get; set; }
        public bool IniciarProcesoGestionDocumental { get; set; }
        public bool IniciarProcesoManualmente { get; set; }
        public bool AdministrarProcesos { get; set; }
        public bool AdministrarCargaDeTareas { get; set; }
        public bool DashboardProcesos { get; set; }
        public bool DashboardTareas { get; set; }
        public bool CuentasUsuario { get; set; }
        public bool PerfilesDeUsuario { get; set; }
        public bool Configuracion { get; set; }
        public bool MisTareasYDocumentos { get; set; }
        public bool ConsultaOrganizaciones { get; set; }
        public bool ConsultaProcesos { get; set; }
        public bool ConsultaDocumentos { get; set; }
        public bool ConsultaAyuda { get; set; }
        public bool ConsultaFiscalizaciones { get; set; }
        public bool ExportarProcesosExcel { get; set; }
        public bool ExportarTareasExcel { get; set; }
        public bool ReportePMG { get; set; }
        public bool CambiarContraseña { get; set; }

        public bool AdministracionModulos { get; set; }
        public bool Neuronales { get; set; }
        public bool DocumentoFiscalizador { get; set; }
        public bool Periodo { get; set; }

        public bool VisualizadorDocumentos { get; set; }

        public bool VisualizadorFiscalizacion { get; set; }

        public bool VisualizadorSupervisor { get; set; }

        public bool VisualizadorCoordinador { get; set; }

        public bool VisualizadorArchivarDocumento { get; set; }

    }
}