using Microsoft.EntityFrameworkCore.Migrations;

namespace Resource.Infrastructure.Persistence.Migrations
{
    public partial class UpdatedPackageForShiftFrequency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShiftFrequencyId",
                table: "Packages",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Packages_ShiftFrequencyId",
                table: "Packages",
                column: "ShiftFrequencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Packages_ShiftFrequencies_ShiftFrequencyId",
                table: "Packages",
                column: "ShiftFrequencyId",
                principalTable: "ShiftFrequencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Packages_ShiftFrequencies_ShiftFrequencyId",
                table: "Packages");

            migrationBuilder.DropIndex(
                name: "IX_Packages_ShiftFrequencyId",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "ShiftFrequencyId",
                table: "Packages");
        }
    }
}
