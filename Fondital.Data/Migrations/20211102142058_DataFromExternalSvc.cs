using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fondital.Data.Migrations
{
    public partial class DataFromExternalSvc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Descrizione",
                table: "Ricambi",
                newName: "RUDescription");

            migrationBuilder.AddColumn<string>(
                name: "BankCode",
                table: "ServicePartner",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BankName",
                table: "ServicePartner",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CC",
                table: "ServicePartner",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CCC",
                table: "ServicePartner",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "ServicePartner",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ContractDate",
                table: "ServicePartner",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContractNr",
                table: "ServicePartner",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "ServicePartner",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HouseNr",
                table: "ServicePartner",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "INN",
                table: "ServicePartner",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KPP",
                table: "ServicePartner",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ManagerName",
                table: "ServicePartner",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ServicePartner",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "ServicePartner",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "ServicePartner",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Region",
                table: "ServicePartner",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StateRegistrationNr",
                table: "ServicePartner",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "ServicePartner",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Costo",
                table: "Ricambi",
                type: "decimal(6,2)",
                precision: 6,
                scale: 2,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Ricambi",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ITDescription",
                table: "Ricambi",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BankCode",
                table: "ServicePartner");

            migrationBuilder.DropColumn(
                name: "BankName",
                table: "ServicePartner");

            migrationBuilder.DropColumn(
                name: "CC",
                table: "ServicePartner");

            migrationBuilder.DropColumn(
                name: "CCC",
                table: "ServicePartner");

            migrationBuilder.DropColumn(
                name: "City",
                table: "ServicePartner");

            migrationBuilder.DropColumn(
                name: "ContractDate",
                table: "ServicePartner");

            migrationBuilder.DropColumn(
                name: "ContractNr",
                table: "ServicePartner");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "ServicePartner");

            migrationBuilder.DropColumn(
                name: "HouseNr",
                table: "ServicePartner");

            migrationBuilder.DropColumn(
                name: "INN",
                table: "ServicePartner");

            migrationBuilder.DropColumn(
                name: "KPP",
                table: "ServicePartner");

            migrationBuilder.DropColumn(
                name: "ManagerName",
                table: "ServicePartner");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ServicePartner");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "ServicePartner");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "ServicePartner");

            migrationBuilder.DropColumn(
                name: "Region",
                table: "ServicePartner");

            migrationBuilder.DropColumn(
                name: "StateRegistrationNr",
                table: "ServicePartner");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "ServicePartner");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Ricambi");

            migrationBuilder.DropColumn(
                name: "ITDescription",
                table: "Ricambi");

            migrationBuilder.RenameColumn(
                name: "RUDescription",
                table: "Ricambi",
                newName: "Descrizione");

            migrationBuilder.AlterColumn<int>(
                name: "Costo",
                table: "Ricambi",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,2)",
                oldPrecision: 6,
                oldScale: 2);
        }
    }
}
