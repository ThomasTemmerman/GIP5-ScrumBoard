using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GIP5_ScrumBoard.Migrations
{
    /// <inheritdoc />
    public partial class MilestoneWithRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Tickets",
                table: "Tickets");

            migrationBuilder.RenameTable(
                name: "Tickets",
                newName: "Ticket");

            migrationBuilder.AddColumn<int>(
                name: "MilestoneId",
                table: "Ticket",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ticket",
                table: "Ticket",
                column: "TicketId");

            migrationBuilder.CreateTable(
                name: "Milestone",
                columns: table => new
                {
                    MilestoneId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndDate = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Milestone", x => x.MilestoneId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_MilestoneId",
                table: "Ticket",
                column: "MilestoneId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Milestone_MilestoneId",
                table: "Ticket",
                column: "MilestoneId",
                principalTable: "Milestone",
                principalColumn: "MilestoneId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Milestone_MilestoneId",
                table: "Ticket");

            migrationBuilder.DropTable(
                name: "Milestone");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ticket",
                table: "Ticket");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_MilestoneId",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "MilestoneId",
                table: "Ticket");

            migrationBuilder.RenameTable(
                name: "Ticket",
                newName: "Tickets");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tickets",
                table: "Tickets",
                column: "TicketId");
        }
    }
}
