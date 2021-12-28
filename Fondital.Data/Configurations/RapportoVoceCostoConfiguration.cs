using Fondital.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fondital.Data.Configurations
{
    public class RapportoVoceCostoConfiguration : IEntityTypeConfiguration<RapportoVoceCosto>
    {
        public void Configure(EntityTypeBuilder<RapportoVoceCosto> builder)
        {
            builder.HasKey(m => new { m.RapportoId, m.VoceCostoId });

            builder.Property(x => x.Quantita).IsRequired();

            builder.HasOne(rvc => rvc.VoceCosto).WithMany(v => v.RapportiVociCosto).HasForeignKey(rvc => rvc.VoceCostoId);
            builder.HasOne(rvc => rvc.Rapporto).WithMany(r => r.RapportiVociCosto).HasForeignKey(rvc => rvc.RapportoId);

            builder.ToTable("RapportiVociCosto");
        }
    }
}