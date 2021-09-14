
using ExpressiveAnnotations.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.SistemaIntegrado
{
    [Table("ActaFiscalizacionHechoLegal")]
    public class ActaFiscalizacionHechoLegal
    {
        public ActaFiscalizacionHechoLegal()
        {
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int ActaFiscalizacionHechoLegalId { get; set; }

        //[Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Acta de fiscalización")]
        public int ActaFiscalizacionId { get; set; }
        public virtual ActaFiscalizacion ActaFiscalizacion { get; set; }

        //[Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Acta de fiscalización")]
        public int HechoLegalId { get; set; }
        public virtual HechoLegal HechoLegal { get; set; }

        [Display(Name = "Requerido?")]
        public bool Requerido { get; set; }

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