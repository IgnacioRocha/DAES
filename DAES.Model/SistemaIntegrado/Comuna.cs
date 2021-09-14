using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.SistemaIntegrado
{
    [Table("Comuna")]
    public class Comuna
    {
        public Comuna()
        {
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int ComunaId { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Código")]
        public int Codigo { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Comuna")]
        public string Nombre { get; set; }

        //[Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Ciudad")]
        public int? CiudadId { get; set; }
        public virtual Ciudad Ciudad { get; set; }

        //[Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Región")]
        public int RegionId { get; set; }
        public virtual Region Region { get; set; }
    }
}
