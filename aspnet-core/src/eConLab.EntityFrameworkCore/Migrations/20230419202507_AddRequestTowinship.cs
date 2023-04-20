using Microsoft.EntityFrameworkCore.Migrations;

namespace eConLab.Migrations
{
    public partial class AddRequestTowinship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TownShipId",
                table: "Requests",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_TownShipId",
                table: "Requests",
                column: "TownShipId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_TownShips_TownShipId",
                table: "Requests",
                column: "TownShipId",
                principalTable: "TownShips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_TownShips_TownShipId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_TownShipId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "TownShipId",
                table: "Requests");
        }
    }
}
