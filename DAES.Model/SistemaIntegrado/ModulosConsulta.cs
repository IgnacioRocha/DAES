using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAES.Model.SistemaIntegrado
{
    [Table("ModulosConsulta")]
    public class ModulosConsulta
    {
        [Key]
        [Display(Name = "ID")]
        public int IdModulos { get; set; }

        public string Id { get; set; }
        [Display(Name = "Nombre de Funcionario")]
        public string NombreFuncionario { get; set; }

        [Display(Name ="Nombre de Usuario")]
        public string UserName { get; set; }


        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Ayuda")]
        public bool Ayuda { get; set; }

        [Display(Name = "Cargos")]
        public bool Cargos { get; set; }

        [Display(Name = "Comunas")]
        public bool Comunas { get; set; }

        [Display(Name = "Configuración Certificados")]
        public bool ConfigurarCertificados { get; set; }

        [Display(Name = "Estados de Organización")]
        public bool EstadosDeOrganizacion { get; set; }

        [Display(Name = "Firmantes")]
        public bool Firmantes { get; set; }

        [Display(Name = "Generos")]
        public bool Generos { get; set; }

        [Display(Name = "Hechos Legales")]
        public bool HechosLegales { get; set; }

        [Display(Name = "Hechos Contables")]
        public bool HechosContables { get; set; }

        [Display(Name = "Organizaciones")]
        public bool Organizaciones { get; set; }

        [Display(Name = "Regiones")]
        public bool Regiones { get; set; }

        [Display(Name = "Rubros")]
        public bool Rubros { get; set; }

        [Display(Name = "Situación")]
        public bool Situacion { get; set; }

        [Display(Name = "Sub-Rubros")]
        public bool SubRubros { get; set; }

        [Display(Name = "Tipo de Documentos")]
        public bool TiposDeDocumentos { get; set; }

        [Display(Name = "Tipos de Organización")]
        public bool TiposDeOrganizaciones { get; set; }

        [Display(Name = "Tipos de Materia")]
        public bool TiposDeMateria { get; set; }

        [Display(Name = "Tipos de Criterios")]
        public bool TiposDeCriterios { get; set; }

        [Display(Name = "Tipos De Fiscalización")]
        public bool TiposDeFiscalizacion { get; set; }

        [Display(Name = "Tipos De Hallazgos")]
        public bool TiposDeHallazgos { get; set; }

        [Display(Name = "Tipos De Oficio")]
        public bool TiposDeOficios { get; set; }

        [Display(Name = "Definir Tareas")]
        public bool DefinirTareas { get; set; }

        [Display(Name = "Definir Procesos")]
        public bool DefinirProcesos { get; set; }

        [Display(Name = "Iniciar Proceso desde gestor Documental")]
        public bool IniciarProcesoGestionDocumental { get; set; }

        [Display(Name = "Iniciar Proceso Manualmente")]
        public bool IniciarProcesoManualmente { get; set; }

        [Display(Name = "Administrar Procesos")]
        public bool AdministrarProcesos { get; set; }

        [Display(Name = "Administrar Carga de Tarea")]
        public bool AdministrarCargaDeTareas { get; set; }

        [Display(Name = "DashBoard Procesos")]
        public bool DashboardProcesos { get; set; }

        [Display(Name = "DashBoard Tareas")]
        public bool DashboardTareas { get; set; }

        [Display(Name = "Cuentas de Usuarios")]
        public bool CuentasUsuario { get; set; }

        [Display(Name = "Perfiles de Usuarios")]
        public bool PerfilesDeUsuario { get; set; }

        [Display(Name = "Configuración")]
        public bool Configuracion { get; set; }

        [Display(Name = "Mis Tareas y Documentos")]
        public bool MisTareasYDocumentos { get; set; }

        [Display(Name = "Consulta de Organizaciones")]
        public bool ConsultaOrganizaciones { get; set; }

        [Display(Name = "Consulta de Procesos")]
        public bool ConsultaProcesos { get; set; }

        [Display(Name = "Consulta de Documentos")]
        public bool ConsultaDocumentos { get; set; }

        [Display(Name = "Consulta de Ayuda")]
        public bool ConsultaAyuda { get; set; }

        [Display(Name = "Consulta de Fiscalizaciones")]
        public bool ConsultaFiscalizaciones { get; set; }

        [Display(Name = "Exportar procesos a excel")]
        public bool ExportarProcesosExcel { get; set; }

        [Display(Name = "Exportar tareas a excel")]
        public bool ExportarTareasExcel { get; set; }
        [Display(Name = "Reporte PMG")]
        public bool ReportePMG { get; set; }

        [Display(Name = "CambiarContraseña")]
        public bool CambiarContraseña { get; set; }

        [Display(Name = "Administración de Módulos")]
        public bool AdministracionModulos { get; set; }



    }
}
