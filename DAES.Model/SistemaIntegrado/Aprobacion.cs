using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.SistemaIntegrado
{
    [Table("Aprobacion")]
    public class Aprobacion
    {
        public Aprobacion()
        {
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int? AprobacionId { get; set; }

        
        [Display(Name = "Tipo organización")]
        public string Nombre { get; set; }

    }
}
