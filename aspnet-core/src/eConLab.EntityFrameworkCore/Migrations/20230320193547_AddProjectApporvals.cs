using Microsoft.EntityFrameworkCore.Migrations;

namespace eConLab.Migrations
{
    public partial class AddProjectApporvals : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
