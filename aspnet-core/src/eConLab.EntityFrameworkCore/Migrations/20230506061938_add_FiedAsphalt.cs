using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eConLab.Migrations
{
    public partial class add_FiedAsphalt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AsphaltFields",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LabDensity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Moisture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LayerThickness = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PointNo1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PointNo2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PointNo3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PointNo4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PointNo5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PointNo6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompactionRation1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompactionRation2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompactionRation3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompactionRation4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompactionRation5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompactionRation6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Moisture1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Moisture2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Moisture3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Moisture4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Moisture5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Moisture6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LayerType1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LayerType2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LayerType3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LayerType4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LayerType5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LayerType6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remark1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remark2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remark3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remark4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remark5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remark6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestId = table.Column<long>(type: "bigint", nullable: false),
                    RequestInspectionTestId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_AsphaltFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AsphaltFields_RequestInspectionTests_RequestInspectionTestId",
                        column: x => x.RequestInspectionTestId,
                        principalTable: "RequestInspectionTests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AsphaltFields_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AsphaltFields_RequestId",
                table: "AsphaltFields",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_AsphaltFields_RequestInspectionTestId",
                table: "AsphaltFields",
                column: "RequestInspectionTestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AsphaltFields");
        }
    }
}
