using DAES.Model.SistemaIntegrado;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.DTO
{

    public class DTOExistencia
    {
        public DTOExistencia()
        {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int ExistenciaId { get; set; }

        [Display(Name = "Tipo Norma")]
        public int? TipoNormaId { get; set; }

        public virtual TipoNorma tipoNorma { get; set; }


        [Display(Name = "Numero de norma")]
        public int? NumeroNorma { get; set; }

        [Display(Name = "Fecha Publicacion")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaPublicacion { get; set; }

        [Display(Name = "Fecha Norma")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaNorma { get; set; }

        [Display(Name = " Autorizado Por")]
        public string AutorizadoPor { get; set; }

        [Display(Name = "Organización")]
        public int? OrganizacionId { get; set; }
        public virtual Organizacion Organizacion { get; set; }

        [Display(Name = "Fecha constitutiva socios")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaConstitutivaSocios { get; set; }

        [Display(Name = "Fecha escritura publica")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaEscrituraPublica { get; set; }

        [Display(Name = "Datos generales notario")]
        public string DatosGeneralesNotario { get; set; }

        [Display(Name = "Fojas")]
        public int? Fojas { get; set; }

        [Display(Name = "Año Inscripción")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaInscripcion { get; set; }

        [Display(Name = "Datos CBR")]
        public string DatosCBR { get; set; }

        [Display(Name = "Número oficio")]
        public int? NumeroOficio { get; set; }

        [Display(Name = "Fecha Oficio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaOficio { get; set; }


    }
}
