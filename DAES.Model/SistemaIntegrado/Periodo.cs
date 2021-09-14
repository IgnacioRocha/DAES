using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.SistemaIntegrado
{
    [Table("Periodo")]
    public class Periodo
    {
        public Periodo()
        {
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Id")]
        public int PeriodoId { get; set; }

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Display(Name = "Tipo")]
        public string Tipo { get; set; }

    }
}
