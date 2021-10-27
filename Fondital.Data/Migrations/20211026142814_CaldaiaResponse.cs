using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fondital.Data.Migrations
{
    public partial class CaldaiaResponse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ModelloCaldaia",
                table: "Rapporti",
                newName: "Model");

            migrationBuilder.AddColumn<string>(
                name: "BrandCode",
                table: "Rapporti",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BrandDesc",
                table: "Rapporti",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GroupCode",
                table: "Rapporti",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GroupDesc",
                table: "Rapporti",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Manufacturer",
                table: "Rapporti",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ManufacturingDate",
                table: "Rapporti",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BrandCode",
                table: "Rapporti");

            migrationBuilder.DropColumn(
                name: "BrandDesc",
                table: "Rapporti");

            migrationBuilder.DropColumn(
                name: "GroupCode",
                table: "Rapporti");

            migrationBuilder.DropColumn(
                name: "GroupDesc",
                table: "Rapporti");

            migrationBuilder.DropColumn(
                name: "Manufacturer",
                table: "Rapporti");

            migrationBuilder.DropColumn(
                name: "ManufacturingDate",
                table: "Rapporti");

            migrationBuilder.RenameColumn(
                name: "Model",
                table: "Rapporti",
                newName: "ModelloCaldaia");
        }
    }
}
