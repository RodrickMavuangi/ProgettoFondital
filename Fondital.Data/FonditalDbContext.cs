using Fondital.Data.Configurations;
using Fondital.Shared.Models;
using Fondital.Shared.Models.Auth;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Extensions;
using IdentityServer4.EntityFramework.Interfaces;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Fondital.Data
{
    public class FonditalDbContext : IdentityDbContext<Utente, Ruolo, int>, IPersistedGrantDbContext
    {
        private readonly IOptions<OperationalStoreOptions> _operationalStoreOptions;

        public FonditalDbContext(DbContextOptions<FonditalDbContext> options, IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options)
        {
            _operationalStoreOptions = operationalStoreOptions;
        }

        public DbSet<Utente> Utenti { get; set; }
        public DbSet<ServicePartner> ServicePartners { get; set; }
        public DbSet<Configurazione> Configurazioni { get; set; }
        public DbSet<Difetto> Difetti { get; set; }
        public DbSet<VoceCosto> VociCosto { get; set; }
        public DbSet<Listino> Listini { get; set; }
        public DbSet<Lavorazione> Lavorazioni { get; set; }
        public DbSet<Rapporto> Rapporti { get; set; }
        public DbSet<RapportoVoceCosto> RapportiVociCosto { get; set; }

        public DbSet<PersistedGrant> PersistedGrants { get; set; }
        public DbSet<DeviceFlowCodes> DeviceFlowCodes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(UtenteConfiguration)));

            builder.ConfigurePersistedGrantContext(_operationalStoreOptions.Value);

            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        Task<int> IPersistedGrantDbContext.SaveChangesAsync() => base.SaveChangesAsync();
    }
}