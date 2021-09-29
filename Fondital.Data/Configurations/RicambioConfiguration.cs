using Fondital.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fondital.Data.Configurations
{
    class RicambioConfiguration : IEntityTypeConfiguration<Ricambio>
    {
        public void Configure(EntityTypeBuilder<Ricambio> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(m => m.Id).UseIdentityColumn();

            builder.Property(m => m.Quantita);
            builder.Property(m => m.Costo);
            builder.Property(m => m.Descrizione);

            builder.HasOne(m => m.Rapporto).WithMany(s => s.Ricambi);

            builder.ToTable("Ricambi");
        }
    }
}
