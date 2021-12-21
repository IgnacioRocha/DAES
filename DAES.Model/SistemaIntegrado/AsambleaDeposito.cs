using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.SistemaIntegrado
{
    [Table("AsambleaDeposito")]
    public class AsambleaDeposito
    {
        public AsambleaDeposito()
        {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int AsambleaDepId { get; set; }


        [Display(Name = "Asamblea/Depósito")]
        public string Descripcion { get; set; }

    }
}
