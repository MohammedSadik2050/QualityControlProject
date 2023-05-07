using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eConLab.Migrations
{
    public partial class add_RC2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RC2",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestStationA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestStationB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestStationC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrayWeightWhithoutAsphaltA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrayWeightWhithoutAsphaltB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrayWeightWhithoutAsphaltC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrayWeightWhithAsphaltA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrayWeightWhithAsphaltB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrayWeightWhithAsphaltC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AreaOfTrayA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AreaOfTrayB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AreaOfTrayC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequiredRate = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_RC2", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RC2_RequestInspectionTests_RequestInspectionTestId",
                        column: x => x.RequestInspectionTestId,
                        principalTable: "RequestInspectionTests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RC2_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RC2_RequestId",
                table: "RC2",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_RC2_RequestInspectionTestId",
                table: "RC2",
                column: "RequestInspectionTestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RC2");
        }
    }
}
