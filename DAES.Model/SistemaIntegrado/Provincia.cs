using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.SistemaIntegrado
{
    [Table("Provincia")]
    public class Provincia
    {
        public Provincia()
        {
            Comunas = new HashSet<Comuna>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int ProvinciaId { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Código")]
        public int Codigo { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Provincia")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Región")]
        public int RegionId { get; set; }
        public virtual Region Region { get; set; }

        public virtual ICollection<Comuna> Comunas { get; set; }

    }
}
