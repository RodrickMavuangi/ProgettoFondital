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

            builder.Property(m => m.Stato);
            builder.Property(m => m.DataIntervento);
            builder.Property(m => m.MotivoIntervento);
            builder.Property(m => m.TipoLavoro);

            builder.OwnsOne(m => m.Cliente,
                            cliente => 
                            {
                                cliente.Property(m => m.Nome).HasColumnName("NomeCliente");
                                cliente.Property(m => m.Cognome).HasColumnName("CognomeCliente");
                                cliente.Property(m => m.Citta).HasColumnName("CittaCliente");
                                cliente.Property(m => m.Via).HasColumnName("ViaCliente");
                                cliente.Property(m => m.NumCivico).HasColumnName("NumCivicoCliente");
                                cliente.Property(m => m.NumTelefono).HasColumnName("NumTelefonoCliente");
                                cliente.Property(m => m.Email).HasColumnName("EmailCliente");
                            });

            builder.OwnsOne(m => m.Caldaia,
                            caldaia =>
                            {
                                caldaia.Property(m => m.Matricola).HasColumnName("MatricolaCaldaia");
                                caldaia.Property(m => m.Modello).HasColumnName("ModelloCaldaia");
                                caldaia.Property(m => m.Versione).HasColumnName("VersioneCaldaia");
                                caldaia.Property(m => m.DataVendita).HasColumnName("DataVenditaCaldaia");
                                caldaia.Property(m => m.DataMontaggio).HasColumnName("DataMontaggioCaldaia");
                                caldaia.Property(m => m.DataAvvio).HasColumnName("DataAvvioCaldaia");
                                caldaia.Property(m => m.TecnicoPrimoAvvio).HasColumnName("TecnicoPrimoAvvioCaldaia");
                                caldaia.Property(m => m.NumCertificatoTecnico).HasColumnName("NumCertificatoTecnicoCaldaia");
                                caldaia.Property(m => m.DittaPrimoAvvio).HasColumnName("DittaPrimoAvvioCaldaia");
                            });            

            builder.HasOne(m => m.Utente).WithMany(s => s.Rapporti).IsRequired();

            builder.HasMany(m => m.VociDiCosto).WithMany(x => x.Rapporti);

            builder.ToTable("Rapporti");
        }
    }
}
