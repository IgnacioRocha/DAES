using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.SistemaIntegrado
{
    [Table("Region")]
    public class Region
    {
        public Region()
        {
            Comunas = new HashSet<Comuna>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int RegionId { get; set; }

        [Display(Name = "Código")]
        public int? Codigo { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Región")]
        public string Nombre { get; set; }

        public virtual ICollection<Comuna> Comunas { get; set; }
    }
}
