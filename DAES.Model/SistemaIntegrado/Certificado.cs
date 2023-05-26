using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.SistemaIntegrado
{
    [Table("Certificado")]
    public class Certificado
    {
        public Certificado()
        {
        }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        [Key]
        public int CertificadoId { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Tipo certificado")]
        public int TipoCertificadoId { get; set; }
        //public virtual TipoCertificado TipoCertificado { get; set; }

        [Display(Name = "Firmante")]
        public int? FirmanteId { get; set; }
        public virtual Firmante Firmante { get; set; }

        public byte[] PDF { get; set; }

        [Display(Name = "Proceso")]
        public int? ProcesoId { get; set; }
        public virtual Proceso Proceso { get; set; }






        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //[Display(Name = "Fecha")]
        //[DataType(DataType.Date)]
        //public DateTime? FechaSolicitud { get; set; }

        //[Display(Name = "RUT")]
        //public string Rut { get; set; }

        //[Display(Name = "Nombres")]
        //public string Nombres { get; set; }

        //[Display(Name = "Apellidos")]
        //public string Apellidos { get; set; }

        //[Display(Name = "Dirección")]
        //public string Direccion { get; set; }

        //[Display(Name = "Fono")]
        //public string Fono { get; set; }

        //[Display(Name = "Email")]
        //public string Email { get; set; }


        //[Display(Name = "Región")]
        //public int? RegionId { get; set; }
        //public virtual Region Region { get; set; }

        //[Display(Name = "Organización")]
        //public int? OrganizacionId { get; set; }
        //public virtual Organizacion Organizacion { get; set; }

        //[NotMapped]
        //[Display(Name = "Organización")]
        //public string NombreOrganizacion { get; set; }
    }
}