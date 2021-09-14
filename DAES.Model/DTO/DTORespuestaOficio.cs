using System;
using System.ComponentModel.DataAnnotations;
using System.Web;
using DAES.Model.SistemaIntegrado;

namespace DAES.Model.DTO
{
    public class DTORespuestaOficio: DTOSolicitante
    {
        public DTORespuestaOficio()
        {
        }

        [Required(ErrorMessage = "Es necesario especificar el dato Organización")]
        [Display(Name = "Organización")]
        public int OrganizacionId { get; set; }
        public virtual Organizacion Organizacion { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el dato Número de oficio")]
        [Display(Name = "Número de oficio")]
        public string NumeroDeOficio { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el dato Fecha de emisión de oficio")]
        [Display(Name = "Fecha de emisión de oficio")]
        [DataType(DataType.Date)]
        public DateTime? FechaSalidaOficio { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el dato Documento")]
        [Display(Name = "Documento")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase File1 { get; set; }

        [Display(Name = "Documento")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase File2 { get; set; }

        [Display(Name = "Documento")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase File3 { get; set; }

        [Display(Name = "Documento")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase File4 { get; set; }

        [Display(Name = "Documento")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase File5 { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el dato Descripción del documento")]
        [Display(Name = "Descripción del documento")]
        [DataType(DataType.MultilineText)]
        public string DescripcionDocumento { get; set; }
    }
}
