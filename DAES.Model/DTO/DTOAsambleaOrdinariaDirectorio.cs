using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DAES.Model.SistemaIntegrado;

namespace DAES.Model.DTO
{
    public  class DTOAsambleaOrdinariaDirectorio : DTOSolicitanteCore
    {
        public DTOAsambleaOrdinariaDirectorio()
        {
            Directorio = new List<DTODirectorio>();
        }

        [Required(ErrorMessage = "Es necesario especificar el dato Organización")]
        [Display(Name = "Organización")]
        public int OrganizacionId { get; set; }
        public virtual Organizacion Organizacion { get; set; }

        [Display(Name = "Tipo organización")]
        public int TipoOrganizacionId { get; set; }
        public virtual TipoOrganizacion TipoOrganizacion { get; set; }

        [Display(Name = "Estado")]
        public int? EstadoId { get; set; }
        public virtual Estado Estado { get; set; }

        [Display(Name = "Situación")]
        public int? SituacionId { get; set; }
        public virtual Situacion Situacion { get; set; }

        [Display(Name = "Rubro")]
        public int? RubroId { get; set; }
        public virtual Rubro Rubro { get; set; }

        [Display(Name = "Subrubro")]
        public int? SubRubroId { get; set; }
        public virtual SubRubro SubRubro { get; set; }

        [Display(Name = "Región")]
        public int? RegionId { get; set; }
        public virtual Region Region { get; set; }

        [Display(Name = "Comuna")]
        public int? ComunaId { get; set; }
        public virtual Comuna Comuna { get; set; }

        [Display(Name = "Número registro")]
        public string NumeroRegistro { get; set; }

        [Display(Name = "Razón social")]
        [DataType(DataType.MultilineText)]
        public string RazonSocial { get; set; }

        [Display(Name = "Sigla")]
        public string Sigla { get; set; }

        [Display(Name = "Dirección")]
        [DataType(DataType.MultilineText)]
        public string Direccion { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Es necesario especificar el dato Acta de Junta General de Socios.")]
        [Display(Name = "(*) Acta de Junta General de Socios.")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase File1 { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el dato Acta de sesión de Comité de Administración con asignación de cargos directivos.")]
        [Display(Name = "(*) Acta de sesión de Comité de Administración con asignación de cargos directivos.")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase File2 { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el dato Comprobante de medios de convocatorias empleadas.")]
        [Display(Name = "(*) Comprobante de medios de convocatorias empleadas.")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase File3 { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el dato Documentos complementarios (escrituras, declaraciones, certificados y otros).")]
        [Display(Name = "Documentos complementarios (escrituras, declaraciones, certificados y otros).")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase File4 { get; set; }

        public virtual List<DTODirectorio> Directorio { get; set; }
    }
}
