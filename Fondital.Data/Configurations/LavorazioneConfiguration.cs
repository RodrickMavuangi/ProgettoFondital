using Fondital.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fondital.Data.Configurations
{
    public class LavorazioneConfiguration : IEntityTypeConfiguration<Lavorazione>
    {
        public void Configure(EntityTypeBuilder<Lavorazione> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(m => m.Id).UseIdentityColumn();

            builder.Property(m => m.NomeItaliano).IsRequired();
            builder.Property(m => m.NomeRusso).IsRequired();

            builder.ToTable("Lavorazioni");
        }
    }
}
