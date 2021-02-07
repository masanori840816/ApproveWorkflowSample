using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApprovementWorkflowSample.Migrations
{
    public partial class AddApproveedDateIntoWorkflow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "approved_date",
                table: "Workflow",
                type: "timestamp with time zone",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "approved_date",
                table: "Workflow");
        }
    }
}
