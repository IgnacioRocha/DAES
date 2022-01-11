﻿using DAES.Model.SistemaIntegrado;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace DAES.Model.DTO
{
    [Table("EstudioSocioEconomico")]

    public class DTOEstudioSocioeconomico 
    {
        public DTOEstudioSocioeconomico()
        {
            
        }

        public object EstudioSocioEconomico { get; set; }

        [Key]
        [Display(Name = "Id")]
        public int IdEstudio { get; set; }

        [Required(ErrorMessage = "Es necesario adjuntar un documento")]
        [Display(Name = "Documento")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase DocumentoAdjunto { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaCreacion { get; set; }
        public string NombreArchivo { get; set; }

        [Display(Name = "Proceso")]
        public int? ProcesoId { get; set; }
        public virtual Proceso Proceso { get; set; }

        //Datos tipo organización - cambiar el display
        [Display(Name = "Tipo Organizacion")]
        public int? TipoOrganizacionId { get; set; }
        public virtual TipoOrganizacion TipoOrganizacion { get; set; }

        //Datos organizacion
        [Display(Name = "Organizacion")]
        public int OrganizacionId { get; set; }
        public virtual Organizacion Organizacion { get; set; }


        [Required(ErrorMessage = "Debe especificar la razón social de la organización")]
        [Display(Name = "*Razón social")]
        public string RazonSocial { get; set; }

        [Display(Name = "Rubro")]
        public int? RubroId { get; set; }
        public virtual Rubro Rubro { get; set; }

        [Display(Name = "Subrubro")]
        public int? SubRubroId { get; set; }
        public virtual SubRubro SubRubro { get; set; }

        [Required(ErrorMessage = "Debe especificar la dirección de la organización")]
        [Display(Name = "*Dirección")]
        public string Direccion { get; set; }
        public string Rut { get; set; }

        [Display(Name = "*Sigla")]
        public string Sigla { get; set; }

        [MaxLength(12)]
        [Required(ErrorMessage = "Debe especificar el fono de la organización")]
        [Display(Name = "*Fono")]
        public string Fono { get; set;}

        [Required(ErrorMessage = "Debe especificar el Email de la organización")]
        [DataType(DataType.EmailAddress, ErrorMessage = "El formato del correo es invalido")]
        [Display(Name = "*Email")]
        public string Email { get; set; }

        //Datos Region - Quitar Display
        [Display (Name = "*Region")]
        [Required(ErrorMessage = "Es necesario especificar el dato Región")]
        public int? RegionId { get; set; }
        public virtual Region Region { get; set; }

        //Datos Comuna - Quitar Display
        [Display (Name = "*Comuna")]
        [Required(ErrorMessage = "Es necesario especificar el dato Comuna")]
        public int? ComunaId { get; set; }
        public virtual Comuna Comuna { get; set; }

        //Dato solicitante - Quitar Display de RegionSolicitante
        [Display(Name = "*Rut del Solicitante")]
        public string RutSolicitante { get; set; }
        public string Apellidos { get; set; }
        public string Nombres { get; set; }

        [Display (Name = "*Region Solicitante")]
        [Required(ErrorMessage = "Debe especificar el dato Región")]
        public int? RegionSolicitante { get; set; }

        [MaxLength(12,ErrorMessage ="Máximo debe ser 12 números")]
        [Required(ErrorMessage = "Debe ingresar números")]
        [Display(Name = "*Fono Solicitante")]
        public string FonoSolicitante { get; set; }

        [Required(ErrorMessage = "Debe ingresar un Email del solicitante")]
        [DataType(DataType.EmailAddress, ErrorMessage = "El formato del correo es invalido")]
        [Display(Name = "*Mail del solicitante")]
        public string MailSolicitante { get; set; }
        public string Observacion { get; set; }
       

    }
}




