using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.SistemaIntegrado
{
    [Table("CargaSituacion")]
    public class CargaSituacion
    {
        public CargaSituacion()
        {
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int Id { get; set; }

        public int OrganizacionId { get; set; }
        public string Situacion { get; set; }
    }
}
