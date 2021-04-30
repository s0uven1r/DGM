using Microsoft.EntityFrameworkCore.Migrations;

namespace Auth.Infrastructure.Persistence.Migrations.IdentityServer
{
    public partial class IdentityServerAppClaimMenuDbMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dc08aa63-be1c-4bdc-86f3-82acbbcda294");

            migrationBuilder.DropColumn(
                name: "ClaimType",
                table: "AspNetUserClaims");

            migrationBuilder.DropColumn(
                name: "ClaimValue",
                table: "AspNetUserClaims");

            migrationBuilder.DropColumn(
                name: "ClaimType",
                table: "AspNetRoleClaims");

            migrationBuilder.DropColumn(
                name: "ClaimValue",
                table: "AspNetRoleClaims");

            migrationBuilder.AddColumn<string>(
                name: "ClaimId",
                table: "AspNetUserClaims",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClaimId",
                table: "AspNetRoleClaims",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ControllerClaim",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControllerClaim", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MenuControl",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Alias = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Class = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FaClass = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RouteUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ParentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Rank = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuControl", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuControl_ControllerClaim_ClaimId",
                        column: x => x.ClaimId,
                        principalTable: "ControllerClaim",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MenuControl_MenuControl_ParentId",
                        column: x => x.ParentId,
                        principalTable: "MenuControl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6635e876-3043-43ee-af04-4eec5b6fd447", "46c1bd39-9471-48e4-8c24-471bb70aee82", "consumer", "CONSUMER" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_ClaimId",
                table: "AspNetUserClaims",
                column: "ClaimId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_ClaimId",
                table: "AspNetRoleClaims",
                column: "ClaimId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuControl_ClaimId",
                table: "MenuControl",
                column: "ClaimId",
                unique: true,
                filter: "[ClaimId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_MenuControl_ParentId",
                table: "MenuControl",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_ControllerClaim_ClaimId",
                table: "AspNetRoleClaims",
                column: "ClaimId",
                principalTable: "ControllerClaim",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_ControllerClaim_ClaimId",
                table: "AspNetUserClaims",
                column: "ClaimId",
                principalTable: "ControllerClaim",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_ControllerClaim_ClaimId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_ControllerClaim_ClaimId",
                table: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "MenuControl");

            migrationBuilder.DropTable(
                name: "ControllerClaim");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserClaims_ClaimId",
                table: "AspNetUserClaims");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoleClaims_ClaimId",
                table: "AspNetRoleClaims");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6635e876-3043-43ee-af04-4eec5b6fd447");

            migrationBuilder.DropColumn(
                name: "ClaimId",
                table: "AspNetUserClaims");

            migrationBuilder.DropColumn(
                name: "ClaimId",
                table: "AspNetRoleClaims");

            migrationBuilder.AddColumn<string>(
                name: "ClaimType",
                table: "AspNetUserClaims",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClaimValue",
                table: "AspNetUserClaims",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClaimType",
                table: "AspNetRoleClaims",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClaimValue",
                table: "AspNetRoleClaims",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "dc08aa63-be1c-4bdc-86f3-82acbbcda294", "64ba759a-345a-4599-a521-1ba5d17d2ac4", "consumer", "CONSUMER" });
        }
    }
}
