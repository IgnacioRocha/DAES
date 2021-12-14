using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.SistemaIntegrado
{
    [Table("DisolucionCooperativaPosterior")]
    public class DisolucionCooperativaPosterior
    {
        public DisolucionCooperativaPosterior()
        {
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int DisolucionCooperativaPosteriorId { get; set; }

        [Display(Name = "Organización")]
        public int? OrganizacionId { get; set; }
        public virtual Organizacion Organizacion { get; set; }

        [Display(Name = "Fecha Junta General de Socios")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaJuntaSocios { get; set; }

        [Display(Name = "Fecha de Escritura Publica")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaEscrituraPublica { get; set; }

        [Display(Name = "Datos Generales Notario Público y Notaría")]
        public string MinistroDeFe { get; set; }

        [Display(Name = "Fecha publicación diario oficial")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaPublicacionDiarioOficial { get; set; }

        [Display(Name = "Fojas; Número")]
        public int NumeroFojas { get; set; }

        [Display(Name ="Año de inscripción")]
        public int AñoInscripcion { get; set; }

        [Display(Name ="Datos Conserbador Bienes Raices")]
        public string DatosCBR { get; set; }

        [Display(Name ="Comisión Liquidadora")]
        public bool ComisionLiquidadora { get; set; }
    }
}
