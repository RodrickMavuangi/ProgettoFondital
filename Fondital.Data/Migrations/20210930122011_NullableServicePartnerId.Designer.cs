﻿// <auto-generated />
using System;
using Fondital.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Fondital.Data.Migrations
{
    [DbContext(typeof(FonditalDbContext))]
    [Migration("20210930122011_NullableServicePartnerId")]
    partial class NullableServicePartnerId
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Fondital.Shared.Models.Auth.Ruolo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Fondital.Shared.Models.Auth.Utente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Cognome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("IsAbilitato")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Pw_LastChanged")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Pw_MustChange")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ServicePartnerId")
                        .HasColumnType("int");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("ServicePartnerId");

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Fondital.Shared.Models.Configurazione", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Chiave")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Valore")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Configurazioni");
                });

            modelBuilder.Entity("Fondital.Shared.Models.Difetto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsAbilitato")
                        .HasColumnType("bit");

                    b.Property<string>("NomeItaliano")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("NomeRusso")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("NomeItaliano")
                        .IsUnique();

                    b.HasIndex("NomeRusso")
                        .IsUnique();

                    b.ToTable("Difetti");
                });

            modelBuilder.Entity("Fondital.Shared.Models.Lavorazione", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsAbilitato")
                        .HasColumnType("bit");

                    b.Property<string>("NomeItaliano")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomeRusso")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Lavorazioni");
                });

            modelBuilder.Entity("Fondital.Shared.Models.Listino", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Raggruppamento")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ServicePartnerId")
                        .HasColumnType("int");

                    b.Property<int>("Valore")
                        .HasColumnType("int");

                    b.Property<int>("VoceCostoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ServicePartnerId");

                    b.HasIndex("VoceCostoId");

                    b.ToTable("Listini");
                });

            modelBuilder.Entity("Fondital.Shared.Models.Rapporto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("DataIntervento")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataRapporto")
                        .HasColumnType("datetime2");

                    b.Property<string>("MotivoIntervento")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomeTecnico")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Stato")
                        .HasColumnType("int");

                    b.Property<string>("TipoLavoro")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UtenteId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UtenteId");

                    b.ToTable("Rapporti");
                });

            modelBuilder.Entity("Fondital.Shared.Models.RapportoVoceCosto", b =>
                {
                    b.Property<int>("RapportoId")
                        .HasColumnType("int");

                    b.Property<int>("VoceCostoId")
                        .HasColumnType("int");

                    b.Property<int>("Quantita")
                        .HasColumnType("int");

                    b.HasKey("RapportoId", "VoceCostoId");

                    b.HasIndex("VoceCostoId");

                    b.ToTable("RapportiVociCosto");
                });

            modelBuilder.Entity("Fondital.Shared.Models.Ricambio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Costo")
                        .HasColumnType("int");

                    b.Property<string>("Descrizione")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantita")
                        .HasColumnType("int");

                    b.Property<int?>("RapportoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RapportoId");

                    b.ToTable("Ricambi");
                });

            modelBuilder.Entity("Fondital.Shared.Models.ServicePartner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CodiceCliente")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CodiceFornitore")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RagioneSociale")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("CodiceCliente")
                        .IsUnique();

                    b.HasIndex("CodiceFornitore")
                        .IsUnique();

                    b.HasIndex("RagioneSociale")
                        .IsUnique();

                    b.ToTable("ServicePartner");
                });

            modelBuilder.Entity("Fondital.Shared.Models.VoceCosto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsAbilitato")
                        .HasColumnType("bit");

                    b.Property<string>("NomeItaliano")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("NomeRusso")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Tipologia")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("NomeItaliano")
                        .IsUnique();

                    b.HasIndex("NomeRusso")
                        .IsUnique();

                    b.ToTable("VociCosto");
                });

            modelBuilder.Entity("IdentityServer4.EntityFramework.Entities.DeviceFlowCodes", b =>
                {
                    b.Property<string>("UserCode")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasMaxLength(50000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("DeviceCode")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime?>("Expiration")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("SessionId")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("SubjectId")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("UserCode");

                    b.HasIndex("DeviceCode")
                        .IsUnique();

                    b.HasIndex("Expiration");

                    b.ToTable("DeviceCodes");
                });

            modelBuilder.Entity("IdentityServer4.EntityFramework.Entities.PersistedGrant", b =>
                {
                    b.Property<string>("Key")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime?>("ConsumedTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasMaxLength(50000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime?>("Expiration")
                        .HasColumnType("datetime2");

                    b.Property<string>("SessionId")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("SubjectId")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Key");

                    b.HasIndex("Expiration");

                    b.HasIndex("SubjectId", "ClientId", "Type");

                    b.HasIndex("SubjectId", "SessionId", "Type");

                    b.ToTable("PersistedGrants");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Fondital.Shared.Models.Auth.Utente", b =>
                {
                    b.HasOne("Fondital.Shared.Models.ServicePartner", "ServicePartner")
                        .WithMany("Utenti")
                        .HasForeignKey("ServicePartnerId");

                    b.Navigation("ServicePartner");
                });

            modelBuilder.Entity("Fondital.Shared.Models.Listino", b =>
                {
                    b.HasOne("Fondital.Shared.Models.ServicePartner", "ServicePartner")
                        .WithMany("Listini")
                        .HasForeignKey("ServicePartnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Fondital.Shared.Models.VoceCosto", "VoceCosto")
                        .WithMany("Listini")
                        .HasForeignKey("VoceCostoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ServicePartner");

                    b.Navigation("VoceCosto");
                });

            modelBuilder.Entity("Fondital.Shared.Models.Rapporto", b =>
                {
                    b.HasOne("Fondital.Shared.Models.Auth.Utente", "Utente")
                        .WithMany("Rapporti")
                        .HasForeignKey("UtenteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Fondital.Shared.Models.Caldaia", "Caldaia", b1 =>
                        {
                            b1.Property<int>("RapportoId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<DateTime?>("DataAvvio")
                                .HasColumnType("datetime2")
                                .HasColumnName("DataAvvioCaldaia");

                            b1.Property<DateTime?>("DataMontaggio")
                                .HasColumnType("datetime2")
                                .HasColumnName("DataMontaggioCaldaia");

                            b1.Property<DateTime?>("DataVendita")
                                .HasColumnType("datetime2")
                                .HasColumnName("DataVenditaCaldaia");

                            b1.Property<string>("DittaPrimoAvvio")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("DittaPrimoAvvioCaldaia");

                            b1.Property<string>("Matricola")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("MatricolaCaldaia");

                            b1.Property<string>("Modello")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("ModelloCaldaia");

                            b1.Property<int>("NumCertificatoTecnico")
                                .HasColumnType("int")
                                .HasColumnName("NumCertificatoTecnicoCaldaia");

                            b1.Property<string>("TecnicoPrimoAvvio")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("TecnicoPrimoAvvioCaldaia");

                            b1.Property<string>("Versione")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("VersioneCaldaia");

                            b1.HasKey("RapportoId");

                            b1.ToTable("Rapporti");

                            b1.WithOwner()
                                .HasForeignKey("RapportoId");
                        });

                    b.OwnsOne("Fondital.Shared.Models.Cliente", "Cliente", b1 =>
                        {
                            b1.Property<int>("RapportoId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("Citta")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("CittaCliente");

                            b1.Property<string>("Cognome")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("CognomeCliente");

                            b1.Property<string>("Email")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("EmailCliente");

                            b1.Property<string>("Nome")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("NomeCliente");

                            b1.Property<string>("NumCivico")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("NumCivicoCliente");

                            b1.Property<string>("NumTelefono")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("NumTelefonoCliente");

                            b1.Property<string>("Via")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("ViaCliente");

                            b1.HasKey("RapportoId");

                            b1.ToTable("Rapporti");

                            b1.WithOwner()
                                .HasForeignKey("RapportoId");
                        });

                    b.Navigation("Caldaia");

                    b.Navigation("Cliente");

                    b.Navigation("Utente");
                });

            modelBuilder.Entity("Fondital.Shared.Models.RapportoVoceCosto", b =>
                {
                    b.HasOne("Fondital.Shared.Models.Rapporto", "Rapporto")
                        .WithMany("VociCostoRapporti")
                        .HasForeignKey("RapportoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Fondital.Shared.Models.VoceCosto", "VoceCosto")
                        .WithMany("VociCostoRapporti")
                        .HasForeignKey("VoceCostoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rapporto");

                    b.Navigation("VoceCosto");
                });

            modelBuilder.Entity("Fondital.Shared.Models.Ricambio", b =>
                {
                    b.HasOne("Fondital.Shared.Models.Rapporto", "Rapporto")
                        .WithMany("Ricambi")
                        .HasForeignKey("RapportoId");

                    b.Navigation("Rapporto");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("Fondital.Shared.Models.Auth.Ruolo", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("Fondital.Shared.Models.Auth.Utente", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("Fondital.Shared.Models.Auth.Utente", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("Fondital.Shared.Models.Auth.Ruolo", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Fondital.Shared.Models.Auth.Utente", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("Fondital.Shared.Models.Auth.Utente", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Fondital.Shared.Models.Auth.Utente", b =>
                {
                    b.Navigation("Rapporti");
                });

            modelBuilder.Entity("Fondital.Shared.Models.Rapporto", b =>
                {
                    b.Navigation("Ricambi");

                    b.Navigation("VociCostoRapporti");
                });

            modelBuilder.Entity("Fondital.Shared.Models.ServicePartner", b =>
                {
                    b.Navigation("Listini");

                    b.Navigation("Utenti");
                });

            modelBuilder.Entity("Fondital.Shared.Models.VoceCosto", b =>
                {
                    b.Navigation("Listini");

                    b.Navigation("VociCostoRapporti");
                });
#pragma warning restore 612, 618
        }
    }
}
