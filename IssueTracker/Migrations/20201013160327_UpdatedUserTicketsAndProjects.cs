using Microsoft.EntityFrameworkCore.Migrations;

namespace IssueTracker.Migrations
{
    public partial class UpdatedUserTicketsAndProjects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProject_AspNetUsers_Id",
                table: "UserProject");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTicket_AspNetUsers_Id",
                table: "UserTicket");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "UserTicket",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserTicket_Id",
                table: "UserTicket",
                newName: "IX_UserTicket_UserId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "UserProject",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserProject_Id",
                table: "UserProject",
                newName: "IX_UserProject_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProject_AspNetUsers_UserId",
                table: "UserProject",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTicket_AspNetUsers_UserId",
                table: "UserTicket",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProject_AspNetUsers_UserId",
                table: "UserProject");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTicket_AspNetUsers_UserId",
                table: "UserTicket");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserTicket",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_UserTicket_UserId",
                table: "UserTicket",
                newName: "IX_UserTicket_Id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserProject",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_UserProject_UserId",
                table: "UserProject",
                newName: "IX_UserProject_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProject_AspNetUsers_Id",
                table: "UserProject",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTicket_AspNetUsers_Id",
                table: "UserTicket",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
