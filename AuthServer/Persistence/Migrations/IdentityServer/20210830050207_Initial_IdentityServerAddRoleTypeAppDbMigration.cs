using Microsoft.EntityFrameworkCore.Migrations;

namespace AuthServer.Persistence.Migrations.IdentityServer
{
    public partial class Initial_IdentityServerAddRoleTypeAppDbMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "AspNetRoles",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "AspNetRoles");
        }
    }
}
