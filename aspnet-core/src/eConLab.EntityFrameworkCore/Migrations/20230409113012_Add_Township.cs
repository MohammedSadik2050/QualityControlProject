using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eConLab.Migrations
{
    public partial class Add_Township : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "RequestWFs",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "RequestWFs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "RequestWFs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "RequestWFHistories",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "RequestWFHistories",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "RequestWFHistories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "Requests",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "Requests",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Requests",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "RequestProjectItems",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "RequestProjectItems",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "RequestProjectItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "RequestInspectionTests",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "RequestInspectionTests",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "RequestInspectionTests",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "QCUsers",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "QCUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "QCUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "Projects",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "Projects",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Projects",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "ProjectItems",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "ProjectItems",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ProjectItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "InspectionTests",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "InspectionTests",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "InspectionTests",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "Departments",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "Departments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Departments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "Attachments",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "Attachments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Attachments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "AgencyTypes",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "AgencyTypes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AgencyTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "Agencies",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "Agencies",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Agencies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "TownShips",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TownShips", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TownShips");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "RequestWFs");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "RequestWFs");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "RequestWFs");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "RequestWFHistories");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "RequestWFHistories");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "RequestWFHistories");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "RequestProjectItems");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "RequestProjectItems");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "RequestProjectItems");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "RequestInspectionTests");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "RequestInspectionTests");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "RequestInspectionTests");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "QCUsers");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "QCUsers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "QCUsers");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "ProjectItems");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "ProjectItems");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ProjectItems");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "InspectionTests");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "InspectionTests");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "InspectionTests");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "AgencyTypes");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "AgencyTypes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AgencyTypes");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "Agencies");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "Agencies");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Agencies");
        }
    }
}
