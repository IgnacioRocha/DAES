using DAES.Model.SistemaIntegrado;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DAES.Model.DTO
{
    public class DTOSupervisionNOCAC : DTOSolicitante
    {
        [Required(ErrorMessage = "Es necesario especificar el dato Organización")]
        [Display(Name = "Organización")]
        public int OrganizacionId { get; set; }
        public virtual Organizacion Organizacion { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Periodo")]
        public string Periodo { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el archivo Balance v.4")]
        [Display(Name = "Balance v.4")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase BalanceV4 { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el archivo Cartera Vencida v.3")]
        [Display(Name = "Cartera Vencida v.3")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase CarteraVencidaV3 { get; set; }

        //[Required]
        //[Display(Name = "Deudores v.3")]
        //[DataType(DataType.Upload)]
        //public HttpPostedFileBase DeudoresV3 { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el archivo Deudores v.4 (con soporte para 1.000.000 de filas)")]
        [Display(Name = "Deudores v.4 (con soporte para 1.000.000 de filas)")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase DeudoresV4 { get; set; }

        [Display(Name = "Observación")]
        [DataType(DataType.MultilineText)]
        public string Observacion { get; set; }
    }
}