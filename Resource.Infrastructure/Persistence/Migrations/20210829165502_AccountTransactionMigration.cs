using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Resource.Infrastructure.Persistence.Migrations
{
    public partial class AccountTransactionMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClosingBalances",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccountNumber = table.Column<decimal>(type: "decimal(15,4)", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(15,4)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClosingBalances", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(15,4)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(15,4)", nullable: false),
                    NetAmount = table.Column<decimal>(type: "decimal(15,4)", nullable: false),
                    DueAmount = table.Column<decimal>(type: "decimal(15,4)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VehicleNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsVehicle = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransactionDetails",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AmountDebit = table.Column<decimal>(type: "decimal(15,4)", nullable: false),
                    AmountCredit = table.Column<decimal>(type: "decimal(15,4)", nullable: false),
                    ReferenceAccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactionId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionDetails_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransactionDetails_TransactionId",
                table: "TransactionDetails",
                column: "TransactionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClosingBalances");

            migrationBuilder.DropTable(
                name: "TransactionDetails");

            migrationBuilder.DropTable(
                name: "Transactions");
        }
    }
}
