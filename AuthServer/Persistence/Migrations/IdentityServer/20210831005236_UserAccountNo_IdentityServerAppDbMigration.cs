using Microsoft.EntityFrameworkCore.Migrations;

namespace AuthServer.Persistence.Migrations.IdentityServer
{
    public partial class UserAccountNo_IdentityServerAppDbMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccountNumber",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountNumber",
                table: "AspNetUsers");
        }
    }
}
