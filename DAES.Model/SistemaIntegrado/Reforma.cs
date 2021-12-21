using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.SistemaIntegrado
{
    [Table("Reforma")]
    public class Reforma
    {
        //public Reforma()
        //{
        //}

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int IdReforma { get; set; }

        [Display(Name = "Fecha de Reforma")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaReforma { get; set; }

        
        [Display(Name = "Fecha de Reforma")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaReformaa { get; set; }

        [Display(Name = "Última reforma?")]
        public bool UltimaReforma { get; set; }

        [NotMapped]
        [Display(Name = "Última reforma?")]
        public bool UltimaReformaa { get; set; }

        [NotMapped]
        [Display(Name = "Última reforma?")]
        public bool? UltimaReformaaa { get; set; }

        [Display(Name = "Tipo de Norma")]
        public int? TipoNormaId { get; set; }
        public virtual TipoNorma TipoNorma { get; set; }


        [Display(Name = "Número de Norma")]
        public int? NumeroNormaa { get; set; }

        [Display(Name = "Fecha de Norma")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaNormaa { get; set; }

        [Display(Name = "Fecha Publicación diario oficial")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaPublicacionDiario { get; set; }

        [Display(Name = "Datos general del notario")]
        public string DatosGeneralNotario { get; set; }

        [Display(Name = "Organización")]
        public int? OrganizacionId { get; set; }
        public virtual Organizacion Organizacion { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaJuntaGeneral { get; set; }

        [Display(Name ="Fecha junta general de Socios")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaPublicacion { get; set; }


        [Display(Name = "Año de inscripción")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? AnoInscripcion { get; set; }

        public string DatosCBR { get; set; }

        [Display(Name ="Fecha de la escritura Pública")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaEscrituraPublica { get; set; }

        [Display(Name ="Fojas;Número")]
        public int? Fojas { get; set; }

        [Display(Name = "Asamblea/Deposito")]
        public int? AsambleaDepId { get; set; }
        public virtual AsambleaDeposito AsambleaDeposito { get; set; }

        [Display(Name ="Fecha Asamblea/depósito")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaAsambleaDep { get; set; }

        [Display(Name ="Fecha de Oficio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaOficio { get; set; }

        [Display(Name ="Número de Oficio")]
        public int? NumeroOficio { get; set; }

        public virtual Aprobacion Aprobacion { get; set; }

        [Display(Name ="¿Aprobacion?")]
        public int? AprobacionId { get; set; }


    }
}
