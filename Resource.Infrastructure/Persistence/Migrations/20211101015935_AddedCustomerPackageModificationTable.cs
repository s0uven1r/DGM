using Microsoft.EntityFrameworkCore.Migrations;

namespace Resource.Infrastructure.Persistence.Migrations
{
    public partial class AddedCustomerPackageModificationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "CustomerPayments",
                newName: "PaymentGateway");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PaymentGateway",
                table: "CustomerPayments",
                newName: "Status");
        }
    }
}
