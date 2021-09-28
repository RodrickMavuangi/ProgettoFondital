using Microsoft.EntityFrameworkCore.Migrations;

namespace Fondital.Data.Migrations
{
    public partial class ModifiedRapportoVoceCosto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ricambio_Rapporti_RapportoId",
                table: "Ricambio");

            migrationBuilder.DropTable(
                name: "RapportoVoceCosto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ricambio",
                table: "Ricambio");

            migrationBuilder.RenameTable(
                name: "Ricambio",
                newName: "Ricambi");

            migrationBuilder.RenameIndex(
                name: "IX_Ricambio_RapportoId",
                table: "Ricambi",
                newName: "IX_Ricambi_RapportoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ricambi",
                table: "Ricambi",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "RapportiVociCosto",
                columns: table => new
                {
                    RapportoId = table.Column<int>(type: "int", nullable: false),
                    VoceCostoId = table.Column<int>(type: "int", nullable: false),
                    Quantita = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RapportiVociCosto", x => new { x.RapportoId, x.VoceCostoId });
                    table.ForeignKey(
                        name: "FK_RapportiVociCosto_Rapporti_RapportoId",
                        column: x => x.RapportoId,
                        principalTable: "Rapporti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RapportiVociCosto_VociCosto_VoceCostoId",
                        column: x => x.VoceCostoId,
                        principalTable: "VociCosto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RapportiVociCosto_VoceCostoId",
                table: "RapportiVociCosto",
                column: "VoceCostoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ricambi_Rapporti_RapportoId",
                table: "Ricambi",
                column: "RapportoId",
                principalTable: "Rapporti",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ricambi_Rapporti_RapportoId",
                table: "Ricambi");

            migrationBuilder.DropTable(
                name: "RapportiVociCosto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ricambi",
                table: "Ricambi");

            migrationBuilder.RenameTable(
                name: "Ricambi",
                newName: "Ricambio");

            migrationBuilder.RenameIndex(
                name: "IX_Ricambi_RapportoId",
                table: "Ricambio",
                newName: "IX_Ricambio_RapportoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ricambio",
                table: "Ricambio",
                column: "Id");

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

            migrationBuilder.CreateIndex(
                name: "IX_RapportoVoceCosto_VociDiCostoId",
                table: "RapportoVoceCosto",
                column: "VociDiCostoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ricambio_Rapporti_RapportoId",
                table: "Ricambio",
                column: "RapportoId",
                principalTable: "Rapporti",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
