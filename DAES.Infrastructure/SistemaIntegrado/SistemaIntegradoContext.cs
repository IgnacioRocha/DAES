
using DAES.Model.Core;
using DAES.Model.FirmaDocumento;
using DAES.Model.SistemaIntegrado;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DAES.Infrastructure.SistemaIntegrado
{
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
        public virtual DbSet<TipoNorma> TipoNorma { get; set; }
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
        public virtual DbSet<Fiscalizacion> Fiscalizacion { get; set; }
        public virtual DbSet<Hallazgo> Hallazgo { get; set; }
        public virtual DbSet<TipoFiscalizacion> TipoFiscalizacion { get; set; }
        public virtual DbSet<TipoHallazgo> TipoHallazgo { get; set; }
        public virtual DbSet<TipoMateria> TipoMateria { get; set; }
        public virtual DbSet<TipoCriterio> TipoCriterio { get; set; }
        public virtual DbSet<TipoOficio> TipoOficio { get; set; }
        public virtual DbSet<AsambleaDeposito> AsambleaDeposito { get; set; }
        public virtual DbSet<Rubrica> Rubrica { get; set; }
        public virtual DbSet<Reforma> Reforma { get; set; }
        public virtual DbSet<Saneamiento> Saneamiento { get; set; }
        public virtual DbSet<ExistenciaLegal> ExistenciaLegal { get; set; }
        public virtual DbSet<EstudioSocioEconomico> EstudioSocioEconomico { get; set; }
        public virtual DbSet<CooperativaAbierta> CooperativaViviendaAbierta { get; set; }
        public virtual DbSet<ComisionLiquidadora> ComisionLiquidadora { get; set; }
        public virtual DbSet<TipoPersonaJuridica> TipoPersonaJuridicas { get; set; }
        public virtual DbSet<SupervisorAuxiliar> SupervisorAuxiliars { get; set; }
        public virtual DbSet<RepresentanteLegal> RepresentantesLegals { get; set; }
        public virtual DbSet<EscrituraConstitucion> EscrituraConstitucions { get; set; }
        public virtual DbSet<ExtractoAuxiliar> ExtractoAuxiliars { get; set; }
        public virtual DbSet<PersonaFacultada> PersonaFacultadas { get; set; }
        public virtual DbSet<Aprobacion> Aprobacion { get; set; }
        public virtual DbSet<ReformaAnterior> ReformaAnterior { get; set; }
        public virtual DbSet<ReformaPosterior> ReformaPosterior { get; set; }
        public virtual DbSet<ReformaAGAC> ReformaAGAC { get; set; }
        public virtual DbSet<ExistenciaAnterior> ExistenciaLegalAnterior { get; set; }
        public virtual DbSet<ExistenciaPosterior> ExistenciaPosterior { get; set; }
        public virtual DbSet<FirmaDocumento> FirmaDocumento { get; set; }
        /*
         * Tablas de Actualizacion de Supervisor
         */
        public virtual DbSet<ActualizacionSupervisor> ActualizacionSupervisors { get; set; }
        public virtual DbSet<ActualizacionEscrituraConstitucion> ActualizacionEscrituraConstitucions { get; set; }
        public virtual DbSet<ActualizacionPersonaFacultada> ActualizacionPersonaFacultadas { get; set; }
        public virtual DbSet<ActualizacionRepresentante> ActualizacionRepresentantes { get; set; }
        public virtual DbSet<ActualizacionExtractoAuxiliar> ActualizacionExtractoAuxiliars { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Database.SetInitializer<SistemaIntegradoContext>(null);

            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }
    }
}