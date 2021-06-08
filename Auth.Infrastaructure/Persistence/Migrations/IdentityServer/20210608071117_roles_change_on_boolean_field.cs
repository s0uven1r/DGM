using Microsoft.EntityFrameworkCore.Migrations;

namespace Auth.Infrastructure.Persistence.Migrations.IdentityServer
{
    public partial class roles_change_on_boolean_field : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "AspNetRoles");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "AspNetRoles",
                newName: "IsSuperAdmin");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsSuperAdmin",
                table: "AspNetRoles",
                newName: "IsDeleted");

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "AspNetRoles",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
