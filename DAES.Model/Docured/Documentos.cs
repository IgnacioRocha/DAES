namespace DAES.Model.Docured
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Documentos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Documentos()
        {
            Tramites = new HashSet<Tramites>();
        }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Folio { get; set; }

        public int Codigo_Ficha { get; set; }

        public int Codigo_Carpeta { get; set; }

        [Required]
        [StringLength(255)]
        public string Imagen { get; set; }

        [StringLength(2)]
        public string Respuesta { get; set; }

        [StringLength(2)]
        public string Terminado_Fin_Ciclo { get; set; }

        [StringLength(255)]
        public string Observaciones { get; set; }

        [Required]
        [StringLength(150)]
        public string Campo1 { get; set; }

        [Required]
        [StringLength(500)]
        public string Campo2 { get; set; }

        [Required]
        [StringLength(150)]
        public string Campo3 { get; set; }

        [Required]
        [StringLength(150)]
        public string Campo4 { get; set; }

        [Required]
        [StringLength(150)]
        public string Campo5 { get; set; }

        [Required]
        [StringLength(150)]
        public string Campo6 { get; set; }

        [Required]
        [StringLength(150)]
        public string Campo7 { get; set; }

        [Required]
        [StringLength(150)]
        public string Campo8 { get; set; }

        [Required]
        [StringLength(150)]
        public string Campo9 { get; set; }

        [Required]
        [StringLength(150)]
        public string Campo10 { get; set; }

        [Required]
        [StringLength(150)]
        public string Campo11 { get; set; }

        [Required]
        [StringLength(150)]
        public string Campo12 { get; set; }

        [Required]
        [StringLength(8)]
        public string Fecha_Creacion { get; set; }

        [StringLength(255)]
        public string Texto_Ocr { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int codigo_empresa { get; set; }

        public int? Contador_Accesos { get; set; }

        [StringLength(255)]
        public string Observaciones1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tramites> Tramites { get; set; }
    }
}
