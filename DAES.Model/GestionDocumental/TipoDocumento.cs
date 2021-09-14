namespace DAES.Model.GestionDocumental
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("TipoDocumento")]
    public partial class TipoDocumento
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TipoDocumento()
        {
            Documento = new HashSet<Documento>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Tdo_Nombre { get; set; }

        public DateTime Tdo_FechaCreacion { get; set; }

        public DateTime? Tdo_FechaModificacion { get; set; }

        public bool Activo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Documento> Documento { get; set; }
    }
}
