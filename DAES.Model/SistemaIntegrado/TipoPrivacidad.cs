using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.SistemaIntegrado
{
    [Table("TipoPrivacidad")]
    public class TipoPrivacidad
    {
        public TipoPrivacidad()
        {

        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int TipoPrivacidadId { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Tipo privacidad")]
        public string Nombre { get; set; }
    }
}