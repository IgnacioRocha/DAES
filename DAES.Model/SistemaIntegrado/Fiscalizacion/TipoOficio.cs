using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.SistemaIntegrado
{
    [Table("TipoOficio")]
    public class TipoOficio
    {
        public TipoOficio() {
        }

        [Key]
        public int TipoOficioId { get; set; }

        [Required(ErrorMessage = "Es necesario especificar la Descripción")]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }
    }
}
