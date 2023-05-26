using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.Supervision
{
    [Table("cacAutoevaluacionDetalle")]
    public partial class cacAutoevaluacionDetalle
    {
        [Key]
        public int id { get; set; }

        public int autoevaluacionId { get; set; }
        public virtual cacAutoevaluacion cacAutoevaluacion { get; set; }

        public int? factorId { get; set; }
        public virtual cacFactor cacFactor { get; set; }

        [Display(Name = "Respuesta")]
        public int? conceptoId { get; set; }
    }
}