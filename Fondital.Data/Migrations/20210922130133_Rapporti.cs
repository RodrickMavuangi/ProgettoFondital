using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fondital.Data.Migrations
{
    public partial class Rapporti : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rapporti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UtenteId = table.Column<int>(type: "int", nullable: false),
                    Stato = table.Column<int>(type: "int", nullable: false),
                    DataRapporto = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NomeCliente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CognomeCliente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CittaCliente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ViaCliente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumCivicoCliente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumTelefonoCliente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailCliente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MatricolaCaldaia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModelloCaldaia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VersioneCaldaia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataVenditaCaldaia = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataMontaggioCaldaia = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataAvvioCaldaia = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TecnicoPrimoAvvioCaldaia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumCertificatoTecnicoCaldaia = table.Column<int>(type: "int", nullable: true),
                    DittaPrimoAvvioCaldaia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataIntervento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MotivoIntervento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoLavoro = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rapporti", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rapporti_AspNetUsers_UtenteId",
                        column: x => x.UtenteId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RapportoVoceCosto",
                columns: table => new
                {
                    RapportiId = table.Column<int>(type: "int", nullable: false),
                    VociDiCostoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RapportoVoceCosto", x => new { x.RapportiId, x.VociDiCostoId });
                    table.ForeignKey(
                        name: "FK_RapportoVoceCosto_Rapporti_RapportiId",
                        column: x => x.RapportiId,
                        principalTable: "Rapporti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RapportoVoceCosto_VociCosto_VociDiCostoId",
                        column: x => x.VociDiCostoId,
                        principalTable: "VociCosto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ricambio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantita = table.Column<int>(type: "int", nullable: false),
                    Costo = table.Column<int>(type: "int", nullable: false),
                    Descrizione = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RapportoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ricambio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ricambio_Rapporti_RapportoId",
                        column: x => x.RapportoId,
                        principalTable: "Rapporti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rapporti_UtenteId",
                table: "Rapporti",
                column: "UtenteId");

            migrationBuilder.CreateIndex(
                name: "IX_RapportoVoceCosto_VociDiCostoId",
                table: "RapportoVoceCosto",
                column: "VociDiCostoId");

            migrationBuilder.CreateIndex(
                name: "IX_Ricambio_RapportoId",
                table: "Ricambio",
                column: "RapportoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RapportoVoceCosto");

            migrationBuilder.DropTable(
                name: "Ricambio");

            migrationBuilder.DropTable(
                name: "Rapporti");
        }
    }
}
