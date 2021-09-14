using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.SistemaIntegrado
{
    [Table("RespuestaOficio")]
    public class RespuestaOficio
    {
        public RespuestaOficio()
        {
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int RespuestaOficioId { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Organización")]
        public int OrganizacionId { get; set; }
        public virtual Organizacion Organizacion { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el dato Número de oficio")]
        [Display(Name = "Número de oficio")]
        public string NumeroDeOficio { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el dato Fecha de salida del oficio")]
        [Display(Name = "Fecha de salida del oficio")]
        [DataType(DataType.Date)]
        public DateTime? FechaSalidaOficio { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha estado asignación rol")]
        [DataType(DataType.Date)]
        public DateTime? FechaAsignacionRol { get; set; }

        [Display(Name = "Proceso")]
        public int? ProcesoId { get; set; }
        public virtual Proceso Proceso { get; set; }
    }
}