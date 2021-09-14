using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.Supervision
{
    [Table("cacHistorialProcesoWorkflow")]
    public partial class cacAcacHistorialProcesoWorkflow
    {
        [Key]
        public long Id { get; set; }
        public DateTime? Fecha { get; set; }
        public int? FichaId { get; set; }
        public string Comentario { get; set; }
        public string Responsable { get; set; }
        public string Usuario { get; set; }

        public virtual cacInfCont cacInfCont { get; set; }

        [ForeignKey("cacEstadoActual"), Column(Order = 0)]
        public int? EstadoActual { get; set; }
        public virtual cacEstado cacEstadoActual { get; set; }

        [ForeignKey("cacNuevoEstado"), Column(Order = 1)]
        public int? NuevoEstado { get; set; }
        public virtual cacEstado cacNuevoEstado { get; set; }
    }
}