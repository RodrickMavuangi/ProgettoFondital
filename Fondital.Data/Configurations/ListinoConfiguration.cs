using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fondital.Shared.Models;
using Fondital.Shared.Models.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fondital.Data.Configurations
{
    public class ListinoConfiguration : IEntityTypeConfiguration<Listino>
    {
        public void Configure(EntityTypeBuilder<Listino> builder )
        {
            builder.HasKey(a => a.Id);
            builder.Property(m => m.Id).UseIdentityColumn();

            builder.Property(m => m.Raggruppamento).IsRequired();
            builder.Property(m => m.Valore).IsRequired();

            builder.HasOne(m => m.ServicePartner).WithMany(s => s.Listini).IsRequired();
            builder.HasOne(m => m.VoceCosto).WithMany(v => v.Listini).IsRequired();

            builder.ToTable("Listini");
        }
    }
}
