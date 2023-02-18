using Microsoft.EntityFrameworkCore.Migrations;

namespace eConLab.Migrations
{
    public partial class ModifyRequestTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_RequestInspectionTests_InspectionTestId",
                table: "RequestInspectionTests",
                column: "InspectionTestId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestInspectionTests_InspectionTests_InspectionTestId",
                table: "RequestInspectionTests",
                column: "InspectionTestId",
                principalTable: "InspectionTests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestInspectionTests_InspectionTests_InspectionTestId",
                table: "RequestInspectionTests");

            migrationBuilder.DropIndex(
                name: "IX_RequestInspectionTests_InspectionTestId",
                table: "RequestInspectionTests");
        }
    }
}
