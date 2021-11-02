using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Resource.Infrastructure.Persistence.Migrations
{
    public partial class AddedCustomerPackageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PackageAmount",
                table: "CustomerPayments");

            migrationBuilder.AddColumn<string>(
                name: "CustomerPackageId",
                table: "CustomerPayments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "CustomerPayments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CustomerPackages",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PackageId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CustomerAccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PackageStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PackageStartDateNp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PackageEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PackageEndDateNp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerPackages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerPackages_Packages_PackageId",
                        column: x => x.PackageId,
                        principalTable: "Packages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPayments_CustomerPackageId",
                table: "CustomerPayments",
                column: "CustomerPackageId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPackages_PackageId",
                table: "CustomerPackages",
                column: "PackageId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerPayments_CustomerPackages_CustomerPackageId",
                table: "CustomerPayments",
                column: "CustomerPackageId",
                principalTable: "CustomerPackages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerPayments_CustomerPackages_CustomerPackageId",
                table: "CustomerPayments");

            migrationBuilder.DropTable(
                name: "CustomerPackages");

            migrationBuilder.DropIndex(
                name: "IX_CustomerPayments_CustomerPackageId",
                table: "CustomerPayments");

            migrationBuilder.DropColumn(
                name: "CustomerPackageId",
                table: "CustomerPayments");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "CustomerPayments");

            migrationBuilder.AddColumn<decimal>(
                name: "PackageAmount",
                table: "CustomerPayments",
                type: "decimal(15,4)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
