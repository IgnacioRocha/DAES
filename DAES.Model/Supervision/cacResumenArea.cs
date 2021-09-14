namespace DAES.Model.Supervision
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("cacResumenArea")]
    public partial class cacResumenArea
    {
        [Key]
        public long rearid { get; set; }
        public long? refoid { get; set; }
        public int? areaid { get; set; }
        public decimal? rearcalificacion { get; set; }
        public decimal? rearponderacion { get; set; }
        public string rearnivel { get; set; }
        public bool? valida { get; set; }

        public virtual cacResumenFormulario cacResumenFormulario { get; set; }
        public virtual cacArea cacArea { get; set; }
    }
}
