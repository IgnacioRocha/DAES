using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.SistemaIntegrado
{
    [Table("ConfiguracionCertificado")]
    public class ConfiguracionCertificado
    {
        public ConfiguracionCertificado()
        {

        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int ConfiguracionCertificadoId { get; set; }

        //[Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Tipo documento")]
        public int? TipoDocumentoId { get; set; }
        public virtual TipoDocumento TipoDocumento { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Tipo organización")]
        public int TipoOrganizacionId { get; set; }
        public virtual TipoOrganizacion TipoOrganizacion { get; set; }

        //[Required(ErrorMessage = "Es necesario especificar este dato")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Párrafo 1")]
        public string Parrafo1 { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Párrafo 2")]
        public string Parrafo2 { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Párrafo 3")]
        public string Parrafo3 { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Párrafo 4")]
        public string Parrafo4 { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Párrafo 5")]
        public string Parrafo5 { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [DataType(DataType.MultilineText)]
        public string Titulo { get; set; }

        [DataType(DataType.MultilineText)]
        public string Ciudad { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Unidad organizacional")]
        public string UnidadOrganizacional { get; set; }

        [Display(Name = "XML")]
        [DataType(DataType.MultilineText)]
        public string XML { get; set; }

        [Display(Name = "Activo?")]
        public bool IsActivo { get; set; }

        [Display(Name = "Tiene directorio?")]
        public bool TieneDirectorio { get; set; }

        [Display(Name = "Tiene estatúto?")]
        public bool TieneEstatuto { get; set; }

        //Parrafo 2 con Existencia Anterior
        [DataType(DataType.MultilineText)]
        public string Parrafo2ExAnterior { get; set; }

        //Parrafo 2 con Existencia Posterior
        [DataType(DataType.MultilineText)]
        public string Parrafo2ExPosterior { get; set; }

        //Párrafo 4 con Reforma Anterior
        [DataType(DataType.MultilineText)]
        public string Parrafo4ReAnterior { get; set; }

        //Parrafo 4 con ReformaPosterior
        [DataType(DataType.MultilineText)]
        public string Parrafo4RePosterior { get; set; }

        //Parrafo 1 con Disulucion Anterior a 2003
        [DataType(DataType.MultilineText)]
        public string Parrafo1DisAnt { get; set; }

        //Parrafo 1 con Disolucion Posterior a 2003
        [DataType(DataType.MultilineText)]
        public string Parrafo1DisPos { get; set; }
    }
}