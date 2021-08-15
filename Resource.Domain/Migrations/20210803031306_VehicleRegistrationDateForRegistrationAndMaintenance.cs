using Microsoft.EntityFrameworkCore.Migrations;

namespace Resource.Domain.Migrations
{
    public partial class VehicleRegistrationDateForRegistrationAndMaintenance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Manufacturer",
                table: "VehicleMaintenaceDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegisterDateEN",
                table: "VehicleMaintenaceDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegisterDateNP",
                table: "VehicleMaintenaceDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Manufacturer",
                table: "VehicleDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegisterDateEN",
                table: "VehicleDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegisterDateNP",
                table: "VehicleDetails",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Manufacturer",
                table: "VehicleMaintenaceDetails");

            migrationBuilder.DropColumn(
                name: "RegisterDateEN",
                table: "VehicleMaintenaceDetails");

            migrationBuilder.DropColumn(
                name: "RegisterDateNP",
                table: "VehicleMaintenaceDetails");

            migrationBuilder.DropColumn(
                name: "Manufacturer",
                table: "VehicleDetails");

            migrationBuilder.DropColumn(
                name: "RegisterDateEN",
                table: "VehicleDetails");

            migrationBuilder.DropColumn(
                name: "RegisterDateNP",
                table: "VehicleDetails");
        }
    }
}
