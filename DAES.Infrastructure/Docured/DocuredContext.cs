namespace DAES.Infrastructure
{
    using System.Data.Entity;
    using DAES.Model.Docured;

    public partial class DocuredContext : DbContext
    {
        public DocuredContext() : base("name=Docured")
        {
        }

        public virtual DbSet<Documentos> Documentos { get; set; }
        public virtual DbSet<Fichas> Fichas { get; set; }
        public virtual DbSet<Tramites> Tramites { get; set; }
        public virtual DbSet<Empresa> Empresa { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Documentos>()
                .Property(e => e.Imagen)
                .IsUnicode(false);

            modelBuilder.Entity<Documentos>()
                .Property(e => e.Respuesta)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Documentos>()
                .Property(e => e.Terminado_Fin_Ciclo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Documentos>()
                .Property(e => e.Observaciones)
                .IsUnicode(false);

            modelBuilder.Entity<Documentos>()
                .Property(e => e.Campo1)
                .IsUnicode(false);

            modelBuilder.Entity<Documentos>()
                .Property(e => e.Campo2)
                .IsUnicode(false);

            modelBuilder.Entity<Documentos>()
                .Property(e => e.Campo3)
                .IsUnicode(false);

            modelBuilder.Entity<Documentos>()
                .Property(e => e.Campo4)
                .IsUnicode(false);

            modelBuilder.Entity<Documentos>()
                .Property(e => e.Campo5)
                .IsUnicode(false);

            modelBuilder.Entity<Documentos>()
                .Property(e => e.Campo6)
                .IsUnicode(false);

            modelBuilder.Entity<Documentos>()
                .Property(e => e.Campo7)
                .IsUnicode(false);

            modelBuilder.Entity<Documentos>()
                .Property(e => e.Campo8)
                .IsUnicode(false);

            modelBuilder.Entity<Documentos>()
                .Property(e => e.Campo9)
                .IsUnicode(false);

            modelBuilder.Entity<Documentos>()
                .Property(e => e.Campo10)
                .IsUnicode(false);

            modelBuilder.Entity<Documentos>()
                .Property(e => e.Campo11)
                .IsUnicode(false);

            modelBuilder.Entity<Documentos>()
                .Property(e => e.Campo12)
                .IsUnicode(false);

            modelBuilder.Entity<Documentos>()
                .Property(e => e.Fecha_Creacion)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Documentos>()
                .Property(e => e.Texto_Ocr)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Documentos>()
                .Property(e => e.Observaciones1)
                .IsUnicode(false);

            modelBuilder.Entity<Documentos>()
                .HasMany(e => e.Tramites)
                .WithRequired(e => e.Documentos)
                .HasForeignKey(e => new { e.Folio, e.codigo_empresa })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Fichas>()
                .Property(e => e.Nombre_Ficha)
                .IsFixedLength()
                .IsUnicode(false);
            
            modelBuilder.Entity<Tramites>()
                .Property(e => e.Usuario)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Tramites>()
                .Property(e => e.Fecha)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Tramites>()
                .Property(e => e.Hora)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Tramites>()
                .Property(e => e.Nota)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Tramites>()
                .Property(e => e.Usuario_Destino)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Tramites>()
                .Property(e => e.Observaciones)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Empresa>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Empresa>()
                .Property(e => e.direccion)
                .IsUnicode(false);

            modelBuilder.Entity<Empresa>()
                .Property(e => e.telefono)
                .IsUnicode(false);

            modelBuilder.Entity<Empresa>()
                .Property(e => e.comuna)
                .IsUnicode(false);

            modelBuilder.Entity<Empresa>()
                .Property(e => e.ciudad)
                .IsUnicode(false);

            modelBuilder.Entity<Empresa>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<Empresa>()
                .Property(e => e.Path_Imagenes)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Empresa>()
                .Property(e => e.Path_Internet)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Empresa>()
                .Property(e => e.Servidor_Web)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Empresa>()
                .Property(e => e.Servidor_SMTP)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Empresa>()
                .Property(e => e.Servidor_FTP)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Empresa>()
                .Property(e => e.Puerto_FTP)
                .IsUnicode(false);

            modelBuilder.Entity<Empresa>()
                .Property(e => e.Usuario_FTP)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Empresa>()
                .Property(e => e.Clave_FTP)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Empresa>()
                .Property(e => e.Puerto_SMTP)
                .IsUnicode(false);
        }
    }
}
