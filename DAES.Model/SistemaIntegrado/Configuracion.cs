using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.SistemaIntegrado
{
    [Table("Configuracion")]
    public class Configuracion
    {
        public Configuracion()
        {
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int ConfiguracionId { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Clave")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Valor")]
        public string Valor { get; set; }
    }
}