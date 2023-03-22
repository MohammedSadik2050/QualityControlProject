using Microsoft.EntityFrameworkCore.Migrations;

namespace eConLab.Migrations
{
    public partial class ProjectUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovedByConsultant",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ApprovedByLabProjectManager",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ApprovedBySupervising",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "LabProjectManagerId",
                table: "Projects");

            migrationBuilder.AddColumn<int>(
                name: "Entity",
                table: "RequestWFs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Entity",
                table: "RequestWFs");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Projects");

            migrationBuilder.AddColumn<bool>(
                name: "ApprovedByConsultant",
                table: "Projects",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ApprovedByLabProjectManager",
                table: "Projects",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ApprovedBySupervising",
                table: "Projects",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "LabProjectManagerId",
                table: "Projects",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
