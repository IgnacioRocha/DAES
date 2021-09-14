using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.SistemaIntegrado
{
    [Table("DefinicionWorkflow")]
    public class DefinicionWorkflow
    {
        public DefinicionWorkflow()
        {
            Workflows = new HashSet<Workflow>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int DefinicionWorkflowId { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Definición proceso")]
        public int DefinicionProcesoId { get; set; }
        public virtual DefinicionProceso DefinicionProceso { get; set; }

        [Display(Name = "Perfil")]
        public int? PerfilId { get; set; }
        public virtual Perfil Perfil { get; set; }

        [ForeignKey("User"), Column(Order = 0)]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        [Display(Name = "Secuencia")]
        public int Secuencia { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Habilitado?")]
        public bool Habilitado { get; set; }

        [ForeignKey("TipoWorkflow")]
        public int TipoWorkflowId { get; set; }
        public virtual TipoWorkflow TipoWorkflow { get; set; }

        [Display(Name = "Requiere aprobación de todos los usuarios para continuar?")]
        public bool RequiereAprobacionGrupal { get; set; }

        [Display(Name = "Rechazo")]
        [ForeignKey("DefinicionWorkflowRechazo")]
        public int? DefinicionWorkflowRechazoId { get; set; }

        [Display(Name = "Depende de")]
        public int? DefinicionWorkflowDependeDeId { get; set; }

        public virtual DefinicionWorkflow DefinicionWorkflowRechazo { get; set; }
        public virtual ICollection<Workflow> Workflows { get; set; }
    }
}