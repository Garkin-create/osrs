using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    public partial class AddRptProductDailyBalance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RptProductDailyBalances",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoFactura = table.Column<short>(type: "smallint", nullable: false),
                    IdProducto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Producto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Simbolo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdClase = table.Column<short>(type: "smallint", nullable: true),
                    idEmpresa = table.Column<int>(type: "int", nullable: true),
                    CantTotalFact = table.Column<double>(type: "float", nullable: true),
                    CantFactTPA = table.Column<double>(type: "float", nullable: true),
                    CantFact15G = table.Column<double>(type: "float", nullable: true),
                    ImporteFact = table.Column<double>(type: "float", nullable: true),
                    CantTotalTransf = table.Column<double>(type: "float", nullable: true),
                    CantTransTPA = table.Column<double>(type: "float", nullable: true),
                    CantTrans15G = table.Column<double>(type: "float", nullable: true),
                    ImporteTrans = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RptProductDailyBalances", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RptProductDailyBalances");
        }
    }
}
