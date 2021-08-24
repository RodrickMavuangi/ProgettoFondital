using Microsoft.EntityFrameworkCore.Migrations;

namespace Fondital.Data.Migrations
{
    public partial class AddServicePartner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SP_Id",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "ServicePartnerId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ServicePartner",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodiceFornitore = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RagioneSociale = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodiceCliente = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicePartner", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ServicePartnerId",
                table: "AspNetUsers",
                column: "ServicePartnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_ServicePartner_ServicePartnerId",
                table: "AspNetUsers",
                column: "ServicePartnerId",
                principalTable: "ServicePartner",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_ServicePartner_ServicePartnerId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ServicePartner");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ServicePartnerId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ServicePartnerId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "SP_Id",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
