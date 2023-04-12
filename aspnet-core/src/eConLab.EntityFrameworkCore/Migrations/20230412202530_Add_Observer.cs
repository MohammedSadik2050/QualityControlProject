using Microsoft.EntityFrameworkCore.Migrations;

namespace eConLab.Migrations
{
    public partial class Add_Observer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ObserverId",
                table: "Requests",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_ObserverId",
                table: "Requests",
                column: "ObserverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Observers_ObserverId",
                table: "Requests",
                column: "ObserverId",
                principalTable: "Observers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Observers_ObserverId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_ObserverId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "ObserverId",
                table: "Requests");
        }
    }
}
