using Microsoft.EntityFrameworkCore.Migrations;

namespace Auth.Infrastructure.Persistence.Migrations.IdentityServer
{
    public partial class AppRoleandAppUserColumnUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastUpdated",
                table: "AspNetUsers",
                newName: "LastUpdatedBy");

            migrationBuilder.RenameColumn(
                name: "LastUpdated",
                table: "AspNetRoles",
                newName: "LastUpdatedBy");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastUpdatedBy",
                table: "AspNetUsers",
                newName: "LastUpdated");

            migrationBuilder.RenameColumn(
                name: "LastUpdatedBy",
                table: "AspNetRoles",
                newName: "LastUpdated");
        }
    }
}
