using Fondital.Shared.Models.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fondital.Data.Configurations
{
    public class RuoloConfiguration : IEntityTypeConfiguration<Ruolo>
    {
        public void Configure(EntityTypeBuilder<Ruolo> builder )
        {
            builder.HasKey(a => a.Id);
            builder.Property(m => m.Id).UseIdentityColumn();

            builder.Property(u => u.Name).IsRequired();
            builder.HasIndex(u => u.Name).IsUnique();

            builder.ToTable("AspNetRoles");
        }
    }
}