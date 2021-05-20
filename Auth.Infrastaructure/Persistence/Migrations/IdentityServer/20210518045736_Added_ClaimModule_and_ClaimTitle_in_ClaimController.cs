using Microsoft.EntityFrameworkCore.Migrations;

namespace Auth.Infrastructure.Persistence.Migrations.IdentityServer
{
    public partial class Added_ClaimModule_and_ClaimTitle_in_ClaimController : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClaimModule",
                table: "ControllerClaim",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClaimTitle",
                table: "ControllerClaim",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClaimModule",
                table: "ControllerClaim");

            migrationBuilder.DropColumn(
                name: "ClaimTitle",
                table: "ControllerClaim");
        }
    }
}
