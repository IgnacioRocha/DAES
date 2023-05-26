using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.SistemaIntegrado
{
    [Table("TipoMateria")]
    public class TipoMateria
    {
        public TipoMateria() {
        }

        [Key]
        public int TipoMateriaId { get; set; }

        [Required(ErrorMessage = "Es necesario especificar la Descripción")]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }
    }
}
