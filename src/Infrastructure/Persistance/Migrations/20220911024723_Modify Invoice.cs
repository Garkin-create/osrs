using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    public partial class ModifyInvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChapaDeposito",
                table: "RptInvoices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChapaTractor",
                table: "RptInvoices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LugIdSIC2",
                table: "RptInvoices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NoOperacionalDeposito",
                table: "RptInvoices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NoOperacionalTractor",
                table: "RptInvoices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "idClasifLugarEntrega",
                table: "RptInvoices",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "idVehiculoDeposito",
                table: "RptInvoices",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "idVehiculoTractor",
                table: "RptInvoices",
                type: "smallint",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChapaDeposito",
                table: "RptInvoices");

            migrationBuilder.DropColumn(
                name: "ChapaTractor",
                table: "RptInvoices");

            migrationBuilder.DropColumn(
                name: "LugIdSIC2",
                table: "RptInvoices");

            migrationBuilder.DropColumn(
                name: "NoOperacionalDeposito",
                table: "RptInvoices");

            migrationBuilder.DropColumn(
                name: "NoOperacionalTractor",
                table: "RptInvoices");

            migrationBuilder.DropColumn(
                name: "idClasifLugarEntrega",
                table: "RptInvoices");

            migrationBuilder.DropColumn(
                name: "idVehiculoDeposito",
                table: "RptInvoices");

            migrationBuilder.DropColumn(
                name: "idVehiculoTractor",
                table: "RptInvoices");
        }
    }
}
