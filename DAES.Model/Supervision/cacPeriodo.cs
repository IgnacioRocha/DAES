namespace DAES.Model.Supervision
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("cacPeriodo")]
    public partial class cacPeriodo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PeriodoId { get; set; }

        public int? Anio { get; set; }

        public int? Mes { get; set; }

        [Required]
        [StringLength(50)]
        public string PeriodoGlosa { get; set; }

        public double? IPC { get; set; }

        public double? UTM { get; set; }

        public double? Dolar { get; set; }

        public double? UF { get; set; }
    }
}
