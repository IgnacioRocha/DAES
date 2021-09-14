using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.SistemaIntegrado
{
    [Table("TipoWorkflow")]
    public class TipoWorkflow
    {
        public TipoWorkflow()
        {
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int TipoWorkflowId { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Tipo workflow")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Descripción de tarea")]
        [DataType(DataType.MultilineText)]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Formulario")]
        public string Formulario { get; set; }
    }
}
