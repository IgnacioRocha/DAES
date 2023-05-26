namespace DAES.Model.GestionDocumental
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("EntidadExterna")]
    public partial class EntidadExterna
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EntidadExterna()
        {
            Documento = new HashSet<Documento>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Ext_Nombre { get; set; }

        [StringLength(200)]
        public string Ext_Mail { get; set; }

        public DateTime Ext_FechaCreacion { get; set; }

        public DateTime? Ext_FechaModificacion { get; set; }

        public bool Activo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Documento> Documento { get; set; }
    }
}
