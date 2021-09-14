using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.SistemaIntegrado
{
    [Table("Ciudad")]
    public class Ciudad
    {
        public Ciudad()
        {
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int CiudadId { get; set; }

        //[Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Ciudad")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Región")]
        public int RegionId { get; set; }
        public virtual Region Region { get; set; }

    }
}
