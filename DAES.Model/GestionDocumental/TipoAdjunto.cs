namespace DAES.Model.GestionDocumental
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("TipoAdjunto")]
    public partial class TipoAdjunto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TipoAdjunto()
        {
            Adjunto = new HashSet<Adjunto>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Tad_Nombre { get; set; }

        public DateTime Tad_FechaCreacion { get; set; }

        public DateTime? Tad_FechaModificacion { get; set; }

        public bool Activo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Adjunto> Adjunto { get; set; }
    }
}
