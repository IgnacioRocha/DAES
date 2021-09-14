using DAES.Model.SistemaIntegrado;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DAES.Model.DTO
{
    public class DTOInformacionSemestral : DTOSolicitante
    {
        [Required(ErrorMessage = "Es necesario especificar el dato Organización")]
        [Display(Name = "Organización")]
        public int OrganizacionId { get; set; }
        public virtual Organizacion Organizacion { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el periodo")]
        [Display(Name = "Periodo")]
        public string Periodo { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el archivo Ficha detallada de fuentes de financiamiento")]
        [Display(Name = "Ficha detallada de fuentes de financiamientos (formato pdf)")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase FichaDetalladaFuentesFinanciamiento { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el archivo Estados financieros intermedios")]
        [Display(Name = "Estados financieros intermedios (formato pdf)")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase EstadosFinancierosIntermedios { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el cargo o función")]
        [Display(Name = "Cargo o función del solicitante")]
        public string CargoFuncion { get; set; }

        [Display(Name = "Observación")]
        [DataType(DataType.MultilineText)]
        public string Observacion { get; set; }
    }
}