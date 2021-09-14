using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.SistemaIntegrado
{
    [Table("TipoFiscalizacion")]
    public class TipoFiscalizacion
    {
        public TipoFiscalizacion() {
        }

        [Key]
        public int TipoFiscalizacionId { get; set; }

        [Required(ErrorMessage = "Es necesario especificar la Descripción")]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }
    }
}
