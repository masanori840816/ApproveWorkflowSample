using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ApprovementWorkflowSample.Migrations
{
    public partial class AddWorkflowModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApproverRole",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApproverRole", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "WorkflowType",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowType", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Workflow",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: false),
                    workflow_type_id = table.Column<int>(type: "integer", nullable: false),
                    last_update_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workflow", x => x.id);
                    table.ForeignKey(
                        name: "FK_Workflow_WorkflowType_workflow_type_id",
                        column: x => x.workflow_type_id,
                        principalTable: "WorkflowType",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApproverGroup",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    workflow_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApproverGroup", x => x.id);
                    table.ForeignKey(
                        name: "FK_ApproverGroup_Workflow_workflow_id",
                        column: x => x.workflow_id,
                        principalTable: "Workflow",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Approver",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    approver_group_id = table.Column<int>(type: "integer", nullable: false),
                    approver_role_id = table.Column<int>(type: "integer", nullable: false),
                    last_update_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Approver", x => x.id);
                    table.ForeignKey(
                        name: "FK_Approver_ApproverGroup_approver_group_id",
                        column: x => x.approver_group_id,
                        principalTable: "ApproverGroup",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Approver_ApproverRole_approver_role_id",
                        column: x => x.approver_role_id,
                        principalTable: "ApproverRole",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ApproverRole",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Author" },
                    { 2, "Checker" },
                    { 3, "Approver" }
                });

            migrationBuilder.InsertData(
                table: "WorkflowType",
                columns: new[] { "id", "Name" },
                values: new object[,]
                {
                    { 1, "WorkflowTypeA" },
                    { 2, "WorkflowTypeA" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Approver_approver_group_id",
                table: "Approver",
                column: "approver_group_id");

            migrationBuilder.CreateIndex(
                name: "IX_Approver_approver_role_id",
                table: "Approver",
                column: "approver_role_id");

            migrationBuilder.CreateIndex(
                name: "IX_ApproverGroup_workflow_id",
                table: "ApproverGroup",
                column: "workflow_id");

            migrationBuilder.CreateIndex(
                name: "IX_Workflow_workflow_type_id",
                table: "Workflow",
                column: "workflow_type_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Approver");

            migrationBuilder.DropTable(
                name: "ApproverGroup");

            migrationBuilder.DropTable(
                name: "ApproverRole");

            migrationBuilder.DropTable(
                name: "Workflow");

            migrationBuilder.DropTable(
                name: "WorkflowType");
        }
    }
}
