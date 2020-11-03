using Microsoft.EntityFrameworkCore.Migrations;

namespace IssueTracker.Migrations
{
    public partial class UpdateTCIdm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketComment_AspNetUsers_UserId",
                table: "TicketComment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TicketComment",
                table: "TicketComment");

            migrationBuilder.DropIndex(
                name: "IX_TicketComment_UserId",
                table: "TicketComment");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "TicketComment",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "TicketComment",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TicketComment",
                table: "TicketComment",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketComment_ApplicationUserId",
                table: "TicketComment",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketComment_AspNetUsers_ApplicationUserId",
                table: "TicketComment",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketComment_AspNetUsers_ApplicationUserId",
                table: "TicketComment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TicketComment",
                table: "TicketComment");

            migrationBuilder.DropIndex(
                name: "IX_TicketComment_ApplicationUserId",
                table: "TicketComment");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "TicketComment");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "TicketComment",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TicketComment",
                table: "TicketComment",
                columns: new[] { "TicketId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_TicketComment_UserId",
                table: "TicketComment",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketComment_AspNetUsers_UserId",
                table: "TicketComment",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
