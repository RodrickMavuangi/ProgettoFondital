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

            builder.ToTable("RapportiVociCosto");
        }
    }
}
