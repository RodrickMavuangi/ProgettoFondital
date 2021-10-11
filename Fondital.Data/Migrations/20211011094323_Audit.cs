using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fondital.Data.Migrations
{
    public partial class Audit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DataRapporto",
                table: "Rapporti",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "AuditRapporti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UtenteId = table.Column<int>(type: "int", nullable: true),
                    RapportoId = table.Column<int>(type: "int", nullable: true),
                    StatoIniziale = table.Column<int>(type: "int", nullable: false),
                    DataIntervento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditRapporti", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditRapporti_AspNetUsers_UtenteId",
                        column: x => x.UtenteId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuditRapporti_Rapporti_RapportoId",
                        column: x => x.RapportoId,
                        principalTable: "Rapporti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuditRapporti_RapportoId",
                table: "AuditRapporti",
                column: "RapportoId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditRapporti_UtenteId",
                table: "AuditRapporti",
                column: "UtenteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditRapporti");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataRapporto",
                table: "Rapporti",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
