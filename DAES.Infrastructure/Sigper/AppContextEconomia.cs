using DAES.Model.Sigper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAES.Infrastructure.Sigper
{
    public class AppContextEconomia : DbContext
    {
        public AppContextEconomia() : base("name=SIGPEREconomia")
        {
            Configuration.AutoDetectChangesEnabled = false;
            Configuration.ValidateOnSaveEnabled = false;
        }
        public virtual DbSet<PEDATPER> PEDATPER { get; set; }
        public virtual DbSet<PeDatLab> PeDatLab { get; set; }
        public virtual DbSet<PLUNILAB> PLUNILAB { get; set; }
        public virtual DbSet<PEFERJEFAF> PEFERJEFAF { get; set; }
        public virtual DbSet<PEFERJEFAJ> PEFERJEFAJ { get; set; }
        public virtual DbSet<PECARGOS> PECARGOS { get; set; }
        public virtual DbSet<DGREGIONES> DGREGIONES { get; set; }
        public virtual DbSet<DGCOMUNAS> DGCOMUNAS { get; set; }
        public virtual DbSet<DGESCALAFONES> DGESCALAFONES { get; set; }
        public virtual DbSet<DGESTAMENTOS> DGESTAMENTOS { get; set; }
        public virtual DbSet<ReContra> ReContra { get; set; }
        public virtual DbSet<REPYT> REPYT { get; set; }
        public virtual DbSet<DGCONTRATOS> DGCONTRATOS { get; set; }
        //public virtual DbSet<SIGPERTipoVehiculo> SIGPERTipoVehiculo { get; set; }
        //public virtual DbSet<CentroCosto> CentroCosto { get; set; }
        //public virtual DbSet<TipoAsignacion> TipoAsignacion { get; set; }
        //public virtual DbSet<TipoCapitulo> TipoCapitulo { get; set; }
        //public virtual DbSet<TipoItem> TipoItem { get; set; }
        //public virtual DbSet<TipoSubAsignacion> TipoSubAsignacion { get; set; }
        //public virtual DbSet<TipoSubTitulo> TipoSubTitulo { get; set; }
        //public virtual DbSet<RegionComunaContraloria> RegionComunaContraloria { get; set; }
        public virtual DbSet<RePagHisDet> RePagHisDet { get; set; }
        public virtual DbSet<LREMREP1Level1> LREMREP1Level1 { get; set; }
        public virtual DbSet<MARCACIONES> MARCACIONES { get; set; }
       // public virtual DbSet<Localidad> Localidad { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<AppContextEconomia>(null);
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
    }
}
