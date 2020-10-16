using Microsoft.EntityFrameworkCore.Migrations;

namespace IssueTracker.Migrations
{
    public partial class ModifiedTablesTicket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_SubmittedById",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_UpdatedById",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_SubmittedById",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_UpdatedById",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "SubmittedById",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "Tickets");

            migrationBuilder.AddColumn<string>(
                name: "SubmittedByEmail",
                table: "Tickets",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubmittedByName",
                table: "Tickets",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedByEmail",
                table: "Tickets",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedByName",
                table: "Tickets",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubmittedByEmail",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "SubmittedByName",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "UpdatedByEmail",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "UpdatedByName",
                table: "Tickets");

            migrationBuilder.AddColumn<string>(
                name: "SubmittedById",
                table: "Tickets",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedById",
                table: "Tickets",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_SubmittedById",
                table: "Tickets",
                column: "SubmittedById");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_UpdatedById",
                table: "Tickets",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_SubmittedById",
                table: "Tickets",
                column: "SubmittedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_UpdatedById",
                table: "Tickets",
                column: "UpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}