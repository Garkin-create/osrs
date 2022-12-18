using System;

namespace OSRS.Domain.Entities.Contracts
{
    public class RptContracts : RptContractsBase, IEntity<long>
    {
        public int IdContrato { get; set; }
        public string NumeroContrato { get; set; }
        public string Estado { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string IdProducto { get; set; }
        public string Producto { get; set; }
    }
}