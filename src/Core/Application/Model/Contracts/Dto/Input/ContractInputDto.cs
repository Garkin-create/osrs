using System;

namespace OSRS.Application.Contracts.Dto.Input
{
    public class ContractInputDto
    {
        public int IdContrato { get; set; }
        public string Cliente { get; set; }
        public string Provincia { get; set; }
        public string NumeroContrato { get; set; }
        public string Estado { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public int IdDivision { get; set; }
        public string Division { get; set; }
        public string IdProducto { get; set; }
        public string Producto { get; set; }
        public int IdCliente { get; set; }
    }
}