﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using DAES.Model.SistemaIntegrado;

namespace DAES.Model.DTO
{
    public class DTOCreacionOrganizacion
    {
        public DTOCreacionOrganizacion()
        {
        }

        [Required(ErrorMessage = "Es necesario especificar el dato RUT")]
        [Display(Name = "RUT (sin puntos y sin guión)")]
        public string RUTSolicitante { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el dato Nombres")]
        [Display(Name = "Nombres")]
        public string NombresSolicitante { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el dato Apellidos")]
        [Display(Name = "Apellidos")]
        public string ApellidosSolicitante { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato Fono")]
        [Display(Name = "Fono")]
        public string FonoSolicitante { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato Email")]
        [Display(Name = "Email")]
        public string EmailSolicitante { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato Región")]
        [Display(Name = "Región")]
        public int RegionSolicitanteId { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato Organización")]
        [Display(Name = "Organización")]
        public int OrganizacionId { get; set; }
        public virtual Organizacion Organizacion { get; set; }

        [Display(Name = "Tipo organización")]
        public int TipoOrganizacionId { get; set; }
        public virtual TipoOrganizacion TipoOrganizacion { get; set; }

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

        [Display(Name = "Ciudad")]
        public int? CiudadId { get; set; }
        public virtual Ciudad Ciudad { get; set; }

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
        public string Email { get; set; }

        [Display(Name = "Sitio web")]
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
        public string EmailContacto { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha celebración")]
        public DateTime? FechaCelebracion { get; set; }

        [Display(Name = "Fecha publicación")]
        [DataType(DataType.Date)]
        public DateTime? FechaPubliccionDiarioOficial { get; set; }

        [Display(Name = "Fecha actualización")]
        [DataType(DataType.Date)]
        public DateTime? FechaActualizacion { get; set; }

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

        [Required(ErrorMessage = "Es necesario especificar el dato documento")]
        [Display(Name = "Documento")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase File1 { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el dato documento")]
        [Display(Name = "Documento")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase File2 { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el dato documento")]
        [Display(Name = "Documento")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase File3 { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el dato documento")]
        [Display(Name = "Documento")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase File4 { get; set; }

        public virtual ICollection<Directorio> Directorios { get; set; }
    }
}