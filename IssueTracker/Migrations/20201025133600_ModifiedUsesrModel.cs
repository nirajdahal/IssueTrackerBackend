using Microsoft.EntityFrameworkCore.Migrations;

namespace IssueTracker.Migrations
{
    public partial class ModifiedUsesrModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectManager_AspNetUsers_UserId",
                table: "ProjectManager");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ProjectManager",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectManager_AspNetUsers_UserId",
                table: "ProjectManager",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectManager_AspNetUsers_UserId",
                table: "ProjectManager");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ProjectManager",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectManager_AspNetUsers_UserId",
                table: "ProjectManager",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}