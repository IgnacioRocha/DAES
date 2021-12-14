namespace DAES.Infrastructure.SistemaIntegrado
{
    using DAES.Model.SistemaIntegrado;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public partial class SistemaIntegradoContext : IdentityDbContext<ApplicationUser>
    {
        public SistemaIntegradoContext() : base("name=SistemaIntegrado")
        {
        }

        public static SistemaIntegradoContext Create()
        {
            return new SistemaIntegradoContext();
        }

        public virtual DbSet<Cargo> Cargo { get; set; }
        public virtual DbSet<Ciudad> Ciudad { get; set; }
        public virtual DbSet<Comuna> Comuna { get; set; }
        public virtual DbSet<ConfiguracionCertificado> ConfiguracionCertificado { get; set; }
        public virtual DbSet<Configuracion> Configuracion { get; set; }
        public virtual DbSet<Directorio> Directorio { get; set; }
        public virtual DbSet<Estado> Estado { get; set; }
        public virtual DbSet<Firmante> Firmante { get; set; }
        public virtual DbSet<Genero> Genero { get; set; }
        public virtual DbSet<Organizacion> Organizacion { get; set; }
        public virtual DbSet<Perfil> Perfil { get; set; }
        public virtual DbSet<Region> Region { get; set; }
        public virtual DbSet<Rubro> Rubro { get; set; }
        public virtual DbSet<SubRubro> SubRubro { get; set; }
        public virtual DbSet<TipoDocumento> TipoDocumento { get; set; }
        public virtual DbSet<TipoPrivacidad> TipoPrivacidad { get; set; }
        public virtual DbSet<TipoOrganizacion> TipoOrganizacion { get; set; }
        public virtual DbSet<Ayuda> Ayuda { get; set; }
        public virtual DbSet<Proceso> Proceso { get; set; }
        public virtual DbSet<Workflow> Workflow { get; set; }
        public virtual DbSet<TipoWorkflow> TipoWorkflow { get; set; }
        public virtual DbSet<DefinicionProceso> DefinicionProceso { get; set; }
        public virtual DbSet<DefinicionWorkflow> DefinicionWorkflow { get; set; }
        public virtual DbSet<Documento> Documento { get; set; }
        public virtual DbSet<DocumentoSinContenido> DocumentoSinContenido { get; set; }
        public virtual DbSet<TipoAprobacion> TipoAprobacion { get; set; }
        public virtual DbSet<Log> Log { get; set; }
        public virtual DbSet<ActaFiscalizacion> ActaFiscalizacion { get; set; }
        public virtual DbSet<HechoLegal> HechoLegal { get; set; }
        public virtual DbSet<HechoContable> HechoContable { get; set; }
        public virtual DbSet<ActaFiscalizacionHechoContable> ActaFiscalizacionHechoContable { get; set; }
        public virtual DbSet<ActaFiscalizacionHechoLegal> ActaFiscalizacionHechoLegal { get; set; }
        public virtual DbSet<ActaFiscalizacionFiscalizadorContable> ActaFiscalizacionFiscalizadorContable { get; set; }
        public virtual DbSet<ActaFiscalizacionFiscalizadorLegal> ActaFiscalizacionFiscalizadorLegal { get; set; }
        public virtual DbSet<Solicitante> Solicitante { get; set; }
        public virtual DbSet<ActualizacionOrganizacion> ActualizacionOrganizacion { get; set; }
        public virtual DbSet<ActualizacionOrganizacionbkp> ActualizacionOrganizacionbkp { get; set; }
        public virtual DbSet<Situacion> Situacion { get; set; }
        public virtual DbSet<CargaSituacion> CargaSituacion { get; set; }
        public virtual DbSet<DocumentoOrganizacion> DocumentoOrganizacion { get; set; }
        public virtual DbSet<DocumentoWorkflow> DocumentoWorkflow { get; set; }
        public virtual DbSet<Articulo91> Articulo91 { get; set; }
        public virtual DbSet<Periodo> Periodo { get; set; }
        public virtual DbSet<ModificacionEstatuto> ModificacionEstatutos { get; set; }
        public virtual DbSet<Disolucion> Disolucions { get; set; }
        /*public virtual DbSet<DisolucionAsociacion> DisolucionAsociacions { get; set; }
        public virtual DbSet<DisolucionCooperativa> DisolucionCooperativas { get; set; }
        public virtual DbSet<DisolucionCooperativaAnterior> DisolucionCooperativaAnteriors { get; set; }
        public virtual DbSet<DisolucionCooperativaPosterior> DisolucionCooperativaPosteriors { get; set; }
        public virtual DbSet<DisolucionAsociacionConsumidores> DisolucionConsumidores { get; set; }
        public virtual DbSet<DisolucionAsociacionGremial> DisolucionAsociacionGremials { get; set; }*/
        public virtual DbSet<Fiscalizacion> Fiscalizacion { get; set; }
        public virtual DbSet<Hallazgo> Hallazgo { get; set; }
        public virtual DbSet<TipoNorma> TipoNorma { get; set; }
        public virtual DbSet<TipoFiscalizacion> TipoFiscalizacion { get; set; }
        public virtual DbSet<TipoHallazgo> TipoHallazgo { get; set; }
        public virtual DbSet<TipoMateria> TipoMateria { get; set; }
        public virtual DbSet<TipoCriterio> TipoCriterio { get; set; }
        public virtual DbSet<TipoOficio> TipoOficio { get; set; }
        public virtual DbSet<ComisionLiquidadora> ComisionLiquidadora { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Database.SetInitializer<SistemaIntegradoContext>(null);

            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }
    }
}