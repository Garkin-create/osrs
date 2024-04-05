using Microsoft.EntityFrameworkCore.Migrations;

namespace OSRS.Infrastructure.Migrations
{
    public partial class Addpositiontracking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "Tracking",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Position",
                table: "Tracking");
        }
    }
}
