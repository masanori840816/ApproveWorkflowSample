using Microsoft.EntityFrameworkCore.Migrations;

namespace ApprovementWorkflowSample.Migrations
{
    public partial class AddUniqueConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUsers_mail",
                table: "ApplicationUsers",
                column: "mail",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ApplicationUsers_mail",
                table: "ApplicationUsers");
        }
    }
}
