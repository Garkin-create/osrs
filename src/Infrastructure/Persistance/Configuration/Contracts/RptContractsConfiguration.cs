using OSRS.Domain.Entities;
using OSRS.Domain.Entities.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OSRS.Persistance.Configuration.Contracts
{
    public class RptContractsConfiguration : IEntityTypeConfiguration<RptContracts>
    {
        public void Configure(EntityTypeBuilder<RptContracts> builder)
        {
            builder.ToTable("RptContratos", "Contratos");
            builder.HasKey(k => k.Id).HasName("PK_RptContratos");
        }
    }
}