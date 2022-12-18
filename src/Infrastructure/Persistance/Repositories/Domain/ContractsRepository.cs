using OSRS.Domain.Entities.Contracts;
using OSRS.Domain.IRepositories;
using OSRS.Persistance.Db;
using OSRS.Persistance.Helper;

namespace OSRS.Persistance.Repositories
{
    public class ContractsRepository: 
        BaseEntityRepository<RptContracts>,
        IEntityRepository<RptContracts>
    {
        private IEntityRepository<RptContracts> _entityRepositoryImplementation;

        public ContractsRepository(OSRSWriteDbContext dbContext, ISystemLogger logger) : base(dbContext, logger)
        {
        }
    }
}
