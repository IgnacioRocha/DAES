using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.SistemaIntegrado
{
    [Table("ActaFiscalizacionFiscalizadorContable")]
    public class ActaFiscalizacionFiscalizadorContable
    {
        public ActaFiscalizacionFiscalizadorContable()
        {
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int ActaFiscalizacionFiscalizadorContableId { get; set; }

        [Display(Name = "Acta de fiscalización")]
        public int ActaFiscalizacionId { get; set; }
        public virtual ActaFiscalizacion ActaFiscalizacion { get; set; }

        [Display(Name = "Fiscalizador contable")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        [Display(Name = "Seleccionado?")]
        public bool Seleccionado { get; set; }
    }
}