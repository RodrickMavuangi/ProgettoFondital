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
                                caldaia.OwnsOne(m => m.Brand,
                                    brand =>
                                    {
                                        brand.Property(b => b.Code).HasColumnName("BrandCode");
                                        brand.Property(b => b.Desc).HasColumnName("BrandDesc");
                                    });
                                caldaia.OwnsOne(m => m.Group,
                                    group =>
                                    {
                                        group.Property(g => g.Code).HasColumnName("GroupCode");
                                        group.Property(g => g.Desc).HasColumnName("GroupDesc");
                                    });
                                caldaia.Property(m => m.Manufacturer).HasColumnName("Manufacturer");
                                caldaia.Property(m => m.Model).HasColumnName("Model");
                                caldaia.Property(m => m.ManufacturingDate).HasColumnName("ManufacturingDate");
                                caldaia.Property(m => m.Versione).HasColumnName("VersioneCaldaia");
                                caldaia.Property(m => m.DataVendita).HasColumnName("DataVenditaCaldaia");
                                caldaia.Property(m => m.DataMontaggio).HasColumnName("DataMontaggioCaldaia");
                                caldaia.Property(m => m.DataAvvio).HasColumnName("DataAvvioCaldaia");
                                caldaia.Property(m => m.TecnicoPrimoAvvio).HasColumnName("TecnicoPrimoAvvioCaldaia");
                                caldaia.Property(m => m.NumCertificatoTecnico).HasColumnName("NumCertificatoTecnicoCaldaia");
                                caldaia.Property(m => m.DittaPrimoAvvio).HasColumnName("DittaPrimoAvvioCaldaia");
                            });            

            builder.HasOne(m => m.Utente).WithMany(s => s.Rapporti).IsRequired();
        }
    }
}