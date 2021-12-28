using Fondital.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fondital.Data.Configurations
{
    public class AuditRapportoConfiguration : IEntityTypeConfiguration<AuditRapporto>
    {
        public void Configure(EntityTypeBuilder<AuditRapporto> builder )
        {
            builder.HasKey(a => a.Id);
            builder.Property(m => m.Id).UseIdentityColumn();

            builder.Property(m => m.DataIntervento).IsRequired();
            builder.Property(m => m.StatoIniziale).IsRequired();

            builder.HasOne(m => m.Utente).WithMany(s => s.AuditRapporti);
            builder.HasOne(m => m.Rapporto).WithMany(s => s.AuditRapporti);

            builder.ToTable("AuditRapporti");
        }
    }
}