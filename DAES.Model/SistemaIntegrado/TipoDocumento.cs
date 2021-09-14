using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.SistemaIntegrado
{
    [Table("TipoDocumento")]
    public class TipoDocumento
    {
        public TipoDocumento()
        {

        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int TipoDocumentoId { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Tipo documento")]
        public string Nombre { get; set; }

        [Display(Name = "Generación manual?")]
        public bool GeneracionManual { get; set; } = false;

        [Display(Name = "Vigente?")]
        public bool EsVigente { get; set; } = true;

        [Display(Name = "Dias vigencia")]
        public int? DiasVigencia { get; set; }

        [Display(Name = "Externo?")]
        public bool EsExterno { get; set; } = false;
    }
}