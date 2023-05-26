using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.SistemaIntegrado
{
    [Table("Dumy")]
    public class Dumy
    {
        public Dumy()
        {
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public string imagen { get; set; }
        public bool pendiente { get; set; }
        public string razon { get; set; }

    }

}
