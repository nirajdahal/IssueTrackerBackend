using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace IssueTracker.Migrations
{
    public partial class ModifiedProjectManager : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "userProjectId",
                table: "UserProject",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "ProjectManager",
                columns: table => new
                {
                    projectManagerId = table.Column<Guid>(nullable: false),
                    ProjectId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectManager", x => x.projectManagerId);
                    table.ForeignKey(
                        name: "FK_ProjectManager_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectManager_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectManager_UserId",
                table: "ProjectManager",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectManager_ProjectId",
                table: "ProjectManager",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectManager");

            migrationBuilder.DropColumn(
                name: "userProjectId",
                table: "UserProject");
        }
    }
}