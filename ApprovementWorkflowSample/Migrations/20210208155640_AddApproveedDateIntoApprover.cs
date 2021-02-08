using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApprovementWorkflowSample.Migrations
{
    public partial class AddApproveedDateIntoApprover : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "approved_date",
                table: "Workflow",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "approved_date",
                table: "Approver",
                type: "date",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "approved_date",
                table: "Approver");

            migrationBuilder.AlterColumn<DateTime>(
                name: "approved_date",
                table: "Workflow",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);
        }
    }
}
