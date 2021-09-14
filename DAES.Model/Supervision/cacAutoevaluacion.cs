using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.Supervision
{
    [Table("cacAutoevaluacion")]
    public partial class cacAutoevaluacion
    {
        public cacAutoevaluacion()
        {
            cacAutoevaluacionDetalle = new HashSet<cacAutoevaluacionDetalle>();
        }
        [Key]
        public int id { get; set; }
        public virtual ICollection<cacAutoevaluacionDetalle> cacAutoevaluacionDetalle { get; set; }

        public int? areaId { get; set; }
        public virtual cacArea cacArea { get; set; }
    }
}