using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fondital.Data.Migrations
{
    public partial class CampiUserPw : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "pw_lastChanged",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "pw_mustChange",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "pw_lastChanged",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "pw_mustChange",
                table: "AspNetUsers");
        }
    }
}
