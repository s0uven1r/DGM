using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Resource.Infrastructure.Persistence.Migrations
{
    public partial class AddedIndividualShiftTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IndividualShifts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ShiftId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PackageId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    VehicleId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrainerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrainingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TrainingDateNp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserAccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndividualShifts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IndividualShifts_Packages_PackageId",
                        column: x => x.PackageId,
                        principalTable: "Packages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IndividualShifts_Shifts_ShiftId",
                        column: x => x.ShiftId,
                        principalTable: "Shifts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IndividualShifts_PackageId",
                table: "IndividualShifts",
                column: "PackageId");

            migrationBuilder.CreateIndex(
                name: "IX_IndividualShifts_ShiftId",
                table: "IndividualShifts",
                column: "ShiftId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IndividualShifts");
        }
    }
}
