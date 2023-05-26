using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.SistemaIntegrado
{
    [Table("ActaFiscalizacionFiscalizadorLegal")]
    public class ActaFiscalizacionFiscalizadorLegal
    {
        public ActaFiscalizacionFiscalizadorLegal()
        {
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int ActaFiscalizacionFiscalizadorLegalId { get; set; }

        [Display(Name = "Acta de fiscalización")]
        public int ActaFiscalizacionId { get; set; }
        public virtual ActaFiscalizacion ActaFiscalizacion { get; set; }

        [Display(Name = "Fiscalizador legal")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        [Display(Name = "Seleccionado?")]
        public bool Seleccionado { get; set; }
    }
}