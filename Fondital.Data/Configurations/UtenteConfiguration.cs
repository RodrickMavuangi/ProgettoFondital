using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fondital.Shared.Models;
using Fondital.Shared.Models.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fondital.Data.Configurations
{
    public class UtenteConfiguration : IEntityTypeConfiguration<Utente>
    {
        public void Configure(EntityTypeBuilder<Utente> builder )
        {
            builder.HasKey(a => a.Id);
            builder.Property(m => m.Id).UseIdentityColumn();

            builder.Property(m => m.UserName).IsRequired();
            builder.Property(m => m.Nome).IsRequired();
            builder.Property(m => m.Cognome).IsRequired();
            builder.Property(m => m.Email).IsRequired();

            builder.HasOne(m => m.ServicePartner).WithMany(s => s.Utenti).IsRequired();

            builder.ToTable("AspNetUsers");
        }
    }
}
