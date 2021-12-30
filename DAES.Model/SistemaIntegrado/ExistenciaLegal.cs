using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.SistemaIntegrado
{
  [Table("ExistenciaLegal")]
  public class ExistenciaLegal
  {
    public ExistenciaLegal()
    {

    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Display(Name = "Id")]
    public int? ExistenciaId { get; set; }

    [Display(Name = "Tipo Norma")]
    public int? TipoNormaId { get; set; }

    public virtual TipoNorma tipoNorma { get; set; }


    [Display(Name = "Número de norma")]
    public int? NumeroNorma { get; set; }


    [Display(Name = "Fecha de Publicación (Diario oficial)")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime? FechaPublicacionn { get; set; }


    [Display(Name = "Fecha de la Norma")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime? FechaNorma { get; set; }


    [Display(Name = " Autorizado Por")]
    public string AutorizadoPor { get; set; }

    [Display(Name = "Organización")]
    public int? OrganizacionId { get; set; }
    public virtual Organizacion Organizacion { get; set; }

    [Display(Name = "Fecha constitutiva de socios")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime? FechaConstitutivaSocios { get; set; }

    [Display(Name = "Fecha de escritura publica")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime? FechaEscrituraPublica { get; set; }

    [Display(Name = "Datos generales notario")]
    public string DatosGeneralesNotario { get; set; }

    [Display(Name = "Fojas;Número")]
    public string Fojas { get; set; }

    [Display(Name = "Año de Inscripción")]
    public int? FechaInscripcion { get; set; }

    [Display(Name = "Datos CBR")]
    public string DatosCBR { get; set; }

    [Display(Name = "Número de Oficio")]
    public int? NumeroOficio { get; set; }

    [Display(Name = "Fecha Oficio")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime? FechaOficio { get; set; }

    [Display(Name = "¿Aprobacion?")]
    public int? AprobacionId { get; set; }
    public virtual Aprobacion Aprobacion { get; set; }

    [NotMapped]
    [Display(Name = "Fecha Publicación (Diario Oficial)")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime? FechaPubli { get; set; }

   
    [Display(Name = "Fecha Publicación (Diario Oficial)")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime? FechaPublic { get; set; }

    //[Display(Name = "Fecha asamblea constitutiva")]
    //[DataType(DataType.Date)]
    //public DateTime? FechaAsambleaConstitutiva { get; set; }
    //public virtual List<ExistenciaLegal> ExistenciaLegals { get; set; }

  }
}
