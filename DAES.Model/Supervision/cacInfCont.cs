namespace DAES.Model.Supervision
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("cacInfCont")]
    public partial class cacInfCont
    {
        public cacInfCont()
        {
            cacAcacHistorialProcesoWorkflows = new HashSet<cacAcacHistorialProcesoWorkflow>();
        }

        [Key]
        public int FichaId { get; set; }

        public int? RolCooperativa { get; set; }

        public int? EstadoId { get; set; }

        [Display(Name = "FechaCreacion")]
        public DateTime? FechaIngreso { get; set; }

        [Display(Name = "Periodo")]
        public int? Periodo { get; set; }

        public int? TipoFichaId { get; set; }

        public string Comentario { get; set; }

        public int? NumIngreso { get; set; }

        public DateTime? FechaDesde { get; set; }

        public DateTime? FechaHasta { get; set; }

        [StringLength(100)]
        public string NombreResp { get; set; }

        [StringLength(200)]
        public string CargoResp { get; set; }

        [StringLength(100)]
        public string NombGerente { get; set; }

        [StringLength(15)]
        public string RutCoop { get; set; }

        [StringLength(500)]
        public string NombCoop { get; set; }

        [StringLength(200)]
        public string RutaArchivo { get; set; }

        public string CorreoElectronico { get; set; }

        public bool? hasChanged { get; set; }

        public int? Holgura { get; set; }

        public virtual cacTipoFicha cacTipoFicha { get; set; }
        public virtual cacEstado cacEstado { get; set; }
        public virtual ICollection<cacAcacHistorialProcesoWorkflow> cacAcacHistorialProcesoWorkflows { get; set; }

    }
}
