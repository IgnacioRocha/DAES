using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.SistemaIntegrado
{
    [Table("CreacionOrganizacion")]
    public class CreacionOrganizacion
    {
        public CreacionOrganizacion()
        {
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int CreacionOrganizacionId { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Organización")]
        public int OrganizacionId { get; set; }
        public virtual Organizacion Organizacion { get; set; }

        [Display(Name = "Tipo organización")]
        public int? TipoOrganizacionId { get; set; }
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

        [Display(Name = "Provincia")]
        public int? ProvinciaId { get; set; }
        public virtual Provincia Provincia { get; set; }

        [Display(Name = "Comuna")]
        public int? ComunaId { get; set; }
        public virtual Comuna Comuna { get; set; }

        [Display(Name = "Ciudad")]
        public int? CiudadId { get; set; }
        public virtual Ciudad Ciudad { get; set; }

        [Display(Name = "Número registro")]
        public string NumeroRegistro { get; set; }

        [Display(Name = "RUT")]
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

        [Display(Name = "Socios constituyentes")]
        public int? NumeroSociosConstituyentes { get; set; }

        [Display(Name = "Total socios")]
        public int? NumeroSocios { get; set; }

        [Display(Name = "Socios hombres")]
        public int? NumeroSociosHombres { get; set; }

        [Display(Name = "Socios mujeres")]
        public int? NumeroSociosMujeres { get; set; }

        [Display(Name = "Ministro fé")]
        public string MinistroDeFe { get; set; }

        [Display(Name = "Exclusivo mujeres?")]
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
        //[EmailAddress(ErrorMessage = "Email inválido")]
        public string EmailContacto { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha creación")]
        [DataType(DataType.Date)]
        public DateTime? FechaCreacion { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha celebración")]
        [DataType(DataType.Date)]
        public DateTime? FechaCelebracion { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha publicación")]
        [DataType(DataType.Date)]
        public DateTime? FechaPubliccionDiarioOficial { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha actualización")]
        [DataType(DataType.Date)]
        public DateTime? FechaActualizacion { get; set; }

        [Display(Name = "Imp. económica?")]
        public bool? EsImportanciaEconomica { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha estado vigente")]
        [DataType(DataType.Date)]
        public DateTime? FechaVigente { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha estado disolución")]
        [DataType(DataType.Date)]
        public DateTime? FechaDisolucion { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha estado constitución")]
        [DataType(DataType.Date)]
        public DateTime? FechaConstitucion { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha estado cancelación")]
        [DataType(DataType.Date)]
        public DateTime? FechaCancelacion { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha estado inexistencia")]
        [DataType(DataType.Date)]
        public DateTime? FechaInexistencia { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha estado asignación rol")]
        [DataType(DataType.Date)]
        public DateTime? FechaAsignacionRol { get; set; }

        [Display(Name = "Proceso")]
        public int? ProcesoId { get; set; }
        public virtual Proceso Proceso { get; set; }
    }
}