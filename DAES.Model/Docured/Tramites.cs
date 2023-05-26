namespace DAES.Model.Docured
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Tramites
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Folio { get; set; }

        [Required]
        [StringLength(15)]
        public string Usuario { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Correlativo { get; set; }

        [Required]
        [StringLength(8)]
        public string Fecha { get; set; }

        [Required]
        [StringLength(8)]
        public string Hora { get; set; }

        [StringLength(100)]
        public string Nota { get; set; }

        [StringLength(20)]
        public string Usuario_Destino { get; set; }

        [StringLength(100)]
        public string Observaciones { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int codigo_empresa { get; set; }

        public virtual Documentos Documentos { get; set; }
    }
}
