namespace DAES.Model.Supervision
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("cacResumenFormulario")]
    public partial class cacResumenFormulario
    {
        public cacResumenFormulario()
        {
            cacResumenAreas = new HashSet<cacResumenArea>();
        }

        [Key]
        public long refoid { get; set; }

        public int? formid { get; set; }

        public long? evalid { get; set; }

        public int? fichaId { get; set; }

        [Display(Name = "Calificación")]
        public decimal? refocalificacion { get; set; }

        [Display(Name = "Nivel")]
        public string refonivel { get; set; }

        [Display(Name = "Comentario")]
        public string comentario { get; set; }

        public bool? valida { get; set; }

        [Display(Name = "Creación")]
        public DateTime? creacion { get; set; }

        public virtual cacEvaluacion cacEvaluacion { get; set; }

        public virtual cacFormulario cacFormulario { get; set; }

        public virtual ICollection<cacResumenArea> cacResumenAreas { get; set; }

    }
}
