namespace DAES.Model.Supervision
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("cacFactor")]
    public partial class cacFactor
    {
        [Key]
        [Display(Name = "Id")]
        public int factId { get; set; }

        [Display(Name = "Item")]
        public string factDescripcion { get; set; }

        public int? areaid { get; set; }
        public virtual cacArea cacArea { get; set; }

        [NotMapped]
        //[Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Respuesta")]
        public int concId { get; set; }
    }
}
