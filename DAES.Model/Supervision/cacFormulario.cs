namespace DAES.Model.Supervision
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("cacFormulario")]
    public partial class cacFormulario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public cacFormulario()
        {
            cacResumenFormulario = new HashSet<cacResumenFormulario>();
            cacArea = new HashSet<cacArea>();
        }

        [Key]
        public int formId { get; set; }

        [StringLength(1000)]
        public string formDescripcion { get; set; }

        public int? formSecuencia { get; set; }

        public decimal? formPonderacion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<cacResumenFormulario> cacResumenFormulario { get; set; }
        public virtual ICollection<cacArea> cacArea { get; set; }
    }
}
