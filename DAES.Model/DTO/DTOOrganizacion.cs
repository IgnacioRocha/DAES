using DAES.Model.SistemaIntegrado;
using System.ComponentModel.DataAnnotations;

namespace DAES.Model
{
    public class DTOOrganizacion
    {

        [Display(Name = "Tipo organización")]
        public int TipoOrganizacionId { get; set; }
        public virtual TipoOrganizacion TipoOrganizacion { get; set; }

        public int RegionId { get; set; }
        public virtual Region Region { get; set; }

        [Display(Name = "Razón social")]
        [DataType(DataType.MultilineText)]
        public string RazonSocial { get; set; }

        [Display(Name = "RUT")]
        [StringLength(13)]
        public string RUT { get; set; }


        [Display(Name = "Sigla")]
        public string Sigla { get; set; }
    }
}
