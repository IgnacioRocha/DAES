using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.SistemaIntegrado
{
    [Table("PeriodoAC")]
    public class PeriodoAC
    {
        public PeriodoAC()
        {
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Periodo")]
        public int PeriodoACId { get; set; }

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Display(Name = "Semestral/Anual")]
        public string Tipo { get; set; }
    }
}
