using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IssueTracker.Migrations
{
    public partial class UpdatedModelsDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProject_AspNetUsers_UserId1",
                table: "UserProject");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTicket_AspNetUsers_UserId1",
                table: "UserTicket");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTicket",
                table: "UserTicket");

            migrationBuilder.DropIndex(
                name: "IX_UserTicket_UserId1",
                table: "UserTicket");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserProject",
                table: "UserProject");

            migrationBuilder.DropIndex(
                name: "IX_UserProject_UserId1",
                table: "UserProject");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserTicket");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UserTicket");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserProject");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UserProject");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "UserTicket",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "UserProject",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTicket",
                table: "UserTicket",
                columns: new[] { "TicketId", "Id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserProject",
                table: "UserProject",
                columns: new[] { "ProjectId", "Id" });

            migrationBuilder.CreateIndex(
                name: "IX_UserTicket_Id",
                table: "UserTicket",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserProject_Id",
                table: "UserProject",
                column: "Id");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProject_AspNetUsers_Id",
                table: "UserProject");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTicket_AspNetUsers_Id",
                table: "UserTicket");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTicket",
                table: "UserTicket");

            migrationBuilder.DropIndex(
                name: "IX_UserTicket_Id",
                table: "UserTicket");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserProject",
                table: "UserProject");

            migrationBuilder.DropIndex(
                name: "IX_UserProject_Id",
                table: "UserProject");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserTicket");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserProject");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "UserTicket",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "UserTicket",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "UserProject",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "UserProject",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTicket",
                table: "UserTicket",
                columns: new[] { "TicketId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserProject",
                table: "UserProject",
                columns: new[] { "ProjectId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserTicket_UserId1",
                table: "UserTicket",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_UserProject_UserId1",
                table: "UserProject",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProject_AspNetUsers_UserId1",
                table: "UserProject",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTicket_AspNetUsers_UserId1",
                table: "UserTicket",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
