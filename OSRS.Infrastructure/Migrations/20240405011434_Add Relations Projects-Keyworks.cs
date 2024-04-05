using Microsoft.EntityFrameworkCore.Migrations;

namespace OSRS.Infrastructure.Migrations
{
    public partial class AddRelationsProjectsKeyworks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Project",
                table: "Project");

            migrationBuilder.AddColumn<long>(
                name: "ProjectId",
                table: "Keyword",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Word",
                table: "Keyword",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Keyword",
                table: "Project",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Keyword_ProjectId",
                table: "Keyword",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Keyword_Project_ProjectId",
                table: "Keyword",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Keyword_Project_ProjectId",
                table: "Keyword");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Keyword",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Keyword_ProjectId",
                table: "Keyword");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Keyword");

            migrationBuilder.DropColumn(
                name: "Word",
                table: "Keyword");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Project",
                table: "Project",
                column: "Id");
        }
    }
}
