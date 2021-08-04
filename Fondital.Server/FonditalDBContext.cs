using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using Fondital.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Components;

namespace Fondital.Server
{
    public class FonditalDBContext : DbContext
    {
        public FonditalDBContext(DbContextOptions<FonditalDBContext> options) : base(options)
        { }

        public DbSet<Utente> Utenti { get; set; }
        public DbSet<Trace> Traces { get; set; }

    }
}
