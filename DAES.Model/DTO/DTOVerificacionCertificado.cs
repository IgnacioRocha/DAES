using System.ComponentModel.DataAnnotations;
using DAES.Model.SistemaIntegrado;

namespace DAES.Model.DTO
{
    public class DTOVerificacionCertificado
    {
        public DTOVerificacionCertificado()
        {
            documento = new Documento();
        }

        [Required(ErrorMessage = "Es necesario especificar el dato número de documento")]
        [Display(Name = "Id de documento")]
        public int? id { get; set; }

        public Documento documento { get; set; }

        public bool HasParameter { get; set; }
    }
}
