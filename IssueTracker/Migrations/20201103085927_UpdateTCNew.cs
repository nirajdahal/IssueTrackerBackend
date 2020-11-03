using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IssueTracker.Migrations
{
    public partial class UpdateTCNew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TicketComment",
                table: "TicketComment");

            migrationBuilder.DropColumn(
                name: "TicketCommentId",
                table: "TicketComment");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TicketComment");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "TicketComment",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "TicketComment",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TicketComment",
                table: "TicketComment",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TicketComment_TicketId",
                table: "TicketComment",
                column: "TicketId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TicketComment",
                table: "TicketComment");

            migrationBuilder.DropIndex(
                name: "IX_TicketComment_TicketId",
                table: "TicketComment");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TicketComment");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "TicketComment");

            migrationBuilder.AddColumn<Guid>(
                name: "TicketCommentId",
                table: "TicketComment",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "TicketComment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TicketComment",
                table: "TicketComment",
                column: "TicketId");
        }
    }
}
