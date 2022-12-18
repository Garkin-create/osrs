namespace OSRS.Domain.Entities.Contracts
{
    public class RptContractsBase
    {
        public long Id { get; set; }
        public int IdCliente { get; set; }
        public string Cliente { get; set; }
        public int IdDivision { get; set; }
        public string Division { get; set; }
        public string Provincia { get; set; }
    }
}