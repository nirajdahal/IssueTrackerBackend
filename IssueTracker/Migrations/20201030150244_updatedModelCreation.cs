using Microsoft.EntityFrameworkCore.Migrations;

namespace IssueTracker.Migrations
{
    public partial class updatedModelCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectManager",
                table: "ProjectManager");

            migrationBuilder.DropIndex(
                name: "IX_ProjectManager_ProjectId",
                table: "ProjectManager");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectManager",
                table: "ProjectManager",
                columns: new[] { "ProjectId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectManager_UserId",
                table: "ProjectManager",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectManager",
                table: "ProjectManager");

            migrationBuilder.DropIndex(
                name: "IX_ProjectManager_UserId",
                table: "ProjectManager");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectManager",
                table: "ProjectManager",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectManager_ProjectId",
                table: "ProjectManager",
                column: "ProjectId");
        }
    }
}