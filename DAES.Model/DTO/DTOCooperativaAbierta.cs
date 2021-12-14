using DAES.Model.SistemaIntegrado;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DAES.Model.DTO
{
    [Table("CooperativaAbierta")]

    public class DTOCooperativaAbierta
    {

        public DTOCooperativaAbierta()
        {

        }
        public object CooperativaAbierta { get; set; }

        [Key]
        [Display(Name = "Id")]
        public int IdCooperativaAbierta { get; set; }

        [Required(ErrorMessage = "Debe adjuntar un documento")]
        [Display(Name = "*Documento")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase DocumentoAdjunto { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaCreacion { get; set; }

        [Display(Name = "Proceso")]
        public int? ProcesoId { get; set; }
        public virtual Proceso Proceso { get; set; }

        [Display(Name = "Organizacion")]
        public int OrganizacionId { get; set; }
        public virtual Organizacion Organizacion { get; set; }

        [Display(Name = "Tipo Organizacion")]
        public int? TipoOrganizacionId { get; set; }
        public virtual TipoOrganizacion TipoOrganizacion { get; set; }


        [Display(Name = "*Razón Social")]
        [Required(ErrorMessage = "Se debe especificar la razón social")]
        public string RazonSocial { get; set; }

        [Display(Name = "*Dirección")]
        [Required(ErrorMessage = "Debe especificar la dirección")]
        public string Direccion { get; set; }

        [Display(Name = "Rubro")]
        public int? RubroId { get; set; }
        public virtual Rubro Rubro { get; set; }

        [Display(Name = "Sub-Rubro")]
        public int ? SubRubroId { get; set; }
        public virtual SubRubro SubRubro { get; set; }

        [Display(Name = "*Región")]
        [Required(ErrorMessage = "Debe especificar una región")]
        public int RegionId { get; set; }
        public virtual Region Region { get; set; }


        [Display(Name = "*Comuna")]
        [Required(ErrorMessage = "Debe especificar una comuna")]
        public int ComunaId { get; set; }
        public virtual Comuna Comuna { get; set; }

        [Display(Name = "*Rut")]
        [Required(ErrorMessage = "Debe especificar el rut")]
        public string Rut { get; set; }

        [Display(Name = "*Sigla")]
        [Required(ErrorMessage = "Debe especificar la sigla")]
        public string Sigla { get; set; }

        [Display(Name = "*Fono")]
        [MaxLength(9, ErrorMessage = "El fono debe tener un máximo de 9")]
        [Required(ErrorMessage = "Debe especificar el fono")]
        public string Fono { get; set; }

        [Display(Name = "*Email")]
        [Required(ErrorMessage = "Debe especificar el email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Debe especificar un correo válido")]
        public string Email { get; set; }

        [Display(Name="Rut Solicitante: ")]
        public string RutSolicitante { get; set; }

        [Display(Name = "Nombres Solicitante: ")]
        public string Nombres { get; set; }

        [Display(Name = "Apellidos Solicitante: ")]
        public string Apellidos { get; set; }

        [Display(Name = "*Email Soliciante")]
        [Required(ErrorMessage = "Debe especificar el email del solicitante")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Debe especificar un correo válido")]
        public string MailSolicitante { get; set; }

        [Display(Name = "*Región Solicitante")]
        [Required(ErrorMessage = "Debe especificar una región para el soliciante")]
        public int? RegionSolicitante { get; set; }

        [Display(Name = "*Fono Solicitante")]
        [MaxLength(9, ErrorMessage = "El fono debe tener un máximo de 9")]
        [Required(ErrorMessage = "Debe especificar el fono del solicitante")]
        public string FonoSolicitante { get; set; }

        public string Observacion { get; set; }
    }
}
