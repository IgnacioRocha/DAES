using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.SistemaIntegrado
{
    [Table("TipoCriterio")]
    public class TipoCriterio
    {
        public TipoCriterio() {
        }

        [Key]
        public int TipoCriterioId { get; set; }

        [Required(ErrorMessage = "Es necesario especificar la Descripción")]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }
    }
}
