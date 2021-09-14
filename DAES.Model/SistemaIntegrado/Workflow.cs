using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.SistemaIntegrado
{
    [Table("Workflow")]
    public class Workflow
    {
        public Workflow()
        {
            Documentos = new HashSet<Documento>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int WorkflowId { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Proceso")]
        public int ProcesoId { get; set; }
        public virtual Proceso Proceso { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Definición de tarea detalle")]
        public int DefinicionWorkflowId { get; set; }
        public virtual DefinicionWorkflow DefinicionWorkflow { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha creación")]
        [DataType(DataType.Date)]
        public DateTime FechaCreacion { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha término")]
        [DataType(DataType.Date)]
        public DateTime? FechaTermino { get; set; }

        [Display(Name = "Perfil")]
        public int? PerfilId { get; set; }
        public virtual Perfil Perfil { get; set; }

        [Display(Name = "Observación")]
        [DataType(DataType.MultilineText)]
        public string Observacion { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Tipo aprobación")]
        public int TipoAprobacionId { get; set; }
        public virtual TipoAprobacion TipoAprobacion { get; set; }

        [Display(Name = "Terminada?")]
        public bool Terminada { get; set; }

        public string NextUserId { get; set; }

        [ForeignKey("User"), Column(Order = 0)]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public int? WorkflowGrupoId { get; set; }

        public virtual ICollection<Documento> Documentos { get; set; }

    }
}
