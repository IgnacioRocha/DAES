using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.SistemaIntegrado
{
    [Table("TipoNorma")]
    public class TipoNorma
    {

        public TipoNorma()
        {
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int TipoNormaId { get; set; }

      
        [Display(Name = "Tipo organización")]
        public string Nombre { get; set; }

        


    }
}
