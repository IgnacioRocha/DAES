namespace DAES.Infrastructure.GestionDocumental
{
    using System.Data.Entity;
    using DAES.Model.GestionDocumental;

    public partial class GestionDocumentalContext : DbContext
    {
        public GestionDocumentalContext(): base("name=Gestiondocumental")
        {
        }

        public virtual DbSet<Adjunto> Adjunto { get; set; }
        public virtual DbSet<Documento> Documento { get; set; }
        public virtual DbSet<EntidadExterna> EntidadExterna { get; set; }
        public virtual DbSet<ProcesoDocumento> ProcesoDocumento { get; set; }
        public virtual DbSet<TipoAdjunto> TipoAdjunto { get; set; }
        public virtual DbSet<TipoDestino> TipoDestino { get; set; }
        public virtual DbSet<TipoDocumento> TipoDocumento { get; set; }
        public virtual DbSet<TipoProceso> TipoProceso { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Adjunto>()
                .Property(e => e.IdTabla)
                .IsUnicode(false);

            modelBuilder.Entity<Adjunto>()
                .Property(e => e.Adj_IdSharePoint)
                .IsUnicode(false);

            modelBuilder.Entity<Adjunto>()
                .Property(e => e.Adj_Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Adjunto>()
                .Property(e => e.Adj_TipoMime)
                .IsUnicode(false);

            modelBuilder.Entity<Adjunto>()
                .Property(e => e.Adj_Numero)
                .IsUnicode(false);

            modelBuilder.Entity<Adjunto>()
                .Property(e => e.Adj_Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Adjunto>()
                .Property(e => e.Adj_LoginUsuario)
                .IsUnicode(false);

            modelBuilder.Entity<Adjunto>()
                .Property(e => e.Adj_Url)
                .IsUnicode(false);

            modelBuilder.Entity<Documento>()
                .Property(e => e.Doc_UnidadServicio)
                .IsUnicode(false);

            modelBuilder.Entity<Documento>()
                .Property(e => e.Doc_NombreFuncionario)
                .IsUnicode(false);

            modelBuilder.Entity<Documento>()
                .Property(e => e.Doc_Correlativo)
                .IsUnicode(false);

            modelBuilder.Entity<Documento>()
                .Property(e => e.Doc_NumeroExterno)
                .IsUnicode(false);

            modelBuilder.Entity<Documento>()
                .Property(e => e.Doc_Asunto)
                .IsUnicode(false);

            modelBuilder.Entity<Documento>()
                .Property(e => e.Doc_Referencia)
                .IsUnicode(false);

            modelBuilder.Entity<Documento>()
                .Property(e => e.Doc_Codigo)
                .IsUnicode(false);

            modelBuilder.Entity<Documento>()
                .Property(e => e.Doc_Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Documento>()
                .Property(e => e.Doc_Folio)
                .IsUnicode(false);

            modelBuilder.Entity<Documento>()
                .HasMany(e => e.ProcesoDocumento)
                .WithRequired(e => e.Documento)
                .HasForeignKey(e => e.Documento_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EntidadExterna>()
                .Property(e => e.Ext_Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<EntidadExterna>()
                .Property(e => e.Ext_Mail)
                .IsUnicode(false);

            modelBuilder.Entity<EntidadExterna>()
                .HasMany(e => e.Documento)
                .WithOptional(e => e.EntidadExterna)
                .HasForeignKey(e => e.EntidadExterna_Id);

            modelBuilder.Entity<ProcesoDocumento>()
                .Property(e => e.Pdo_NombreFuncionario)
                .IsUnicode(false);

            modelBuilder.Entity<ProcesoDocumento>()
                .Property(e => e.Pdo_NombreUnidadFuncionario)
                .IsUnicode(false);

            modelBuilder.Entity<ProcesoDocumento>()
                .Property(e => e.Pdo_ObsSiguienteProceso)
                .IsUnicode(false);

            modelBuilder.Entity<ProcesoDocumento>()
                .Property(e => e.Pdo_NombreUnidadEntidadDestino)
                .IsUnicode(false);

            modelBuilder.Entity<ProcesoDocumento>()
                .Property(e => e.Pdo_NombreUsuarioDestino)
                .IsUnicode(false);

            modelBuilder.Entity<TipoAdjunto>()
                .Property(e => e.Tad_Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<TipoAdjunto>()
                .HasMany(e => e.Adjunto)
                .WithOptional(e => e.TipoAdjunto)
                .HasForeignKey(e => e.TipoAdjunto_Id);

            modelBuilder.Entity<TipoDestino>()
                .Property(e => e.Tde_Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<TipoDestino>()
                .HasMany(e => e.ProcesoDocumento)
                .WithRequired(e => e.TipoDestino)
                .HasForeignKey(e => e.TipoDestino_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TipoDocumento>()
                .Property(e => e.Tdo_Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<TipoDocumento>()
                .HasMany(e => e.Documento)
                .WithOptional(e => e.TipoDocumento)
                .HasForeignKey(e => e.TipoDocumento_Id);

            modelBuilder.Entity<TipoProceso>()
                .Property(e => e.Tpo_Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<TipoProceso>()
                .HasMany(e => e.ProcesoDocumento)
                .WithRequired(e => e.TipoProceso)
                .HasForeignKey(e => e.TipoProceso_Id)
                .WillCascadeOnDelete(false);
        }
    }
}
