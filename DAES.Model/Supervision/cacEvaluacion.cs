namespace DAES.Model.Supervision
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("cacEvaluacion")]
    public partial class cacEvaluacion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public cacEvaluacion()
        {
            cacResumenFormulario = new HashSet<cacResumenFormulario>();
        }

        [Key]
        public long evalid { get; set; }

        [Display(Name = "Periodo")]
        public int? evalperiodo { get; set; }

        [Display(Name = "Calificación")]
        public decimal? evalcalificacion { get; set; }

        [Display(Name = "Nivel")]
        [StringLength(2)]
        public string evalnivel { get; set; }

        [Display(Name = "Rol")]
        public int? evalrolcoop { get; set; }

        [Display(Name = "FechaCreacion")]
        public DateTime? evalfecha { get; set; }

        [Display(Name = "Valida?")]
        public bool? valida { get; set; }

        [Display(Name = "Comentario")]
        public string comentario { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<cacResumenFormulario> cacResumenFormulario { get; set; }
    }
}
