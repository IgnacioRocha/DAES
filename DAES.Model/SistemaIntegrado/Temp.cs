using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.SistemaIntegrado
{
    [Table("Temp")]
    public class Temp
    {
        public Temp()
        {
        }
        [Key]
        public int DocumentoId { get; set; }
        public string OrganizacionId { get; set; }
        public string User { get; set; }
    }
}
