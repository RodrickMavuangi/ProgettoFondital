using Microsoft.EntityFrameworkCore.Migrations;

namespace Fondital.Data.Migrations
{
    public partial class AnagraficaListino : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Listini",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServicePartnerId = table.Column<int>(type: "int", nullable: false),
                    VoceCostoId = table.Column<int>(type: "int", nullable: false),
                    Raggruppamento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Valore = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Listini", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Listini_ServicePartner_ServicePartnerId",
                        column: x => x.ServicePartnerId,
                        principalTable: "ServicePartner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Listini_VociCosto_VoceCostoId",
                        column: x => x.VoceCostoId,
                        principalTable: "VociCosto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Listini_ServicePartnerId",
                table: "Listini",
                column: "ServicePartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Listini_VoceCostoId",
                table: "Listini",
                column: "VoceCostoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Listini");
        }
    }
}
