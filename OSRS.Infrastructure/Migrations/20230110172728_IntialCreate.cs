using Microsoft.EntityFrameworkCore.Migrations;

namespace OSRS.Infrastructure.Migrations
{
    public partial class IntialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Alchemy");

            migrationBuilder.CreateTable(
                name: "Alchemy",
                schema: "Alchemy",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    ItemName = table.Column<int>(type: "int", nullable: false),
                    ItemCant = table.Column<int>(type: "int", nullable: false),
                    ItemPrice = table.Column<int>(type: "int", nullable: false),
                    NaturePrice = table.Column<int>(type: "int", nullable: false),
                    ItemHighAlchemy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alchemy", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alchemy",
                schema: "Alchemy");
        }
    }
}
