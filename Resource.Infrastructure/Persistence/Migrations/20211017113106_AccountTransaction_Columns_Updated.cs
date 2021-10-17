using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Resource.Infrastructure.Persistence.Migrations
{
    public partial class AccountTransaction_Columns_Updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVehicle",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "VehicleNumber",
                table: "Transactions",
                newName: "Type");

            migrationBuilder.AddColumn<DateTime>(
                name: "TransactionDate",
                table: "Transactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "TransactionDateNP",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TransactionDateNP",
                table: "TransactionDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "TransactionDetails",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransactionDate",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TransactionDateNP",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TransactionDateNP",
                table: "TransactionDetails");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "TransactionDetails");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Transactions",
                newName: "VehicleNumber");

            migrationBuilder.AddColumn<bool>(
                name: "IsVehicle",
                table: "Transactions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
