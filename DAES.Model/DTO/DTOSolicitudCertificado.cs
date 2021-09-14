using System;
using System.ComponentModel.DataAnnotations;
using DAES.Model.SistemaIntegrado;

namespace DAES.Model.DTO
{
    public class DTOSolicitudCertificado
    {
        public DTOSolicitudCertificado()
        {
        }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "FechaCreacion")]
        [DataType(DataType.Date)]
        public DateTime FechaSolicitud { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el dato RUT")]
        [Display(Name = "RUT (sin puntos y sin guión)")]
        public string Rut { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el dato Nombres")]
        [Display(Name = "Nombres")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el dato Apellidos")]
        [Display(Name = "Apellidos")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el dato Fono")]
        [Display(Name = "Fono (solo números)")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Solo se permiten números")]
        public string Fono { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el dato Email")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el dato Región")]
        [Display(Name = "Región")]
        public int RegionId { get; set; }
        public virtual Region Region { get; set; }

        [Display(Name = "Organización")]
        public int? OrganizacionId { get; set; }
        public virtual Organizacion Organizacion { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el dato Tipo documento")]
        [Display(Name = "Tipo documento")]
        public int TipoDocumentoId { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el dato Organización")]
        [Display(Name = "Organización")]
        public string NombreOrganizacion { get; set; }
    }
}