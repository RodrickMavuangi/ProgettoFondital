using Microsoft.EntityFrameworkCore.Migrations;

namespace Fondital.Data.Migrations
{
    public partial class AddServicePartnerRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_ServicePartner_ServicePartnerId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "ServicePartnerId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_ServicePartner_ServicePartnerId",
                table: "AspNetUsers",
                column: "ServicePartnerId",
                principalTable: "ServicePartner",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_ServicePartner_ServicePartnerId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "ServicePartnerId",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_ServicePartner_ServicePartnerId",
                table: "AspNetUsers",
                column: "ServicePartnerId",
                principalTable: "ServicePartner",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
