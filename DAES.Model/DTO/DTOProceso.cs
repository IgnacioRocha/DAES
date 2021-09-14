using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAES.Model.SistemaIntegrado;
using DAES.Model.GestionDocumental;
using ExpressiveAnnotations.Attributes;

namespace DAES.Model.DTO
{
    public class DTOProceso
    {
        public DTOProceso()
        {
        }

        public int DocumentoId { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el dato Definición de proceso")]
        [Display(Name = "Definición proceso")]
        public int DefinicionProcesoId { get; set; }

        [RequiredIf("DefinicionProcesoId != 51 && DefinicionProcesoId != 82", ErrorMessage = "Es necesario especificar el dato Organización")]
        [Display(Name = "Organización")]
        public int? OrganizacionId { get; set; }

        [RequiredIf("DefinicionProcesoId != 51 && DefinicionProcesoId != 82", ErrorMessage = "Es necesario especificar el dato Nombre organización")]
        [Display(Name = "Nombre organización")]
        public string NombreOrganizacion { get; set; }

        [Display(Name = "Usuario")]
        public string UserId { get; set; }

        [Display(Name = "Observaciones")]
        [DataType(DataType.MultilineText)]
        public string Observacion { get; set; }

        public DAES.Model.GestionDocumental.Documento Documento { get; set; }
        public Solicitante Solicitante { get; set; }
        public List<Adjunto> Adjuntos { get; set; }
        public List<ProcesoDocumento> ProcesoDocumentos { get; set; }
    }
}
