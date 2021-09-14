using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model
{
    [Table("Funcionario")]
    public class Funcionario
    {
        public Funcionario()
        {
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int FuncionarioId { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Nombre completo")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }
    }
}
