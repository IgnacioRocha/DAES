using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.SistemaIntegrado
{
    [Table("TipoHallazgo")]
    public class TipoHallazgo
    {
        public TipoHallazgo() {
        }

        [Key]
        public int TipoHallazgoId { get; set; }

        [Required(ErrorMessage = "Es necesario especificar la Descripción")]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }
    }
}
