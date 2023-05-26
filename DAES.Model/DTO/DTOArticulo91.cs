using DAES.Model.SistemaIntegrado;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DAES.Model.DTO
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;
        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        public override bool IsValid(object value)
        {
            var file = value as HttpPostedFileBase;
            if (file == null)
            {
                return false;
            }
            return file.ContentLength <= _maxFileSize;
        }

        public override string FormatErrorMessage(string name)
        {
            return base.FormatErrorMessage(_maxFileSize.ToString());
        }
    }
    public class DTOArticulo91 : DTOSolicitante
    {
        public DTOArticulo91()
        {
            Documentos = new List<Documento>();
        }

        [Required(ErrorMessage = "Es necesario especificar el dato Organización")]
        [Display(Name = "Organización")]
        public int OrganizacionId { get; set; }
        public virtual Organizacion Organizacion { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el dato Año")]
        [Display(Name = "Año")]
        public string Periodo { get; set; }

        [MaxFileSize(20 * 1024  * 1024 , ErrorMessage = "Tamaño máximo de archivo es {0} MB")]
        [Required(ErrorMessage = "Es necesario especificar el Balance General Anual")]
        [Display(Name = "Balance General Anual")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase Balance { get; set; }

        [MaxFileSize(20 * 1024 * 1024, ErrorMessage = "Tamaño máximo de archivo es {0} MB")]
        [Required(ErrorMessage = "Es necesario especificar el Estado de Resultados")]
        [Display(Name = "Estado de Resultados")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase Estadoresultado { get; set; }

        [MaxFileSize(20 * 1024 * 1024, ErrorMessage = "Tamaño máximo de archivo es {0} MB")]
        [Required(ErrorMessage = "Es necesario especificar el Dictamen de Auditores Externos")]
        [Display(Name = "Dictamen de Auditores Externos")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase DictamenAuditorExterno { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el Nombre del Gerente")]
        [Display(Name = "Nombre del Gerente")]
        public string NombreGerente { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el Nombre Contador General")]
        [Display(Name = "Nombre Contador General")]
        public string NombreContadorGeneral { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el Nombre Contador General")]
        [Display(Name = "Fecha celebración última Junta General de Socios")]
        [DataType(DataType.Date)]
        public DateTime FechaCelebracionUltimaJuntaGeneralSocios { get; set; }

        public DateTime Fecha { get; set; }

        public virtual List<Documento> Documentos { get; set; }
    }
}