using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.SistemaIntegrado
{
    [Table("Firmante")]
    public class Firmante
    {
        public Firmante()
        {
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int FirmanteId { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Nombre firmante")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Código firmante")]
        public int Codigo { get; set; }

        [Display(Name = "Cargo")]
        public string Cargo { get; set; }


        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Unidad organizacional")]
        [DataType(DataType.MultilineText)]
        public string UnidadOrganizacional { get; set; }

        [Display(Name = "Id Firma")]
        public string IdFirma { get; set; }

        [Display(Name = "Activo?")]
        public bool EsActivo { get; set; }
    }
}