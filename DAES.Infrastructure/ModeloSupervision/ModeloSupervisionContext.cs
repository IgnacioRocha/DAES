namespace DAES.Infrastructure.ModeloSupervision
{
    using System.Data.Entity;
    using DAES.Model.Supervision;

    public partial class ModeloSupervisionContext : DbContext
    {
        public ModeloSupervisionContext() : base("name=Supervision")
        {
        }

        public virtual DbSet<cacEvaluacion> cacEvaluacion { get; set; }
        public virtual DbSet<cacFactor> cacFactor { get; set; }
        public virtual DbSet<cacFormulario> cacFormulario { get; set; }
        public virtual DbSet<cacInfCont> cacInfCont { get; set; }
        public virtual DbSet<cacPeriodo> cacPeriodo { get; set; }
        public virtual DbSet<cacResumenFormulario> cacResumenFormulario { get; set; }
        public virtual DbSet<cacTipoFicha> cacTipoFicha { get; set; }
        public virtual DbSet<cacAcacHistorialProcesoWorkflow> cacAcacHistorialProcesoWorkflow { get; set; }
        public virtual DbSet<cacArea> cacArea { get; set; }
        public virtual DbSet<cacAutoevaluacion> cacAutoevaluacion { get; set; }
        public virtual DbSet<cacConcepto> cacConcepto { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<cacEvaluacion>()
                .Property(e => e.evalcalificacion)
                .HasPrecision(5, 2);

            modelBuilder.Entity<cacEvaluacion>()
                .Property(e => e.evalnivel)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<cacFormulario>()
                .Property(e => e.formPonderacion)
                .HasPrecision(5, 2);

            modelBuilder.Entity<cacResumenFormulario>()
                .Property(e => e.refocalificacion)
                .HasPrecision(5, 2);
        }
    }
}
