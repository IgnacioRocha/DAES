namespace DAES.Model.Supervision
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("cacTipoFicha")]
    public partial class cacTipoFicha
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public cacTipoFicha()
        {
            cacInfCont = new HashSet<cacInfCont>();
        }

        [Key]
        public int TipoFichaId { get; set; }

        [StringLength(100)]
        [Display(Name = "Documento")]
        public string NombTipoFicha { get; set; }

        public bool? PeriocidadMensual { get; set; }

        public bool? MostrarCombo { get; set; }

        public int? DiasPlazoEntrega { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<cacInfCont> cacInfCont { get; set; }
    }
}
