using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eConLab.Migrations
{
    public partial class add_ConcreteFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConcretFields",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SampleNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SamplePreparationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SamplePreparationEndDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CylindersReceivedNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LandingGear = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CastCylindersNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecoveredCylindersNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LabDeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AirTemp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CementQty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcreteTemp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcreteRank = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LandingMM = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcreteUsing = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcreteSource = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcreteQty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TruckNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TruckLeftDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CastingStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TruckSiteArrivingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_ConcretFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConcretFields_RequestInspectionTests_RequestInspectionTestId",
                        column: x => x.RequestInspectionTestId,
                        principalTable: "RequestInspectionTests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConcretFields_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConcretFields_RequestId",
                table: "ConcretFields",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_ConcretFields_RequestInspectionTestId",
                table: "ConcretFields",
                column: "RequestInspectionTestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConcretFields");
        }
    }
}
