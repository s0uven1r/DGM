using Microsoft.EntityFrameworkCore.Migrations;

namespace Auth.Infrastructure.Persistence.Migrations.IdentityServer
{
    public partial class IdentityServerAppDBSetMenuClaimDbMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a269675e-1d38-4dc1-99ca-e8f02b0e3562");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c1298372-701d-46df-aa3b-427e5b6d0081", "62f0c393-47f8-450c-aab9-7ee3702c8440", "consumer", "CONSUMER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c1298372-701d-46df-aa3b-427e5b6d0081");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a269675e-1d38-4dc1-99ca-e8f02b0e3562", "7b292aba-b2a3-4909-a79d-a9aba6db9ac3", "consumer", "CONSUMER" });
        }
    }
}
