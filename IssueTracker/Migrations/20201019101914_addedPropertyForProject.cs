using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IssueTracker.Migrations
{
    public partial class addedPropertyForProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SubmittedByEmail",
                table: "Projects",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubmittedByName",
                table: "Projects",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "Projects",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedByEmail",
                table: "Projects",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedByName",
                table: "Projects",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubmittedByEmail",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "SubmittedByName",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "UpdatedByEmail",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "UpdatedByName",
                table: "Projects");
        }
    }
}
