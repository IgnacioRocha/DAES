using ExpressiveAnnotations.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.SistemaIntegrado
{
    [Table("ActaFiscalizacionHechoContable")]
    public class ActaFiscalizacionHechoContable
    {
        public ActaFiscalizacionHechoContable()
        {
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int ActaFiscalizacionHechoContableId { get; set; }

        //[Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Acta de fiscalización")]
        public int ActaFiscalizacionId { get; set; }
        public virtual ActaFiscalizacion ActaFiscalizacion { get; set; }

        //[Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Acta de fiscalización")]
        public int HechoContablelId { get; set; }
        public virtual HechoContable HechoContable { get; set; }

        [Display(Name = "Requerido?")]
        public bool Requerido { get; set; }

        [RequiredIf("Requerido == true", ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Años")]
        public string Periodo { get; set; }

        [RequiredIf("Requerido == true", ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Observaciones")]
        public string Observacion { get; set; }

        public byte[] Archivo { get; set; }

        [RequiredIf("Requerido == true", ErrorMessage = "Es necesario especificar este dato")]
        [NotMapped]
        [Display(Name = "Archivo")]
        [DataType(DataType.Upload)]
        public System.Web.HttpPostedFileBase File { get; set; }
    }
}