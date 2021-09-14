namespace DAES.Model.GestionDocumental
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Documento")]
    public partial class Documento
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Documento()
        {
            ProcesoDocumento = new HashSet<ProcesoDocumento>();
        }

        public int Id { get; set; }

        public int IdFuncionario { get; set; }

        public int? Doc_ParentId { get; set; }

        public DateTime Doc_FechaIngreso { get; set; }

        [Required]
        [StringLength(150)]
        public string Doc_UnidadServicio { get; set; }

        public int Doc_UnidadServicio_Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Doc_NombreFuncionario { get; set; }

        [Required]
        [StringLength(15)]
        public string Doc_Correlativo { get; set; }

        [StringLength(150)]
        public string Doc_NumeroExterno { get; set; }

        public string Doc_Asunto { get; set; }

        public string Doc_Referencia { get; set; }

        [StringLength(150)]
        public string Doc_Codigo { get; set; }

        public string Doc_Descripcion { get; set; }

        public DateTime Doc_FechaCreacion { get; set; }

        public DateTime? Doc_FechaModificacion { get; set; }

        public bool Doc_EsInterno { get; set; }

        public int Doc_Version { get; set; }

        public bool Doc_Final { get; set; }

        public bool Activo { get; set; }

        public int? EntidadExterna_Id { get; set; }

        public int? TipoDocumento_Id { get; set; }

        [StringLength(50)]
        public string Doc_Folio { get; set; }

        public virtual EntidadExterna EntidadExterna { get; set; }

        public virtual TipoDocumento TipoDocumento { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProcesoDocumento> ProcesoDocumento { get; set; }

        [NotMapped]
        public bool ConProcesosVigentes { get; set; }
        [NotMapped]
        public int? ProcesoId { get; set; }
    }
}
