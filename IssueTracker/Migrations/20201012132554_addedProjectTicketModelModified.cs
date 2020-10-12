using Microsoft.EntityFrameworkCore.Migrations;

namespace IssueTracker.Migrations
{
    public partial class addedProjectTicketModelModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickett_TicketPriority_PriorityId",
                table: "Tickett");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickett_Project_ProjectId",
                table: "Tickett");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickett_TicketStatus_StatusId",
                table: "Tickett");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickett_TicketType_TypeId",
                table: "Tickett");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProject_Project_ProjectId",
                table: "UserProject");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTicket_Tickett_TicketId",
                table: "UserTicket");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tickett",
                table: "Tickett");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Project",
                table: "Project");

            migrationBuilder.RenameTable(
                name: "Tickett",
                newName: "Tickets");

            migrationBuilder.RenameTable(
                name: "Project",
                newName: "Projects");

            migrationBuilder.RenameIndex(
                name: "IX_Tickett_TypeId",
                table: "Tickets",
                newName: "IX_Tickets_TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickett_StatusId",
                table: "Tickets",
                newName: "IX_Tickets_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickett_ProjectId",
                table: "Tickets",
                newName: "IX_Tickets_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickett_PriorityId",
                table: "Tickets",
                newName: "IX_Tickets_PriorityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tickets",
                table: "Tickets",
                column: "TicketId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Projects",
                table: "Projects",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_TicketPriority_PriorityId",
                table: "Tickets",
                column: "PriorityId",
                principalTable: "TicketPriority",
                principalColumn: "TPriorityId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Projects_ProjectId",
                table: "Tickets",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_TicketStatus_StatusId",
                table: "Tickets",
                column: "StatusId",
                principalTable: "TicketStatus",
                principalColumn: "TStatusId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_TicketType_TypeId",
                table: "Tickets",
                column: "TypeId",
                principalTable: "TicketType",
                principalColumn: "TTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProject_Projects_ProjectId",
                table: "UserProject",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTicket_Tickets_TicketId",
                table: "UserTicket",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "TicketId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_TicketPriority_PriorityId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Projects_ProjectId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_TicketStatus_StatusId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_TicketType_TypeId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProject_Projects_ProjectId",
                table: "UserProject");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTicket_Tickets_TicketId",
                table: "UserTicket");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tickets",
                table: "Tickets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Projects",
                table: "Projects");

            migrationBuilder.RenameTable(
                name: "Tickets",
                newName: "Tickett");

            migrationBuilder.RenameTable(
                name: "Projects",
                newName: "Project");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_TypeId",
                table: "Tickett",
                newName: "IX_Tickett_TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_StatusId",
                table: "Tickett",
                newName: "IX_Tickett_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_ProjectId",
                table: "Tickett",
                newName: "IX_Tickett_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_PriorityId",
                table: "Tickett",
                newName: "IX_Tickett_PriorityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tickett",
                table: "Tickett",
                column: "TicketId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Project",
                table: "Project",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickett_TicketPriority_PriorityId",
                table: "Tickett",
                column: "PriorityId",
                principalTable: "TicketPriority",
                principalColumn: "TPriorityId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickett_Project_ProjectId",
                table: "Tickett",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickett_TicketStatus_StatusId",
                table: "Tickett",
                column: "StatusId",
                principalTable: "TicketStatus",
                principalColumn: "TStatusId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickett_TicketType_TypeId",
                table: "Tickett",
                column: "TypeId",
                principalTable: "TicketType",
                principalColumn: "TTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProject_Project_ProjectId",
                table: "UserProject",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTicket_Tickett_TicketId",
                table: "UserTicket",
                column: "TicketId",
                principalTable: "Tickett",
                principalColumn: "TicketId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
