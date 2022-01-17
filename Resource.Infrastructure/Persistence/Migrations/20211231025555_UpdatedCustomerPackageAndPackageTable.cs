using Microsoft.EntityFrameworkCore.Migrations;

namespace Resource.Infrastructure.Persistence.Migrations
{
    public partial class UpdatedCustomerPackageAndPackageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "CustomerPackages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "CustomerPackages",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "CustomerPackages");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "CustomerPackages");
        }
    }
}
