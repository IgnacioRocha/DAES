using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.SistemaIntegrado
{
    [Table("Ayuda")]
    public class Ayuda
    {
        public Ayuda()
        {
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int AyudaId { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Pregunta")]
        [DataType(DataType.MultilineText)]
        public string Pregunta { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Respuesta")]
        [DataType(DataType.MultilineText)]
        public string Respuesta { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Valoración")]
        public int Valoracion { get; set; }
    }
}