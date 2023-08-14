using DAES.Model.SistemaIntegrado;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DAES.Model.DTO
{
    
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

        [Required(ErrorMessage = "Es necesario especificar el Balance General Anual")]
        [Display(Name = "Balance General Anual")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase Balance { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el Estado de Resultados")]
        [Display(Name = "Estado de Resultados")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase Estadoresultado { get; set; }

        [Display(Name = "Poder representativo")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase PoderRepresentativo { get; set; }

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
        [Display(Name = "Fecha Junta General de Socios (*)")]
        [DataType(DataType.Date)]
        public DateTime FechaCelebracionUltimaJuntaGeneralSocios { get; set; }

        public DateTime Fecha { get; set; }

        public virtual List<Documento> Documentos { get; set; }
    }
}