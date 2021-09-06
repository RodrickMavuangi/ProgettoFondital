using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fondital.Data.Migrations
{
    public partial class AnagraficaVociCosto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trace");

            migrationBuilder.AlterColumn<string>(
                name: "RagioneSociale",
                table: "ServicePartner",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CodiceFornitore",
                table: "ServicePartner",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CodiceCliente",
                table: "ServicePartner",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "NomeRusso",
                table: "Difetti",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "NomeItaliano",
                table: "Difetti",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.CreateTable(
                name: "VociCosto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeItaliano = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NomeRusso = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Tipologia = table.Column<int>(type: "int", nullable: false),
                    IsAbilitato = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VociCosto", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServicePartner_CodiceCliente",
                table: "ServicePartner",
                column: "CodiceCliente",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServicePartner_CodiceFornitore",
                table: "ServicePartner",
                column: "CodiceFornitore",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServicePartner_RagioneSociale",
                table: "ServicePartner",
                column: "RagioneSociale",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Difetti_NomeItaliano",
                table: "Difetti",
                column: "NomeItaliano",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Difetti_NomeRusso",
                table: "Difetti",
                column: "NomeRusso",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserName",
                table: "AspNetUsers",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VociCosto_NomeItaliano",
                table: "VociCosto",
                column: "NomeItaliano",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VociCosto_NomeRusso",
                table: "VociCosto",
                column: "NomeRusso",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VociCosto");

            migrationBuilder.DropIndex(
                name: "IX_ServicePartner_CodiceCliente",
                table: "ServicePartner");

            migrationBuilder.DropIndex(
                name: "IX_ServicePartner_CodiceFornitore",
                table: "ServicePartner");

            migrationBuilder.DropIndex(
                name: "IX_ServicePartner_RagioneSociale",
                table: "ServicePartner");

            migrationBuilder.DropIndex(
                name: "IX_Difetti_NomeItaliano",
                table: "Difetti");

            migrationBuilder.DropIndex(
                name: "IX_Difetti_NomeRusso",
                table: "Difetti");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserName",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "RagioneSociale",
                table: "ServicePartner",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "CodiceFornitore",
                table: "ServicePartner",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "CodiceCliente",
                table: "ServicePartner",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "NomeRusso",
                table: "Difetti",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "NomeItaliano",
                table: "Difetti",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Trace",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descrizione = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Rapportino_Id = table.Column<int>(type: "int", nullable: false),
                    Tipologia = table.Column<int>(type: "int", nullable: false),
                    UtenteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trace", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trace_AspNetUsers_UtenteId",
                        column: x => x.UtenteId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trace_UtenteId",
                table: "Trace",
                column: "UtenteId");
        }
    }
}
