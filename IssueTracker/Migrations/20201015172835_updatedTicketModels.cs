using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IssueTracker.Migrations
{
    public partial class updatedTicketModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubmitterName",
                table: "Tickets");

            migrationBuilder.AddColumn<string>(
                name: "SubmittedById",
                table: "Tickets",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Tickets",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedById",
                table: "Tickets",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "TicketComment",
                maxLength: 160,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "UpdatedAt",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "Tickets");

            migrationBuilder.AddColumn<string>(
                name: "SubmitterName",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "TicketComment",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 160,
                oldNullable: true);
        }
    }
}
