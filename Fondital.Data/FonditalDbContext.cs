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

namespace Fondital.Data
{
    public class FonditalDbContext : IdentityDbContext<Utente, Ruolo, int>
    {
        public FonditalDbContext(DbContextOptions<FonditalDbContext> options) : base(options)
        { }

        public DbSet<Utente> Utenti { get; set; }
        public DbSet<Trace> Traces { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UtenteConfiguration());
            builder.ApplyConfiguration(new TraceConfiguration());
        }
    }
}
