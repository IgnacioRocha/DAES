using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.SistemaIntegrado
{
    [Table("Saneamiento")]
     public class Saneamiento
    {
        public Saneamiento() 
        { 
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int? IdSaneamiento { get; set; }

        [Display(Name = "Fecha de Escritura Pública")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaEscrituraPublicaa { get; set; }

        [Display(Name = "Fecha de Publicacion (Diario oficial)")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaaPublicacionDiario { get; set; }

        [Display(Name ="Datos generales del Notario Público y Notaría")]
        public string DatoGeneralesNotario { get; set; }

        [Display(Name ="Fojas; Número")]
        public int? Fojass { get; set; }

        [Display(Name = "Año de Incripción")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaaInscripcion { get; set; }

        [Display(Name = "Datos CBR")]
        public string DatossCBR { get; set; }

        [Display(Name = "Organización")]
        public int? OrganizacionId { get; set; }
        public virtual Organizacion Organizacion { get; set; }




    }
}
