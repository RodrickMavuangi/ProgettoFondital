﻿using System;
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
    public class ServicePartnerConfiguration : IEntityTypeConfiguration<ServicePartner>
    {
        public void Configure(EntityTypeBuilder<ServicePartner> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(m => m.Id).UseIdentityColumn();

            builder.Property(m => m.CodiceCliente).IsRequired();
            builder.HasIndex(m => m.CodiceCliente).IsUnique();
            builder.Property(m => m.CodiceFornitore).IsRequired();
            builder.HasIndex(m => m.CodiceFornitore).IsUnique();
            builder.Property(m => m.RagioneSociale).IsRequired();
            builder.HasIndex(m => m.RagioneSociale).IsUnique();

            builder.ToTable("ServicePartner");
        }
    }
}
