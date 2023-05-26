namespace DAES.Model.Supervision
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("cacEstado")]
    public partial class cacEstado
    {
        [Key]
        public int EstadoId { get; set; }

        [StringLength(100)]
        [Display(Name = "Estado")]
        public string GlosaEstado { get; set; }
    }
}
