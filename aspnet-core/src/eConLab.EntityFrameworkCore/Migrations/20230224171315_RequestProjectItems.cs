using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eConLab.Migrations
{
    public partial class RequestProjectItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RequestProjectItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestId = table.Column<long>(type: "bigint", nullable: false),
                    ProjectItemId = table.Column<long>(type: "bigint", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestProjectItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestProjectItems_ProjectItems_ProjectItemId",
                        column: x => x.ProjectItemId,
                        principalTable: "ProjectItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequestProjectItems_ProjectItemId",
                table: "RequestProjectItems",
                column: "ProjectItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestProjectItems");
        }
    }
}
