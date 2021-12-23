using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAES.Model.SistemaIntegrado;

namespace DAES.Model.DTO
{
    public class DTODisolucion : DTOSolicitante
    {
        public DTODisolucion() { }

        [Required(ErrorMessage = "Es necesario especificar el dato Organización")]
        [Display(Name = "Organización")]
        public int OrganizacionId { get; set; }
        public virtual Organizacion Organizacion { get; set; }

        [Display(Name = "Tipo organizacion")]
        public int TipoOrganizacionId { get; set; }
        public virtual TipoOrganizacion TipoOrganizacion { get; set; }

        [Display(Name ="Número oficio")]
        public string NumeroOficio { get; set; }
        
        [Display(Name ="Fecha de asamblea")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaAsamblea { get; set; }

        [Display(Name = "Fecha de publicación diario oficial")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaPublicacion { get; set; }


        [Display(Name = "Estado")]
        public int? EstadoId { get; set; }
        public virtual Estado Estado { get; set; }

        [Display(Name = "Rubro")]
        public int? RubroId { get; set; }
        public virtual Rubro Rubro { get; set; }

        [Display(Name = "SubRubro")]
        public int? SubRubroId { get; set; }
        public virtual SubRubro SubRubro { get; set; }

        [Display(Name = "Situacion")]
        public int? SituacionId { get; set; }
        public virtual Situacion Situacion { get; set; }

        [Display(Name = "Región")]
        public int? RegionId { get; set; }
        public virtual Region Region { get; set; }

        [Display(Name = "Comuna")]
        public int? ComunaId { get; set; }
        public virtual Comuna Comuna { get; set; }

        [Display(Name = "Ciudad")]
        public int? CiudadId { get; set; }
        public virtual Ciudad Ciudad { get; set; }

        [Display(Name = "Número registro")]
        public string NumeroRegistro { get; set; }

        [Display(Name = "RUT (sin puntos y sin guión)")]
        [StringLength(13)]
        public string RUT { get; set; }

        [Display(Name = "Razón social")]
        [DataType(DataType.MultilineText)]
        public string RazonSocial { get; set; }

        [Display(Name = "Sigla")]
        public string Sigla { get; set; }

        [Display(Name = "Dirección")]
        [DataType(DataType.MultilineText)]
        public string Direccion { get; set; }

        [Display(Name = "Fono")]
        public string Fono { get; set; }

        [Display(Name = "Fax")]
        public string Fax { get; set; }

        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }

        [Display(Name = "Sitio web")]
        [Url(ErrorMessage = "URL inválida")]
        public string URL { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el dato Socios constituyentes")]
        [Display(Name = "Socios constituyentes")]
        public int NumeroSociosConstituyentes { get; set; } = 0;

        [Required(ErrorMessage = "Es necesario especificar el dato Total socios")]
        [Display(Name = "Total socios")]
        public int NumeroSocios { get; set; } = 0;

        [Display(Name = "Socios hombres")]
        public int? NumeroSociosHombres { get; set; }

        [Display(Name = "Socios mujeres")]
        public int? NumeroSociosMujeres { get; set; }

        [Display(Name = "Ministro fé")]
        public string MinistroDeFe { get; set; }

        [Display(Name = "Es exclusivo de mujeres?")]
        public bool? EsGeneroFemenino { get; set; }

        [Display(Name = "Ciudad asamblea")]
        public string CiudadAsamblea { get; set; }

        [Display(Name = "Nombre contacto")]
        public string NombreContacto { get; set; }

        [Display(Name = "Dirección contacto")]
        [DataType(DataType.MultilineText)]
        public string DireccionContacto { get; set; }

        [Display(Name = "Teléfono contacto")]
        [Phone(ErrorMessage = "Fono inválido")]
        public string TelefonoContacto { get; set; }

        [Display(Name = "Email contacto")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string EmailContacto { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha celebración")]
        public DateTime? FechaCelebracion { get; set; }

        [Display(Name = "Fecha publicación")]
        [DataType(DataType.Date)]
        public DateTime? FechaPublicacionDiarioOficial { get; set; }

        [Display(Name = "Fecha actualización")]
        [DataType(DataType.Date)]
        public DateTime? FechaActualizacion { get; set; }

        [Display(Name = "Es importancia económica?")]
        public bool? EsImportanciaEconomica { get; set; }

        [Display(Name = "Fecha estado vigente")]
        [DataType(DataType.Date)]
        public DateTime? FechaVigente { get; set; }

        [Display(Name = "Fecha estado disolución")]
        [DataType(DataType.Date)]
        public DateTime? FechaDisolucion { get; set; }

        [Display(Name = "Fecha estado constitución")]
        [DataType(DataType.Date)]
        public DateTime? FechaConstitucion { get; set; }

        [Display(Name = "Fecha estado cancelación")]
        [DataType(DataType.Date)]
        public DateTime? FechaCancelacion { get; set; }

        [Display(Name = "Fecha estado inexistencia")]
        [DataType(DataType.Date)]
        public DateTime? FechaInexistencia { get; set; }

        [Display(Name = "Fecha estado asignación rol")]
        [DataType(DataType.Date)]
        public DateTime? FechaAsignacionRol { get; set; }






        [Required(ErrorMessage = "Es necesario especificar el dato Copia autorizada de la Escritura Pública en la cual se contiene el acta de la Junta General de Socios que aprobó la disolución de la Cooperativa")]
        [Display(Name = "Copia autorizada de la Escritura Pública en la cual se contiene el acta de la Junta General de Socios que aprobó la disolución de la Cooperativa")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase File1 { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el dato Copia autorizada del extracto de la escritura antes señalada")]
        [Display(Name = "Copia autorizada del extracto de la escritura antes señalada")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase File2 { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el dato Copia del Diario Oficial en el que se publicó el Extracto referido precedentemente")]
        [Display(Name = "Copia del Diario Oficial en el que se publicó el Extracto referido precedentemente")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase File3 { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el dato Copia autorizada ante notaria de la inscripción del Extracto en el Registro de Comercio del Conservador de Bienes Raíces competente, al margen de la inscripción original")]
        [Display(Name = "Copia autorizada ante notaria de la inscripción del Extracto en el Registro de Comercio del Conservador de Bienes Raíces competente, al margen de la inscripción original")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase File4 { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el dato Formalidades de convocatoria, esto es, copia del aviso de citación publicado; comprobantes de correo y(o copia del Libro de citaciones")]
        [Display(Name = "Formalidades de convocatoria, esto es, copia del aviso de citación publicado; comprobantes de correo y(o copia del Libro de citaciones")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase File5
        { get; set; }
    }
}
