using Fondital.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fondital.Data.Configurations
{
    public class RapportoConfiguration : IEntityTypeConfiguration<Rapporto>
    {
        public void Configure(EntityTypeBuilder<Rapporto> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(m => m.Id).UseIdentityColumn();

            builder.ToTable("Rapporti");
        }
    }
}
