using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.SistemaIntegrado
{
    [Table("DefinicionProceso")]
    public class DefinicionProceso
    {
        public DefinicionProceso()
        {
            DefinicionWorkflows = new HashSet<DefinicionWorkflow>();
            Procesos = new HashSet<Proceso>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int DefinicionProcesoId { get; set; }

        [Display(Name = "Autor")]
        public string Autor { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "FechaCreacion")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Nombre proceso")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Descripción")]
        [DataType(DataType.MultilineText)]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Duración máxima (días)")]
        public int Duracion { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Habilitado?")]
        public bool Habilitado { get; set; }

        public virtual ICollection<DefinicionWorkflow> DefinicionWorkflows { get; set; }
        public virtual ICollection<Proceso> Procesos { get; set; }
    }
}