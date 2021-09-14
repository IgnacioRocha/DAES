using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.SistemaIntegrado
{
    [Table("Hallazgo")]
    public class Hallazgo
    {
        public Hallazgo()
        {
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int HallazgoId { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Tipo materia")]
        public int? TipoMateriaId { get; set; }
        public virtual TipoMateria TipoMateria { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Tipo criterio")]
        public int? TipoCriterioId { get; set; }
        public virtual TipoCriterio TipoCriterio { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Tipo hallazgo")]
        public int? TipoHallazgoId { get; set; }
        public virtual TipoHallazgo TipoHallazgo { get; set; }

        [Display(Name = "Plazo")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Plazo { get; set; }
        
        [Display(Name = "Respondido?")]
        public bool Respondido { get; set; } = false;

        [Display(Name = "Resuelto?")]
        public bool Resuelto { get; set; } = false;

        [Display(Name = "Fiscalización")]
        public int? FiscalizacionId { get; set; }
        public virtual Fiscalizacion Fiscalizacion { get; set; }

        [Display(Name = "Descripción")]
        [DataType(DataType.MultilineText)]
        public string Descripcion { get; set; }

        [Display(Name = "Orden")]
        public int Orden { get; set; } = 0;

        [Display(Name = "Próxima junta o asamblea?")]
        public bool ProximaJuntaoAsamblea { get; set; } = false;

        public bool Activo { get; set; } = true;
        public string EliminadoPor { get; set; }

    }
}
