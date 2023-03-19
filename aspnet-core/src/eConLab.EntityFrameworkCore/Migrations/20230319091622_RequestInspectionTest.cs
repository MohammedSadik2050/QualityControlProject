using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eConLab.Migrations
{
    public partial class RequestInspectionTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.CreateTable(
                name: "RequestInspectionTests",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestId = table.Column<long>(type: "bigint", nullable: false),
                    InspectionTestType = table.Column<int>(type: "int", nullable: false),
                    InspectionTestId = table.Column<long>(type: "bigint", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestInspectionTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestInspectionTests_InspectionTests_InspectionTestId",
                        column: x => x.InspectionTestId,
                        principalTable: "InspectionTests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequestInspectionTests_InspectionTestId",
                table: "RequestInspectionTests",
                column: "InspectionTestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestInspectionTests");

            migrationBuilder.AddColumn<long>(
                name: "InspectionTestId",
                table: "InspectionTests",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "InspectionTestType",
                table: "InspectionTests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "RequestId",
                table: "InspectionTests",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "RequestInspectionTest",
                columns: table => new
                {
                    InspectionTestId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_RequestInspectionTest_InspectionTests_InspectionTestId",
                        column: x => x.InspectionTestId,
                        principalTable: "InspectionTests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InspectionTests_InspectionTestId",
                table: "InspectionTests",
                column: "InspectionTestId");
        }
    }
}
