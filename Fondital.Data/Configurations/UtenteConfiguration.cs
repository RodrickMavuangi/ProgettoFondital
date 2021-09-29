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

            builder.Property(u => u.UserName).IsRequired();
            builder.HasIndex(u => u.UserName).IsUnique();
            builder.Property(u => u.Nome).IsRequired();
            builder.Property(u => u.Cognome).IsRequired();
            builder.Property(u => u.Email).IsRequired();

            builder.HasOne(m => m.ServicePartner).WithMany(s => s.Utenti).IsRequired();

            builder.ToTable("AspNetUsers");
        }
    }
}
