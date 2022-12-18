using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    public partial class CreateAllTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.CreateTable(
                name: "DataUpdates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdDivision = table.Column<int>(type: "int", nullable: false),
                    IdReport = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastDataDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataUpdates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RptA6Cancellations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Chofer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Causa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Responsable = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCancelacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumeroDocumento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Provincia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdDivision = table.Column<int>(type: "int", nullable: false),
                    Division = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Carro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deposito = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RptA6Cancellations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RptA6Conciliations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VolumenTPN = table.Column<double>(type: "float", nullable: false),
                    NumeroDocumento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Provincia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdDivision = table.Column<int>(type: "int", nullable: false),
                    Division = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Carro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deposito = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RptA6Conciliations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RptClientCylinders",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LugarEntrega = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Piso10kg = table.Column<int>(type: "int", nullable: false),
                    Piso45kg = table.Column<int>(type: "int", nullable: false),
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    Cliente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdDivision = table.Column<int>(type: "int", nullable: false),
                    Division = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Provincia = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RptClientCylinders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RptContracts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdContrato = table.Column<int>(type: "int", nullable: false),
                    NumeroContrato = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaVencimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdProducto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Producto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    Cliente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdDivision = table.Column<int>(type: "int", nullable: false),
                    Division = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Provincia = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RptContracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RptDailyAssemblies",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdViaje = table.Column<long>(type: "bigint", nullable: false),
                    NumeroViaje = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CantidadRealCargada = table.Column<double>(type: "float", nullable: false),
                    CapacidadTotal = table.Column<double>(type: "float", nullable: false),
                    NoOperacional = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstadoViaje = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NombreUnidadFuncional = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdDivision = table.Column<int>(type: "int", nullable: false),
                    NombreProvincia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NombreCliente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NombreLugarEntrega = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NombreProducto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdUnidadFuncional = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Division = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RptDailyAssemblies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RptDailyOrders",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPedido = table.Column<int>(type: "int", nullable: false),
                    NoPedido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaPedido = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Provincia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdDeposito = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deposito = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdLugarEntrega = table.Column<int>(type: "int", nullable: false),
                    LugarEntrega = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdProducto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Producto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UM = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoFactura = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Solicitado = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Servido = table.Column<double>(type: "float", nullable: false),
                    FechaServido = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Pendiente = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IdProgramacion = table.Column<int>(type: "int", nullable: false),
                    IdDivision = table.Column<int>(type: "int", nullable: false),
                    Division = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RptDailyOrders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RptDistributedVolumes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdVehiculo = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NoOperacional = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Origen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Provincia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CantidadLt = table.Column<double>(type: "float", nullable: false),
                    Cantidad15Lt = table.Column<double>(type: "float", nullable: false),
                    CantidadHl = table.Column<double>(type: "float", nullable: false),
                    Cantidad15Hl = table.Column<double>(type: "float", nullable: false),
                    CantidadViajes = table.Column<int>(type: "int", nullable: false),
                    UnidadFuncional = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RptDistributedVolumes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RptDistributionPendings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPedido = table.Column<long>(type: "bigint", nullable: false),
                    NoPedido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FehaProgramada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NombreMunicipio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CantidadADistribuir = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CantidadPendiente = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IdLugarEntrega = table.Column<int>(type: "int", nullable: false),
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    IdProducto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NombreProvincia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NombreCliente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NombreLugarEntrega = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NombreProducto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdUnidadFuncional = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Division = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdDivision = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RptDistributionPendings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RptInventories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdTanque = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdProvincia = table.Column<short>(type: "smallint", nullable: false),
                    Provincia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdEmpresa = table.Column<int>(type: "int", nullable: false),
                    Empresa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdDeposito = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deposito = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdEstado = table.Column<short>(type: "smallint", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaMedicion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdProducto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Producto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Inventario = table.Column<double>(type: "float", nullable: false),
                    Disponible = table.Column<double>(type: "float", nullable: false),
                    Capacidad = table.Column<double>(type: "float", nullable: false),
                    Vacio = table.Column<double>(type: "float", nullable: false),
                    UMCapacidad = table.Column<short>(type: "smallint", nullable: false),
                    Fondaje = table.Column<double>(type: "float", nullable: false),
                    UMFondaje = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RptInventories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RptInvoices",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdFactura = table.Column<long>(type: "bigint", nullable: false),
                    NoFactura = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaLiquidacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdEstadoCicloVida = table.Column<short>(type: "smallint", nullable: true),
                    EstadoCicloVida = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdEstadoPrincipal = table.Column<short>(type: "smallint", nullable: true),
                    EstadoPrincipal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdEstadoFinanciero = table.Column<short>(type: "smallint", nullable: true),
                    EstadoFinanciero = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdTipoFactura = table.Column<short>(type: "smallint", nullable: true),
                    TipoFactura = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdTipoFacturacion = table.Column<short>(type: "smallint", nullable: true),
                    TipoFacturacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdTipoVenta = table.Column<short>(type: "smallint", nullable: true),
                    NombreTipoVenta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sector = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdEmpresa = table.Column<int>(type: "int", nullable: true),
                    Empresa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Provincia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodigoProvincia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdDeposito = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deposito = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdClientePaga = table.Column<int>(type: "int", nullable: true),
                    ClientePaga = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdOrganismo = table.Column<int>(type: "int", nullable: true),
                    Organismo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SiglasOrganismo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdClienteFacturar = table.Column<int>(type: "int", nullable: true),
                    ClienteFacturar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdLugarEntrega = table.Column<int>(type: "int", nullable: true),
                    NombreLugarEntrega = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DireccionLugarEntrega = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProvinciaLugarEntrega = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodProvinciaLugarEntrega = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdMoneda = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImportePrincipal = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ImporteTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IdProducto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Producto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cantidad = table.Column<double>(type: "float", nullable: true),
                    CantidadCorregida = table.Column<double>(type: "float", nullable: true),
                    CantFact15G = table.Column<bool>(type: "bit", nullable: true),
                    IdUM = table.Column<short>(type: "smallint", nullable: true),
                    SimboloUM = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrecioCosto = table.Column<double>(type: "float", nullable: true),
                    Precio = table.Column<double>(type: "float", nullable: true),
                    PrecioEmpresa = table.Column<double>(type: "float", nullable: true),
                    PrecioFormacion = table.Column<double>(type: "float", nullable: true),
                    ImporteProducto = table.Column<double>(type: "float", nullable: true),
                    NoOperacional = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumeroViaje = table.Column<short>(type: "smallint", nullable: true),
                    IdFormaTransportacion = table.Column<short>(type: "smallint", nullable: true),
                    NombreFormaTransportacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChapaVehiculo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CriterioPrecio = table.Column<int>(type: "int", nullable: false),
                    IdSector = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RptInvoices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RptProgramingPendings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodCliente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCliente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cliente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Provincia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdDeposito = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deposito = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdLugarEntrega = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NombreLugarEntrega = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LineaDistribucion = table.Column<int>(type: "int", nullable: false),
                    NombreLineaDistribucion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdProducto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Producto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaProgramacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cantidad = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IdDivision = table.Column<int>(type: "int", nullable: false),
                    Division = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RptProgramingPendings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RptTravelCarriers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NoOperacional = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrigenVehiculo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Transportista = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumeroViaje = table.Column<short>(type: "smallint", nullable: false),
                    Chapa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Producto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UM = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstadoViaje = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NombreMunicipio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CantidadTPN = table.Column<double>(type: "float", nullable: false),
                    Cantidad15 = table.Column<double>(type: "float", nullable: false),
                    IdTransportista = table.Column<int>(type: "int", nullable: false),
                    IdLugarEntrega = table.Column<int>(type: "int", nullable: false),
                    UnidadFuncional = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RptTravelCarriers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataUpdates");

            migrationBuilder.DropTable(
                name: "RptA6Cancellations");

            migrationBuilder.DropTable(
                name: "RptA6Conciliations");

            migrationBuilder.DropTable(
                name: "RptClientCylinders");

            migrationBuilder.DropTable(
                name: "RptContracts");

            migrationBuilder.DropTable(
                name: "RptDailyAssemblies");

            migrationBuilder.DropTable(
                name: "RptDailyOrders");

            migrationBuilder.DropTable(
                name: "RptDistributedVolumes");

            migrationBuilder.DropTable(
                name: "RptDistributionPendings");

            migrationBuilder.DropTable(
                name: "RptInventories");

            migrationBuilder.DropTable(
                name: "RptInvoices");

            migrationBuilder.DropTable(
                name: "RptProgramingPendings");

            migrationBuilder.DropTable(
                name: "RptTravelCarriers");

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });
        }
    }
}
