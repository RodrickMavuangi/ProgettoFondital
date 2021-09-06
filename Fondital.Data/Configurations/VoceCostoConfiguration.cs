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
    public class VoceCostoConfiguration : IEntityTypeConfiguration<VoceCosto>
    {
        public void Configure(EntityTypeBuilder<VoceCosto> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(m => m.Id).UseIdentityColumn();

            builder.Property(m => m.NomeItaliano).IsRequired();
            builder.HasIndex(m => m.NomeItaliano).IsUnique();
            builder.Property(m => m.NomeRusso).IsRequired();
            builder.HasIndex(m => m.NomeRusso).IsUnique();
            builder.Property(m => m.Tipologia).IsRequired();

            builder.ToTable("VociCosto");
        }
    }
}
