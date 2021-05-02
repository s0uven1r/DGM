using Microsoft.EntityFrameworkCore.Migrations;

namespace Auth.Infrastructure.Persistence.Migrations.IdentityServer
{
    public partial class IdentityServerAppMenuRelationDbMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6635e876-3043-43ee-af04-4eec5b6fd447");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a269675e-1d38-4dc1-99ca-e8f02b0e3562", "7b292aba-b2a3-4909-a79d-a9aba6db9ac3", "consumer", "CONSUMER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a269675e-1d38-4dc1-99ca-e8f02b0e3562");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6635e876-3043-43ee-af04-4eec5b6fd447", "46c1bd39-9471-48e4-8c24-471bb70aee82", "consumer", "CONSUMER" });
        }
    }
}
