using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using Fondital.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Fondital.Shared.Models;
using Fondital.Shared.Models.Auth;
using Fondital.Data.Configurations;
using IdentityServer4.EntityFramework.Interfaces;
using IdentityServer4.EntityFramework.Entities;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.Extensions;
using IdentityServer4.EntityFramework.Options;
using Microsoft.Extensions.Options;

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
        public DbSet<Trace> Traces { get; set; }
        public DbSet<ServicePartner> ServicePartners { get; set; }
        public DbSet<Configurazione> Configurazioni { get; set; }
        public DbSet<Difetto> Difetti { get; set; }

        public DbSet<PersistedGrant> PersistedGrants { get; set; }
        public DbSet<DeviceFlowCodes> DeviceFlowCodes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UtenteConfiguration());
            builder.ApplyConfiguration(new TraceConfiguration());
            builder.ApplyConfiguration(new ServicePartnerConfiguration());
            builder.ApplyConfiguration(new ConfigurazioneConfiguration());
            builder.ApplyConfiguration(new DifettoConfiguration());

            builder.ConfigurePersistedGrantContext(_operationalStoreOptions.Value);
        }

        Task<int> IPersistedGrantDbContext.SaveChangesAsync() => base.SaveChangesAsync();
    }
}
