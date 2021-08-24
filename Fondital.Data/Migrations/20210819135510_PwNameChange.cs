using Microsoft.EntityFrameworkCore.Migrations;

namespace Fondital.Data.Migrations
{
    public partial class PwNameChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "pw_mustChange",
                table: "AspNetUsers",
                newName: "Pw_MustChange");

            migrationBuilder.RenameColumn(
                name: "pw_lastChanged",
                table: "AspNetUsers",
                newName: "Pw_LastChanged");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Pw_MustChange",
                table: "AspNetUsers",
                newName: "pw_mustChange");

            migrationBuilder.RenameColumn(
                name: "Pw_LastChanged",
                table: "AspNetUsers",
                newName: "pw_lastChanged");
        }
    }
}
