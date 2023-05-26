using DAES.Model.SistemaIntegrado;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DAES.Model.DTO
{
    public class DTOSupervisionCAC : DTOSolicitante
    {
        [Required(ErrorMessage = "Es necesario especificar el dato Organización")]
        [Display(Name = "Organización")]
        public int OrganizacionId { get; set; }
        public virtual Organizacion Organizacion { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Periodo")]
        public string Periodo { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el archivo Balance, AC01, AC03, AC12")]
        [Display(Name = "Balance, AC01, AC03, AC12.xlsx")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase AC01 { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el archivo AC-02 Encaje v3")]
        [Display(Name = "AC-02 Encaje v3")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase AC02 { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el archivo AC-04 Margenes de Crédito v4.1")]
        [Display(Name = "AC-04 Margenes de Crédito v4.1")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase AC04 { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el archivo AC-05 Inversiones Activo Fijo v3")]
        [Display(Name = "AC-05 Inversiones Activo Fijo v3")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase AC05 { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el archivo AC-06 Tasas de intereses V2")]
        [Display(Name = "AC-06 Tasas de intereses V2")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase AC06 { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el archivo AC-07 Aporte de capítal")]
        [Display(Name = "AC-07 Aporte de capítal")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase AC07 { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el archivo AC-08 Antecedentes administrativos")]
        [Display(Name = "AC-08 Antecedentes administrativos")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase AC08 { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el archivo AC-09 Deudores")]
        [Display(Name = "AC-09 Deudores")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase AC09 { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el archivo AC-10 Cuentas a la vista")]
        [Display(Name = "AC-10 Cuentas a la vista")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase AC10 { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el archivo AC-11 Cuentas de ahorro a plazo")]
        [Display(Name = "AC-11 Cuentas de ahorro a plazo")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase AC11 { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el archivo CACs MENSUAL (1 de 2) v3")]
        [Display(Name = "CACs MENSUAL (1 de 2) v3")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase CACMensualV3 { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el archivo CACs MENSUAL (2 de 2) V5")]
        [Display(Name = "CACs MENSUAL (2 de 2) V5")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase CACMensualV4 { get; set; }

        [Display(Name = "Observación")]
        [DataType(DataType.MultilineText)]
        public string Observacion { get; set; }
    }
}