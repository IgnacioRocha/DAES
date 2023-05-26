namespace DAES.Model.GestionDocumental
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("TipoProceso")]
    public partial class TipoProceso
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TipoProceso()
        {
            ProcesoDocumento = new HashSet<ProcesoDocumento>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Tpo_Nombre { get; set; }

        public DateTime Tpo_FechaCreacion { get; set; }

        public DateTime? Tpo_FechaModificacion { get; set; }

        public bool Activo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProcesoDocumento> ProcesoDocumento { get; set; }
    }
}
